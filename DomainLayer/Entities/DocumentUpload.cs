using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class DocumentUpload
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
        public int DocumentId { get; set; } // Unique ID for the document
        public string? Name { get; set; } // The name of the document
        public string? Description { get; set; } // Document description
        public string FileName { get; set; } // The file's original name
        public string FilePath { get; set; } // Path where the file is stored
        public long FileSize { get; set; } // Size of the file in Mb
        public DateTime UploadDate { get; set; } // Date when the file was uploaded

     
        public int RegistrationId { get; set; } // Foreign key property for the Registration entity

        // Navigation property to the Registration entity
        // Foreign key pointing to Registration
        [ForeignKey("RegistrationId")]
        public Registration Registration { get; set; }

        public DocumentUpload()
        {
            UploadDate = DateTime.Now; // Automatically set upload date
        }
    }
   
}
