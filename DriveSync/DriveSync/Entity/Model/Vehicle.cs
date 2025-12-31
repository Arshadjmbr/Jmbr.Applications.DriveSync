namespace DriveSync.Entity.Model
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string PlateNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public string Model { get; set; }
        public long RentalCompanyId { get; set; }
        public string? Remarks { get; set; }
        public int Status { get; set; }
        public DateOnly MulkiyaExpiryDate { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set;}
        public int Deleted {  get; set; } = 0;

    }
}
