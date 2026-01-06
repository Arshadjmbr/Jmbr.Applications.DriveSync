SELECT 
    d.Name,
    d.StaffIdNum,
    f.FineNumber,
    f.Amount,
    f.ViolationType,
    f.Reason,
    f.IssuedDate,
    v.PlateNumber,
    f.Paid
FROM 
    Fines f
JOIN Driver d ON f.DriverId = d.Id    
JOIN Vehicle v ON f.VehicleId = v.Id
WHERE 
    (@status IS NULL OR f.Paid = @status) 
    AND (
        @searchText IS NULL OR
        d.StaffIdNum LIKE '%' + @searchText + '%' OR
        d.Name LIKE '%' + @searchText + '%'
    )
    AND (
        @fromDate IS NULL OR @toDate IS NULL 
        OR f.IssuedDate BETWEEN @fromDate AND @toDate
    )
ORDER BY CAST(f.IssuedDate AS DATE) DESC;