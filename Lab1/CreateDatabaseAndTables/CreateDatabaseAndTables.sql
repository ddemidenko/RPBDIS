-- �������� ���� ������
CREATE DATABASE RealEstateAgency;
USE RealEstateAgency;

-- ������� "��������"
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

-- ������� "��������"
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

-- ������� "��������"
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

-- ������� "������"
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY,
    Name NVARCHAR(255),
    Description NVARCHAR(1000),
    Price DECIMAL(10, 2)
);

-- ������� "������������" (��������������� ������� ��� ����� ��������� � �����)
CREATE TABLE ContractServices (
    ContractServiceID INT PRIMARY KEY,
    ContractID INT,
    ServiceID INT,
    FOREIGN KEY (ContractID) REFERENCES Contracts(ContractID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);
