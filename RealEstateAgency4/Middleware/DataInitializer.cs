using Microsoft.AspNetCore.Identity;
using RealEstateAgency4.Models;
using System;
using System.Data;
using System.Linq;

namespace RealEstateAgency4.Middleware
{
    public class DataInitializer
    {
      private static string[] apartmentNames = new string[19]
{
    "Уютное гнездышко",
    "Современная студия",
    "Панорамный вид",
    "Семейное жилье",
    "Элегантный дизайн",
    "Лофт в центре",
    "Роскошная квартира",
    "Зеленый район",
    "Морская тематика",
    "Индустриальный стиль",
    "Арт-студия",
    "Квартира с террасой",
    "Ультрасовременная архитектура",
    "Старинный шарм",
    "Технологичный дом",
    "Атмосфера загородного дома",
    "Футуристический интерьер",
    "Стеклянные стены",
    "Пентхаус в небоскребе"
};

        private static string[] apartmentDescriptions = new string[19]
        {
    "Прекрасное жилье для одиноких путешественников.",
    "Современная студия с минималистичным дизайном.",
    "Квартира с потрясающим панорамным видом из окна.",
    "Просторное жилье для всей семьи.",
    "Интерьер с элегантной и стильной отделкой.",
    "Лофт в историческом центре города.",
    "Роскошная квартира с высокими потолками.",
    "Жилье в зеленом и экологичном районе.",
    "Квартира с морской тематикой и уютной атмосферой.",
    "Стильный дом в индустриальном стиле.",
    "Арт-студия для творческих личностей.",
    "Квартира с уютной террасой для отдыха.",
    "Инновационный дом с современной архитектурой.",
    "Жилье с античным стилем и уникальным шармом.",
    "Технологичный дом с последними новинками.",
    "Атмосфера загородного дома в городе.",
    "Футуристический интерьер с передовыми технологиями.",
    "Квартира с стеклянными стенами и открытым пространством.",
    "Роскошный пентхаус в высоком небоскребе."
        };

        private static string[] additionalPreferences_ = new string[19]
        {
    "Идеальное место для уединения и покоя.",
    "Современные технологии для комфортной жизни.",
    "Уникальные закаты и рассветы каждый день.",
    "Большие комнаты и просторные зоны отдыха.",
    "Элегантная мебель и дизайнерские элементы интерьера.",
    "Близость к историческим достопримечательностям города.",
    "Высококлассные отделочные материалы и оборудование.",
    "Окружение природы и парковых зон в шаговой доступности.",
    "Морской интерьер с подчеркнутой атмосферой спокойствия.",
    "Индивидуальность и выделение среди других.",
    "Креативная атмосфера для вдохновения и творчества.",
    "Открытая терраса для приемов на свежем воздухе.",
    "Современные технологии и интеллектуальные решения.",
    "Уникальный дизайн в сочетании с античными элементами.",
    "Высокотехнологичные системы безопасности и комфорта.",
    "Природный ландшафт и уединенное расположение.",
    "Эксклюзивный интерьер с использованием новейших технологий.",
    "Просторные помещения и ощущение свободы.",
    "Эксклюзивный пентхаус с видом на весь город."
        };

    private static string[] sellerFullNames = new string[20]
{
    "Иванов Иван Иванович",
    "Петрова Анна Сергеевна",
    "Смирнов Алексей Владимирович",
    "Кузнецова Екатерина Александровна",
    "Михайлов Дмитрий Петрович",
    "Федорова Ольга Николаевна",
    "Соколов Игорь Андреевич",
    "Богданова Мария Васильевна",
    "Новиков Сергей Павлович",
    "Козлова Татьяна Дмитриевна",
    "Морозов Артем Станиславович",
    "Волкова Надежда Игоревна",
    "Алексеев Антон Юрьевич",
    "Лебедева Юлия Сергеевна",
    "Семенов Александр Александрович",
    "Егорова Алена Валентиновна",
    "Павлов Николай Валерьевич",
    "Козлов Ярослав Игоревич",
    "Степанова Виктория Анатольевна",
    "Николаев Павел Михайлович"
};
        private static string[] sellerAddresses = new string[20]
{
    "г. Москва, ул. Пушкина, д. 10, кв. 15",
    "г. Санкт-Петербург, пр. Ленина, д. 25, кв. 8",
    "г. Екатеринбург, ул. Гагарина, д. 5, кв. 3",
    "г. Новосибирск, пер. Садовый, д. 12, кв. 7",
    "г. Казань, ул. Лермонтова, д. 18, кв. 21",
    "г. Нижний Новгород, пр. Мира, д. 30, кв. 1",
    "г. Челябинск, ул. Кирова, д. 7, кв. 10",
    "г. Самара, пер. Цветочный, д. 3, кв. 22",
    "г. Омск, ул. Новая, д. 15, кв. 6",
    "г. Ростов-на-Дону, пр. Победы, д. 20, кв. 14",
    "г. Уфа, ул. Солнечная, д. 8, кв. 11",
    "г. Красноярск, пр. Революции, д. 19, кв. 9",
    "г. Воронеж, ул. Звездная, д. 6, кв. 25",
    "г. Волгоград, пер. Радужный, д. 11, кв. 17",
    "г. Курск, ул. Школьная, д. 4, кв. 23",
    "г. Иркутск, пр. Юности, д. 22, кв. 2",
    "г. Тюмень, ул. Озерная, д. 14, кв. 19",
    "г. Барнаул, пер. Сосновый, д. 9, кв. 13",
    "г. Ярославль, ул. Речная, д. 17, кв. 16",
    "г. Тольятти, пр. Заречный, д. 23, кв. 4"
};

        private static string[] sellerApartmentAddresses = new string[20]
        {
    "г. Москва, ул. Садовая, д. 5, кв. 15",
    "г. Санкт-Петербург, пр. Городской, д. 12, кв. 8",
    "г. Екатеринбург, ул. Набережная, д. 18, кв. 3",
    "г. Новосибирск, пер. Луговой, д. 8, кв. 7",
    "г. Казань, ул. Весенняя, д. 25, кв. 21",
    "г. Нижний Новгород, пр. Октябрьский, д. 30, кв. 1",
    "г. Челябинск, ул. Центральная, д. 7, кв. 10",
    "г. Самара, пер. Лесной, д. 3, кв. 22",
    "г. Омск, ул. Речная, д. 15, кв. 6",
    "г. Ростов-на-Дону, пр. Звездный, д. 20, кв. 14",
    "г. Уфа, ул. Первомайская, д. 8, кв. 11",
    "г. Красноярск, пр. Молодежный, д. 19, кв. 9",
    "г. Воронеж, ул. Зеленая, д. 6, кв. 25",
    "г. Волгоград, пер. Солнечный, д. 11, кв. 17",
    "г. Курск, ул. Молодежная, д. 4, кв. 23",
    "г. Иркутск, пр. Солнечный, д. 22, кв. 2",
    "г. Тюмень, ул. Лесная, д. 14, кв. 19",
    "г. Барнаул, пер. Зеленый, д. 9, кв. 13",
    "г. Ярославль, ул. Солнечная, д. 17, кв. 16",
    "г. Тольятти, пр. Луговой, д. 23, кв. 4"
        };

        private static string[] sellerAdditionalInformation = new string[20]
        {
    "Профессиональный риэлтор с опытом работы более 10 лет.",
    "Заботливый и ответственный продавец.",
    "Увлекаюсь декором и созданием уюта в доме.",
    "Гарантирую конфиденциальность и внимательное отношение к каждому клиенту.",
    "Опыт в сфере недвижимости и знание рынка.",
    "Всегда готов предоставить дополнительные фотографии и информацию.",
    "Индивидуальный подход к каждому клиенту.",
    "Активный и целеустремленный в решении задач.",
    "Открыт к долгосрочному сотрудничеству.",
    "Постоянно обучаюсь и следую трендам в недвижимости.",
    "Внимание к деталям и стремление к совершенству.",
    "Сертифицированный специалист в сфере жилой недвижимости.",
    "Индивидуальные условия для каждого клиента.",
    "Профессиональный стиль и умение находить общий язык с клиентами.",
    "Гибкий график работы для вашего удобства.",
    "Внимание к требованиям и пожеланиям клиента.",
    "Информационная поддержка на всех этапах сделки.",
    "Индивидуальные условия для клиентов семей с детьми.",
    "Готов предложить уникальные объекты недвижимости.",
    "Забота о клиенте и стремление сделать процесс продажи максимально комфортным."
        };

        private static string[] serviceNames = new string[20]
{
    "Юридическое сопровождение сделки",
    "Оценка недвижимости",
    "Помощь в получении ипотеки",
    "Подбор объектов по критериям клиента",
    "Консультации по рынку недвижимости",
    "Продвижение объекта на рынке",
    "Оформление документов по сделке",
    "Ремонт и дизайн интерьера",
    "Страхование недвижимости",
    "Проведение показов объектов",
    "Подготовка к продаже объекта",
    "Помощь в выборе района для покупки",
    "Контроль соблюдения законодательства",
    "Профессиональная фотосъемка объектов",
    "Экспертиза документов на объект",
    "Проведение переговоров по цене",
    "Подготовка презентаций объектов",
    "Консультации по обустройству жилья",
    "Услуги по дизайну и архитектуре",
    "Помощь в решении юридических вопросов"
};

        private static string[] serviceDescriptions = new string[20]
        {
    "Полное юридическое сопровождение сделки с недвижимостью.",
    "Профессиональная оценка стоимости вашей недвижимости.",
    "Помощь в оформлении ипотеки на выгодных условиях.",
    "Подбор объектов недвижимости, соответствующих вашим требованиям.",
    "Консультации по текущей ситуации на рынке недвижимости.",
    "Продвижение вашего объекта на рынке для быстрой продажи.",
    "Оформление необходимых документов по совершению сделки.",
    "Ремонт и дизайн интерьера вашей недвижимости.",
    "Страхование вашего жилья на выгодных условиях.",
    "Проведение качественных показов объектов для покупателей.",
    "Подготовка вашего объекта к успешной продаже.",
    "Помощь в выборе оптимального района для покупки жилья.",
    "Контроль за соблюдением законодательства при совершении сделки.",
    "Профессиональная фотосъемка для привлечения внимания к объекту.",
    "Экспертиза документов на объект для безопасной сделки.",
    "Проведение эффективных переговоров по цене объекта.",
    "Подготовка презентаций объектов для привлечения покупателей.",
    "Консультации по обустройству и ремонту при покупке жилья.",
    "Услуги по дизайну и архитектуре вашей недвижимости.",
    "Помощь в решении юридических вопросов при сделках с недвижимостью."
        };

        private static string[] employeeFullNames = new string[20]
        {
    "Иванов Иван Иванович",
    "Петров Петр Петрович",
    "Сидоров Сидор Сидорович",
    "Алексеев Алексей Алексеевич",
    "Николаев Николай Николаевич",
    "Козлов Константин Константинович",
    "Морозов Михаил Михайлович",
    "Федоров Федор Федорович",
    "Андреев Андрей Андреевич",
    "Васильев Василий Васильевич",
    "Степанов Степан Степанович",
    "Егоров Егор Егорович",
    "Макаров Макар Макарович",
    "Григорьев Григорий Григорьевич",
    "Дмитриев Дмитрий Дмитриевич",
    "Артемьев Артем Артемьевич",
    "Сергеев Сергей Сергеевич",
    "Яковлев Яков Яковлевич",
    "Антонов Антон Антонович",
    "Арсеньев Арсений Арсеньевич"
        };

        private static string[] buyerFullNames = new string[20]
        {
    "Смирнова Елена Петровна",
    "Иванова Анна Владимировна",
    "Кузнецова Мария Игоревна",
    "Соколова Наталья Дмитриевна",
    "Попова Ольга Александровна",
    "Лебедева Татьяна Васильевна",
    "Морозова Екатерина Алексеевна",
    "Новикова Анастасия Андреевна",
    "Алексеева Юлия Сергеевна",
    "Федорова Светлана Витальевна",
    "Сидорова Дарья Игоревна",
    "Антонова Виктория Артемовна",
    "Артемьева Ксения Степановна",
    "Петрова Евгения Николаевна",
    "Сергеева Алина Владиславовна",
    "Козлова Ирина Валерьевна",
    "Макарова Любовь Михайловна",
    "Андреева Елена Владимировна",
    "Яковлева Валентина Викторовна",
    "Григорьева Анна Сергеевна"
        };

        public static void Initialize(RealEstateAgencyContext dbContext, UserManager<IdentityUser> userManager)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Apartments.Any())
            {
                return;
            }
            SeedRoles(dbContext);
            SeedAdmin(dbContext, userManager);
            SeedApartments(dbContext);   
            SeedSellers(dbContext);
            SeedServices(dbContext);
            SeedContracts(dbContext);
            SeedContractServices(dbContext);

            dbContext.SaveChanges();
        }

        private static void SeedApartments(RealEstateAgencyContext dbContext)
        {
            for (int apartmentId = 1; apartmentId <= 500; apartmentId++)
            {
                string apartmentName = apartmentNames[new Random().Next(0, apartmentNames.Length)];
                string apartmentDescription = apartmentDescriptions[new Random().Next(0, apartmentDescriptions.Length)];
                int numberOfRooms = new Random().Next(1, 5);
                decimal area = (decimal)(new Random().NextDouble() * 200);

                bool separateBathroom = new Random().Next(2) == 0 ? true : false;
                bool hasPhone = new Random().Next(2) == 0 ? true : false;

                decimal maxPrice = (decimal)(new Random().NextDouble() * 500000);
                string additionalPreferences = additionalPreferences_[new Random().Next(0, additionalPreferences_.Length)];

                dbContext.Apartments.Add(new Apartment
                {
                    Name = apartmentName,
                    Description = apartmentDescription,
                    NumberOfRooms = numberOfRooms,
                    Area = area,
                    SeparateBathroom = separateBathroom,
                    HasPhone = hasPhone,
                    MaxPrice = maxPrice,
                    AdditionalPreferences = additionalPreferences
                });
            }

            dbContext.SaveChanges();
        }




        private static void SeedSellers(RealEstateAgencyContext dbContext)
        {
            for (int sellerId = 1; sellerId <= 500; sellerId++)
            {
                DateTime start = new DateTime(2023, 1, 1);
                DateTime randomDate = start.AddDays(-new Random().Next(365 * 30));

                string fullName = sellerFullNames[new Random().Next(0, sellerFullNames.Length)];
                string gender = new Random().Next(2) == 0 ? "Муж." : "Жен.";
                string address = sellerAddresses[new Random().Next(0, sellerAddresses.Length)];
                string passportData;
                do
                {
                    passportData = "HB" + GenerateRandomPassportDigits();
                } while (dbContext.Sellers.Any(s => s.PassportData == passportData));

                int apartmentId = new Random().Next(1, 35);
                string apartmentAddress = sellerApartmentAddresses[new Random().Next(0, sellerApartmentAddresses.Length)];
                string additionalInformation = sellerAdditionalInformation[new Random().Next(0, sellerAdditionalInformation.Length)];
                decimal price = (decimal)(new Random().NextDouble() * 500000);
                string phone;
                do
                {
                    phone = GenerateRandomPhoneNumber();
                } while (dbContext.Sellers.Any(s => s.Phone == phone));

                dbContext.Sellers.Add(new Seller
                {
                    FullName = fullName,
                    Gender = gender,
                    DateOfBirth = randomDate,
                    Address = address,
                    Phone = phone,
                    PassportData = passportData,
                    ApartmentId = apartmentId,
                    ApartmentAddress = apartmentAddress,
                    Price = price,
                    AdditionalInformation = additionalInformation
                });
            }

            dbContext.SaveChanges();
        }
        private static string GenerateRandomPassportDigits()
        {
            Random random = new Random();
            return $"{random.Next(1000000, 9999999)}";
        }
        private static string GenerateRandomPhoneNumber()
        {
            Random random = new Random();
            return $"{random.Next(100, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
        }

        private static void SeedRoles(RealEstateAgencyContext dbContext)
        {
            dbContext.Roles.Add(new IdentityRole() { Name = "admin", NormalizedName = "ADMIN"});
            dbContext.Roles.Add(new IdentityRole() { Name = "user", NormalizedName = "USER" });
            dbContext.SaveChanges();
            
        }


        private static void SeedAdmin(RealEstateAgencyContext dbContext, UserManager<IdentityUser> userManager)
        {
            var admin = new IdentityUser()
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com"
            };
            userManager.CreateAsync(admin, "Test1!").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(admin, "admin").GetAwaiter().GetResult(); 
            dbContext.SaveChanges();
        }

        private static void SeedServices(RealEstateAgencyContext dbContext)
        {
            for (int i = 1; i <= 1000; i++)
            {
                string serviceName = serviceNames[new Random().Next(0, serviceNames.Length)]; 
                string description = serviceDescriptions[new Random().Next(0, serviceDescriptions.Length)];
                decimal price = (decimal)(new Random().NextDouble() * 100);

                dbContext.Services.Add(new Service
                {
                    Name = serviceName,
                    Description = description,
                    Price = price
                });
            }

            dbContext.SaveChanges();
        }



        private static void SeedContracts(RealEstateAgencyContext dbContext)
        {
            for (int contractId = 1; contractId <= 500; contractId++)
            {
                int sellerId = new Random().Next(1, 35);
                decimal dealAmount = (decimal)(new Random().NextDouble() * 500000);
                decimal serviceCost = (decimal)(new Random().NextDouble() * 1000);
                string employee = employeeFullNames[new Random().Next(0, employeeFullNames.Length)];
                string fioBuyer = buyerFullNames[new Random().Next(0, buyerFullNames.Length)];
                DateTime start = new DateTime(2023, 1, 1);
                int range = (DateTime.Now - start).Days;
                DateTime randomDate = start.AddDays(new Random().Next(range));

                dbContext.Contracts.Add(new Contract
                {
                    DateOfContract = randomDate,
                    SellerId = sellerId,
                    DealAmount = dealAmount,
                    ServiceCost = serviceCost,
                    Employee = employee,
                    Fiobuyer = fioBuyer
                });
            }

            dbContext.SaveChanges();
        }


        private static void SeedContractServices(RealEstateAgencyContext dbContext)
        {
            for (int contractId = 1; contractId <= 500; contractId++)
            {

                    int serviceId = new Random().Next(1, 1000);

                    var existingService = dbContext.Services.Find(serviceId);

                    if (existingService != null)
                    {
                        var contractService = new ContractService
                        {
                            ContractId = contractId,
                            ServiceId = serviceId
                        };

                        dbContext.ContractServices.Add(contractService);
                    }
            }

            dbContext.SaveChanges();
        }





    }
}
