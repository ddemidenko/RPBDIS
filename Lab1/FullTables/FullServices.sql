
DECLARE @i INT = 1;
WHILE @i <= 500 
BEGIN
    INSERT INTO Services (ServiceID, Name, Description, Price)
    VALUES
        (@i, '������ ' + CAST(@i AS NVARCHAR(10)), '�������� ������ ' + CAST(@i AS NVARCHAR(10)), RAND() * 1000);
    SET @i = @i + 1;
END;

