using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonBookWebApplication.Areas.Per.Models
{
    public class PersonViewModel
    {
        [Required(ErrorMessage = "Gerekli")]
        public String? Name { get; set; }
        [Required(ErrorMessage = "Gerekli")]
        public String? Surname { get; set; }
        [Required(ErrorMessage = "Gerekli")]
        public String? OpenAddress { get; set; }
        [Required(ErrorMessage = "Gerekli")]
        public String? No { get; set; }
        [Required(ErrorMessage = "Gerekli")]
        public Gender Gender { get; set; }
    }
}