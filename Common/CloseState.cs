using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CloseState
    {
        private int _Timeout;
        public int Timeout
        {
            get
            {
                return _Timeout;
            }
        }
        private string _Caption;
        /// <summary>
        /// Caption of dialog
        /// </summary>
        public string Caption
        {
            get
            {
                return _Caption;
            }
        }
        public CloseState(string caption, int timeout)
        {
            _Timeout = timeout;
            _Caption = caption;
        }
    }
}
