using System.Collections.Generic;

namespace AutoDriveEntities
{
    public class InstructorEntity
    {
        public string Id { get; set; }

        public string InstructorCode { get; set; }
     
        public string Name { get; set; }
     
        public string Gender { get; set; }
       
        public string Email { get; set; }
     
        public string Mobile { get; set; }
     
        public string Home { get; set; }
     
        public string Address { get; set; }
     
        public SuburbEntity Suburb { get; set;}
     
        public List<AreaEntity> Areas { get; set; }
     
        public string Status { get; set; }
    }
   
}
