using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Person.Address;
using Entities.Concrete.Person.Phone;
using Entities.Concrete.Person.Private;

namespace Entities.Concrete.Person
{
    public class Person : Entity
    {

        public String Name { get; set; }

        public String Surname { get; set; }

        [ForeignKey(nameof(AddressData))]
        public int? AddressId { get; set; }
        public virtual AddressData? Address { get; set; }

        [ForeignKey(nameof(PhoneData))]
        public int? PhoneId { get; set; }
        public virtual PhoneData? Phone { get; set; }

        [ForeignKey(nameof(PrivateData))]
        public int? PrivateId { get; set; }
        public virtual PrivateData? Private { get; set; }

    }
}
