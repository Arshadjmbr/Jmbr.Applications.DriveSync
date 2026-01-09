namespace DriveSync.DTOS.Request
{
    public class FinePaginationRequest
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
