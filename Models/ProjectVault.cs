using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// PDM项目库
    /// </summary>
    [Serializable]
    public class ProjectVault
    {
        public int VaultId { get; set; }
        public string VaultName { get; set; }
    }
}
