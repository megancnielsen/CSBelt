using System;
using System.ComponentModel.DataAnnotations;

namespace CSBelt.Models
{
    public class Add
    {
        [Key]
        public int AddId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int HobbyId { get; set; }
        public int UserId { get; set; }
        public User AddedBy { get; set; }
        public Hobby AddedHobby { get; set; }
    }
}