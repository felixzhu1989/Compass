using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos
{
    /// <summary>
    /// 财务信息
    /// </summary>
    public class FinancialInformationDto:BaseDto
    {
        private decimal vtaValue;
        public decimal VtaValue
        {
            get { return vtaValue; }
            set { vtaValue = value; OnPropertyChanged(); }
        }
        private DateTime invoiceMonth;
        public DateTime InvoiceMonth
        {
            get { return invoiceMonth; }
            set { invoiceMonth = value; OnPropertyChanged(); }
        }

        //-------------------导航属性Project(1:1)---------------------
        private ProjectDto project;
        public ProjectDto Project
        {
            get { return project; }
            set { project = value; OnPropertyChanged(); }
        }
    }
}
