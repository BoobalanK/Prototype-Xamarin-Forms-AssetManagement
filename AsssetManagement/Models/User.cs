using System;
using System.Collections.Generic;
using System.Text;

namespace AsssetManagement.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }

    }
    public enum UserType
    {
        Employee = 1,
        StoreManager = 2
    }
}
