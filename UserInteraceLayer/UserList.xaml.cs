using ApplicationLayer.Services.Interface;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        private readonly IRegistrationRepository _registrationRepository;
        public UserList(IRegistrationRepository registrationRepository)
        {
            InitializeComponent();
            _registrationRepository = registrationRepository;
            _ = LoadUserDataAsync();
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var registrationWindow = new RegistrationWindow(_registrationRepository);
            registrationWindow.ShowDialog();
            // Refresh the user list after adding a new user
            //_ = LoadUserDataAsync();
            this.Close();
        }
        private async Task LoadUserDataAsync()
        {
            try
            {
                // Fetch all users from the repository asynchronously
                var users = await _registrationRepository.GetAllAsync();

                // Bind the fetched users to the DataGrid
                UserDataGrid.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
        // Handle DataGrid selection change to show/hide buttons
        public void UserDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UserDataGrid.SelectedItem != null)
            {
                //EditButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
            }
            else
            {
               // EditButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }

        // Edit button click handler
        //private async void EditButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedUser = (Registration)UserDataGrid.SelectedItem;
        //    if (selectedUser != null)
        //    {
        //        var registrationWindow = new RegistrationWindow(_registrationRepository, selectedUser);
        //        registrationWindow.ShowDialog();

        //        // Refresh the list after editing
        //        await LoadUserDataAsync();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select a user to edit.");
        //    }
        //}

        // Delete button click handler
        public async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (Registration)UserDataGrid.SelectedItem;
            if (selectedUser != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _registrationRepository.DeleteAsync(selectedUser.Id);
                        MessageBox.Show("User deleted successfully.");

                        // Refresh the list after deletion
                        await LoadUserDataAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting user: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }
    }


}

