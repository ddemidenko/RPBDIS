using Microsoft.AspNetCore.Identity;
using RealEstateAgency4.Models;
using System;
using System.Data;
using System.Linq;

namespace RealEstateAgency4.Middleware
{
    public class DataInitializer
    {
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
            for (int apartmentId = 1; apartmentId <= 35; apartmentId++)
            {
                string apartmentName = "Квартира " + apartmentId.ToString();
                string apartmentDescription = "Описание для " + apartmentName;
                int numberOfRooms = new Random().Next(1, 5);
                decimal area = (decimal)(new Random().NextDouble() * 200);

                bool separateBathroom = new Random().Next(2) == 0 ? true : false;
                bool hasPhone = new Random().Next(2) == 0 ? true : false;

                decimal maxPrice = (decimal)(new Random().NextDouble() * 500000);
                string additionalPreferences = "Дополнительные предпочтения для " + apartmentName;

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
            for (int sellerId = 1; sellerId <= 35; sellerId++)
            {
                DateTime start = new DateTime(2023, 1, 1);
                DateTime randomDate = start.AddDays(-new Random().Next(365 * 30));

                string fullName = "Продавец " + sellerId.ToString();
                string gender = new Random().Next(2) == 0 ? "Муж." : "Жен.";
                string address = "Адрес " + fullName;
                string passportData;
                do
                {
                    passportData = "HB" + GenerateRandomPassportDigits();
                } while (dbContext.Sellers.Any(s => s.PassportData == passportData));

                int apartmentId = new Random().Next(1, 35);
                string apartmentAddress = "Адрес квартиры для " + fullName;
                string additionalInformation = "Дополнительная информация для " + fullName;
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
                string serviceName = "Услуга " + i;
                string description = $"Описание для {serviceName}";
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
            for (int contractId = 1; contractId <= 300; contractId++)
            {
                int sellerId = new Random().Next(1, 35);
                decimal dealAmount = (decimal)(new Random().NextDouble() * 500000);
                decimal serviceCost = (decimal)(new Random().NextDouble() * 1000);
                string employee = "Сотрудник " + contractId.ToString();
                string fioBuyer = "Покупатель " + contractId.ToString();
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
            for (int contractId = 1; contractId <= 300; contractId++)
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
