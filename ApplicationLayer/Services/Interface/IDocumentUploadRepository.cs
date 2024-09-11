using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Interface
{
    public interface IDocumentUploadRepository:IDisposable
    {
        Task<DocumentUpload?> GetByIdAsync(int id); // Return nullable DocumentUpload in case it doesn't exist
        Task<IEnumerable<DocumentUpload>> GetAllAsync();
        Task AddAsync(DocumentUpload documentUpload);
        Task UpdateAsync(DocumentUpload documentUpload);
        Task<bool> DeleteAsync(int id); // Optionally return a bool to indicate success or failure
    }
}
