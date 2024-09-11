using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApplicationLayer.ViewModel
{
    public class IRegistrationViewModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        //string Password { get; set; }
        //string ConfirmPassword { get; set; }

        public ICommand SubmitCommand { get; }
    }
}
