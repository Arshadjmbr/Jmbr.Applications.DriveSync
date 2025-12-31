namespace DriveSync.DTOS.Request
{
    public class CreateVehicleRequest
    {
        public string PlateNumber { get; set; }
        public int VehicleCategoryId { get; set; }
        public string Model { get; set; }
        public long RentalCompanyid { get; set; }
        public DateOnly MulkiyaExpiryDate { get; set; }
        public string? Remarks { get; set; }
    }
}
