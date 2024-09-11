using ApplicationLayer.Services.Interface;
using ApplicationLayer.ViewModel;
using DomainLayer.Entities;
using InfraStructureLayer.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using UserInteraceLayer.ViewModel;

namespace UserInteraceLayer
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly IRegistrationRepository _registrationRepository;
        private Registration _currentUser;
        private readonly ILoginService _loginService;
        private readonly IDocumentUploadRepository _documentUploadRepository;
       

        // Constructor that allows passing the ViewModel directly
        public RegistrationWindow(IRegistrationRepository registrationRepository, Registration user = null, ILoginService loginService = null, IDocumentUploadRepository documentUploadRepository = null)
        {
            InitializeComponent();
            _registrationRepository = registrationRepository;
            _loginService = loginService;
            _documentUploadRepository = documentUploadRepository;
            _currentUser = user;
            if (_currentUser != null)
            {
                // Populate fields with the existing user data
                Fname.Text = _currentUser.Name ?? string.Empty;
                Lname.Text = _currentUser.LastName ?? string.Empty;
                Uname.Text = _currentUser.UserName ?? string.Empty;
                Email.Text = _currentUser.Email ?? string.Empty;
                Pnum.Text = _currentUser.PhoneNumber ?? string.Empty;
                passwordBox.Password = _currentUser.Password ?? string.Empty;
                confirmPasswordBox.Password = _currentUser.ConfirmPassword ?? string.Empty;
            }
           
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }

        // Handle ConfirmPasswordBox PasswordChanged event
        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel)
            {
                viewModel.ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (_currentUser == null)
                {
                    var newreg = new Registration
                    {
                        Name = Fname.Text,
                        LastName = Lname.Text,
                        UserName = Uname.Text,
                        Email = Email.Text,
                        PhoneNumber = Pnum.Text,
                        Password = passwordBox.Password,
                        ConfirmPassword = confirmPasswordBox.Password,
                    };

                    await _registrationRepository.AddAsync(newreg);
                    Login loginWindow = new Login(_loginService,_registrationRepository, _documentUploadRepository);
                    loginWindow.Show();
                    this.Close();

                }
                else
                {
                    _currentUser.Name = Fname.Text;
                    _currentUser.LastName = Lname.Text;
                    _currentUser.UserName = Uname.Text;
                    _currentUser.Email = Email.Text;
                    _currentUser.PhoneNumber = Pnum.Text;
                    _currentUser.Password = passwordBox.Password;
                    _currentUser.ConfirmPassword = confirmPasswordBox.Password;

                    await _registrationRepository.UpdateAsync(_currentUser);

                    //UserList userListWindow = new UserList(_registrationRepository);
                    //userListWindow.Show();
                    this.Close();

                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        
    
        public void Dispose()
        {
            Dispose();
        }

        // PasswordBox event handler to update ViewModel.Password

    }
}
