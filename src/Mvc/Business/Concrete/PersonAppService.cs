using AutoMapper;
using Core.Dtos;
using DataAccess.UnitOfWork;
using Entities.Concrete.Person;
using Entities.Concrete.Person.Address;
using Entities.Concrete.Person.Phone;
using Entities.Concrete.Person.Private;
using Mvc.Business.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Business.Concrete
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IUnitOfWork _unitOfWork;


        public PersonAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task CreatePersonAsync(PersonDto person)
        {
            try
            {
                using (await _unitOfWork.BeginAsync())
                {
                    var personRepo = await _unitOfWork.GetGenRepositoryAsync<Person>();

                    await personRepo.AddAsync(new Person()
                    {
                        Name = person.Name,
                        Surname = person.Surname,
                        Address = new AddressData(openAddress: person.OpenAddress),
                        Private = new PrivateData(gender: person.Gender),
                        Phone = new PhoneData(no: person.No)
                    });
                    await _unitOfWork.CommitAsync();

                }
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackAsync();
                throw;
            }


            _unitOfWork.Dispose();
        }
    }
}
