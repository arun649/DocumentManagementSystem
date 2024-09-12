using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.StaticServices
{
    public static class UserSession
    {
        public static int RegistrationId { get; set; }
        public static string Username { get; set; }
        public static string Email { get; set; }
        public static bool IsAuthenticated { get; set; }

        // Clear session on logout
        public static void ClearSession()
        {
            RegistrationId = 0;
            Username = string.Empty;
            Email = string.Empty;
            IsAuthenticated = false;
        }
    }
}
