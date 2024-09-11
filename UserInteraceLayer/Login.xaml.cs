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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IDocumentUploadRepository _documentUploadRepository;

        public Login(ILoginService loginService, IRegistrationRepository registrationRepository, IDocumentUploadRepository documentUploadRepository)
        {
            InitializeComponent();
            _loginService = loginService;
            _registrationRepository = registrationRepository;
            _documentUploadRepository = documentUploadRepository;
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Call the login service to validate credentials
                bool isAuthenticated = await _loginService.ValidateUserCredentialsAsync(username, password);

                if (isAuthenticated)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Open the main application window after successful login
                    var documentWindow = new DocumentUpload(_documentUploadRepository);
                    documentWindow.Show();

                    // Close the login window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RegistrationLink_Click(object sender, RoutedEventArgs e)
        {
            // Create and show the RegistrationWindow
            RegistrationWindow registrationWindow = new RegistrationWindow(_registrationRepository);
            registrationWindow.Show();
        }
    }
}
