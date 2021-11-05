using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    //扩展方法
   public static class ExtensionMethods
    {
        public static Component2 GetComponentByNameWithSuffix(this AssemblyDoc swAssy, string suffix, string partName )
        {
            return swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, partName));
        }

    }
}
