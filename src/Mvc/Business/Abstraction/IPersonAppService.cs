using Core.Dtos;
using Entities.Concrete.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Business.Abstraction
{
    public interface IPersonAppService
    {
        Task CreatePersonAsync(PersonDto person);
    }
}
