
DECLARE @i INT = 1;
WHILE @i <= 500  
BEGIN
    INSERT INTO Sellers (SellerID, FullName, Gender, DateOfBirth, Address, Phone, PassportData, ApartmentID, ApartmentAddress, Price, AdditionalInformation)
    VALUES
        (@i, '�������� ' + CAST(@i AS NVARCHAR(10)), 
         CASE WHEN @i % 2 = 0 THEN '�������' ELSE '�������' END, 
         DATEADD(DAY, -(@i * 10), GETDATE()), 
         '����� �������� ' + CAST(@i AS NVARCHAR(10)), 
         '123456789' + RIGHT('000' + CAST(@i AS NVARCHAR(3)), 3),
         'ABCD' + RIGHT('0000' + CAST(@i AS NVARCHAR(4)), 4),
         CAST(RAND() * 20000 + 1 AS INT), 
         '����� �������� �������� ' + CAST(@i AS NVARCHAR(10)), 
         RAND() * 500000 + 50000, 
         '�������������� ���������� ��� �������� ' + CAST(@i AS NVARCHAR(10)));
    SET @i = @i + 1;
END;
