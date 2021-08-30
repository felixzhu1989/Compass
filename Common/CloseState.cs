namespace Common
{
    public class CloseState
    {
        private readonly int _Timeout;
        public int Timeout
        {
            get
            {
                return _Timeout;
            }
        }
        private readonly string _Caption;
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
