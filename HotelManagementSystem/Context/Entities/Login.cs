using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Context.Entities
{

    public enum LoginType
    {
        clerk,
        kitchen
    }

    public class Login
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginType LoginType { get; set; }
    }

}
