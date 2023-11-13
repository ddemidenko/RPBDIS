using RealEstateAgency4.Models;
using System;
using System.Linq;

namespace RealEstateAgency4.Middleware
{
    public class DataInitializer
    {
        public static void Initialize(RealEstateAgencyContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Apartments.Any())
            {
                return;
            }
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
                string apartmentName = "Apartment_" + apartmentId.ToString();
                string apartmentDescription = "Description for " + apartmentName;
                int numberOfRooms = new Random().Next(1, 5);
                decimal area = (decimal)(new Random().NextDouble() * 200);
                bool separateBathroom = new Random().Next(2) == 0;
                bool hasPhone = new Random().Next(2) == 0;
                decimal maxPrice = (decimal)(new Random().NextDouble() * 500000);
                string additionalPreferences = "Additional preferences for " + apartmentName;

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
                string fullName = "Seller_" + sellerId.ToString();
                string gender = new Random().Next(2) == 0 ? "Male" : "Female";
                DateTime dateOfBirth = DateTime.Now.AddYears(-new Random().Next(30, 60));
                string address = "Address for " + fullName;
                string phone = "123-456-7890";
                string passportData = "Passport data for " + fullName;
                int apartmentId = new Random().Next(1, 35);
                string apartmentAddress = "Apartment address for " + fullName;
                decimal price = (decimal)(new Random().NextDouble() * 500000);
                string additionalInformation = "Additional information for " + fullName;

                dbContext.Sellers.Add(new Seller
                {
                    FullName = fullName,
                    Gender = gender,
                    DateOfBirth = dateOfBirth,
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

        private static void SeedServices(RealEstateAgencyContext dbContext)
        {
            for (int i = 1; i <= 1000; i++)
            {
                string serviceName = "Service_" + i;
                string description = $"Description for {serviceName}";
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
                string employee = "Employee_" + contractId.ToString();
                string fioBuyer = "Buyer_" + contractId.ToString();

                dbContext.Contracts.Add(new Contract
                {
                    DateOfContract = DateTime.Now.AddDays(-contractId),
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
