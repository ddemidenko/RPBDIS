
CREATE VIEW ContractView
AS
SELECT
    c.ContractID,
    c.DateOfContract,
    s.FullName AS SellerFullName,
	b.FIOBuyer AS BuyerName,
    c.DealAmount,
    c.ServiceCost
FROM
    Contracts c
JOIN
    Sellers s ON c.SellerID = s.SellerID
JOIN
    Contracts b ON c.BuyerID = b.BuyerID;
