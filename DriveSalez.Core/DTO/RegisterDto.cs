﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.Core.DTO
{
    public class RegisterDto
    {
	    [Required(ErrorMessage = "Person name cannot be blank!")]
	    public string FirstName { get; set; }
	    
	    [Required(ErrorMessage = "Person surname cannot be blank!")]
	    public string LastName { get; set; }
	    
	    [Required(ErrorMessage = "Email cannot be blank!")]
	    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
	    [DataType(DataType.EmailAddress)]
	    public string Email { get; set; }
	    
        [Required(ErrorMessage = "Phone number cannot be blank!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain only numbers!")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get ; set; }

        [Required(ErrorMessage = "Password cannot be blank!")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        
		[Required(ErrorMessage = "Password cannot be blank!")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
    }
}
