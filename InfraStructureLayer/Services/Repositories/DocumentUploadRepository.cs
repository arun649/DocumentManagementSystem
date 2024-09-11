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
    public class DocumentUploadRepository: IDocumentUploadRepository,IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public DocumentUploadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get a document by its ID
        public async Task<DocumentUpload?> GetByIdAsync(int id)
        {
            return await _context.DocumentUploads.FindAsync(id);
        }

        // Get all documents
        public async Task<IEnumerable<DocumentUpload>> GetAllAsync()
        {
            return await _context.DocumentUploads.ToListAsync();
        }

        // Add a new document
        public async Task AddAsync(DocumentUpload documentUpload)
        {
            
            await _context.DocumentUploads.AddAsync(documentUpload);
            await _context.SaveChangesAsync();
        }

        // Update an existing document
        public async Task UpdateAsync(DocumentUpload documentUpload)
        {
            _context.DocumentUploads.Update(documentUpload);
            await _context.SaveChangesAsync();
        }

        // Delete a document by its ID
        public async Task<bool> DeleteAsync(int id)
        {
            var documentUpload = await _context.DocumentUploads.FindAsync(id);
            if (documentUpload == null)
            {
                return false; // Return false if the document does not exist
            }

            _context.DocumentUploads.Remove(documentUpload);
            await _context.SaveChangesAsync();
            return true; // Return true after successful deletion
        }

        // Dispose pattern to clean up the context and release resources
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

        // Explicit Dispose method implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Suppresses finalization to improve performance
        }
    }
}
