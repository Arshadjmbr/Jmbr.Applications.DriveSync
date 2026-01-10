namespace DriveSync.DTOS.Response
{
    public class VehicleCountResponse
    {
        public int TotalActiveRecords { get; set; }
        public int AvailableCount { get; set; }
        public int AssignedCount { get; set; }
        public int UnderMaintenanceCount { get; set; }
        public int OutOfServiceCount { get; set; }
        public int AvailableWithValidMulkiyaCount { get; set; }
        public int AvailableWithExpiredMulkiyaCount { get; set; }

    }
}
