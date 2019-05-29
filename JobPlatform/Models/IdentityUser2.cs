using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobPlatform.Models
{
    public class IdentityUser2 : IdentityUser<string>
    {
        public IdentityUser2()
        {
        }

        public IdentityUser2(string userName) : base(userName)
        {

        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string About { get; set; }

        public string Skills { get; set; }

        public string Education { get; set; }

        public string WorkInfo { get; set; }

        public string Role { get; set; } = "User";

        private string photoUrl;

        public string PhotoUrl
        {
            get
            {
                return $"/uploads/{photoUrl.Replace("/uploads/", "")}";
            }
            set
            {
                photoUrl = value;
            }
        }
    }
}
