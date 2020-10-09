using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    //1.定义自动作图接口
    public interface IAutoDrawing
    {
        void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath);
    }
}
