using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CSBelt.Models
{
    public class User
    {
        [Key] 
        public int UserId { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least {1} characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least {1} characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

    // (Registration) No duplicate Usernames
        [Required(ErrorMessage = "is required.")]
        [MinLength(3, ErrorMessage = "must be at least {1} characters")]
        [MaxLength(15, ErrorMessage = "cannot be more than {1} characters")]
        [Display(Name = "Username")]
        public string Username { get; set; }        

        [Required(ErrorMessage = "is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(8, ErrorMessage = "must be at least {1} characters")]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "doesn't match password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Hobby> Hobbies { get; set; } // only hobbies created by user
        public List<Add> Adds { get; set; }
        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}