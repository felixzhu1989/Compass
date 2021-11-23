//**********************
//Hosting eDrawings control in Windows Forms
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/edrawings-api/gettings-started/winforms/
//**********************

using eDrawings.Interop.EModelViewControl;
using System;
using System.Windows.Forms;

namespace Compass
{
    public class EDrawingsHost : AxHost
    {
        public event Action<EModelViewControl> ControlLoaded;

        private bool _mIsLoaded;
        
        public EDrawingsHost() : base("22945A69-1191-4DCF-9E6F-409BDE94D101")
        {
            _mIsLoaded = false;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!_mIsLoaded)
            {
                _mIsLoaded = true;
                var ctrl = GetOcx() as EModelViewControl;
                ControlLoaded?.Invoke(GetOcx() as EModelViewControl);
                ctrl.EnableFeatures = (int)EMVEnableFeatures.eMVFullUI;
            }
        }
    }
}
