using ApplicationLayer.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructureLayer.Services.Repositories
{
    public class LoginService : ILoginService
    {
        private readonly IRegistrationRepository _registrationRepository;
        public LoginService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _registrationRepository.GetByUserNameAsync(username);

            if (user != null && user.Password == password)
            {
                return true;
            }

            return false;
        }

        public IRegistrationRepository GetUserRepository()
        {
            return _registrationRepository;
        }
    }
}
