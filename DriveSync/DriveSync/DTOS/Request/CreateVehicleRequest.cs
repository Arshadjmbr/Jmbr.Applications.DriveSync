namespace DriveSync.DTOS.Request
{
    public class CreateVehicleRequest
    {
        public string PlateNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public long RentalCompanyid { get; set; }
        public string? Remarks { get; set; }
    }
}
