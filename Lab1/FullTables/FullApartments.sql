
DECLARE @i INT = 1;
WHILE @i <= 20000  
BEGIN
    INSERT INTO Apartments (ApartmentID, Name, Description, NumberOfRooms, Area, SeparateBathroom, HasPhone, MaxPrice, AdditionalPreferences)
    VALUES
        (@i, '�������� ' + CAST(@i AS NVARCHAR(10)), '�������� �������� ' + CAST(@i AS NVARCHAR(10)),
        RAND() * 5 + 1, RAND() * 100 + 30, CAST(RAND() * 1 AS BIT), CAST(RAND() * 1 AS BIT), RAND() * 5000000 + 1000000, '�������������� ��������� ��� �������� ' + CAST(@i AS NVARCHAR(10)));
    SET @i = @i + 1;
END;
