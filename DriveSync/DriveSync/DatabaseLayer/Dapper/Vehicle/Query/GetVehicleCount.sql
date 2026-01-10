SELECT 
    COUNT(*) AS TotalActiveRecords,
    SUM(CASE WHEN v.Status = 1 THEN 1 ELSE 0 END) AS AvailableCount,
    SUM(CASE WHEN v.Status = 2 THEN 1 ELSE 0 END) AS AssignedCount,
    SUM(CASE WHEN v.Status = 3 THEN 1 ELSE 0 END) AS InMaintenanceCount,
    SUM(CASE WHEN v.Status = 4 THEN 1 ELSE 0 END) AS OutOfServiceCount,
    SUM(CASE WHEN v.Status = 1 AND v.MulkiyaExpiryDate >= GETDATE() THEN 1 ELSE 0 END) AS AvailableWithValidMulkiyaCount,
    SUM(CASE WHEN v.Status = 1 AND v.MulkiyaExpiryDate < GETDATE() THEN 1 ELSE 0 END) AS AvailableWithExpiredMulkiyaCount
FROM Vehicle v
WHERE v.Deleted = 0;