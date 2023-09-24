
CREATE VIEW ServiceView
AS
SELECT
    ServiceID,
    Name AS ServiceName,
    Description AS ServiceDescription,
    Price AS ServicePrice
FROM
    Services;

