using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Data;
using RealEstate.Models;

using static RealEstate.Common.WebConstants;

namespace RealEstate.Seeder
{
    public class RealEstateConstantsSeeder : ISeedDatabase
    {
        private const string AdministratorEmail = "Admin@RealEstate.com";

        private const string AdministratorPassword = "admin123";

        private const string AdministratorUsername = "Admin@admin.com";

        private readonly List<string> PropertyTypeData = new List<string>
        {
            "1 - СТАЕН",
            "2 - СТАЕН",
            "3 - СТАЕН",
            "4 - СТАЕН",
            "5 - СТАЕН",
            "МНОГОСТАЕН",
            "МЕЗОНЕТ",
            "ОФИС",
            "АТЕЛИE",
            "ТАВАН",
            "ЕТАЖ ОТ КЪЩА",
            "КЪЩА",
            "ВИЛА",
            "МАГАЗИН",
            "ЗАВЕДЕНИЕ",
            "СКЛАД",
            "ГАРАЖ",
            "ПРОМ.ПОМЕЩЕНИЕ",
            "ХОТЕЛ",
            "ПАРЦЕЛ",
            "ЗЕМЕДЕЛСКА ЗЕМЯ",
            "БИЗНЕС ИМОТ",
        };

        private readonly List<string> CurrenciesData = new List<string>
        {
            "EUR", "BGN", "USD"
        };

        private readonly List<string> FuturesData = new List<string>
        {
            "В Строеж",
            "С Преход",
            "С Асансьор",
            "С гараж ",
            "С паркинг",
            "С действащ бизнес",
            "В затворен комплекс",
            "Лизинг",
            "Ипотекиран",
            "Бартер",
            "Интернет връзка",
            "Обзаведен",
            "Видео наблюдение",
            "Контрол на достъпа",
            "Охрана",
            "Саниран",
        };

        private readonly List<string> TradeTypeData = new List<string>
        {
           "Купувам", "Продавам", "Наемам", "Заменям", "Съквартиранти"
        };

        private readonly List<string> RegionsData = new List<string>
        {
            "Централна част на града",
            "Северена част на града",
            "Източна част на града",
            "Южна част на града",
            "Западна част на града",
            "Извън жилищната част на града"
        };

        private readonly Dictionary<string, List<string>> AreasAndCities = new Dictionary<string, List<string>>
        {
            ["Благоевград"] = new List<string>
            {
                "Банско",
                "Белица",
                "Благоевград",
                "Гоце Делчев",
                "Гърмен",
                "Кресна",
                "Петрич",
                "Разлог",
                "Сандански",
                "Сатовча",
                "Симитли",
                "Струмяни",
                "Хаджидимово",
                "Якоруда",

            },

            ["Бургас"] = new List<string>
            {
                "Айтос",
                "Бургас",
                "Камено",
                "Карнобат",
                "Малко Търново",
                "Несебър",
                "Поморие",
                "Приморско",
                "Руен",
                "Созопол",
                "Средец",
                "Сунгурларе",
                "Царево",

            },

            ["Варна"] = new List<string>
            {
                "Аврен",
                "Аксаково",
                "Белослав",
                "Бяла",
                "Варна",
                "Ветрино",
                "Вълчи дол",
                "Девня",
                "Долни чифлик",
                "Дългопол",
                "Провадия",
                "Суворово",
            },
        };

        public void SeedConstantData(RealEstateDbContext context)
        {
            this.InsertPropertyTypes(context);
            this.InsertCurrencyTypes(context);
            this.InsertFutures(context);
            this.InsertTradeTypes(context);
            this.InsertRegions(context);
        }

        private void InsertPropertyTypes(RealEstateDbContext context)
        {
            if (context.EstateTypes.Any())
            {
                return;
            }

            List<EstateType> entityTypes = new List<EstateType>();

            foreach (var propertyType in PropertyTypeData)
            {
                entityTypes.Add(new EstateType
                {
                    TypeOfProperty = propertyType,
                });
            }

            context.EstateTypes.AddRange(entityTypes);
            context.SaveChanges();
        }

        private void InsertCurrencyTypes(RealEstateDbContext context)
        {
            if (context.Currencies.Any())
            {
                return;
            }

            foreach (var currency in CurrenciesData)
            {
                context.Currencies.Add(new Currency
                {
                    CurrencyCode = currency,
                });
            }

            context.SaveChanges();
        }

        private void InsertFutures(RealEstateDbContext context)
        {
            if (context.Features.Any())
            {
                return;
            }

            foreach (var future in FuturesData)
            {
                context.Features.Add(new Feature
                {
                    FutureDescription = future,
                });
            }

            context.SaveChanges();
        }

        private void InsertTradeTypes(RealEstateDbContext context)
        {
            if (context.TradeTypes.Any())
            {
                return;
            }

            foreach (var tradeType in TradeTypeData)
            {
                context.TradeTypes.Add(new TradeType
                {
                    TypeOfTransaction = tradeType,
                });
            }

            context.SaveChanges();
        }

        private void InsertRegions(RealEstateDbContext context)
        {
            if (context.Areas.Any() && context.Cities.Any() && context.Neighborhoods.Any())
            {
                return;
            }

            foreach (var area in this.AreasAndCities)
            {
                Area dbArea = new Area
                {
                    AreaName = area.Key
                };

                context.Areas.Add(dbArea);

                foreach (var city in area.Value)
                {
                    var dbCity = new City
                    {
                        CityName = city,
                        Area = dbArea,
                    };

                    context.Cities.Add(dbCity);

                    foreach (var cityRegion in this.RegionsData)
                    {
                        var region = new Neighborhood
                        {
                            Name = cityRegion,
                            City = dbCity
                        };

                        context.Neighborhoods.Add(region);
                    }
                }
            }

            context.SaveChanges();
        }

        public void SeedAdministrator(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                IdentityRole adminRole = new IdentityRole { Name = AdministratorRoleName, NormalizedName = AdministratorRoleNormalizedName };

                await roleManager.CreateAsync(adminRole);

                User administrator = new User
                {
                    Email = AdministratorEmail,
                    UserName = AdministratorUsername,
                    LockoutEnabled = false,
                };

                await userManager.CreateAsync(administrator, AdministratorPassword);

                await userManager.AddToRoleAsync(administrator, adminRole.Name);
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}