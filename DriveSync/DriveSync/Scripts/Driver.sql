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
    "LicenseTypeId" INT NOT NULL,
    "LicenseNumber" VARCHAR(50) NOT NULL,
    "LicenseExpiryDate" DATE NOT NULL,
    "EmiratesZoneId" INT NOT NULL,
    "JoiningDate" DATETIMEOFFSET NOT NULL,
    "ResignationDate" DATETIMEOFFSET,
    "VacationDateFrom" DATE NULL,
    "VacationDateTo" DATE NULL,
    "IsActive" SMALLINT NOT NULL,
    "Status" SMALLINT,
    "CreatedDateTime" DATETIMEOFFSET NOT NULL,
    "UpdatedDateTime" DATETIMEOFFSET NOT NULL,
    "Remarks" VARCHAR(255) NULL,
    "Deleted" SMALLINT NOT NULL DEFAULT 0

    CONSTRAINT Foreign_Key_LicenseType FOREIGN KEY ("LicenseTypeId") REFERENCES "LicenseType"("Id"),
    CONSTRAINT Foreign_Key_EmiratesZone FOREIGN KEY ("EmiratesZoneId") REFERENCES "EmirateZone"("Id")
);

