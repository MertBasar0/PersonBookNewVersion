using Entities.Concrete.Person;
using Entities.Concrete.Person.Address;
using Entities.Concrete.Person.Phone;
using Entities.Concrete.Person.Private;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PersonAppDbContext : DbContext
    {

        public PersonAppDbContext(DbContextOptions<PersonAppDbContext> option): base(option) { }


        public DbSet<Person> Persons { get; set; }
        public DbSet<AddressData> AddressData { get; set; }
        public DbSet<PrivateData> PrivateData { get; set; }
        public DbSet<PhoneData> PhoneData { get; set; }
    }
}
