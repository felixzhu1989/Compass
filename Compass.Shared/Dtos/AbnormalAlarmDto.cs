using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compass.Shared.Const;
using Microsoft.VisualBasic;

namespace Compass.Shared.Dtos
{
    /// <summary>
    /// 异常报警
    /// </summary>
    public class AbnormalAlarmDto:BaseDto
    {
        private Abnormal_e abnormal;
        public Abnormal_e Abnormal
        {
            get { return abnormal; }
            set { abnormal = value; OnPropertyChanged();}
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }
        private DateTime start;
        public DateTime Start
        {
            get { return start; }
            set { start = value; OnPropertyChanged(); }
        }
        private DateTime deadline;
        public DateTime Deadline
        {
            get { return deadline; }
            set { deadline = value; OnPropertyChanged(); }
        }
        

        private string solution;
        public string Solution
        {
            get { return solution; }
            set { solution = value; OnPropertyChanged(); }
        }
        private DateTime close;
        public DateTime Close
        {
            get { return close; }
            set { close = value; OnPropertyChanged(); }
        }


        //-------------------导航属性Reporter(n:1)---------------------
        private UserDto reporter;
        public UserDto Reporter
        {
            get { return reporter; }
            set { reporter = value; OnPropertyChanged(); }
        }

        //-------------------导航属性Responsible(n:1)---------------------
        private UserDto responsible;
        public UserDto Responsible
        {
            get { return responsible; }
            set { responsible = value; OnPropertyChanged(); }
        }
    }
}
