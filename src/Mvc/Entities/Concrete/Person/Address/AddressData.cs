using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Person.Address
{
    public class AddressData : Entity
    {
        public string OpenAddress { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Person PersonData { get; set; }


        public AddressData()
        {
        }

        public AddressData(string openAddress, Person person)
        {
            OpenAddress = openAddress;
            PersonData = person;
        }
    }
}
