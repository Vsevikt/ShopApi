using ShopApplication.DTOs.OrderDTOs;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services
{
    public class OrderService(IProductRepository _productRepository, IOrderRepository _orderRepository, IEmailService _emailService, IAuthRepository _authRepository, IQueueService _queueService) : IOrderService
    {
        public async Task<object> CreateOrderAsync(string userEmail, OrderCreateDTO dto)
        {
            var user = await _authRepository.GetUserByEmailAsync(userEmail);
            if (user == null)
                throw new KeyNotFoundException("Користувача не знайдено");

            var productIds = dto.Products.Select(p => p.ProductId).ToList();
            var productsFromDb = new List<Product>();

            foreach (var id in productIds)
            {
                var product = await _productRepository.GetProductAsync(id);
                if (product != null) productsFromDb.Add(product);
            }

            if (productsFromDb.Count != productIds.Count)
                throw new InvalidOperationException("Один або кілька товарів не знайдено в базі даних");

            bool isOutOfStock = false;
            foreach (var p in dto.Products)
            {
                var dbProduct = productsFromDb.First(dbP => dbP.Id == p.ProductId);

                if (dbProduct.StockQty < p.Count)
                {
                    isOutOfStock = true;
                    break;
                }
            }

            if (isOutOfStock)
            {
                var pendingOrder = new Order
                {
                    UserId = user.Id,
                    Status = OrderStatus.Shipped
                };
                await _orderRepository.AddOrderAsync(pendingOrder);

                return new
                {
                    message = "На жаль, наразі деяких товарів з вашого списку немає на складі. Ваше замовлення переведено в статус очікування."
                };
            }

            var productsWithDetails = dto.Products.Select(p =>
            {
                var dbProduct = productsFromDb.First(dbP => dbP.Id == p.ProductId);
                return new
                {
                    p.ProductId,
                    Name = dbProduct.Name,
                    Category = dbProduct.Category,
                    Description = dbProduct.Description,
                    Count = p.Count,
                    Price = dbProduct.Price,
                    ItemTotalPrice = p.Count * dbProduct.Price
                };
            }).ToList();

            decimal overallTotalPrice = productsWithDetails.Sum(p => p.ItemTotalPrice);

            var newOrder = new Order
            {
                UserId = user.Id,
                Status = OrderStatus.New
            };
            await _orderRepository.AddOrderAsync(newOrder);

            foreach (var p in dto.Products)
            {
                var dbProduct = productsFromDb.First(db => db.Id == p.ProductId);
                dbProduct.StockQty -= p.Count;
                await _productRepository.EditProductAsync(dbProduct);
            }

            var orderMessage = new
            {
                OrderId = newOrder.Id,
                UserEmail = userEmail,
                UserId = user.Id,
                Products = productsWithDetails,
                TotalPrice = overallTotalPrice
            };

            await _queueService.PublishAsync("Orders", orderMessage);

            return new
            {
                message = "Замовлення успішно прийнято в обробку!",
                data = orderMessage
            };
        }
    }
}
