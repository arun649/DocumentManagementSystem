using ApplicationLayer.Services.Interface;
using InfraStructureLayer.Services.Repositories;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserInteraceLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IDocumentUploadRepository _documentUploadRepository;
        private readonly ILoginService _loginService;
        private bool isLoggedIn = false;
       
        public MainWindow(IDocumentUploadRepository documentUploadRepository, IRegistrationRepository registrationRepository, ILoginService loginService)
        {
            InitializeComponent();
            UpdateMenuVisibility();
            _documentUploadRepository = documentUploadRepository;
            _registrationRepository = registrationRepository;
            _loginService = loginService;
            
        }

        private void UpdateMenuVisibility()
        {
            if (isLoggedIn)
            {
                // Show restricted menu items
                UsersButton.Visibility = Visibility.Visible;
                DocumentsButton.Visibility = Visibility.Visible;
                DocumentListButton.Visibility = Visibility.Visible;

                // Hide Login button and show Logout button
                LoginButton.Visibility = Visibility.Collapsed;
                LogoutButton.Visibility = Visibility.Visible;
            }
            else
            {
                // Show only the Dashboard and Login button
                UsersButton.Visibility = Visibility.Collapsed;
                DocumentsButton.Visibility = Visibility.Collapsed;
                DocumentListButton.Visibility = Visibility.Collapsed;

                // Show Login button and hide Logout button
                LoginButton.Visibility = Visibility.Visible;
                LogoutButton.Visibility = Visibility.Collapsed;
            }
        }




        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Simulate a login process
            var loginWindow = new Login(_loginService,_registrationRepository,_documentUploadRepository);
            loginWindow.ShowDialog();

            if (loginWindow.IsLoginSuccessful)
            {
                isLoggedIn = true;
                UpdateMenuVisibility();  // Update the menu after login
            }
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            var userListWindow = new UserList(_registrationRepository);
            userListWindow.Show(); // Opens the UserList window
        }

        // Open the DocumentList window when the Documents button is clicked
        private void DocumentsButton_Click(object sender, RoutedEventArgs e)
        {
            var documentListWindow = new DocumentUpload(_documentUploadRepository);
            documentListWindow.Show(); // Opens the DocumentList window
        }

        private void DocumentListButton_Click(object sender, RoutedEventArgs e)
        {
            var documentListWindow = new DocumentList(_documentUploadRepository);
            documentListWindow.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logging out...");
            isLoggedIn = false;  // Reset login state
            UpdateMenuVisibility();  // Update UI to reflect logged-out state
        }

    }
}