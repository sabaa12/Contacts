using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNg.Data.Models
{
    public class User:IdentityUser
    {
        public ICollection<contact> contacts { get; set; }

    }
}
