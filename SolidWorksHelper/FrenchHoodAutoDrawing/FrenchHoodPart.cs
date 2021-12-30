using SolidWorks.Interop.sldworks;
namespace SolidWorksHelper
{
    public class FrenchHoodPart
    {
        #region 排风腔
        public void FNHE0195(Component2 swComp, double length, int backRivetNum, double backRivetSideDis, string waterCollection, string sidePanel, string outlet, string backToBack)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件3
            swPart.ChangeDim("D2@Base-Flange1", length);
            //铆钉孔
            swPart.ChangeDim("D1@LPattern1", backRivetNum);
            swPart.ChangeDim("D4@Sketch91", backRivetSideDis);
            //集水翻边
            if (waterCollection == "YES")
            {
                if (sidePanel == "RIGHT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.Suppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "LEFT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                    swComp.Suppress("DRAINCHANNEL-RIGHT");
                }
                else if (sidePanel == "BOTH")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
            }
            else
            {
                swComp.Suppress("DRAINCHANNEL-RIGHT");
                swComp.Suppress("DRAINCHANNEL-LEFT");
            }
            //油塞
            if (outlet == "LEFTTAP")
            {
                swComp.UnSuppress("DRAINTAP-LEFT");
                swComp.Suppress("DRAINTAP-RIGHT");
            }
            else if (outlet == "RIGHTTAP")
            {
                swComp.Suppress("DRAINTAP-LEFT");
                swComp.UnSuppress("DRAINTAP-RIGHT");
            }
            else
            {
                swComp.Suppress("DRAINTAP-LEFT");
                swComp.Suppress("DRAINTAP-RIGHT");
            }
            //背靠背
            if (backToBack == "YES") swComp.UnSuppress("BACKTOBACK");
            else swComp.Suppress("BACKTOBACK");

        }

        public void FNHE0196(Component2 swComp, double length, int backRivetNum, double backRivetSideDis, double exRightDis, string UVType, int exNo, double exLength, double exWidth, double exDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件3
            swPart.ChangeDim("D2@Base-Flange1", length - 1d);
            //铆钉孔
            swPart.ChangeDim("D1@LPattern1", backRivetNum); //D1阵列数量,D3阵列距离
            swPart.ChangeDim("D5@Sketch31", backRivetSideDis - 1d);
            //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
            if (UVType == "NO")
            {
                swComp.Suppress("UVSupport");
                swComp.Suppress("UVCABLE");
            }
            else
            {
                swComp.UnSuppress("UVSupport");
                swPart.ChangeDim("D5@Sketch33", exRightDis - 1d);
                if (UVType == "LONG") swPart.ChangeDim("D4@Sketch33", 1611d);
                else swPart.ChangeDim("D4@Sketch33", 899d);

                swComp.UnSuppress("UVSupport2");
                swPart.ChangeDim("D4@Sketch78", exRightDis - 1d);
                if (UVType == "LONG") swPart.ChangeDim("D3@Sketch78", 1617d);
                else swPart.ChangeDim("D3@Sketch78", 905d);

                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D8@Sketch75", exRightDis - 1d);
                if (UVType == "LONG") swPart.ChangeDim("D1@Sketch75", 1400d);
                else swPart.ChangeDim("D1@Sketch75", 700d);
            }
            //排风口
            if (exNo == 1)
            {
                swComp.UnSuppress("EXCOONE");
                swComp.Suppress("EXCOTWO");
                swPart.ChangeDim("D4@Sketch35", exRightDis - 1d);
                swPart.ChangeDim("D3@Sketch35", exLength);
                swPart.ChangeDim("D2@Sketch35", exWidth);
            }
            else
            {
                swComp.Suppress("EXCOONE");
                swComp.UnSuppress("EXCOTWO");
                swPart.ChangeDim("D5@Sketch119",exRightDis - 1d);
                swPart.ChangeDim("D1@Sketch119",exDis );
                swPart.ChangeDim("D3@Sketch119",exLength );
                swPart.ChangeDim("D4@Sketch119",exWidth );
            }
            //测风压口，出线孔是否需要完善
            //swFeat = swComp.Suppress("EXTAB-UP");
            //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            ////UV HoodParent,过滤器感应出线孔，UV门，UV cable-UV灯线缆穿孔避让缺口
            //swFeat = swComp.Suppress("FILTER-CABLE");
            //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
        }

        public void FNHE0197(Component2 swComp, double length, string UVType, double exRightDis, double withoutExSuMiDis, string lightType, double midRoofSidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1",length );
            if (UVType == "LONG")
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D15@Sketch357",exRightDis);
            }
            else if (UVType == "SHORT")
            {
                swComp.Suppress("UVDOOR-LONG");
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D15@Sketch115",exRightDis );
            }
            else
            {
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-SHORT");
            }
            //随着烟罩深度变化,等距居中处理
            swPart.ChangeDim("D1@Sketch1",withoutExSuMiDis + 0.5d);
            //侧板铆钉，大孔相对与铆钉孔居中分布，铆钉孔2个，距离边分别为20和30,与侧板联动
            if (withoutExSuMiDis >= 90)
            {
                //解压
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D3@LPattern1",withoutExSuMiDis - 50d);
                swPart.ChangeDim("D15@Sketch173",(withoutExSuMiDis - 50d) / 2d + 20d);
            }
            else
            {
                //压缩
                swComp.Suppress("LPattern1");
                swPart.ChangeDim("D15@Sketch173",40d );
            }
            //灯具铆钉1200/600，D6@Sketch113
            if (lightType == "SHORT")
            {
                swComp.UnSuppress("Extrude13");
                swPart.ChangeDim("D6@Sketch113",600d );
            }
            else if (lightType == "LONG")
            {
                swComp.UnSuppress("Extrude13");
                swPart.ChangeDim("D6@Sketch113",1200d );
            }
            else
            {
                swComp.Suppress("Extrude13");
            }
            //MidRoof侧板铆钉
            if (midRoofSidePanel < 120d)
            {
                swComp.Suppress("Cut-Extrude2");
            }
            else
            {
                swComp.UnSuppress("Cut-Extrude2");
                swPart.ChangeDim("D1@Sketch398",midRoofSidePanel - 25d );
            }
            //吊耳铆钉孔
            if (midRoofSidePanel < 72d) swComp.Suppress("Extrude14");
            else swComp.UnSuppress("Extrude14");
        }
        #endregion

        #region 排风脖颈，KSA,MESH,灯具，吊装
        public void ExaustSpigot(AssemblyDoc swAssy, string suffix, string ANSUL, string MARVEL, double exLength, double exWidth, double exHeight)
        {
            Component2 swComp;
            if (ANSUL != "YES" && (exHeight == 100d || MARVEL == "YES"))
            {
                swAssy.Suppress(suffix, "FNHE0006-1");
                swAssy.Suppress(suffix, "FNHE0007-1");
                swAssy.Suppress(suffix, "FNHE0008-1");
                swAssy.Suppress(suffix, "FNHE0009-1");
            }
            else
            {
                swComp = swAssy.UnSuppress(suffix, "FNHE0006-1");
                ModelDoc2 swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Base-Flange1",exLength + 50d);
                swPart.ChangeDim("D2@Sketch1",exHeight );
                
                if (ANSUL == "YES") swComp.UnSuppress("ANSUL");
                else swComp.Suppress("ANSUL");
                swComp = swAssy.UnSuppress(suffix, "FNHE0007-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Base-Flange1",exLength + 50d );
                swPart.ChangeDim("D2@Sketch1",exHeight );
                swComp = swAssy.UnSuppress(suffix, "FNHE0008-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@基体-法兰1",exWidth );
                swPart.ChangeDim("D3@草图1",exHeight );
                swComp = swAssy.UnSuppress(suffix, "FNHE0009-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@基体-法兰1",exWidth );
                swPart.ChangeDim("D3@草图1",exHeight );
            }
        }

        public void ExaustRail(AssemblyDoc swAssy, string suffix, string MARVEL, double exLength, double exWidth, int exNo, double exDis)
        {
            Component2 swComp;
            ModelDoc2 swPart;
            if (exWidth == 300d) swAssy.Suppress(suffix, "FNCE0013-2"); 
            else swAssy.UnSuppress(suffix, "FNCE0013-2");
            if (exWidth == 300d) swAssy.Suppress(suffix, "FNCE0013-1"); 
            else
            {
                swComp = swAssy.UnSuppress(suffix, "FNCE0013-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@Sketch1",exLength / 2 + 10d);
                swPart.ChangeDim("D2@Sketch1",exWidth + 20d);
            }
            if (MARVEL == "YES") swAssy.Suppress(suffix, "FNCE0018-2"); 
            else swAssy.UnSuppress(suffix, "FNCE0018-2");
            
            if (MARVEL == "YES") swAssy.Suppress(suffix, "FNCE0018-1");
            else
            {
                swComp = swAssy.UnSuppress(suffix, "FNCE0018-1");
                swPart = swComp.GetModelDoc2();
                if (exNo == 1) swPart.ChangeDim("D2@Base-Flange1",exLength * 2d + 100d);
                else swPart.ChangeDim("D2@Base-Flange1",exLength * 3d + exDis + 100d);
            }
        }

        public void KSAFilter(AssemblyDoc swAssy, string suffix, double ksaSideLength)
        {
            Component2 swComp;
            if (ksaSideLength <= 30d)
            {
                //压缩所有侧板，等确定方案后再修改
               swAssy.Suppress(suffix, "FNHE0201-1");
               swAssy.Suppress(suffix, "FNHE0202-1");
               
            }
            else
            {
                swComp = swAssy.UnSuppress(suffix, "FNHE0201-1");
                ModelDoc2 swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@Sketch1",ksaSideLength);
                swComp = swAssy.UnSuppress(suffix, "FNHE0202-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@Sketch1",ksaSideLength);
            }
        }

        public void MeshFilter(AssemblyDoc swAssy, string suffix, double ksaSideLength)
        {
            Component2 swComp;
            if (ksaSideLength <= 30d)
            {
                //压缩所有侧板，等确定方案后再修改
                swAssy.Suppress(suffix, "FNHE0203-1");
               swAssy.Suppress(suffix, "FNHE0204-1");
            }
            else
            {
                swComp = swAssy.UnSuppress(suffix, "FNHE0203-1");
                ModelDoc2 swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Esquisse1",ksaSideLength + 10d - 2d);
                swComp = swAssy.UnSuppress(suffix, "FNHE0204-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Esquisse1",ksaSideLength - 10d - 2d);
            }
        }

        internal void LightOperate(AssemblyDoc swAssy, string suffix, string lightType)
        {
            Component2 swComp;
            if (lightType == "FSLONG")
            {
                swAssy.Suppress(suffix, "5214020201-1");
                swAssy.Suppress(suffix, "5214020403-1");
                swAssy.Suppress(suffix, "5214020401-1");
                swAssy.Suppress(suffix, "2200300026-1");
                swAssy.Suppress(suffix, "2200300026-2");
                
                swAssy.UnSuppress(suffix, "5214020402-1");
                swAssy.UnSuppress(suffix, "5214020404-1");
                swAssy.UnSuppress(suffix, "5214020202-1");
                swAssy.UnSuppress(suffix, "2200300023-1");
                swAssy.UnSuppress(suffix, "2200300023-2");
               
            }
            else if (lightType == "FSSHORT")
            {
                swAssy.UnSuppress(suffix, "5214020201-1");
                swAssy.UnSuppress(suffix, "5214020403-1");
                swAssy.UnSuppress(suffix, "5214020401-1");
                swAssy.UnSuppress(suffix, "2200300026-1");
                swAssy.UnSuppress(suffix, "2200300026-2");

                swAssy.Suppress(suffix, "5214020402-1");
                swAssy.Suppress(suffix, "5214020404-1");
                swAssy.Suppress(suffix, "5214020202-1");
                swAssy.Suppress(suffix, "2200300023-1");
                swAssy.Suppress(suffix, "2200300023-2");
            }
            else
            {
                swAssy.Suppress(suffix, "5214020201-1");
                swAssy.Suppress(suffix, "5214020403-1");
                swAssy.Suppress(suffix, "5214020401-1");
                swAssy.Suppress(suffix, "2200300026-1");
                swAssy.Suppress(suffix, "2200300026-2");

                swAssy.Suppress(suffix, "5214020402-1");
                swAssy.Suppress(suffix, "5214020404-1");
                swAssy.Suppress(suffix, "5214020202-1");
                swAssy.Suppress(suffix, "2200300023-1");
                swAssy.Suppress(suffix, "2200300023-2");
            }
        }

        public void Hanger(AssemblyDoc swAssy, string suffix, double deepth, string ANSUL, string sidePanel)
        {
            Component2 swComp;
            swComp = swAssy.Suppress(suffix, "2900100001-1");
            ModelDoc2 swPart = swComp.GetModelDoc2();
            if (ANSUL == "YES") swPart.ChangeDim("D2@基体-法兰1").SystemValue = (deepth - 250) );
            else swPart.ChangeDim("D2@基体-法兰1").SystemValue = (deepth - 100d) );
            //只有组拼的时候才需要吊耳
            if (sidePanel == "BOTH")
            {
                swComp = swAssy.Suppress(suffix, "5214020406-1"));
                swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-2"));
                swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-3"));
                swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-4"));
                swComp.SetSuppression2(0); //2解压缩，0压缩
            }
            else if (sidePanel == "LEFT")
            {
                swComp = swAssy.Suppress(suffix, "5214020406-1"));
                swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-2"));
                swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-3"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-4"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
            }
            else if (sidePanel == "RIGHT")
            {
                swComp = swAssy.Suppress(suffix, "5214020406-1"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-2"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-3"));
                swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-4"));
                swComp.SetSuppression2(0); //2解压缩，0压缩
            }
            else
            {
                swComp = swAssy.Suppress(suffix, "5214020406-1"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-2"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-3"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.Suppress(suffix, "5214020406-4"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
            }
        }

        #endregion

        #region 灯板
        public void FNHM0041(Component2 swComp, double midRoofSidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1").SystemValue = (midRoofSidePanel) );//水洗烟罩
            if (midRoofSidePanel < 120d)
            {
                swFeat = swComp.Suppress("Cut-Extrude1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.Suppress("Cut-Extrude1");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
            //吊耳铆钉孔
            if (midRoofSidePanel < 72d)
            {
                swFeat = swComp.Suppress("Extrude4");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.Suppress("Extrude4");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
        }
        #endregion


        #region 法国烟罩大侧板
        internal void FNHS0073(Component2 swComp, double deepth, double height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1").SystemValue = deepth );
            swPart.ChangeDim("D2@Sketch1").SystemValue = (height - 20d) );
            swPart.ChangeDim("D1@LPattern2").SystemValue = sidePanelSideCjNo;
            swPart.ChangeDim("D1@LPattern1").SystemValue = sidePanelDownCjNo;
        }

        internal void FNHS0075(Component2 swComp, double deepth, double height, double withoutExSuMiDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D7@Sketch1").SystemValue = deepth );
            swPart.ChangeDim("D6@Sketch1").SystemValue = (height - 1d) );
            //侧板铆钉，大孔相对与铆钉孔居中分布，铆钉孔2个，距离边分别为20和30,与侧板联动
            if (withoutExSuMiDis >= 90)
            {
                //解压
                swFeat = swComp.Suppress("LPattern1");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.ChangeDim("D3@LPattern1").SystemValue = (withoutExSuMiDis - 50d) );
                swFeat = swComp.Suppress("LPattern2");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.ChangeDim("D3@LPattern2").SystemValue = (withoutExSuMiDis - 50d) );
            }
            else
            {
                //压缩
                swFeat = swComp.Suppress("LPattern1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.Suppress("LPattern2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
        }

        internal void FNHS0074(Component2 swComp, double deepth, double height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1").SystemValue = deepth );
            swPart.ChangeDim("D2@Sketch1").SystemValue = (height - 20d) );
            swPart.ChangeDim("D1@LPattern2").SystemValue = sidePanelSideCjNo;
            swPart.ChangeDim("D1@LPattern1").SystemValue = sidePanelDownCjNo;
        }
        internal void FNHS0076(Component2 swComp, double deepth, double height, double withoutExSuMiDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D7@Sketch1").SystemValue = deepth );
            swPart.ChangeDim("D6@Sketch1").SystemValue = (height - 1d) );
            //侧板铆钉，大孔相对与铆钉孔居中分布，铆钉孔2个，距离边分别为20和30,与侧板联动
            if (withoutExSuMiDis >= 90)
            {
                //解压
                swFeat = swComp.Suppress("LPattern1");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.ChangeDim("D3@LPattern1").SystemValue = (withoutExSuMiDis - 50d) );
                swFeat = swComp.Suppress("LPattern2");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.ChangeDim("D3@LPattern2").SystemValue = (withoutExSuMiDis - 50d) );
            }
            else
            {
                //压缩
                swFeat = swComp.Suppress("LPattern1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.Suppress("LPattern2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
        }

        internal void FNHS0077(Component2 swComp, double deepth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1").SystemValue = (deepth - 283.5d) );
        }
        internal void FNHS0078(Component2 swComp, double deepth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1").SystemValue = (deepth - 283.5d) );
        }
        #endregion


        public void FNHA0115(Component2 swComp, double length, int frontCjNo, double frontCjFirstDis, int frontPanelHoleNo, double frontPanelHoleDis, string bluetooth, string LEDlogo, string waterCollection, string sidePanel)
        {
            //完全使用扩展方法
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length);
            swPart.ChangeDim("D1@CJ CJ 3.5 5.0", frontCjNo);
            swPart.ChangeDim("D1@Sketch70", frontCjFirstDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
            if (bluetooth == "YES") swComp.UnSuppress("BLUETOOTH");
            else swComp.Suppress("BLUETOOTH");

            //百叶孔为奇数时，移动蓝牙孔到两个百叶中间避让
            if (frontPanelHoleNo % 2 != 1) swPart.ChangeDim("D4@Sketch83", length / 2d);
            else swPart.ChangeDim("D4@Sketch83", (length - frontPanelHoleDis) / 2d);

            if (LEDlogo == "YES") swComp.UnSuppress("LEGO");
            else swComp.Suppress("LEGO");

            if (waterCollection == "YES")
            {
                if (sidePanel == "RIGHT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.Suppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "LEFT")
                {
                    swComp.Suppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "BOTH")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
            }
            else
            {
                swComp.Suppress("DRAINCHANNEL-RIGHT");
                swComp.Suppress("DRAINCHANNEL-LEFT");
            }
        }

        public void FNHA0116(Component2 swComp, double length, double withoutExSuMiDis, string lightType, double midRoofSidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length);
            //随着烟罩深度变化,等距居中处理
            swPart.ChangeDim("D2@Sketch1", withoutExSuMiDis + 1d);
            //侧板铆钉，大孔相对与铆钉孔居中分布，铆钉孔2个，距离边分别为20和30,与侧板联动
            if (withoutExSuMiDis >= 90)
            {
                swComp.UnSuppress("LPattern2");
                swPart.ChangeDim("D3@LPattern2", withoutExSuMiDis - 50d);
                swPart.ChangeDim("D9@Sketch24", (withoutExSuMiDis - 50d) / 2d + 20d);
            }
            else
            {
                swComp.Suppress("LPattern2");
                swPart.ChangeDim("D9@Sketch24", 40d);
            }
            //灯具铆钉770/600，D6@Sketch113
            if (lightType == "SHORT")
            {
                swComp.UnSuppress("3.3 Led Light");
                swPart.ChangeDim("D4@Sketch26", 600d);
            }
            else if (lightType == "LONG")
            {
                swComp.UnSuppress("3.3 Led Light");
                swPart.ChangeDim("D4@Sketch26", 1200d);
            }
            else
            {
                swComp.Suppress("3.3 Led Light");
            }
            //MidRoof侧板铆钉
            if (midRoofSidePanel < 120d)
            {
                swComp.Suppress("Cut-Extrude9");
            }
            else
            {
                swComp.UnSuppress("Cut-Extrude9");
                swPart.ChangeDim("D1@Sketch117", midRoofSidePanel - 25d);
            }
            //吊耳铆钉孔
            if (midRoofSidePanel < 72d) swComp.Suppress("3.3 Middle Roof");
            else swComp.UnSuppress("3.3 Middle Roof");

        }

        public void FNHA0117(Component2 swComp, double length, int backRivetNum, double backRivetSideDis, int suNo, double suDis, string bluetooth, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", length - 2d);
            //铆钉孔
            swPart.ChangeDim("D1@LPattern1", backRivetNum);
            //阵列距离
            swPart.ChangeDim("D1@Sketch35", backRivetSideDis - 1d);
            //新风脖颈
            swPart.ChangeDim("D1@Sketch48", suDis * (suNo / 2 - 1) + suDis / 2d);

            if (suNo == 1) swComp.Suppress("LPattern2");
            else
            {
                swComp.UnSuppress("LPattern2");
                swPart.ChangeDim("D1@LPattern2", suNo);
                swPart.ChangeDim("D3@LPattern2", suDis);
            }
            //UV HOOD
            if (bluetooth == "YES") swComp.UnSuppress("Bluetooth Wire Hole");
            else swComp.Suppress("Bluetooth Wire Hole");

            if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("UV Junction Box");
            else swComp.Suppress("UV Junction Box");
        }

        public void FNHA0118(Component2 swComp, double length, int backRivetNum, double backRivetSideDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", length);
            swPart.ChangeDim("D1@LPattern2", backRivetNum);
            swPart.ChangeDim("D1@Sketch31", backRivetSideDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
            //改成卡扣形式? 
        }
        public void FNHA0111(Component2 swComp, double length, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", length - 1d);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }
    }
}
