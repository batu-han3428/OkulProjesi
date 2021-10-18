using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Areas.Admin
{
    public class CustomUser:IdentityUser
    {
        public string Tc { get; set; }
    }
}
