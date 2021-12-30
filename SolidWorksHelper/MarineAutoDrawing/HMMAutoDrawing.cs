using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
namespace SolidWorksHelper
{
    public class HMMAutoDrawing : IAutoDrawing
    {
        readonly HMMService objHMEService = new HMMService();

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
            if (!File.Exists(packedAssyPath))
                packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);

            //查询参数
            HMM item = (HMM)objHMEService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix; //后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;
            Component2 swComp;
            Feature swFeat;
            EditPartMarine swEdit = new EditPartMarine();


            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc; //装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true); //TopOnly参数设置成true，只重建顶层，不重建零件内部
                                         /*注意SolidWorks单位是m，计算是应当/1000d
                                          * 整形与整形运算得出的结果仍然时整形，1640 / 1000d结果为0，因此必须将其中一个转化成decimal型，使用后缀m就可以了
                                          * (int)不进行四舍五入，Convert.ToInt32会四舍五入
                                           */
            #endregion
            try
            {

                #region 顶层
                //swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NTC-anturi-1");
                //swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "TerminalBlock-145-1");//网线右
                swComp.SetSuppression2(2); //2解压缩，0压缩
                #endregion

                //插口位置//右/左/前
                if (item.PlugPosition == "Right")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2xRJ12-2");//网线右
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC31-2");//电源右
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2xRJ12-1");//网线前
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC31-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC21-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else if (item.PlugPosition == "Left")
                {

                }
                else//前面
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2xRJ12-2");//网线右
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC31-2");//电源右
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    if (item.NetPlug == "2xRJ12")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2xRJ12-1");//网线前
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                                                   //压缩其他的网线接口
                                                   //swComp = swAssy.GetComponentByNameWithSuffix(suffix,"XXXXX-1");//网线前
                                                   //swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                    else if (item.NetPlug == "2xRJ45")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2xRJ12-1");//网线前
                        swComp.SetSuppression2(0); //2解压缩，0压缩

                    }
                    else if (item.NetPlug == "Both")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2xRJ12-1");//网线前
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2xRJ12-1");//网线前
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }

                    if (item.PowerPlug == "NAC31")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC31-1");//网线前
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                                                   //压缩其他的接口
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC21-1");//网线前
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                    else if (item.PowerPlug == "NAC21")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC31-1");//网线前
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC21-1");//网线前
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    else if (item.PowerPlug == "Both")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC31-1");//网线前
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC21-1");//网线前
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC31-1");//网线前
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "NAC21-1");//网线前
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                //电加热//YES/NO
                if (item.Heater == "Yes")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "LH-9129-2");//加热管组件
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "LH-9152-1");//加热管组件
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "LH-9151-1");//加热管组件
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                                               //swComp = swAssy.GetComponentByNameWithSuffix(suffix,"LH-9156-1");//加热管组件
                                               //swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                else if (item.Heater == "No")
                {

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "LH-9129-2");//加热管组件
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "LH-9152-1");//加热管组件
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "LH-9151-1");//加热管组件
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "TerminalBlock-145-1");//接线端子排
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                                               //swComp = swAssy.GetComponentByNameWithSuffix(suffix,"LH-9156-1");//加热管组件
                                               //swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                #region 测压管
                //测压管//YES/NO
                if (item.WindPressure == "Yes")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "测风压管^HMM-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩

                }
                else if (item.WindPressure == "No")
                {

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "测风压管^HMM-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩

                }

                #endregion


                //手动复位开关
                if (item.TemperatureSwitch == "No") //手动复位开关
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCO0001-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCO0002-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩 
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCO0001-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCO0002-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩 
                }




                //-------------分块（一组）--------------------

                //----------------外壳-大底板--------------------------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCC0001-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("Pituus@Base-Extrude-Thin").SystemValue = item.Length / 1000d - 4.5d / 1000d;
                swPart.Parameter("Leveys@Sketch1").SystemValue = item.Width / 1000d - 1.5d / 1000d;
                swPart.Parameter("Korkeus@Sketch1").SystemValue = item.Height / 1000d - 1.5d / 1000d;
                //获取对象的成员（属性(扳手图标)或者方法(立方体图标)）需要通过对象去获得item.HangLocation
                //吊脚位置，Up\Mid\Down
                if (item.HangPosition == "Up")
                {
                    //swFeat得先赋值，不然就是空的   1解压，0压缩
                    swFeat = swComp.FeatureByName("HangUp");
                    swFeat.SetSuppression2(1, 2, null);
                    swFeat = swComp.FeatureByName("Cut-Extrude27");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("HangMid-L");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("HangMid-R");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("HangDown");
                    swFeat.SetSuppression2(0, 2, null);

                }
                else if (item.HangPosition == "Mid")
                {
                    swFeat = swComp.FeatureByName("HangUp");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("Cut-Extrude27");
                    swFeat.SetSuppression2(1, 2, null);
                    swFeat = swComp.FeatureByName("HangMid-L");
                    swFeat.SetSuppression2(1, 2, null);
                    swFeat = swComp.FeatureByName("HangMid-R");
                    swFeat.SetSuppression2(1, 2, null);
                    swFeat = swComp.FeatureByName("HangDown");
                    swFeat.SetSuppression2(0, 2, null);

                }
                else
                {
                    swFeat = swComp.FeatureByName("HangUp");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("Cut-Extrude27");
                    swFeat.SetSuppression2(1, 2, null);
                    swFeat = swComp.FeatureByName("HangMid-L");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("HangMid-R");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("HangDown");
                    swFeat.SetSuppression2(1, 2, null);
                }

                //插口，电源和网线(先位置后型号)
                if (item.PlugPosition == "Right")
                {
                    //网线插口
                    if (item.NetPlug == "No")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12-R");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("2xRJ12-R");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D4@Sketch61").SystemValue = item.PowerPlugDis / 1000d - 53d / 1000d;
                    }
                    //电源插口
                    if (item.PowerPlug == "NAC21" || item.PowerPlug == "NAC31")
                    {
                        swFeat = swComp.FeatureByName("PowerPlug-R");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D4@Sketch63").SystemValue = item.PowerPlugDis / 1000d;
                        if (item.PowerPlug == "NAC31")//D1@Sketch63=48
                        {
                            swPart.Parameter("D1@Sketch63").SystemValue = 48d / 1000d;
                        }
                        else//D1@Sketch63=39
                        {
                            swPart.Parameter("D1@Sketch63").SystemValue = 39d / 1000d;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("PowerPlug-R");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    //画好左边特征以后完善
                }
                else if (item.PlugPosition == "Left")
                {
                    swFeat = swComp.FeatureByName("2xRJ12-R");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("PowerPlug-R");
                    swFeat.SetSuppression2(0, 2, null);
                }
                else//前面
                {
                    swFeat = swComp.FeatureByName("2xRJ12-R");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("PowerPlug-R");
                    swFeat.SetSuppression2(0, 2, null);
                }

                //----------外壳-进风口盖板(前)------------------------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCC0002-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("Leveys@Sketch1").SystemValue = item.Width / 1000d + 0.75d / 1000d;
                swPart.Parameter("Korkeus@Base-Extrude-Thin").SystemValue = item.Height / 1000d - 2d / 1000d;
                swPart.Parameter("Halkaisija@Sketch27").SystemValue = item.InletDia / 1000d + 3d / 1000d;
                if (item.PlugPosition == "Front")
                {
                    if (item.NetPlug == "2xRJ12")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D2@Sketch21").SystemValue = 156.5d / 1000d;
                        swFeat = swComp.FeatureByName("2xRJ45");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    else if (item.NetPlug == "2xRJ45")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("2xRJ45");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D11@Sketch38").SystemValue = 156.5d / 1000d;
                    }
                    else if (item.NetPlug == "Both")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D2@Sketch21").SystemValue = 170d / 1000d;
                        swFeat = swComp.FeatureByName("2xRJ45");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D11@Sketch38").SystemValue = 139.5d / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("2xRJ12");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("2xRJ45");
                        swFeat.SetSuppression2(0, 2, null);
                    }

                    if (item.PowerPlug == "NAC31")
                    {
                        swFeat = swComp.FeatureByName("NAC31");
                        swFeat.SetSuppression2(1, 2, null);
                        swFeat = swComp.FeatureByName("NAC21");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    else if (item.PowerPlug == "NAC21")
                    {
                        swFeat = swComp.FeatureByName("NAC31");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("NAC21");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D4@Sketch39").SystemValue = 101d / 1000d;
                    }
                    else if (item.PowerPlug == "Both")
                    {
                        swFeat = swComp.FeatureByName("NAC31");
                        swFeat.SetSuppression2(1, 2, null);
                        swFeat = swComp.FeatureByName("NAC21");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D4@Sketch39").SystemValue = 29 / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("NAC31");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("NAC21");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                }
                else
                {
                    swFeat = swComp.FeatureByName("2xRJ12");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("2xRJ45");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("NAC31");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("NAC21");
                    swFeat.SetSuppression2(0, 2, null);
                }







                //----------内胆---------------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCC0006-2");
                swEdit.MNCC0006(swComp, "HMM", item.Height, item.PowerPlug, item.PowerPlugDis, item.NetPlug, item.PlugPosition, item.Heater, item.TemperatureSwitch, item.WindPressure);




                //
                //----------外壳-底板(后)------------------------MNCC0003
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCC0003-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("Korkeus@Sketch1").SystemValue = item.Height / 1000d - 0.25d / 1000d;

                //----------外壳-出风口大盖板-----------------------MNCC0003
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCC0008-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D@Sketch3").SystemValue = item.OutletDia / 1000d + 9d / 1000d;
                swPart.Parameter("D4@Sketch3").SystemValue = (item.OutletDia / 1000d + 9d / 1000d) / 2 - 4.2d / 1000d;




                //----------外壳-出风口前盖板------------------------MNCC0004
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCC0004-1");
                swPart = swComp.GetModelDoc2();
                if (item.HangPosition == "Up")
                {
                    //swFeat得先赋值，不然就是空的   1解压，0压缩
                    swFeat = swComp.FeatureByName("HangUp");
                    swFeat.SetSuppression2(1, 2, null);
                    swFeat = swComp.FeatureByName("Cut-Extrude17");
                    swFeat.SetSuppression2(0, 2, null);
                }
                else
                {
                    swFeat = swComp.FeatureByName("HangUp");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("Cut-Extrude17");
                    swFeat.SetSuppression2(1, 2, null);
                }
                if (item.NamePlate == "Yes")   // 铭牌  1解压，0压缩
                {
                    swFeat = swComp.FeatureByName("NamePlate");
                    swFeat.SetSuppression2(1, 2, null);

                }
                else
                {
                    swFeat = swComp.FeatureByName("NamePlate");
                    swFeat.SetSuppression2(0, 2, null);

                }

                //////********************进、出风脖颈************************////
                //HMS-S-In，进风脖颈

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "HMS-S-In-3");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D3@Sketch1").SystemValue = (item.InletDia / 1000d - 2d / 1000d) / 2;



                //HMS-S-Out，出风脖颈

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "HMS-S-Out-2");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D3@Sketch1").SystemValue = (item.OutletDia / 1000d - 2d / 1000d) / 2;









                //////********************岩棉************************////


                ////MNCI0001 底板棉

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCI0001-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("Korkeus@Sketch1").SystemValue = item.Height / 1000d - 4d / 1000d;

                ////MNCI0002 侧板棉

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCI0002-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("Korkeus@Sketch1").SystemValue = item.Height / 1000d - 28d / 1000d;

                ////MNCI0003 侧板棉

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCI0003-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("Korkeus@Sketch1").SystemValue = item.Height / 1000d - 28d / 1000d;


                if (item.PlugPosition == "Right")
                {
                    //网线插口
                    if (item.NetPlug == "No")
                    {
                        swFeat = swComp.FeatureByName("I/O conn");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("I/O conn");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D4@Sketch7").SystemValue = item.PowerPlugDis / 1000d - 53d / 1000d;
                    }
                    //电源插口
                    if (item.PowerPlug == "NAC21" || item.PowerPlug == "NAC31")
                    {
                        swFeat = swComp.FeatureByName("NAC");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D4@Sketch9").SystemValue = item.PowerPlugDis / 1000d;
                        if (item.PowerPlug == "NAC31")//D1@Sketch63=48
                        {
                            swPart.Parameter("D2@Sketch9").SystemValue = 52d / 1000d;
                        }
                        else//D1@Sketch63=39
                        {
                            swPart.Parameter("D2@Sketch9").SystemValue = 43d / 1000d;
                        }
                    }
                    else
                    {
                        /* swFeat = swComp.FeatureByName("PowerPlug-R");
                         swFeat.SetSuppression2(0, 2, null);
                         */
                    }
                    //画好左边特征以后完善
                }
                else if (item.PlugPosition == "Left")
                {
                    swFeat = swComp.FeatureByName("2xRJ12-R");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("PowerPlug-R");
                    swFeat.SetSuppression2(0, 2, null);
                }



                // MNCI0009，出风口棉

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCI0009-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D3@Sketch2").SystemValue = item.OutletDia / 1000d + 4d / 1000d;


                // MNCI0007，进风口棉

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCI0007-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D3@Sketch2").SystemValue = item.InletDia / 1000d + 4d / 1000d;
                swPart.Parameter("Korkeus@Sketch1").SystemValue = item.Height / 1000d - 54d / 1000d;
                if (item.PlugPosition == "Front")
                {
                    if (item.NetPlug == "2xRJ12")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D1@Sketch7").SystemValue = 156.5d / 1000d;
                        swFeat = swComp.FeatureByName("2xRJ45");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    else if (item.NetPlug == "2xRJ45")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("2xRJ45");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D4@Sketch8").SystemValue = 156.5d / 1000d;
                    }
                    else if (item.NetPlug == "Both")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D1@Sketch7").SystemValue = 170d / 1000d;
                        swFeat = swComp.FeatureByName("2xRJ45");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D4@Sketch8").SystemValue = 139.5d / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("2xRJ12");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("2xRJ45");
                        swFeat.SetSuppression2(0, 2, null);
                    }

                    if (item.PowerPlug == "NAC31")
                    {
                        swFeat = swComp.FeatureByName("NAC31");
                        swFeat.SetSuppression2(1, 2, null);
                        swFeat = swComp.FeatureByName("NAC21");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    else if (item.PowerPlug == "NAC21")
                    {
                        swFeat = swComp.FeatureByName("NAC31");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("NAC21");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D3@Sketch5").SystemValue = 73d / 1000d;
                    }
                    else if (item.PowerPlug == "Both")
                    {
                        swFeat = swComp.FeatureByName("NAC31");
                        swFeat.SetSuppression2(1, 2, null);
                        swFeat = swComp.FeatureByName("NAC21");
                        swFeat.SetSuppression2(1, 2, null);
                        swPart.Parameter("D3@Sketch5").SystemValue = 28d / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("NAC31");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("NAC21");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                }




                // MNCI0006，内胆前棉

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "MNCI0006-1");
                swPart = swComp.GetModelDoc2();
                swFeat = swComp.FeatureByName("HMM");
                swFeat.SetSuppression2(1, 2, null);
                swFeat = swComp.FeatureByName("HMF");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HME");
                swFeat.SetSuppression2(0, 2, null);


                swPart.Parameter("D3@Sketch12").SystemValue = 90d / 1000d;
                swPart.Parameter("D2@Sketch13").SystemValue = 90d / 1000d;
                swPart.Parameter("D1@Sketch14").SystemValue = 90d / 1000d;
                swPart.Parameter("D2@Sketch15").SystemValue = 90d / 1000d;

                if (item.Height < 220d)
                {
                    swPart.Parameter("Korkeus@Sketch1").SystemValue = 134.5d / 1000d;
                }
                else if (item.Height > 220d || item.Height == 220d)
                {
                    swPart.Parameter("Korkeus@Sketch1").SystemValue = 155d / 1000d;
                }



                if (item.PlugPosition == "Front")
                {

                    if (item.NetPlug == "2xRJ12")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12-F");
                        swFeat.SetSuppression2(1, 2, null);
                        swFeat = swComp.FeatureByName("2xRJ45-F");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    else if (item.NetPlug == "2xRJ45")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12-F");
                        swFeat.SetSuppression2(1, 2, null);
                        swFeat = swComp.FeatureByName("2xRJ45-F");
                        swFeat.SetSuppression2(0, 2, null);
                    }
                    else if (item.NetPlug == "Both")
                    {
                        swFeat = swComp.FeatureByName("2xRJ12-F");
                        swFeat.SetSuppression2(1, 2, null);
                        swFeat = swComp.FeatureByName("2xRJ45-F");
                        swFeat.SetSuppression2(1, 2, null);
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("2xRJ12-F");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("2xRJ45-F");
                        swFeat.SetSuppression2(0, 2, null);
                    }

                    if (item.PowerPlug == "NAC31")
                    {
                        swFeat = swComp.FeatureByName("NAC31-F");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("NAC21-F");
                        swFeat.SetSuppression2(1, 2, null);
                    }
                    else if (item.PowerPlug == "NAC21")
                    {
                        swFeat = swComp.FeatureByName("NAC31-F");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("NAC21-F");
                        swFeat.SetSuppression2(1, 2, null);
                    }
                    else if (item.PowerPlug == "Both")
                    {
                        swFeat = swComp.FeatureByName("NAC31-F");
                        swFeat.SetSuppression2(1, 2, null);
                        swFeat = swComp.FeatureByName("NAC21-F");
                        swFeat.SetSuppression2(1, 2, null);
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("NAC31-F");
                        swFeat.SetSuppression2(0, 2, null);
                        swFeat = swComp.FeatureByName("NAC21-F");
                        swFeat.SetSuppression2(0, 2, null);
                    }

                }
                else
                    {
                    swFeat = swComp.FeatureByName("NAC31-F");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("NAC21-F");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("2xRJ12-F");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("2xRJ45-F");
                    swFeat.SetSuppression2(0, 2, null);
                    }

                /* NTC 温度感应器//  （HMF需要  TerminalBlock-1  NTC-anturi-1两零件释放，其他压缩）

                /* swComp = swAssy.GetComponentByNameWithSuffix(suffix,"NTC-anturi-1");

                 swEdit.NTC-anturi(swComp, "HME");

              */
                swModel.ForceRebuild3(true);//设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
                swModel.Save();//保存，很耗时间
                swApp.CloseDoc(packedAssyPath);//关闭，很快
            }
            catch (Exception ex)
            {
                throw new Exception(packedAssyPath + "作图过程发生异常，详细：" + ex.Message);
            }
            finally
            {
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }
        }

    }
}
