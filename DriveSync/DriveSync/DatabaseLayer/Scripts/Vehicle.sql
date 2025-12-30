CREATE TABLE "Vehicle"
(
    "Id" BIGINT IDENTITY(1,1) PRIMARY KEY,
    "PlateNumber" VARCHAR(50) NOT NULL,
    "VehicleTypeId" INT NOT NULL,
    "Model" VARCHAR(50) NOT NULL,
    "RentalCompanyId" BIGINT NOT NULL,
    "Status" INT NOT NULL,
    "MulkiyaExpiryDate" DATE NOT NULL,
    "CreatedDateTime" DATETIMEOFFSET NOT NULL,
    "UpdatedDateTime" DATETIMEOFFSET NOT NULL,
    "Remarks" VARCHAR(255) NULL,
    "Deleted" INT NOT NULL DEFAULT 0
    CONSTRAINT FK_Vehicle_VehicleType FOREIGN KEY ("VehicleTypeId") REFERENCES "VehicleType"("Id"),
    CONSTRAINT FK_Rental_RentalcompanyId FOREIGN KEY ("RentalCompanyId") REFERENCES "RentalCompanies"("Id")
)