using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualGameStore.ModelView
{
    public class Member
    {
        public IdentityUser user { get; set; }
        public bool isMember { get; set; }
        public bool isAdministrator { get; set; }
    }
}
