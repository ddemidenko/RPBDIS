-- ���������� ������� "��������" ���������� �������
DECLARE @k INT = 1;
WHILE @k <= 500  -- ��������� �� ����� 500 �������
BEGIN
    INSERT INTO Contracts (ContractID, DateOfContract, SellerID, BuyerID, DealAmount, ServiceCost, Employee)
    VALUES
        (@k, DATEADD(DAY, -@k, GETDATE()), 
         CAST(RAND() * 500 + 1 AS INT),  -- �������� � ID �� 1 �� 500
         CAST(RAND() * 500 + 1 AS INT),  -- ���������� � ID �� 1 �� 500
         RAND() * 500000 + 50000,      -- ����� ������ �� 50000 �� 550000
         RAND() * 5000 + 1000,          -- ��������� ������ �� 1000 �� 6000
         '��������� ' + CAST(@k AS NVARCHAR(10)));
    SET @k = @k + 1;
END;
