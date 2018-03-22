using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSecurity.POCOs
{
    public class UserProfile
    {
        public string UserId { get; set; } // ASPNetUsers table
        public string UserName { get; set; } // ASPNetUsers table
        public int? EmployeeId { get; set; } // ASPNetUsers table
        public int? CustomerId { get; set; } // ASPNetUsers table
        public string FirstName { get; set; } // Employees table
        public string LastName { get; set; } // Employees table
        public string Email { get; set; } // ASPNetUsers table
        public bool EmailConfirmation { get; set; } // ASPNetUsers table
        public string RequestedPassord { get; set; } // ASPNetUsers table
        public IEnumerable<string> RoleMemberships { get; set; } // SecurityRoles class
    }
}
