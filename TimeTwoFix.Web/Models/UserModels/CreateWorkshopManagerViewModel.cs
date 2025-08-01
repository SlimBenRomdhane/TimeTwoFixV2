﻿using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.UserModels
{
    public class CreateWorkshopManagerViewModel
    {
        //Common properties
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Address { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        public int ZipCode { get; set; }

        [Required, MaxLength(50)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string ImageURL { get; set; }

        [DataType(DataType.Date)]
        public DateOnly HireDate { get; set; }

        public int YearsOfExperience { get; set; }
        public string LastEmployer { get; set; }

        //Workshop Manager properties
        public int TeamSize { get; set; }
    }
}