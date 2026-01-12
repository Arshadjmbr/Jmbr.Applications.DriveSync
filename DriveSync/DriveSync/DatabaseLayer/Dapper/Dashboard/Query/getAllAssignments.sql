SELECT 
    COUNT(*) OVER() AS TotalRecords,
    Sum(CASE WHEN a.Status = 1 THEN 1 ELSE 0 END) OVER() AS ActiveAssignmentsCount,
    Sum(CASE WHEN a.Status = 2 THEN 1 ELSE 0 END) OVER() AS CompletedAssignmentsCount,
    a.Id,
    d.Name,
    d.StaffIdNum,
    v.PlateNumber AS VehicleNumber,
    d.Age,
    d.MobileNumber,
    d.EmiratesId,
    d.PassportNum,
    d.JoiningDate,
    a.StartDateTime,
    a.EndDateTime,
    a.Status,
    a.Remarks
FROM Assignments a
JOIN Driver d ON a.DriverId = d.Id
JOIN Vehicle v ON a.VehicleId = v.Id
WHERE 
    d.Deleted = 0 
    AND v.Deleted = 0
    AND (
        @searchText IS NULL OR
        d.Name LIKE '%' + @searchText + '%' OR
        v.PlateNumber LIKE '%' + @searchText + '%'
    )
    AND (
        @fromDate IS NULL OR @toDate IS NULL OR
        a.CreatedDateTime BETWEEN @fromDate AND @toDate
    )
ORDER BY a.CreatedDateTime DESC 
OFFSET @rowSkip ROWS 
FETCH NEXT @takeRows ROWS ONLY;
