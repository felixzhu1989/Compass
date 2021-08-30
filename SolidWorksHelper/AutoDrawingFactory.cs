using Models;
using System.Reflection;

namespace SolidWorksHelper
{
    //3.定义工厂类，选择不同的实现方式
    public class AutoDrawingFactory
    {
        public static IAutoDrawing ChooseDrawingType(ModuleTree tree)
        {
            //使用反射创建接口实现类的对象
            return (IAutoDrawing)Assembly.Load("SolidWorksHelper").CreateInstance("SolidWorksHelper." + tree.CategoryName+ "AutoDrawing");
        }
    }
}
