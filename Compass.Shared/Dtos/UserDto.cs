using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compass.Shared.Const;

namespace Compass.Shared.Dtos
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserDto:BaseDto
    {
        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged();}
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }

        //-------------------导航属性Role(1:n)---------------------
        private List<RoleDto> role;
        public List<RoleDto> Role
        {
            get { return role; }
            set { role = value; OnPropertyChanged(); }
        }
    }
}
