﻿using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.UserModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}