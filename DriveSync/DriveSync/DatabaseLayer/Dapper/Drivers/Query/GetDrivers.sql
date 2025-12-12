 SELECT
 d.Name,
 d.Age,
 d.StaffIdNum,
 d.Email,
 d.MobileNumber,
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
 d.IsActive,
 d.CreatedDateTime,
 d.UpdatedDateTime,
 d.Deleted,
 d.Remarks
FROM Driver d
JOIN LicenseType lt ON lt.Id = d.LicenseTypeId
JOIN EmirateZone ez ON ez.Id = d.EmiratesZoneId
WHERE IsActive = 1 AND Deleted = 0