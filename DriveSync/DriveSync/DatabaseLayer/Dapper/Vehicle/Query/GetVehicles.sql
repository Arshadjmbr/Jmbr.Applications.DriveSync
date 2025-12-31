SELECT v.Id,
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
WHERE v.Status = 1 AND v.Deleted =0