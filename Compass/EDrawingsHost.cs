//**********************
//Hosting eDrawings control in Windows Forms
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/edrawings-api/gettings-started/winforms/
//**********************

using eDrawings.Interop.EModelViewControl;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Compass
{
    public class EDrawingsHost : AxHost
    {
        public event Action<EModelViewControl> ControlLoaded;

        private bool m_IsLoaded;
        //EModelNonVersionSpecificViewControl = 22945A69-1191-4DCF-9E6F-409BDE94D101 
        //EModelNonVersionSpecificMarkupControl = 5BBBC05A-BD4D-4e3b-AD5B-51A79DFC522F
        //EModelView.EModelViewControl.21 = DF78AAAB-45C8-420C-9DB3-CAD13762FE35
        public EDrawingsHost() : base("22945A69-1191-4DCF-9E6F-409BDE94D101")
        {
            m_IsLoaded = false;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (!m_IsLoaded)
            {
                m_IsLoaded = true;
                var ctrl = this.GetOcx() as EModelViewControl;
                ControlLoaded?.Invoke(this.GetOcx() as EModelViewControl);
                ctrl.EnableFeatures = (int)EMVEnableFeatures.eMVFullUI;
                //ctrl.EnableFeature[EMVEnableFeatures.eMVFullUI] = true;
                //ctrl.FullUI =(int) EMVEnableFeatures.eMVFullUI;
            }
        }
    }
}
