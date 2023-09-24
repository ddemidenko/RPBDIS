
DECLARE @j INT = 1;
WHILE @j <= 2000  
BEGIN
    INSERT INTO ContractServices (ContractServiceID, ContractID, ServiceID)
    VALUES
        (@j, CAST(RAND() * 500 + 1 AS INT), CAST(RAND() * 500 + 1 AS INT));
    SET @j = @j + 1;
END;
