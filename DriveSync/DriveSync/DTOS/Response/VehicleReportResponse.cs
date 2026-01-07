using Microsoft.Identity.Client;

namespace DriveSync.DTOS.Response
{
    public class VehicleReportResponse
    {
        public string PlateNumber { get; set; }
        public string Name { get; set; }
        public string? StaffIdNum { get; set; }
        public string LicenseNumber { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
    }
}
