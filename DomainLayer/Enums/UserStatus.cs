using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum UserStatus
    {
        [Display(Name = "Active")]
        Active,

        [Display(Name = "InActive")]
        InActive,

        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Approved")]
        Approved
    }
}
