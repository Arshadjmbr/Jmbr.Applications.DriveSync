SELECT 
    v.PlateNumber,
    d.Name,
    d.StaffIdNum,
    d.LicenseNumber,
    a.StartDateTime,
    a.EndDateTime
FROM 
     Assignments a 
JOIN Vehicle v ON a.VehicleId = v.Id
JOIN Driver d ON a.DriverId = d.Id
WHERE 
    v.Deleted = 0
    AND (
        @searchText IS NULL OR
        v.PlateNumber LIKE '%' + @searchText + '%' OR
        d.Name LIKE '%' + @searchText + '%'
    )
    AND (
        @fromDate IS NULL OR @toDate IS NULL 
        OR a.CreatedDateTime BETWEEN @fromDate AND @toDate
    )
ORDER BY CAST(a.CreatedDateTime AS DATE) DESC;
