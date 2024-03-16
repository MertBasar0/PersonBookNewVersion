using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstraction
{
    public interface IIdentityManager
    {
        Task<String> GetUserMail();
    }
}
