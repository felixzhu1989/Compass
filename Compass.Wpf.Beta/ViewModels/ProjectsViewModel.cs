using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Wpf.Beta.ViewModels
{
    public class ProjectsViewModel : BindableBase
    {
        #region 属性
        private string search;
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }
        private DateTime yearMonth = DateTime.Now;
        public DateTime YearMonth
        {
            get { return yearMonth; }
            set { yearMonth = value; RaisePropertyChanged(); }
        }
        


        #endregion
        #region 命令
        public DelegateCommand QueryCommand { get; }

        #endregion


        public ProjectsViewModel()
        {
            
        }


    }
}
