CREATE TABLE dbo.Driver
(
    "Id" BIGINT IDENTITY(1,1) PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Age" SMALLINT NOT NULL,
    "StaffIdNum" VARCHAR(30) NULL,
    "Email" VARCHAR(40) NULL,
    "MobileNumber" BIGINT NOT NULL,
    "EmiratesId" VARCHAR(30) NULL,
    "PassportNum" VARCHAR(20) NULL,
    "LicenseType" VARCHAR(20) NOT NULL,
    "LicenseNumber" VARCHAR(50) NOT NULL,
    "LicenseExpiryDate" DATE NOT NULL,
    "EmiratesZone" VARCHAR(50) NOT NULL,
    "JoiningDate" DATETIMEOFFSET NOT NULL,
    "ResignationDate" DATETIMEOFFSET,
    "VacationDateFrom" DATE NULL,
    "VacationDateTo" DATE NULL,
    "IsActive" SMALLINT NOT NULL,
    "CreatedDateTime" DATETIMEOFFSET NOT NULL,
    "UpdatedDateTime" DATETIMEOFFSET NOT NULL,
    "Remarks" VARCHAR(255) NULL,
    "Deleted" SMALLINT NOT NULL DEFAULT 0
);

