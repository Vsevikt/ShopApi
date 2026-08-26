using AutoMapper;
using ShopApplication.DTOs.UserDTOs;
using ShopApplication.Interfaces.Helpers;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Interfaces.Services;
using ShopDomain.Enums;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services
{
    public class AuthService(IMapper _mapper, IAuthRepository _repository, IHashHelper _hashHelper, IJWTService _jwtService, IRefreshTokenRepository _refreshTokenRepository, IPasswordResetTokenRepository _passwordResetTokenRepository, IEmailService _emailService, IQueueService _queueService) : IAuthService
    {
        public async Task<(UserReadDTO? User, string? Token, string? RefreshToken)> RegisterAsync(UserCreateDTO dto)
        {
            var isExist = await _repository.IsExistEmailAsync(dto.Email);
            if (!isExist)
            {
                var hash = _hashHelper.Hash(dto.Password);
                var user = _mapper.Map<User>(dto);
                var registerUser = await _repository.RegisterUserAsync(user, hash);

                if (registerUser != null)
                {
                    var token = _jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString());
                    var refreshToken = _jwtService.GenerateRefreshToken();

                    await _refreshTokenRepository.AddTokenAsync(new RefreshToken
                    {
                        Token = refreshToken.Item1,
                        UserId = user.Id,
                        IsRevoked = false,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(refreshToken.Item2)
                    });

                    await _queueService.PublishAsync<UserCreateDTO>("Users", dto);
                    return (_mapper.Map<UserReadDTO>(registerUser), token, refreshToken.Item1);
                }
            }
            return (null, null, null);
        }

        public async Task<(UserReadDTO? User, string? Token, string? RefreshToken)> LoginAsync(string email, string password)
        {
            var user = await _repository.GetUserByEmailAsync(email);
            if (user != null)
            {
                var isPasswordValid = _hashHelper.IsValidPassword(password, user.PasswordHash);
                if (isPasswordValid)
                {
                    var token = _jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString());
                    var refreshToken = _jwtService.GenerateRefreshToken();

                    await _refreshTokenRepository.AddTokenAsync(new RefreshToken
                    {
                        Token = refreshToken.Item1,
                        UserId = user.Id,
                        IsRevoked = false,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(refreshToken.Item2)
                    });

                    return (_mapper.Map<UserReadDTO>(user), token, refreshToken.Item1);
                }
            }
            return (null, null, null);
        }

        public async Task<(UserReadDTO? User, string? Token, string? RefreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetTokenAsync(refreshToken);
            if (token != null && !token.IsRevoked && token.ExpiresAt > DateTime.UtcNow)
            {
                var user = await _repository.GetUserByIdAsync(token.UserId);
                if (user != null)
                {
                    var newToken = _jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(user), user.Role.ToString());
                    var newRefreshToken = _jwtService.GenerateRefreshToken();

                    token.IsRevoked = true;
                    await _refreshTokenRepository.UpdateTokenAsync(token);

                    await _refreshTokenRepository.AddTokenAsync(new RefreshToken
                    {
                        Token = newRefreshToken.Item1,
                        UserId = user.Id,
                        IsRevoked = false,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(newRefreshToken.Item2)
                    });
                    return (_mapper.Map<UserReadDTO>(user), newToken, newRefreshToken.Item1);
                }
            }
            return (null, null, null);
        }

        public async Task<(UserReadDTO? User, string? Token, string? RefreshToken)> UpdateUserAsync(UserUpdateDTO dto)
        {
            var user = await _repository.GetUserByIdAsync(dto.Id);
            if (user != null)
            {
                _mapper.Map(dto, user);
                var updatedUser = await _repository.UpdateUserAsync(user);
                if (updatedUser != null)
                {
                    var newToken = _jwtService.GenerateAccessToken(_mapper.Map<UserLoginDTO>(updatedUser), updatedUser.Role.ToString());
                    var refreshToken = _jwtService.GenerateRefreshToken();

                    await _refreshTokenRepository.AddTokenAsync(new RefreshToken
                    {
                        Token = refreshToken.Item1,
                        UserId = updatedUser.Id,
                        IsRevoked = false,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(refreshToken.Item2)
                    });
                    return (_mapper.Map<UserReadDTO>(updatedUser), newToken, refreshToken.Item1);
                }
            }
            return (null, null, null);
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            return await _repository.IsExistEmailAsync(email);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _repository.GetUserByEmailAsync(email);

            if (user == null)
                return false;

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var resetToken = new PasswordResetToken
            {
                Token = token,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await _passwordResetTokenRepository.AddAsync(resetToken);

            await _emailService.SendPasswordResetEmailAsync(user.Email, token);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var resetToken = await _passwordResetTokenRepository.GetByTokenAsync(token);

            if (resetToken == null)
                return false;

            if (resetToken.IsUsed)
                return false;

            if (resetToken.ExpiresAt < DateTime.UtcNow)
                return false;

            if (resetToken.User == null)
                return false;

            resetToken.User.PasswordHash = _hashHelper.Hash(newPassword);

            resetToken.IsUsed = true;

            await _passwordResetTokenRepository.UpdateAsync(resetToken);

            return true;
        }

        public async Task<bool> ChangeRoleAsync(Guid userId, UserRole role)
        {
            var user = await _repository.GetUserByIdAsync(userId);

            if (user == null)
                return false;

            user.Role = role;

            await _repository.UpdateUserAsync(user);

            return true;
        }
    }
}
