using System.ComponentModel.DataAnnotations;

namespace PersonBookWebApplication.Models
{
    public class LoginViewModel
    {
        [Required]
        public String Mail { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
