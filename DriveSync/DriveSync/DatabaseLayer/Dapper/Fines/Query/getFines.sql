SELECT 
f.Id,
f.FineNumber,
f.Amount,
f.ReceiptNumber,
f.IssuedDate,
f.ViolationType,
f.Paid,
f.PaidDate,
f.Reason,
d.Name AS DriverName,
v.PlateNumber AS VehiclePlateNumber
FROM Fines f
JOIN Driver d ON f.DriverId = d.Id
JOIN Vehicle v ON f.VehicleId = v.Id
ORDER BY f.CreatedDateTime DESC
