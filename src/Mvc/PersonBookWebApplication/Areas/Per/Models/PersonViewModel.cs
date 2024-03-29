using Core.Enums;

namespace PersonBookWebApplication.Areas.Per.Models
{
    public class PersonViewModel
    {
        public String Name { get; set; }

        public String Surname { get; set; }

        public String OpenAddress { get; set; }

        public String No { get; set; }

        public Gender Gender { get; set; }
    }
}