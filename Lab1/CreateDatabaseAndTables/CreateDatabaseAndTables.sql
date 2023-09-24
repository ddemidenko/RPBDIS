-- Создание базы данных
CREATE DATABASE RealEstateAgency;
USE RealEstateAgency;

-- Таблица "Квартиры"
CREATE TABLE Apartments (
    ApartmentID INT PRIMARY KEY,
    Name NVARCHAR(255),
    Description NVARCHAR(1000),
    NumberOfRooms INT,
    Area DECIMAL(10, 2),
    SeparateBathroom BIT,
    HasPhone BIT,
    MaxPrice DECIMAL(10, 2),
    AdditionalPreferences NVARCHAR(1000)
);

-- Таблица "Продавцы"
CREATE TABLE Sellers (
    SellerID INT PRIMARY KEY,
    FullName NVARCHAR(255),
    Gender NVARCHAR(10),
    DateOfBirth DATE,
    Address NVARCHAR(500),
    Phone NVARCHAR(20),
    PassportData NVARCHAR(255),
    ApartmentID INT,
    ApartmentAddress NVARCHAR(500),
    Price DECIMAL(10, 2),
    AdditionalInformation NVARCHAR(1000),
    FOREIGN KEY (ApartmentID) REFERENCES Apartments(ApartmentID)
);

-- Таблица "Договоры"
CREATE TABLE Contracts (
    ContractID INT PRIMARY KEY,
    DateOfContract DATE,
    SellerID INT,
    BuyerID INT,
    DealAmount DECIMAL(10, 2),
    ServiceCost DECIMAL(10, 2),
    Employee NVARCHAR(255),
    FOREIGN KEY (SellerID) REFERENCES Sellers(SellerID),
    FOREIGN KEY (BuyerID) REFERENCES Apartments(ApartmentID)
);

-- Таблица "Услуги"
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY,
    Name NVARCHAR(255),
    Description NVARCHAR(1000),
    Price DECIMAL(10, 2)
);

-- Таблица "СделкиУслуги" (вспомогательная таблица для связи договоров и услуг)
CREATE TABLE ContractServices (
    ContractServiceID INT PRIMARY KEY,
    ContractID INT,
    ServiceID INT,
    FOREIGN KEY (ContractID) REFERENCES Contracts(ContractID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);
