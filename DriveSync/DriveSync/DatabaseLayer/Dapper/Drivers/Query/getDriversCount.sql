SELECT
COUNT(*) AS TotalDrivers,
SUM(CASE WHEN Status = 1 THEN 1 ELSE 0 END) AS DriverAvailableCount,
SUM(CASE WHEN Status = 2 THEN 1 ELSE 0 END) AS DriverAssignedCount,
SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) AS DriverSickCount,
SUM(CASE WHEN Status = 4 THEN 1 ELSE 0 END) AS DriverOnVacationCount
FROM Driver
WHERE Deleted = 0