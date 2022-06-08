using Models;
using System.Collections.Generic;

namespace SolidWorksHelper
{
    public interface ICreateBom
    {
        void CreateSemiBom(ModuleTree tree,Dictionary<string, int> semiBomDic);
    }
}
