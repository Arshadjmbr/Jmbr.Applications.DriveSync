CREATE TABLE Driver
(
    "Id" BIGINT IDENTITY(1,1) PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Age" INT NOT NULL,
    "StaffIdNum" VARCHAR(30) NULL,
    "Email" VARCHAR(40) NULL,
    "MobileNumber" BIGINT NOT NULL,
    "AlternateMobileNumber" BIGINT NULL,
    "EmiratesId" VARCHAR(30) NULL,
    "PassportNum" VARCHAR(20) NULL,
    "LicenseTypeId" INT NOT NULL,
    "LicenseNumber" VARCHAR(50) NOT NULL,
    "LicenseExpiryDate" DATE NOT NULL,
    "EmiratesZoneId" INT NOT NULL,
    "JoiningDate" DATE NOT NULL,
    "ResignationDate" DATE,
    "VacationDateFrom" DATE NULL,
    "VacationDateTo" DATE NULL,
    "Status" INT NOT NULL,
    "CreatedDateTime" DATETIMEOFFSET NOT NULL,
    "UpdatedDateTime" DATETIMEOFFSET NOT NULL,
    "Remarks" VARCHAR(255) NULL,
    "Deleted" INT NOT NULL DEFAULT 0

    CONSTRAINT Foreign_Key_LicenseType FOREIGN KEY ("LicenseTypeId") REFERENCES "LicenseType"("Id"),
    CONSTRAINT Foreign_Key_EmiratesZone FOREIGN KEY ("EmiratesZoneId") REFERENCES "EmirateZone"("Id")
);

