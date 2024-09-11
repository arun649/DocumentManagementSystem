using ApplicationLayer.Services.Interface;
using ApplicationLayer.ViewModel;
using DomainLayer.Entities;
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
using UserInteraceLayer.ViewModel;

namespace UserInteraceLayer
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly IRegistrationRepository _registrationRepository;


        // Constructor that allows passing the ViewModel directly
        public RegistrationWindow(IRegistrationRepository registrationRepository)
        {
            InitializeComponent();
            _registrationRepository = registrationRepository;
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


        private async void  Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newreg = new Registration()
                {
                   // Id = 5,
                    Name = Fname.Text,
                    LastName = Lname.Text,
                    UserName = Uname.Text,
                    Email = Email.Text,
                    PhoneNumber = Pnum.Text,
                    Password = passwordBox.Password,
                    ConfirmPassword = confirmPasswordBox.Password,
                };

               await  _registrationRepository.AddAsync(newreg);

                UserList userListWindow = new UserList(_registrationRepository);
                userListWindow.Show();

                // Close the current registration window
                this.Close();
            }
            catch (Exception ex)
            {
                //
            }
        }
        public void Dispose()
        {
            Dispose();
        }

        // PasswordBox event handler to update ViewModel.Password

    }
}
