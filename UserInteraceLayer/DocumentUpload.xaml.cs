using ApplicationLayer.Services.Interface;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for DocumentUpload.xaml
    /// </summary>
    public partial class DocumentUpload : Window
    {
        private readonly IDocumentUploadRepository _documentUploadRepository;
        private string _selectedFilePath = string.Empty;
        public DocumentUpload(IDocumentUploadRepository documentUploadRepository)
        {
            InitializeComponent();
            _documentUploadRepository = documentUploadRepository;
            LoadUploadedDocuments();
        }
        // Event handler for Browse button to select a file
        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _selectedFilePath = openFileDialog.FileName;
                FilePathTextBox.Text = _selectedFilePath; // Display selected file path
            }
        }
        private async void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            // Ensure that the document name and file path are not empty
            if (string.IsNullOrWhiteSpace(DocumentNameTextBox.Text) || string.IsNullOrWhiteSpace(_selectedFilePath))
            {
                MessageBox.Show("Please enter the document name and select a file to upload.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Define the destination folder and ensure it exists
                string destinationFolder = DestinationFilePathTextBox.Text; // Folder where the file will be saved
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder); // Create the folder if it doesn't exist
                }

                // Prepare the full destination file path
                string destinationFilePath = System.IO.Path.Combine(destinationFolder, System.IO.Path.GetFileName(_selectedFilePath));

                // Copy the file to the destination folder
                File.Copy(_selectedFilePath, destinationFilePath, overwrite: true); // Set 'overwrite' to true to allow overwriting files

                // Prepare document upload data
                var newDocument = new DomainLayer.Entities.DocumentUpload
                {
                    Name = DocumentNameTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    FileName = System.IO.Path.GetFileName(_selectedFilePath), // Just the file name
                    FilePath = destinationFilePath, // Full file path where the file is saved
                    FileSize = new FileInfo(_selectedFilePath).Length / 1024, // File size in KB
                    RegistrationId = 1 // For example, replace with actual user ID or registration ID
                };

                // Add document to repository and save
                await _documentUploadRepository.AddAsync(newDocument);
                MessageBox.Show("Document uploaded and saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh document list in DataGrid
                LoadUploadedDocuments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadUploadedDocuments()
        {
            var documents = await _documentUploadRepository.GetAllAsync();
            DocumentsDataGrid.ItemsSource = documents;
        }

        // Close the window
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
