using System;
using MicroCRM.Entities;
using MicroCRM.Services.Encryption;

namespace MicroCRM.Data
{
    internal static class DemoDataSnapshot
    {
        public static void CreateDemoData(DataContext dataContext, IEncryptionService encryptionService)
        {
            var users = new[]
            {
                new User
                {
                    Username = "admin@microcrm.com",
                    Password = encryptionService.ComputeHexString("admin"),
                    Role = Role.Admin
                },
                new User
                {
                    Username = "owner@microcrm.com",
                    Password = encryptionService.ComputeHexString("owner"),
                    Role = Role.Owner
                },
                new User
                {
                    Username = "sales@microcrm.com",
                    Password = encryptionService.ComputeHexString("sales"),
                    Role = Role.Sales
                }
            };

            var customers = new[]
            {
                new Customer
                {
                    Name = "John Doe",
                    Gender = Gender.Male,
                    EmailAddress = "john.doe@example.com"
                },
                new Customer
                {
                    Name = "Jane Doe",
                    Gender = Gender.Female,
                    EmailAddress = "jane.doe@example.com"
                }
            };

            var products = new[]
            {
                new Product { Name = "Apple iPhone 5", CostPrice = 200.00m, Price = 240.00m },
                new Product { Name = "Apple iPhone 5S", CostPrice = 280.00m, Price = 320.00m },
                new Product { Name = "Apple iPhone 5C", CostPrice = 240.00m, Price = 280.00m },
                new Product { Name = "Apple iPhone 6", CostPrice = 320.00m, Price = 360.00m },
                new Product { Name = "Apple iPhone 6 Plus", CostPrice = 380.00m, Price = 420.00m },
                new Product { Name = "Apple iPhone 6S", CostPrice = 340.00m, Price = 400.00m },
                new Product { Name = "Apple iPhone 6S Plus", CostPrice = 400.00m, Price = 440.00m },
                new Product { Name = "Apple iPhone 7S", CostPrice = 440.00m, Price = 500.00m },
                new Product { Name = "Apple iPhone 7S Plus", CostPrice = 480.00m, Price = 520.00m },
                new Product { Name = "Apple iPhone 7", CostPrice = 500.00m, Price = 550.00m },
                new Product { Name = "Apple iPhone 7 Plus", CostPrice = 520.00m, Price = 580.00m },
                new Product { Name = "Apple iPhone 8", CostPrice = 600.00m, Price = 640.00m },
                new Product { Name = "Apple iPhone 8 Plus", CostPrice = 680.00m, Price = 720.00m },
                new Product { Name = "Apple iPhone X", CostPrice = 900.00m, Price = 1200.00m }
            };

            var orders = new[]
            {
                new Order { CustomerId = customers[0].Id, CreatedAt = DateTime.Parse("2017/12/21"), OrderStatus = OrderStatus.Fulfilled, OrderNumber = 1 },
                new Order { CustomerId = customers[0].Id, CreatedAt = DateTime.Parse("2017/12/22"), OrderStatus = OrderStatus.Invoiced, OrderNumber = 2 },
                new Order { CustomerId = customers[1].Id, CreatedAt = DateTime.Parse("2017/12/27"), OrderStatus = OrderStatus.Created, OrderNumber = 3 }
            };

            var orderLines = new[]
            {
                new OrderLine { Quantity = 1, OrderId = orders[0].Id, ProductId = products[13].Id },
                new OrderLine { Quantity = 2, OrderId = orders[0].Id, ProductId = products[11].Id },
                new OrderLine { Quantity = 1, OrderId = orders[0].Id, ProductId = products[10].Id },
                new OrderLine { Quantity = 1, OrderId = orders[1].Id, ProductId = products[12].Id },
                new OrderLine { Quantity = 2, OrderId = orders[1].Id, ProductId = products[10].Id },
                new OrderLine { Quantity = 3, OrderId = orders[2].Id, ProductId = products[13].Id }
            };

            dataContext.Users.AddRange(users);
            dataContext.Customers.AddRange(customers);
            dataContext.Products.AddRange(products);
            dataContext.Orders.AddRange(orders);
            dataContext.OrderLines.AddRange(orderLines);

            dataContext.SaveChanges();
        }
    }
}
