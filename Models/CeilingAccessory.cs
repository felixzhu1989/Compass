using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
   public class CeilingAccessory
    {
        public int CeilingPackingListId { get; set; }
        public string CeilingAccessoryId { get; set; }
        public int ProjectId { get; set; }
        public int ClassNo { get; set; }//0日本项目不要的配件，1适用于所有项目的配件，2自制件折弯，3自制件焊接
        public string PartDescription { get; set; }
        public string PartNo { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Material { get; set; }
        public string Remark { get; set; }
        public string CountingRule { get; set; }
        public DateTime AddedDate { get; set; }
        public int UserId { get; set; }
        public string Location { get; set; }
        //简单扩展
        public string ODPNo { get; set; }
        public string ProjectName { get; set; }
        public string UserAccount { get; set; }
        
    }
}
