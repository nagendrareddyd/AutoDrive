using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDriveAPI.Util
{
    public class Constants
    {
        //general constants
        public const string SavedSuccessfully = "Saved successfully";
        public const string DeletedSuccessfully = "Deleted successfully";

        //general error codes
        public const string ErrorCode8001 = "Save Failed, Please try again";
        public const string ErrorCode8002 = "Delete Failed, Please try again";

        // Error code starts with 9001
        public const string ErrorCode9001 = "No Areas found";
        public const string ErrorCode9002 = "No Area found";
        public const string ErrorCode9003 = "Save Failed, Area Code already exists";

        public const string ErrorCode9011 = "No Instructors found";
        public const string ErrorCode9012 = "No Instructor found";
        public const string ErrorCode9013 = "Save Failed, Instructor Code already exists";

        public const string ErrorCode9021 = "No Students found";
        public const string ErrorCode9022 = "No Student found";
        public const string ErrorCode9023 = "Save Failed, Student Code already exists";

        public const string ErrorCode9031 = "No Bookings found";
        public const string ErrorCode9032 = "No Booking found";
        public const string ErrorCode9033 = "Save Failed, Booking already exists";

    }
}