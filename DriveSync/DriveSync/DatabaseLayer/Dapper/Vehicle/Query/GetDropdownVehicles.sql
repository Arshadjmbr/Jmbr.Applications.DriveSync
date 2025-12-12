SELECT 
Id,
PlateNumber
FROM Vehicle 
WHERE IsActive = 1 AND Deleted =0 AND Status IS NULL