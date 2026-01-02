SELECT 
Id,
PlateNumber
FROM Vehicle 
WHERE Status = 1 AND Deleted =0 AND MulkiyaExpiryDate >= GETDATE()