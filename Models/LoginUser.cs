using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBelt.Models
{
    [NotMapped]
    public class LoginUser
    {
        [Required(ErrorMessage = "required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }
        // LoginEmail will be the asp-validation-for in the _Login.cshtml
        [Required(ErrorMessage = "required.")]
        [MinLength(8, ErrorMessage = "must be at least 8 characters")]
        [DataType(DataType.Password)] // auto fills input type attr
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    }
}