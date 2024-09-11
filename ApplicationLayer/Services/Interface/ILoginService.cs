using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Interface
{
    public interface ILoginService
    {
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
        IRegistrationRepository GetUserRepository();
    }
}
