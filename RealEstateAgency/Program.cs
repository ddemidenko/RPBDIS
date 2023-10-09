using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealEstateAgency
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Выборка всех услуги");
                Console.WriteLine("2. Выборка услуг с ценой выше 900");
                Console.WriteLine("3. Вывести группированные данные о квартирах (группировка по количеству комнат)");
                Console.WriteLine("4. Вывести договоры по услугам");
                Console.WriteLine("5. Вывести данные о квартирах с высокой стоимостью договоров (свыше 500_000)");
                Console.WriteLine("6. Добавить новую услугу");
                Console.WriteLine("7. Добавить новый договор");
                Console.WriteLine("8. Удалить услугу");
                Console.WriteLine("9. Удалить договор");
                Console.WriteLine("10. Обновить цены на услуги (если стоимость меньше 100, поднмаем в полтора раза)");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            DisplayAllServices();
                            break;
                        case 2:
                            DisplayServicesAbovePrice();
                            break;
                        case 3:
                            DisplayGroupedApartments();
                            break;
                        case 4:
                            DisplayServiceContracts();
                            break;
                        case 5:
                            DisplayApartmentsWithHighValueContracts();
                            break;
                        case 6:
                            AddNewService();
                            break;
                        case 7:
                            AddNewContract();
                            break;
                        case 8:
                            DeleteService();
                            break;
                        case 9:
                            DeleteContract();
                            break;
                        case 10:
                            UpdateServicePrices();
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, выберите снова.");
                }
            }
            
        }
        static void DisplayAllServices()
        {
            using (var context = new RealEstateAgencyContext()) //Выборка всех данных из таблицы Services
            {
                var allServices = context.Services.ToList();

                foreach (var service in allServices)
                {
                    Console.WriteLine($"Номер услуги: {service.ServiceId} | Название услуги: {service.Name} | Цена услуги: {service.Price} | Описание услуги: {service.Description}");
                }
            }
        }
        static void DisplayServicesAbovePrice()
        {
            using (var context = new RealEstateAgencyContext())  //Выборка данных из таблицы, отфильтрованную по определенному условию, налагающему ограничения на одно поле
            {
                decimal priceThreshold = 900.0m;

                var selectedServices = context.Services
                    .Where(service => service.Price > priceThreshold)
                    .ToList();

                foreach (var service in selectedServices)
                {
                    Console.WriteLine($"Номер услуги: {service.ServiceId} | Название услуги: {service.Name} | Цена услуги: {service.Price} | Описание услуги: {service.Description}");
                }
            }
        }

        static void DisplayGroupedApartments()
        {
            using (var context = new RealEstateAgencyContext()) //Выборка данных, сгруппированных по любому из полей данных с выводом какого-либо итогового результата по выбранным полям из таблицы Apartments
            {
                var groupedApartments = context.Apartments
                    .GroupBy(apartment => apartment.NumberOfRooms)
                    .Select(group => new
                    {
                        NumberOfRooms = group.Key,
                        AverageArea = group.Average(apartment => apartment.Area),
                        MaxPrice = group.Max(apartment => apartment.MaxPrice),
                        Count = group.Count()
                    })
                    .ToList();

                foreach (var group in groupedApartments)
                {
                    Console.WriteLine($"Количество комнат: {group.NumberOfRooms} | Средняя площадь: {group.AverageArea} | Максимальная стоимость: {group.MaxPrice} | Количество квартир: {group.Count}");
                }
            }
        }

        static void DisplayServiceContracts()
        {
            using (var context = new RealEstateAgencyContext()) //Выборка данных из двух полей двух таблиц, связанных между собой отношением «один-ко-многим»
            {
                var serviceContracts = context.Services
                    .SelectMany(service => service.ContractServices, (service, contractService) => new
                    {
                        ServiceName = service.Name,
                        ContractDate = contractService.Contract.DateOfContract.GetValueOrDefault().ToShortDateString()
                    })
                    .ToList();

                foreach (var entry in serviceContracts)
                {
                    Console.WriteLine($"Услуга: {entry.ServiceName}, Дата заключения договора: {entry.ContractDate}");
                }
            }
        }

        static void DisplayApartmentsWithHighValueContracts()
        {

            using (var context = new RealEstateAgencyContext()) //Выборка данных из двух таблиц Apartments и Contracts, связанных между собой отношением «один-ко-многим» и отфильтрованным по некоторому условию, налагающему ограничения на значения одного или нескольких полей
            {
                var apartmentsWithHighValueContracts = context.Apartments
                .Where(apartment =>
                    context.Contracts.Any(contract =>
                        contract.BuyerId == apartment.ApartmentId &&
                        contract.DealAmount > 500000))
                .ToList();

                foreach (var apartment in apartmentsWithHighValueContracts)
                {
                    Console.WriteLine($"Квартиры, по цене договора свыше 500000 руб.: {apartment.Name}");
                }
            }
        }


        static void AddNewService()
        {
            using (var context = new RealEstateAgencyContext()) //Вставка данных в таблицу Services, стоящей на стороне отношения «Один»  
            {
                var newService = new Services
                {
                    Name = "Новая услуга",
                    Description = "Описание новой услуги",
                    Price = 1500.00m
                };
                context.Services.Add(newService);
                context.SaveChanges();

                Console.WriteLine("Новая услуга успешно добавлена.");
            }
        }

        static void AddNewContract()
        {
            using (var context = new RealEstateAgencyContext()) //Вставка данных в таблицу Contracts, стоящей на стороне отношения «Многие»  
            {

                int sellerId = context.Sellers.First().SellerId;

                var newContract = new Contracts
                {
                    DateOfContract = DateTime.Now,
                    SellerId = sellerId,
                    BuyerId = 3,
                    DealAmount = 200000.00m,
                    ServiceCost = 5000.0m,
                    Employee = "Новый сотрудник",
                    Fiobuyer = "Новый покупатель"
                };
                context.Contracts.Add(newContract);
                context.SaveChanges();

                Console.WriteLine("Новый договор успешно добавлен.");
            }
        }




        static void DeleteService()
        {
            using (var context = new RealEstateAgencyContext()) //Удаление данных из таблицы Services, стоящей на стороне отношения «Один» 
            {
                var serviceToDelete = context.Services.FirstOrDefault(s => s.Name == "Новая услуга");

                if (serviceToDelete != null)
                {
                    context.Services.Remove(serviceToDelete);
                    context.SaveChanges();

                    Console.WriteLine("Услуга успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Услуга не найдена.");
                }
            }
        }

        static void DeleteContract()
        {
            using (var context = new RealEstateAgencyContext()) //Удаление данных из таблицы Contracts, стоящей на стороне отношения «Многие» 
            {

                var contractToDelete = context.Contracts.FirstOrDefault(c => c.ContractId == 0);

                if (contractToDelete != null)
                {
                    context.Contracts.Remove(contractToDelete);
                    context.SaveChanges();

                    Console.WriteLine("Договор успешно удален.");
                }
                else
                {
                    Console.WriteLine("Договор не найден.");
                }
            }
        }

        static void UpdateServicePrices()
        {
            using (var context = new RealEstateAgencyContext()) //Обновление удовлетворяющих определенному условию записей в таблице Services, если цена ниже 100, тогда увечиваем её в полтора раза
            {
                var servicesToUpdate = context.Services.Where(s => s.Price < 100);

                foreach (var service in servicesToUpdate)
                {
                    if (service.Price.HasValue)
                    {
                        service.Price *= 1.5m;
                    }

                }
                context.SaveChanges();

                Console.WriteLine("Записи успешно обновлены.");
            }
        }
    }
}
