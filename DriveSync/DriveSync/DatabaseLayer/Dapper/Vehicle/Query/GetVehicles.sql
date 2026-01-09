SELECT
COUNT(*) OVER() AS TotalRecords,
v.Id,
v.PlateNumber,
v.Model,
vt.Type,
rc.CompanyName,
v.Status,
v.MulkiyaExpiryDate,
v.CreatedDateTime,
v.UpdatedDateTime,
v.Remarks
FROM Vehicle v
JOIN VehicleType vt ON v.VehicleTypeId = vt.Id
JOIN RentalCompanies rc ON v.RentalCompanyId = rc.Id 
WHERE v.Deleted =0
AND
        (@searchText IS NULL OR
        v.PlateNumber LIKE '%' + @searchText + '%' OR
        rc.CompanyName LIKE '%' + @searchText + '%')
ORDER BY v.CreatedDateTime DESC
OFFSET @rowSkip ROWS 
FETCH NEXT @takeRows ROWS ONLY;