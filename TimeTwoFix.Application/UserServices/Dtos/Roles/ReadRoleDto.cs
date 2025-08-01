﻿namespace TimeTwoFix.Application.UserServices.Dtos.Roles
{
    public class ReadRoleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}