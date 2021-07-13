using System.Collections.Generic;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Seeder
{
    public class RealEstateDbContextSeeder : ISeedDatabase
    {
        private readonly RealEstateDbContext Context;

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

        private readonly List<string> TrateTypeData = new List<string>
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

        public RealEstateDbContextSeeder(RealEstateDbContext context)
        {
            this.Context = context;
        }

        public void Seed()
        {
            this.InsertPropertyTypes();
            this.InsertCurrencyTypes();
            this.InsertFutures();
            this.InsertTradeTypes();
            this.InsertRegions();

        }

        private void InsertPropertyTypes()
        {

            List<EstateType> entityTypes = new List<EstateType>();

            foreach (var propertyType in PropertyTypeData)
            {
                entityTypes.Add(new EstateType
                {
                    TypeOfProperty = propertyType,
                });
            }

            this.Context.EstateTypes.AddRange(entityTypes);
            this.Context.SaveChanges();
        }

        private void InsertCurrencyTypes()
        {
            foreach (var currency in CurrenciesData)
            {
                this.Context.Add(new Currency
                {
                    CurrencyCode = currency,
                });
            }

            this.Context.SaveChanges();
        }

        private void InsertFutures()
        {
            foreach (var future in FuturesData)
            {
                this.Context.Features.Add(new Feature
                {
                    FutureDescription = future,
                });
            }

            this.Context.SaveChanges();
        }

        private void InsertTradeTypes()
        {
            foreach (var tradeType in TrateTypeData)
            {
                this.Context.TradeTypes.Add(new TradeType
                {
                    TypeOfTransaction = tradeType,
                });
            }

            this.Context.SaveChanges();
        }

        private void InsertRegions()
        {
            foreach (var area in this.AreasAndCities)
            {
                Area dbArea = new Area
                {
                    AreaName = area.Key
                };

                this.Context.Areas.Add(dbArea);

                foreach (var city in area.Value)
                {
                    var dbCity = new City
                    {
                        CityName = city,
                        Area = dbArea,
                    };

                    this.Context.Cities.Add(dbCity);

                    foreach (var cityRegion in this.RegionsData)
                    {
                        var region = new Neighborhood
                        {
                            Name = cityRegion,
                            City = dbCity
                        };

                        this.Context.Neighborhoods.Add(region);
                    }
                }
            }

            this.Context.SaveChanges();
        }
    }
}