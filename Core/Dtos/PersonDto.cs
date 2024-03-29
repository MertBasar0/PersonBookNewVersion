using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class PersonDto
    {
        public String Name { get; set; }

        public String Surname { get; set; }

        public String OpenAddress { get; set; }

        public String No { get; set; }

        public Gender Gender { get; set; }

    }
}
