using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ShopApi.Controllers;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.Interfaces;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using Xunit;

namespace ShopTest
{
    public class CategoryControllerTests
    {
        [Fact]
        public async Task GetCategory_ReturnsOk_WhenCategoryExists()
        {
            // Arrange
            var mockService = new Mock<ICategoryService>();
            var mockImageService = new Mock<IImageService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockCreateValidator = new Mock<IValidator<CategoryCreateDTO>>();
            var mockUpdateValidator = new Mock<IValidator<CategoryUpdateDTO>>();

            var testCategory = new CategoryReadDTO
            {
                Id = 11,
                Name = "jjjj"
            };

            mockService
                .Setup(service => service.GetCategoryByIdAsync(1))
                .ReturnsAsync(testCategory);

            var controller = new CategoryController(
                mockService.Object,
                mockImageService.Object,
                mockConfiguration.Object,
                mockConfiguration.Object,
                mockCreateValidator.Object,
                mockUpdateValidator.Object
            );

            // Act
            var result = await controller.GetCategoryById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<CategoryReadDTO>(okResult.Value);

            Assert.Equal("jjjj", returnedCategory.Name);
        }
    }
}