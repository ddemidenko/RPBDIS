
CREATE VIEW ApartmentView
AS
SELECT
    ApartmentID,
    Name,
    Description,
    NumberOfRooms,
    Area,
    SeparateBathroom,
    HasPhone,
    MaxPrice,
    AdditionalPreferences
FROM
    Apartments;
