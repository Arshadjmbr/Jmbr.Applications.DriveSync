namespace DriveSync.DTOS.Response
{
    public class PaginatedResponse<T>
    {
        public int TotalRecords { get; set; }
        public List<T>? Response { get; set; }

    }
}
