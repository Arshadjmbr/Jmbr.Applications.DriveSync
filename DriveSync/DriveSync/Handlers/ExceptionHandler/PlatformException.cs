namespace DriveSync.Handlers.ExceptionHandler
{
    public class PlatformException:Exception
    {
        public int StatusCode { get; set; }
        public PlatformException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
