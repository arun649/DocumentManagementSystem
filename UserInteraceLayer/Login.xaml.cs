using ApplicationLayer.Services.Interface;
using ApplicationLayer.StaticServices;
using InfraStructureLayer.Services.Repositories;
using System;
using System.Windows;

namespace UserInteraceLayer
{
    public partial class Login : Window
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IDocumentUploadRepository _documentUploadRepository;

        // Constructor
        public Login(ILoginService loginService, IRegistrationRepository registrationRepository, IDocumentUploadRepository documentUploadRepository)
        {
            InitializeComponent();
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _registrationRepository = registrationRepository ?? throw new ArgumentNullException(nameof(registrationRepository));
            _documentUploadRepository = documentUploadRepository ?? throw new ArgumentNullException(nameof(documentUploadRepository));
        }

        // Public property to track login success
        public bool IsLoginSuccessful { get; private set; }

        // Login button click handler
        public async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Validate input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Validate user credentials using the login service
                bool isAuthenticated = await _loginService.ValidateUserCredentialsAsync(username, password);

                if (isAuthenticated)
                {
                    // Fetch user details
                    var user = await _registrationRepository.GetByUserNameAsync(username);
                    if (user == null)
                    {
                        MessageBox.Show("User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Set user session details
                    UserSession.RegistrationId = user.Id;
                    UserSession.Username = user.UserName;
                    UserSession.Email = user.Email;
                    UserSession.IsAuthenticated = true;

                    // Set login status as successful
                    IsLoginSuccessful = true;

                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Open the main window and close the login window
                    //var mainWindow = new MainWindow(_documentUploadRepository, _registrationRepository, _loginService);
                    //mainWindow.Show();
                    this.Close();
                }
                else
                {
                    // Show login failure message
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors during the login process
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Handle registration link click
        private void RegistrationLink_Click(object sender, RoutedEventArgs e)
        {
            // Open the registration window
            var registrationWindow = new RegistrationWindow(_registrationRepository);
            registrationWindow.Show();
        }
    }
}
