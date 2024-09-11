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
        public Login(ILoginService loginService)
        {
            InitializeComponent();
            _loginService = loginService;
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
                    var userListWindow = new UserList(_loginService.GetUserRepository());
                    userListWindow.Show();

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
    }
}
