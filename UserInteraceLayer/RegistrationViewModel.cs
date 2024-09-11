using ApplicationLayer.Services.Interface;
using ApplicationLayer.ViewModel;
using DomainLayer.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace UserInteraceLayer
{
    public class RegistrationViewModel : IRegistrationViewModel, INotifyPropertyChanged
    {
        private readonly IRegistrationRepository _registrationRepository;


        public RegistrationViewModel(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
            SubmitCommand = new RelayCommand(ExecuteSubmitCommand);
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
                CheckUserNameUniqueness();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                ValidateEmail();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public ICommand SubmitCommand { get; }

        private void ExecuteSubmitCommand()
        {
            // Handle form submission logic here
            // E.g., validate and use _registrationRepository to save data
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Validation handling
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(Email):
                        if (!IsValidEmail(Email))
                        {
                            result = "Invalid email format";
                        }
                        break;
                    case nameof(UserName):
                        if (string.IsNullOrWhiteSpace(UserName))
                        {
                            result = "Username cannot be empty";
                        }
                        break;
                    case nameof(Password):
                        if (Password != ConfirmPassword)
                        {
                            result = "Passwords do not match";
                        }
                        break;
                }
                return result;
            }
        }

        // Method to check UserName uniqueness
        private async void CheckUserNameUniqueness()
        {
            bool isUnique = await _registrationRepository.IsUserNameUnique(UserName);
            if (!isUnique)
            {
                MessageBox.Show("Username is already taken.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Email validation method
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Regular expression to validate email format
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void ValidateEmail()
        {
            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}

