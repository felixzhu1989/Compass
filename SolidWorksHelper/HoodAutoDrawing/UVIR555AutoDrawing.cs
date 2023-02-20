using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class UVIR555AutoDrawing : IAutoDrawing
    {
        Component2 swComp;
        readonly UVIR555Service objUVIR555Service = new UVIR555Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
            //创建项目模型存放地址
            string itemPath = $@"{projectPath}\{tree.Item}-{tree.Module}-{tree.CategoryName}";
            if (!CommonFunc.CreateProjectPath(itemPath)) return;
            //Pack的后缀
            string suffix = $@"{tree.Item}-{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";

            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = $@"{itemPath}\{tree.CategoryName.ToLower()}_{suffix}.sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);


            //查询参数
            UVIR555 item = (UVIR555)objUVIR555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;
           
            Feature swFeat;

            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc;//装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部 
            #endregion

            #region 计算中间值
            /*注意SolidWorks单位是m，计算是应当/1000d
                 * 整形与整形运算得出的结果仍然时整形，1640 / 1000d结果为0，因此必须将其中一个转化成double型，使用后缀m就可以了
                 * (int)不进行四舍五入，Convert.ToInt32会四舍五入
                */
            //-----------计算中间值，----------
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.ExBeamLength - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((item.ExBeamLength - (midRoofHoleNo - 1) * 400d) / 2) / 1000d;
            //KSA数量，KSA侧板长度(以全长计算)/M型侧板与MESH一样需要减去三角板厚度,排风腔实际多减去2dm
            int ksaNo = (int)((item.ExBeamLength - 4d) / 498d);
            double ksaSideLength = Convert.ToDouble((item.ExBeamLength - 5d - ksaNo * 498d) / 2) / 1000d;
            //MESH侧板长度(除去排风三角板3dm计算)
            double meshSideLength = Convert.ToDouble((item.ExBeamLength - 5d - (int)((item.ExBeamLength - 2d) / 498d) * 498d) / 2) / 1000d;


            //圆环新风
            double innerRadius = (double)item.Deepth / 2d - 90d;//内半径
            double beta = Math.PI - 2 * Math.Asin(400d / innerRadius);//圆心角弧度
            double innerRound = innerRadius * beta - 4d;//弧长
            int innerCjNo = (int)((innerRound - 30d) / 32d) + 1;//CJ孔数量
            double innerCjFirstDis = (innerRound - (innerCjNo - 1) * 32d) / 2d;

            double downRound = beta * ((double)item.Deepth / 2d - 45d);
            int downCjNo = (int)((downRound - 30d) / 32d) + 1; 
            #endregion

            try
            {
                #region Top Level装配体顶层
                //吊装槽钢
                //swModel.Parameter("D3@LocalLPattern1").SystemValue = (item.ExBeamLength - 50d) / 1000d;

                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.Parameter("D1@Distance40").SystemValue = (item.ExRightDis - item.ExLength / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.ExNo == 2 && (item.MARVEL == "YES" || item.ExHeight == 100d))
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swModel.Parameter("D1@Distance40").SystemValue =
                        (item.ExRightDis - item.ExLength - item.ExDis / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.ExNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue =
                        (item.ExDis + item.ExLength) / 1000d; //D1阵列数量,D3阵列距离
                }
                #endregion
                
                #region 排风腔
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0042-1");
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExBeamLength-2d) / 1000d;
                swPart.Parameter("D1@Sketch16").SystemValue = midRoofSecondHoleDis;
                if (midRoofHoleNo == 1)
                {
                    swFeat = swComp.FeatureByName("LPattern1");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("LPattern1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //排风口
                if (item.ExNo == 1)
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch12").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D1@Sketch12").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D2@Sketch12").SystemValue = item.ExWidth / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch13").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D2@Sketch13").SystemValue = item.ExDis / 1000d;
                    swPart.Parameter("D3@Sketch13").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D4@Sketch13").SystemValue = item.ExWidth / 1000d;
                }

                //MARVEL
                if (item.MARVEL == "YES")
                {
                    swFeat = swComp.FeatureByName("MA-TAB");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("MA-NTC");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.ExNo == 1) swPart.Parameter("D1@Sketch23").SystemValue = (item.ExRightDis + item.ExLength / 2 + 50d) / 1000d;
                    else swPart.Parameter("D1@Sketch23").SystemValue = (item.ExRightDis + item.ExDis / 2 + item.ExLength + 50d) / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("MA-TAB");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("MA-NTC");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D7@Sketch2").SystemValue = item.ExRightDis / 1000d;
                //非UVHood
                //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                if (item.UVType == "LONG") swPart.Parameter("D3@Sketch2").SystemValue = 1680d / 1000d;
                else swPart.Parameter("D3@Sketch2").SystemValue = 975d / 1000d;
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D4@Sketch15").SystemValue = item.ExRightDis / 1000d;
                //非UVHood
                //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                if (item.UVType == "LONG") swPart.Parameter("D2@Sketch15").SystemValue = 1120d / 1000d;
                else swPart.Parameter("D2@Sketch15").SystemValue = 520d / 1000d;
                //UVHood解压特征
                swFeat = swComp.FeatureByName("UV-TAB");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("KSACABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX UV");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX LIGHT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩

                #endregion

                #region 排风腔前面板、后面板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0044-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.ExBeamLength / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0045-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.ExBeamLength / 1000d;
                #endregion

                #region 排风腔底部
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0043-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExBeamLength / 1000d;
                #endregion

                #region 油塞
                if (item.Outlet == "LEFTTAP")
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.Outlet == "RIGHTTAP")
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                #endregion
                
                #region 三角板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0050-1");
                //UV烟罩解压缩特征
                swFeat = swComp.FeatureByName("Cut-Extrude2");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                #endregion

                #region KSA侧边
                if (ksaSideLength < 12d / 1000d && ksaSideLength > 2d / 1000d)
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;
                    //背面
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0027-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0026-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0028-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;


                }
                else if (ksaSideLength < 25d / 1000d && ksaSideLength >= 12d / 1000d)
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    //背面
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0027-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0026-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0028-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩

                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    //背面
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0027-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0026-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0028-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                #endregion

                #region 排风滑门/导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0013-3");
                if (item.ExWidth == 300d) swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0013-4");
                if (item.ExWidth == 300d) swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2 + 10d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20d) / 1000d;
                }
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0018-2");
                if (item.MARVEL == "YES") swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0018-1");
                if (item.MARVEL == "YES") swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    if (item.ExNo == 1) swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 100d) / 1000d;
                    else swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 3 + item.ExDis + 100d) / 1000d;
                }
                #endregion

                #region 排风脖颈
                if (item.ANSUL != "YES" && (item.ExHeight == 100d || item.MARVEL == "YES"))
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0006-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0007-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0008-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0009-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0006-1");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0007-1");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0008-1");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0009-1");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                }
                #endregion

                #region MESH油网侧板
                if (item.ANSUL == "YES")
                {
                    if (meshSideLength * 2 < 57d / 1000d) meshSideLength += 249d / 1000d;
                    if ((meshSideLength - 20d / 1000d) > 57d / 1000d)
                    {
                        if (item.ANSide == "LEFT")
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                        }
                        else if (item.ANSide == "RIGHT")
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                    }
                    else
                    {
                        if (item.ANSide == "LEFT")
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            //背面
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                        }
                        else if (item.ANSide == "RIGHT")
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                    }
                }
                else
                {
                    if (2 * meshSideLength < 15d / 1000d && meshSideLength > 1.5d / 1000d)
                        meshSideLength += 249d / 1000d;
                    if (meshSideLength > 40d / 1000d)
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        //背面
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    else if (meshSideLength <= 40d / 1000d && meshSideLength > 1.5d / 1000d)
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        //背面
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0012-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0013-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        //背面
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0030-3");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0029-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                #endregion

                #region 排风腔内部零件
                //MESH油网下导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0047-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.ExBeamLength - 8d) / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0046-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.ExBeamLength - 5d) / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0048-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.ExBeamLength - 3.5d) / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0049-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.ExBeamLength - 9d) / 1000d;
                #endregion

                #region MiddleRoof灯板/前
                //弧形条子
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0021-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Deepth/2d - 90d-1d) / 1000d;

                //内半径（Deepth/2d）=外半径（Deepth/2d）-90d
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0018-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D5@Sketch2").SystemValue = (item.Deepth / 2d - 90d) / 1000d;
                swPart.Parameter("D4@Sketch18").SystemValue = midRoofSecondHoleDis - 52.5d / 1000d;
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //灯具
                if (item.LightType == "LED60")
                {
                    swFeat = swComp.FeatureByName("LED60");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    if (item.LEDSpotNo == 1) swPart.Parameter("D4@Sketch10").SystemValue = 0;
                    else
                    {
                        swPart.Parameter("D4@Sketch10").SystemValue = (item.LEDSpotDis * (item.LEDSpotNo / 2d - 1) + item.LEDSpotDis / 2d) / 1000d;
                        swFeat = swComp.FeatureByName("LPattern2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                        swPart.Parameter("D1@LPattern2").SystemValue = item.LEDSpotNo;
                        swPart.Parameter("D3@LPattern2").SystemValue = item.LEDSpotDis / 1000d;
                    }
                }
                else
                {
                    swFeat = swComp.FeatureByName("LED60");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                } 
                #endregion

                #region MiddleRoof灯板/后
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0019-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D5@Sketch2").SystemValue = (item.Deepth / 2d - 90d) / 1000d;
                swPart.Parameter("D4@Sketch18").SystemValue = midRoofSecondHoleDis - 52.5d / 1000d;
                //swFeat = swComp.FeatureByName("NAMEPLATE");
                //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //灯具
                if (item.LightType == "LED60")
                {

                    swFeat = swComp.FeatureByName("LED60");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    if (item.LEDSpotNo == 1) swPart.Parameter("D4@Sketch10").SystemValue = 0;
                    else
                    {
                        swPart.Parameter("D4@Sketch10").SystemValue = (item.LEDSpotDis * (item.LEDSpotNo / 2d - 1) + item.LEDSpotDis / 2d) / 1000d;
                        swFeat = swComp.FeatureByName("LPattern2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                        swPart.Parameter("D1@LPattern2").SystemValue = item.LEDSpotNo;
                        swPart.Parameter("D3@LPattern2").SystemValue = item.LEDSpotDis / 1000d;
                    }
                }
                else
                {
                    swFeat = swComp.FeatureByName("LED60");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 

                } 
                #endregion

                #region MiddleRoof灯板/左右
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0020-2");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D4@Sketch9").SystemValue = (item.Deepth - 2d) / 2000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0009-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D4@Sketch9").SystemValue = (item.Deepth  - 2d) / 2000d;
                #endregion
                
                #region 吊装槽钢
                //利用三角关系半径平方-长度一半的平方再开根号乘以2
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = 2d * Math.Sqrt(Math.Pow((double)item.Deepth / 2d, 2) - Math.Pow((double)item.ExBeamLength / 2d, 2)) / 1000d; 
                #endregion

                #region 圆环新风
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0048-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth / 2d - 1d) / 1000d;
                swPart.Parameter("D3@Sketch1").SystemValue = (item.Deepth / 2d - 2d) / 1000d;

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0049-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth / 2d - 90d) / 1000d;
                swPart.Parameter("D3@Sketch1").SystemValue = (item.Deepth / 2d - 90d+0.5d - 400d) / 1000d;
                swPart.Parameter("D3@Sketch6").SystemValue = innerCjFirstDis / 1000d;
                swPart.Parameter("D1@LPattern1").SystemValue = innerCjNo;

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0050-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth / 2d - 1d) / 1000d;
                swPart.Parameter("D4@Sketch1").SystemValue = (innerRadius + 1d) / 1000d;
                swPart.Parameter("D3@Sketch2").SystemValue = (item.Deepth - 90d) / 1000d;
                //D3@CirPattern1
                swPart.Parameter("D3@CirPattern1").SystemValue = beta;//弧度
                swPart.Parameter("D1@CirPattern1").SystemValue = downCjNo;

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0051-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D6@Sketch1").SystemValue = (item.Deepth / 2d - 1d) / 1000d;
                swPart.Parameter("D3@Sketch1").SystemValue = (innerRadius + 1d) / 1000d;
                swPart.Parameter("D2@Sketch3").SystemValue = (item.Deepth - 90d) / 1000d;
                swPart.Parameter("D1@Sketch3").SystemValue = (item.ExBeamLength - 50d) / 1000d;
                #endregion

                swModel.ForceRebuild3(true);//设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
                swModel.Save();//保存，很耗时间
                swApp.CloseDoc(packedAssyPath);//关闭，很快
            }
            catch (Exception ex)
            {
                throw new Exception($"{packedAssyPath} 作图过程发生异常。\n零件：{swComp.Name}\n详细：{ex.Message}");
            }
            finally
            {
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }
        }
    }
}
