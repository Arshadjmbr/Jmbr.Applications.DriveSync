namespace DriveSync.DTOS.Request
{
    public class AssignmentPaginationRequest
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
    }
}
