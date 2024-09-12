using ApplicationLayer.Services.Interface;
using InfraStructureLayer.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserInteraceLayer
{
    /// <summary>
    /// Interaction logic for DocumentList.xaml
    /// </summary>
    public partial class DocumentList : Window
    {
        private readonly IDocumentUploadRepository _documentUploadRepository;

        public DocumentList(IDocumentUploadRepository documentUploadRepository)
        {
            InitializeComponent();
            _documentUploadRepository = documentUploadRepository;

            // Load the documents when the window is opened
            LoadDocuments();
        }

        private async void LoadDocuments()
        {
            // Fetch the list of documents from the repository
            var documents = await _documentUploadRepository.GetAllAsync();

            // Bind the documents to the DataGrid
            DocumentDataGrid.ItemsSource = documents;
        }

        private void ViewDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to view the selected document
            var selectedDocument = (DocumentUpload)DocumentDataGrid.SelectedItem;
            if (selectedDocument != null)
            {
                MessageBox.Show($"Viewing Document: {selectedDocument.Name}");
                // Logic to open or view the document can be implemented here
            }
            else
            {
                MessageBox.Show("Please select a document to view.", "No Document Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to delete the selected document
            var selectedDocument = (DocumentUpload)DocumentDataGrid.SelectedItem;
            if (selectedDocument != null)
            {
                MessageBox.Show($"Deleting Document: {selectedDocument.Name}");
                // Logic to delete the document can be implemented here
            }
            else
            {
                MessageBox.Show("Please select a document to delete.", "No Document Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
