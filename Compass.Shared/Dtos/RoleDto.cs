using Compass.Shared.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos
{
    public class RoleDto:BaseDto
    {
        private Role_e roleName;
        public Role_e RoleName
        {
            get { return roleName; }
            set { roleName = value; OnPropertyChanged(); }
        }
    }
}
