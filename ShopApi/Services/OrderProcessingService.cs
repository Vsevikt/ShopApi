using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShopApplication.DTOs.OrderDTOs;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Interfaces.Services;
using System.Text;
using System.Text.Json;

namespace ShopApi.Services
{
    public class OrderProcessingService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderProcessingService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "Orders", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    Console.WriteLine("Фоновий сервіс отримав повідомлення з черги");

                    var body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Отриманий JSON: {jsonMessage}");

                    var orderData = JsonSerializer.Deserialize<OrderMessageDTO>(jsonMessage);

                    if (orderData != null)
                    {
                        await ProcessOrderLogicAsync(orderData);
                    }

                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                    Console.WriteLine("Повідомлення успішно опрацьоване та підтверджене");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Критична помилка у фоновому сервісі:\n{ex.Message}\n{ex.StackTrace}\n");
                    await channel.BasicNackAsync(ea.DeliveryTag, false, requeue: true);
                }
            };

            await channel.BasicConsumeAsync(queue: "Orders", autoAck: false, consumer: consumer);
        }

        private async Task ProcessOrderLogicAsync(OrderMessageDTO orderData)
        {
            using var scope = _scopeFactory.CreateScope();

            var productRepo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            var orderRepo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var userRepo = scope.ServiceProvider.GetRequiredService<IAuthRepository>();
            var user = await userRepo.GetUserByIdAsync(orderData.UserId);

            string targetEmail = user?.Email ?? orderData.UserEmail;

            if (string.IsNullOrWhiteSpace(targetEmail))
            {
                Console.WriteLine($"Помилка: У користувача {orderData.UserId} немає email в базі даних.");
                return;
            }

            bool hasEnoughStock = true;
            foreach (var item in orderData.Products)
            {
                var productInDb = await productRepo.GetProductAsync(item.ProductId);
                if (productInDb == null || productInDb.StockQty < item.Count)
                {
                    hasEnoughStock = false;
                    break;
                }
            }

            if (hasEnoughStock)
            {
                var newOrder = new ShopDomain.Models.Order
                {
                    UserId = (Guid)orderData.UserId,
                    Status = ShopDomain.Models.OrderStatus.New,
                    Paid = false,

                    OrderDetails = orderData.Products.Select(p => new ShopDomain.Models.OrderDetail
                    {
                        ProductId = p.ProductId,
                        Count = p.Count,
                        Price = p.Price
                    }).ToList()
                };

                await orderRepo.AddOrderAsync(newOrder);

                string emailBody = "<h2>Ваше замовлення успішно оформлено!</h2><ul>";

                decimal finalTotalPrice = 0;

                foreach (var p in orderData.Products)
                {
                    decimal itemTotal = p.Count * p.Price;
                    finalTotalPrice += itemTotal;

                    emailBody += $"<li><b>{p.Name}</b>: {p.Count} шт. х {p.Price} грн. = {itemTotal} грн.</li>";
                }

                emailBody += $"</ul><h3>Загальна сума до сплати: {finalTotalPrice} грн.</h3>";

                await emailService.SendEmailAsync(targetEmail, "Замовлення підтверджено", emailBody);
            }
            else
            {
                string waitBody = "<h2>Оновлення статусу замовлення</h2>" +
                                  "<p>На жаль, наразі деяких товарів з вашого списку немає на складі.</p>" +
                                  "<p>Ваше замовлення переведено в статус <b>очікування</b>.</p>";

                await emailService.SendEmailAsync(targetEmail, "Замовлення в очікуванні", waitBody);
            }
        }
    }
}