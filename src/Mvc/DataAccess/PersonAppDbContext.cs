using Entities.Concrete.Person;
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
    }
}
