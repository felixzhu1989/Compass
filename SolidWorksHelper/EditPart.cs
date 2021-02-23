using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class EditPart
    {
        ModelDoc2 swPart = default(ModelDoc2);
        Feature swFeat = default(Feature);

        #region Hood SidePanel 普通烟罩大侧板

        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0001(Component2 swComp, decimal deepth,decimal height,int sidePanelSideCjNo,int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = height / 1000m;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
        }
        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0002(Component2 swComp, decimal deepth,decimal height)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79m) / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39m) / 1000m;
            if (height==555m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (height == 400m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
        }

        /// <summary>
        /// 右边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0003(Component2 swComp, decimal deepth, decimal height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = height / 1000m;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
        }
        /// <summary>
        /// 右边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0004(Component2 swComp, decimal deepth, decimal height)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79m) / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39m) / 1000m;
            if (height == 555m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (height == 400m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
        }

        #endregion



    }
}
