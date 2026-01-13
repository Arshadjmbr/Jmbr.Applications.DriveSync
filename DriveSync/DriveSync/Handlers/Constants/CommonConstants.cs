namespace DriveSync.Handlers.Constants
{
    public class CommonConstants
    {

        public const int USER_STATUS_ACTIVE = 1;
        public const int USER_STATUS_INACTIVE = 2;

        public const int ASSIGNMENT_ASSIGNED = 1;
        public const int ASSIGNMENT_RETURNED = 2;

        public const int VEHICLE_AVAILABLE = 1;
        public const int VEHICLE_ASSIGNED = 2;
        public const int VEHICLE_UNDER_MAINTENANCE = 3;
        public const int VEHICLE_DECOMMISSIONED = 4;


        public const int DRIVER_AVAILABLE = 1;
        public const int DRIVER_ASSIGNED = 2;
        public const int DRIVER_SICK = 3;
        public const int DRIVER_ON_VACATION = 4;
        public const int DRIVER_RESIGNED = 5;



        public const int RENTAL_COMPANY_ACTIVE = 1;
        public const int RENTAL_COMPANY_INACTIVE = 2;


        public const string EXCEL_CONTENT_TYPE = "application/.xlsx";
        public const string EXCEL_FINE_FILE_NAME = "Fine_Report.xlsx";
        public const string PDF_CONTENT_TYPE = "application/pdf";
        public const string PDF_FINE_FILE_NAME = "Fine_Report.pdf";
        public const string INTERVAL_OFFSET = "$offset minutes";

        public const string EXCEL_VEHICLE_FILE_NAME = "Vehicle_Report.xlsx";
        public const string PDF_VEHICLE_FILE_NAME = "Vehicle_Report.pdf";

        public const string EXCEL_DRIVER_FILE_NAME = "Driver_Report.xlsx";
        public const string PDF_DRIVER_FILE_NAME = "Driver_Report.pdf";

    }
}
