﻿namespace AutoDriveEntities
{
    public class UserEntity
	{
		public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }        
        public string Role { get; set; }        
        public string InstructorCode { get; set; }
    }
}
