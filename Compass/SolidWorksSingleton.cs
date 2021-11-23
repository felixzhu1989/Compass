using System;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    public class SolidWorksSingleton
    {
        private static SldWorks _swApp;
        /// <summary>
        /// 连接SW，已经不使用了
        /// </summary>
        /// <returns></returns>
        public static SldWorks GetApplication()
        {
            if (_swApp == null)
            {
                _swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                _swApp.Visible = true;
                return _swApp;
            }
            return _swApp;
        }
        /// <summary>
        /// 以异步的方式连接SW
        /// </summary>
        /// <returns></returns>
        public async static Task<SldWorks> GetApplicationAsync()
        {
            if (_swApp == null)
            {
                return await Task.Run(() =>
                {
                    _swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                    _swApp.Visible = true;
                    return _swApp;
                });
            }
            return _swApp;
        }
        /// <summary>
        /// 释放SW
        /// </summary>
        public static void Dipose()
        {
            if (_swApp != null)
            {
                _swApp.ExitApp();
                _swApp = null;
            }
        }
    }
}
