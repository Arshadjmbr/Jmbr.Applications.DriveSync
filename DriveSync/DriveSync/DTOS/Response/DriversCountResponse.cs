namespace DriveSync.DTOS.Response
{
    public class DriversCountResponse
    {
        public int TotalDrivers { get; set; }
        public int DriverAvailableCount { get; set; }
        public int DriverAssignedCount { get; set; }
        public int DriverSickCount { get; set; }
        public int DriverOnVacationCount { get; set; }
    }
}
