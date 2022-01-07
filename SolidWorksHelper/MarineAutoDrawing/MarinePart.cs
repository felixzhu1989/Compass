using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    class MarinePart
    {
        ModelDoc2 swPart;
        Feature swFeat;

        public void MNCC0006(Component2 swComp, string model, double height, string powerPlug, double powerPlugDis, string netPlug, string plugPosition, string heater, string temperatureSwitch, string windPressure)
        {
            swPart = swComp.GetModelDoc2();
            if (height < 220d)
            {
                swPart.Parameter("Korkeus2@Sketch1").SystemValue = 109.75d / 1000d;
                swPart.Parameter("D3@Sketch16").SystemValue = 14.5d / 1000d;
            }

            if (height ==220d)
            {
                swPart.Parameter("Korkeus2@Sketch1").SystemValue = 129.75d / 1000d;
                swPart.Parameter("D3@Sketch16").SystemValue = 22.5d / 1000d;
            }
            
            if (height > 220d)
            {
                swPart.Parameter("Korkeus2@Sketch1").SystemValue = 129.75d / 1000d;
                swPart.Parameter("D3@Sketch16").SystemValue = 14.5d / 1000d;
            }

            #region 模型
            if (model == "HME")
            {
                swFeat = swComp.FeatureByName("HME");
                swFeat.SetSuppression2(1, 2, null);
                swFeat = swComp.FeatureByName("HMF");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HMM");
                swFeat.SetSuppression2(0, 2, null);
            }
            else if (model == "HMF")
            {
                swFeat = swComp.FeatureByName("HME");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HMF");
                swFeat.SetSuppression2(1, 2, null);
                swFeat = swComp.FeatureByName("HMM");
                swFeat.SetSuppression2(0, 2, null);

            }
            else//HMM
            {
                swFeat = swComp.FeatureByName("HME");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HMF");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HMM");
                swFeat.SetSuppression2(1, 2, null);
                if(heater == "No")
                {
                    swFeat = swComp.FeatureByName("Relay_HMM");
                    swFeat.SetSuppression2(0, 2, null);

                }
                else
                {
                    swFeat = swComp.FeatureByName("Relay_HMM");
                    swFeat.SetSuppression2(1, 2, null);

                }

            }
            #endregion

            #region 网线和电源插口
            if (plugPosition == "Right")
            {
                //网线插口
                if (netPlug == "No")
                {
                    swFeat = swComp.FeatureByName("2xRJ12-R");
                    swFeat.SetSuppression2(0, 2, null);
                }
                else
                {
                    swFeat = swComp.FeatureByName("2xRJ12-R");
                    swFeat.SetSuppression2(1, 2, null);
                    swPart.Parameter("D3@Sketch67").SystemValue = (powerPlugDis - 140d - 51d) / 1000d;
                }
                //电源插口
                if (powerPlug == "NAC21" || powerPlug == "NAC31")
                {
                    swFeat = swComp.FeatureByName("PowerPlug-R");
                    swFeat.SetSuppression2(1, 2, null);
                    swPart.Parameter("D3@Sketch68").SystemValue = (powerPlugDis - 140d) / 1000d;
                    if (powerPlug == "NAC31")//D1@Sketch63=48
                    {
                        swPart.Parameter("D1@Sketch68").SystemValue = 48d / 1000d;
                    }
                    else//D1@Sketch63=39
                    {
                        swPart.Parameter("D1@Sketch68").SystemValue = 39d / 1000d;
                    }
                }
                else
                {
                    swFeat = swComp.FeatureByName("PowerPlug-R");
                    swFeat.SetSuppression2(0, 2, null);
                }
                swFeat = swComp.FeatureByName("2XRJ12-F");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("2xRJ45-F");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("NAC31-F");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("NAC21-F");
                swFeat.SetSuppression2(0, 2, null);

                //画好左边特征以后完善
            }
            else if (plugPosition == "Left")
            {
                swFeat = swComp.FeatureByName("2xRJ12-R");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("PowerPlug-R");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("2XRJ12-F");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("2xRJ45-F");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("NAC31-F");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("NAC21-F");
                swFeat.SetSuppression2(0, 2, null);
            }
            else//前面
            {
                swFeat = swComp.FeatureByName("2xRJ12-R");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("PowerPlug-R");
                swFeat.SetSuppression2(0, 2, null);
                if (netPlug == "2xRJ12")
                {
                    swFeat = swComp.FeatureByName("2xRJ12-F");
                    swFeat.SetSuppression2(1, 2, null);
                    swFeat = swComp.FeatureByName("2xRJ45-F");
                    swFeat.SetSuppression2(0, 2, null);
                }
                else if (netPlug == "2xRJ45")
                {
                    swFeat = swComp.FeatureByName("2xRJ12-F");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("2xRJ45-F");
                    swFeat.SetSuppression2(1, 2, null);
                }
                else if (netPlug == "Both")
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

                if (powerPlug == "NAC31")
                {
                    swFeat = swComp.FeatureByName("NAC31-F");
                    swFeat.SetSuppression2(1, 2, null);
                    swFeat = swComp.FeatureByName("NAC21-F");
                    swFeat.SetSuppression2(0, 2, null);
                }
                else if (powerPlug == "NAC21")
                {
                    swFeat = swComp.FeatureByName("NAC31-F");
                    swFeat.SetSuppression2(0, 2, null);
                    swFeat = swComp.FeatureByName("NAC21-F");
                    swFeat.SetSuppression2(1, 2, null);
                }
                else if (powerPlug == "Both")
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
            #endregion

            //加热管
            swFeat = swComp.FeatureByName("Heater");
            if (heater == "Yes") swFeat.SetSuppression2(1, 2, null);
            else swFeat.SetSuppression2(0, 2, null);
            //temperatureSwitch

            if (temperatureSwitch == "Yes" && model == "HMF")
            {
                swFeat = swComp.FeatureByName("TemperatureSwitch");
                swFeat.SetSuppression2(1, 2, null);
                swPart.Parameter("D4@Sketch92").SystemValue = 53d / 1000d;

            }
            else if (temperatureSwitch == "Yes" && model == "HMM")
            {

                swFeat = swComp.FeatureByName("TemperatureSwitch");
                swFeat.SetSuppression2(1, 2, null);
                swPart.Parameter("D4@Sketch92").SystemValue = 43d / 1000d;
            }
            else if (temperatureSwitch == "No")
            {
                swFeat = swComp.FeatureByName("TemperatureSwitch");
                swFeat.SetSuppression2(0, 2, null);

            }
            //测压管
            if (windPressure == "Yes")
            {
                swFeat = swComp.FeatureByName("Cut-Extrude44");
                swFeat.SetSuppression2(1, 2, null);
            }
            else
            {
                swFeat = swComp.FeatureByName("Cut-Extrude44");
                swFeat.SetSuppression2(0, 2, null);

            }
        }
        
        public void MNCI0006(Component2 swComp, string model)
        {
            if (model == "HME")
            {
                swFeat = swComp.FeatureByName("HME");
                swFeat.SetSuppression2(1, 2, null);
                swFeat = swComp.FeatureByName("HMF");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HMM");
                swFeat.SetSuppression2(0, 2, null);

            }
            else if (model == "HMF")
            {
                swFeat = swComp.FeatureByName("HME");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HMF");
                swFeat.SetSuppression2(1, 2, null);
                swFeat = swComp.FeatureByName("HMM");
                swFeat.SetSuppression2(0, 2, null);

            }
            else if (model == "HMM")
            {
                swFeat = swComp.FeatureByName("HME");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HMF");
                swFeat.SetSuppression2(0, 2, null);
                swFeat = swComp.FeatureByName("HMM");
                swFeat.SetSuppression2(1, 2, null);

            }
        }


    }
}
