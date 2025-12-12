CREATE TABLE "Vehicle"
(
    "Id" BIGINT IDENTITY(1,1) PRIMARY KEY,
    "PlateNumber" VARCHAR(50) NOT NULL,
    "VehicleTypeId" INT NOT NULL,
    "Model" VARCHAR(50) NOT NULL,
    "Category" VARCHAR(50) NOT NULL,
    "RentalCompanyId" BIGINT NOT NULL,
    "IsActive" INT NOT NULL,
    "Status" SMALLINT,
    "CreatedDateTime" DATETIMEOFFSET NOT NULL,
    "UpdatedDateTime" DATETIMEOFFSET NOT NULL,
    "Remarks" VARCHAR(255) NULL,
    "Deleted" SMALLINT NOT NULL DEFAULT 0
    CONSTRAINT FK_Vehicle_VehicleType FOREIGN KEY ("VehicleTypeId") REFERENCES "VehicleType"("Id"),
    CONSTRAINT FK_Rental_RentalcompanyId FOREIGN KEY ("RentalCompanyId") REFERENCES "RentalCompanies"("Id")
)