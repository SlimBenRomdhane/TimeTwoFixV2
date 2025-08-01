﻿namespace TimeTwoFix.Application.UserServices.Dtos.Users
{
    public class DeleteUserDto
    {
        //Common properties
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public DateOnly HireDate { get; set; }
        public int YearsOfExperience { get; set; }
        public string LastEmployer { get; set; }
        public string Status { get; set; }
    }
}