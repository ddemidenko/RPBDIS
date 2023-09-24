CREATE PROCEDURE InsertSeller
    @FullName NVARCHAR(255),
    @Gender NVARCHAR(10),
    @Birthdate DATE,
    @Address NVARCHAR(500),
    @PhoneNumber NVARCHAR(15),
    @PassportData NVARCHAR(255)
AS
BEGIN
    INSERT INTO Sellers (FullName, Gender, DateOfBirth, Address, Phone, PassportData)
    VALUES (@FullName, @Gender, @BirthDate, @Address, @PhoneNumber, @PassportData)
END;
