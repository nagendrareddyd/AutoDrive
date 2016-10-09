using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveEntities
{
    public class BookingEntity
    {
        public string Id { get; set; }
        public BookingInstructor Instructor { get; set; }
        public BookingStudent Student { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string PickUplocation { get; set; }
        public string ContactNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime DateCreatedOn { get; set; }
        public DateTime DateModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Title { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
    }
    public class BookingInstructor 
    {
        public string Id { get; set; }
        public string InstructorName { get; set; }
    }
    public class BookingStudent 
    {
        public string Id { get; set; }
        public string StudentName { get; set; }
    }
}
