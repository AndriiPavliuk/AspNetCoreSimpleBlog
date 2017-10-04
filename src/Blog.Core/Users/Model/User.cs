using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Users.Model
{
    public class User : IdentityUser
    {
        public const string AdminEmail = "admin@qq.com";
        public const string AdminName = "admin";
        public const string DefaultPsw = "123qwe";
    }
}
