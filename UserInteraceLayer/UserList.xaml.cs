using ApplicationLayer.Services.Interface;
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

    }
}
