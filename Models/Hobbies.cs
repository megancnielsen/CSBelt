using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CSBelt.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = " Must be at least {1} characters!")]
        public string Name { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = " Must be at least {1} characters!")]
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public string Username { get; set; }
        public User SubittedBy { get; set; }
        public List<Add> Adds { get; set; }

        public Hobby (string username, string body)
        {
            Username = username;
            Body = body;
        }
        public Hobby ()
        {
            
        }
    }
}