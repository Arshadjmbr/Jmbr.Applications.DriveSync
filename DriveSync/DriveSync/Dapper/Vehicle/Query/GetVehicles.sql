SELECT v.Id,
v.PlateNumber,
v.Model,
v.Category,
vt.Type,
rc.CompanyName,
v.IsActive,
v.CreatedDateTime,
v.UpdatedDateTime,
v.Remarks
FROM Vehicle v
JOIN VehicleType vt ON v.VehicleTypeId = vt.Id
JOIN RentalCompanies rc ON v.RentalCompanyId = rc.Id 
WHERE v.IsActive = 1 AND v.Deleted =0