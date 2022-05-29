using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    //扩展方法
    public static class ExtensionMethods
    {
        /// <summary>
        /// 选择零件带后缀
        /// </summary>
        public static Component2 GetComponentByNameWithSuffix(this AssemblyDoc swAssy, string suffix, string partName)
        {
            return swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, partName));
        }
        /// <summary>
        /// 更改尺寸，int数量
        /// </summary>
        public static void ChangeDim(this ModelDoc2 swModel, string dimName, int intValue)
        {
            swModel.Parameter(dimName).SystemValue = intValue;
        }
        /// <summary>
        /// 更改尺寸，double距离
        /// </summary>
        public static void ChangeDim(this ModelDoc2 swModel, string dimName, double dblValue)
        {
            swModel.Parameter(dimName).SystemValue = dblValue / 1000d;
        }
        /// <summary>
        /// 部件解压特征
        /// </summary>
        public static void Suppress(this Component2 swComp, string featureName)
        {
            swComp.FeatureByName(featureName).SetSuppression2(0, 2, null);
        }
        /// <summary>
        /// 部件不解压特征
        /// </summary>
        public static void UnSuppress(this Component2 swComp, string featureName)
        {
            swComp.FeatureByName(featureName).SetSuppression2(1, 2, null);
        }
        /// <summary>
        /// 装配体解压特征
        /// </summary>
        public static void Suppress(this AssemblyDoc swAssy, string featureName)
        {
            swAssy.FeatureByName(featureName).SetSuppression2(0, 2, null);
        }
        /// <summary>
        /// 装配体不解压特征
        /// </summary>
        public static void UnSuppress(this AssemblyDoc swAssy, string featureName)
        {
            swAssy.FeatureByName(featureName).SetSuppression2(1, 2, null);
        }
        /// <summary>
        /// 装配体解压部件
        /// </summary>
        public static void Suppress(this AssemblyDoc swAssy, string suffix, string compName)
        {
            swAssy.GetComponentByNameWithSuffix(suffix, compName).SetSuppression2(0);
        }
        /// <summary>
        /// 装配体不解压部件
        /// </summary>
        public static Component2 UnSuppress(this AssemblyDoc swAssy, string suffix, string compName)
        {
            Component2 swComp = swAssy.GetComponentByNameWithSuffix(suffix, compName);
            swComp.SetSuppression2(2);
            return swComp;
        }

    }
}