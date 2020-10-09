using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    public class SolidWorksSingleton
    {
        private static SldWorks swApp;
        /// <summary>
        /// 连接SW，已经不使用了
        /// </summary>
        /// <returns></returns>
        public static SldWorks GetApplication()
        {
            if (swApp == null)
            {
                swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                swApp.Visible = true;
                return swApp;
            }
            return swApp;
        }
        /// <summary>
        /// 以异步的方式连接SW
        /// </summary>
        /// <returns></returns>
        public async static Task<SldWorks> GetApplicationAsync()
        {
            if (swApp == null)
            {
                return await Task<SldWorks>.Run(() =>
                {
                    swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                    swApp.Visible = true;
                    return swApp;
                });
            }
            return swApp;
        }
        /// <summary>
        /// 释放SW
        /// </summary>
        public static void Dipose()
        {
            if (swApp != null)
            {
                swApp.ExitApp();
                swApp = null;
            }
        }
    }
}
