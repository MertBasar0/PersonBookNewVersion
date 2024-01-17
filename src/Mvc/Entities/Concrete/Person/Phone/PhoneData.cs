using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Person.Phone
{
    public class PhoneData : Entity
    {
        public String No { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Person PersonData { get; set; }


        public PhoneData()
        {
        }

        public PhoneData(string no, int personId, Person personData)
        {
            No = no;
            PersonId = personId;
            PersonData = personData;
        }
    }
}
