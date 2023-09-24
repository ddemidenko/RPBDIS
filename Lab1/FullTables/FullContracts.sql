-- Заполнение таблицы "Договоры" случайными данными
DECLARE @k INT = 1;
WHILE @k <= 500  -- Заполняем не менее 500 записей
BEGIN
    INSERT INTO Contracts (ContractID, DateOfContract, SellerID, BuyerID, DealAmount, ServiceCost, Employee)
    VALUES
        (@k, DATEADD(DAY, -@k, GETDATE()), 
         CAST(RAND() * 500 + 1 AS INT),  -- Продавцы с ID от 1 до 500
         CAST(RAND() * 500 + 1 AS INT),  -- Покупатели с ID от 1 до 500
         RAND() * 500000 + 50000,      -- Сумма сделки от 50000 до 550000
         RAND() * 5000 + 1000,          -- Стоимость услуги от 1000 до 6000
         'Сотрудник ' + CAST(@k AS NVARCHAR(10)));
    SET @k = @k + 1;
END;
