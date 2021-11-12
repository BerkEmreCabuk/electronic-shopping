using ElectronicShopping.Api.Helpers;
using ElectronicShopping.Api.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicShopping.Api.Infrastructure.Database
{
    public class SeedDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ElectronicShoppingDbContext(serviceProvider.GetRequiredService<DbContextOptions<ElectronicShoppingDbContext>>()))
            {
                if (!context.Users.Any())
                {
                    var user = new UserEntity()
                    {
                        Name = "Berk Emre",
                        Surname = "Çabuk",
                        Email = "berkemrecabuk@gmail.com",
                        UserName = "emrecabuk",
                        Password = SecurityHelper.ComputeSha256Hash("asd123")
                    };
                    user.Add();
                    context.Users.Add(user);
                }

                List<ItemEntity> itemList = new();
                if (!context.Items.Any())
                {
                    var item = new ItemEntity()
                    {
                        Name = "i12 TWS Dokunmatik Kablosuz Bluetooth Kulaklık",
                        Description = "i12 TWS Dokunmatik Kablosuz Bluetooth Kulaklık",
                        Code = "kc2758298",
                        Price = 59
                    };
                    item.Add();
                    context.Items.Add(item);
                    itemList.Add(item);

                    var item1 = new ItemEntity()
                    {
                        Name = "Samsung Galaxy Tab A7",
                        Description = "Samsung Galaxy Tab A7 Lite Wi-Fi SM-T220 Gri 32 GB 8.7",
                        Code = "kcm75455416",
                        Price = 1172
                    };
                    item1.Add();
                    context.Items.Add(item1);
                    itemList.Add(item1);

                    var item2 = new ItemEntity()
                    {
                        Name = "CVS DN 19812 Coffee Master Filtre Kahve Makinesi",
                        Description = "CVS DN 19812 Coffee Master Filtre Kahve Makinesi",
                        Code = "kcm90432875",
                        Price = 100
                    };
                    item2.Add();
                    context.Items.Add(item2);
                    itemList.Add(item2);
                }

                if (!context.Stocks.Any())
                {
                    var stock = new StockEntity()
                    {
                        Item = itemList[0],
                        FreeQuantity = 4,
                        Quantity = 8,
                    };
                    stock.Add();
                    context.Stocks.Add(stock);

                    var stock1 = new StockEntity()
                    {
                        Item = itemList[1],
                        FreeQuantity = 1,
                        Quantity = 1
                    };
                    stock1.Add();
                    context.Stocks.Add(stock1);

                    var stock2 = new StockEntity()
                    {
                        Item = itemList[2],
                        FreeQuantity = 3,
                        Quantity = 4
                    };
                    stock2.Add();
                    context.Stocks.Add(stock2);
                }

                context.SaveChanges();
            }
        }
    }
}
