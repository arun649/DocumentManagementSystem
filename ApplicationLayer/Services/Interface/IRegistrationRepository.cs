using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Interface
{
    public interface IRegistrationRepository
    {
        Task<bool> IsUserNameUnique(string userName);
        Task<Registration> GetByIdAsync(string id);
        Task<IEnumerable<Registration>> GetAllAsync();
        Task AddAsync(Registration registration);
        Task UpdateAsync(Registration registration);
        Task DeleteAsync(int id);
        void Dispose();
    }

}
