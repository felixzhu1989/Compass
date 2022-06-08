using Models;
using System.Reflection;

namespace SolidWorksHelper
{
    public class CreateBomFactory
    {
        public static ICreateBom ChooseDrawingType(ModuleTree tree)
        {
            //使用反射创建接口实现类的对象
            return (ICreateBom)Assembly.Load("SolidWorksHelper").CreateInstance("SolidWorksHelper." + tree.CategoryName+ "AutoDrawing");
        }
    }
}
