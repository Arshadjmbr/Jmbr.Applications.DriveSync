 SELECT
 d.Id,
 d.Name,
 d.Age,
 d.StaffIdNum,
 d.Email,
 d.MobileNumber,
 d.AlternateMobileNumber,
 d.EmiratesId,
 d.PassportNum,
 lt.Type AS "LicenseType",
 ez.Zone AS "EmirateZone",
  d.LicenseNumber,
 d.LicenseExpiryDate,
 d.JoiningDate,
 d.ResignationDate,
 d.VacationDateFrom,
 d.VacationDateTo,
 d.Status,
 d.CreatedDateTime,
 d.UpdatedDateTime,
 d.Deleted,
 d.Remarks
FROM Driver d
JOIN LicenseType lt ON lt.Id = d.LicenseTypeId
JOIN EmirateZone ez ON ez.Id = d.EmiratesZoneId
WHERE Deleted = 0
AND
    (@status IS NULL OR d.Status = @status) 
    AND (
        @searchText IS NULL OR
        d.StaffIdNum LIKE '%' + @searchText + '%' OR
        d.Name LIKE '%' + @searchText + '%'
    )
AND (
        @fromDate IS NULL OR @toDate IS NULL 
        OR d.JoiningDate BETWEEN @fromDate AND @toDate
    )
ORDER BY CAST(d.JoiningDate AS DATE) DESC;
