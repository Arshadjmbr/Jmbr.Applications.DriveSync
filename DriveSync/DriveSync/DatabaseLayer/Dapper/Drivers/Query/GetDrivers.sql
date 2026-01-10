 SELECT
     COUNT(*) OVER() AS TotalRecords,
     SUM(CASE WHEN d.Status = 1 THEN 1 ELSE 0 END) OVER() AS AvailableCount,
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
        (@searchText IS NULL OR
        d.Name LIKE '%' + @searchText + '%' OR
        d.StaffIdNum LIKE '%' + @searchText + '%' OR
        d.Email LIKE '%' + @searchText + '%' OR
        d.EmiratesId LIKE '%' + @searchText + '%' OR
        d.PassportNum LIKE '%' + @searchText + '%')
ORDER BY d.CreatedDateTime DESC
OFFSET @rowSkip ROWS 
FETCH NEXT @takeRows ROWS ONLY;
