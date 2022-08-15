using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class KVIR555AutoDrawing : IAutoDrawing
    {
        private Component2 swComp;
        private readonly KVIR555Service objKVIR555Service = new KVIR555Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoHood(tree, projectPath, out string suffix);

            //查询参数
            KVIR555 item = (KVIR555)objKVIR555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            HoodPart swHoodPart=new HoodPart();

            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swAssy = swModel as AssemblyDoc;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部 
            #endregion
            
            #region 计算中间值
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.ExBeamLength - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((item.ExBeamLength - (midRoofHoleNo - 1) * 400d) / 2d);

            //KSA数量，KSA侧板长度(以全长计算)/M型侧板与MESH一样需要减去三角板厚度
            int ksaNo = (int)((item.ExBeamLength -2d) / 498d);
            double ksaSideLength = Convert.ToDouble((item.ExBeamLength - 3d - ksaNo * 498d) / 2);


            //圆环新风
            double innerRadius= item.Deepth / 2d - 90d;
            double alpha = Math.Asin(400d/innerRadius);
            double beta = Math.PI - 2 * alpha;
            double innerRound = beta * innerRadius - 4d;
            int innerCjNo = (int)((innerRound - 30d) / 32d) + 1;
            double innerCjFirstDis = (innerRound - (innerCjNo - 1) * 32d) / 2d;

            double downMidRound = beta * (innerRadius + 45d);
            int downCjNo = (int)((downMidRound - 30d) / 32d) + 1;
            double downCjFirstDis = (downMidRound - (innerCjNo - 1) * 32d) / 2d;
            #endregion

            try
            {
                #region Top Level
                //吊装槽钢
                swModel.ChangeDim("D3@LocalLPattern1", item.ExBeamLength - 50d);

                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.ChangeDim("D1@Distance40", item.ExRightDis - item.ExLength / 2d);
                    swAssy.Suppress("LocalLPattern2");
                }
                else if (item.ExNo == 2 && (item.MARVEL == "YES" || item.ExHeight == 100d))
                {
                    swAssy.Suppress("LocalLPattern2");
                }
                else
                {
                    swModel.ChangeDim("D1@Distance40", item.ExRightDis - item.ExLength - item.ExDis / 2d);
                    swAssy.UnSuppress("LocalLPattern2");
                    swModel.ChangeDim("D1@LocalLPattern2", item.ExNo); //D1阵列数量,D3阵列距离
                    swModel.ChangeDim("D3@LocalLPattern2", item.ExDis + item.ExLength); //D1阵列数量,D3阵列距离
                }
                #endregion

                //----------R圆形烟罩排风腔----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0042-1");
                swHoodPart.FNHE0042(swComp,item.ExBeamLength,midRoofSecondHoleDis,midRoofHoleNo,item.ExNo,item.ExRightDis,item.ExLength,item.ExWidth,item.ExDis,item.MARVEL,"NO");

                //----------排风腔底部----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0043-1");
                swHoodPart.FNHE0043(swComp, item.ExBeamLength,item.Outlet);

                //----------排风腔前面板、后面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0044-1");
                swHoodPart.FNHE0044(swComp,item.ExBeamLength);
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0045-1");
                swHoodPart.FNHE0045(swComp, item.ExBeamLength);

                //----------三角板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0050-1");
                swHoodPart.FNHE0050(swComp,"NO");

                //----------KSA侧边----------
                swHoodPart.KSAFilter(swAssy, suffix, ksaSideLength, "FNHE0003-1", "FNHE0004-1", "FNHE0005-1");
                swHoodPart.KSAFilter(swAssy, suffix, ksaSideLength,  "FNHE0026-2", "FNHE0027-2", "FNHE0028-1");


                //----------排风滑门/导轨----------
                swHoodPart.ExhaustRail(swAssy, suffix, item.MARVEL, item.ExLength, item.ExWidth, item.ExNo, item.ExDis, "FNCE0013-3", "FNCE0013-4", "FNCE0018-1", "FNCE0018-2");

                //----------排风脖颈----------
                swHoodPart.ExhaustSpigot(swAssy, suffix, item.ANSUL, item.MARVEL, item.ExLength, item.ExWidth, item.ExHeight, "FNHE0006-1", "FNHE0007-1", "FNHE0008-1", "FNHE0009-1");
                
                
                //----------MiddleRoof灯板/前----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0018-1");
                swHoodPart.FNHM0018(swComp,item.Deepth,midRoofSecondHoleDis,midRoofHoleNo,item.LightType,item.LEDSpotNo,item.LEDSpotDis);
                
                //----------MiddleRoof灯板/后----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0019-1");
                swHoodPart.FNHM0018(swComp, item.Deepth, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, item.LEDSpotNo, item.LEDSpotDis);

                //----------MiddleRoof灯板/左右----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0009-2");
                swHoodPart.FNHM0009(swComp,item.Deepth);

                //----------MiddleRoof灯板/弯条----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0021-2");
                swHoodPart.FNHM0021(swComp,item.Deepth);
                

                //----------吊装槽钢----------
                //利用三角关系半径平方-长度一半的平方再开根号乘以2
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swHoodPart.Std2900100001(swComp,"NO", 2d * Math.Sqrt(Math.Pow(item.Deepth / 2d, 2) - Math.Pow(item.ExBeamLength / 2d, 2))+100d);

                //----------圆环新风----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0048-1");
                swHoodPart.FNHA0048(swComp,item.Deepth);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0049-1");
                swHoodPart.FNHA0049(swComp, item.Deepth, innerCjFirstDis, innerCjNo);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0050-1");
                swHoodPart.FNHA0050(swComp,item.Deepth, downCjFirstDis, downCjNo,beta);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0051-1");
                swHoodPart.FNHA0051(swComp,item.Deepth,item.ExBeamLength);

                swModel.ForceRebuild3(true);//设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
                swModel.Save();//保存，很耗时间
                swApp.CloseDoc(packedAssyPath);//关闭，很快
            }
            catch (Exception ex)
            {
                //以后记录在日志中
                throw new Exception($"作图过程发生异常：{packedAssyPath} 。\n零件：{swComp.Name}\n对象：{ex.Source}\n详细：{ex.Message}");
            }
            finally
            {
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }
        }
    }
}
