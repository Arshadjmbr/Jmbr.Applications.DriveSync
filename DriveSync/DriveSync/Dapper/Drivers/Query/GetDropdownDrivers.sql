SELECT
Id,
Name
FROM Driver
WHERE IsActive = 1 AND Deleted = 0 AND Status IS NULL
