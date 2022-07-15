using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos
{
    /// <summary>
    /// 项目跟踪
    /// </summary>
    public class ProjectTrackingDto:BaseDto
    {
        private string semiProductStatus;
        public string SemiProductStatus
        {
            get { return semiProductStatus; }
            set { semiProductStatus = value; OnPropertyChanged(); }
        }
        private string purchaseStatus;
        public string PurchaseStatus
        {
            get { return purchaseStatus; }
            set { purchaseStatus = value; OnPropertyChanged(); }
        }
        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; OnPropertyChanged(); }
        }

        private DateTime odpReleaseActual;
        public DateTime OdpReleaseActual
        {
            get { return odpReleaseActual; }
            set { odpReleaseActual = value; OnPropertyChanged(); }
        }
        private DateTime kickOffActual;
        public DateTime KickOffActual
        {
            get { return kickOffActual; }
            set { kickOffActual = value; OnPropertyChanged(); }
        }
        private DateTime drwReleaseActual;
        public DateTime DrwReleaseActual
        {
            get { return drwReleaseActual; }
            set { drwReleaseActual = value; OnPropertyChanged(); }
        }
        private DateTime prodFinishActual;
        public DateTime ProdFinishActual
        {
            get { return prodFinishActual; }
            set { prodFinishActual = value; OnPropertyChanged(); }
        }
        private DateTime goodsDeliveryActual;
        public DateTime GoodsDeliveryActual
        {
            get { return goodsDeliveryActual; }
            set { goodsDeliveryActual = value; OnPropertyChanged(); }
        }
        //-------------------导航属性Project(1:1)---------------------
        private ProjectDto project;
        public ProjectDto Project
        {
            get { return project; }
            set { project = value;OnPropertyChanged(); }
        }

    }
}
