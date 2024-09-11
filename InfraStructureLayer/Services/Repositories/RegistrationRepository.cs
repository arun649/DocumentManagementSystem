using ApplicationLayer.Services.Interface;
using DomainLayer.Entities;
using InfraStructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructureLayer.Services.Repositories
{
    public class RegistrationRepository: IRegistrationRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;
        public RegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

     
        public async Task<bool> IsUserNameUnique(string userName)
        {
            return !await _context.Registrations.AnyAsync(r => r.UserName == userName);
        }

        public async Task<Registration?> GetByIdAsync(string id)
        {
            return await _context.Registrations.FindAsync(id);
        }

        public async Task<IEnumerable<Registration>> GetAllAsync()
        {
            return await _context.Registrations.ToListAsync();
        }

        public async Task AddAsync(Registration registration)
        {
            await _context.Registrations.AddAsync(registration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Registration registration)
        {
            _context.Registrations.Update(registration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        // Public Dispose method to be called explicitly if needed
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
