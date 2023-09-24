USE RealEstateAgency;
GO

CREATE PROCEDURE InsertApartment
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000),
    @NumberOfRooms INT,
    @Area DECIMAL(10, 2),
    @SeparateBathroom BIT,
    @HasPhone BIT,
    @MaxPrice DECIMAL(10, 2),
    @AdditionalPreferences NVARCHAR(1000)
AS
BEGIN
    INSERT INTO Apartments (Name, Description, NumberOfRooms, Area, SeparateBathroom, HasPhone, MaxPrice, AdditionalPreferences)
    VALUES (@Name, @Description, @NumberOfRooms, @Area, @SeparateBathroom, @HasPhone, @MaxPrice, @AdditionalPreferences)
END;
