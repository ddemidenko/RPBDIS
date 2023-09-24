CREATE PROCEDURE InsertService
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000),
    @Price DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Services (Name, Description, Price)
    VALUES (@Name, @Description, @Price)
END;
