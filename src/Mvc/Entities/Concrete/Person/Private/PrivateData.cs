using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Person.Private
{
    public class PrivateData : Entity
    {
        public Gender Gender { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Person PersonData { get; set; }

        public PrivateData()
        {
        }

        public PrivateData(Gender gender, int personId, Person personData)
        {
            Gender = gender;
            PersonId = personId;
            PersonData = personData;
        }
    }
}
