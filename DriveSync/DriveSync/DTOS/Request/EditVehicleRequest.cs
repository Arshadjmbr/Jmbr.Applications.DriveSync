namespace DriveSync.DTOS.Request
{
    public class EditVehicleRequest
    {
        public long Id { get; set; }
        public string PlateNumber { get; set; }
        public int? VehicleTypeId { get; set; }
        public string Model { get; set; }
        public long? RentalCompanyId { get; set; }
        public DateOnly? MulkiyaExpiryDate { get; set; }
        public string? Remarks { get; set; }
    }
}
