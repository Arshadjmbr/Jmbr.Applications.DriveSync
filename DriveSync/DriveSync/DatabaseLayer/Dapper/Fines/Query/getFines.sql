SELECT 
    COUNT(*) OVER() AS TotalRecords,
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
WHERE f.Deleted = 0
AND
    (@searchText IS NULL OR
     d.Name LIKE '%' + @searchText + '%' OR
     v.PlateNumber LIKE '%' + @searchText + '%' OR
     f.FineNumber LIKE '%' + @searchText + '%')
ORDER BY f.CreatedDateTime DESC
OFFSET @rowSkip ROWS 
FETCH NEXT @takeRows ROWS ONLY;
