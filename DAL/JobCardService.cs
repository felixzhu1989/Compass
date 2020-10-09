using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Model;

namespace DAL
{
    public class JobCardService
    {
        public JobCard GetJobCard(ModuleTree tree)
        {
            string sql = "select ODPNo,BPONo,ProjectName,CustomerName,Item,Module,Categories.Model,ShippingTime,Length,Deepth,Height,SidePanel,LabelImage,HoodType from ModuleTree"
                + " inner join DrawingPlan on DrawingPlan.DrawingPlanId=ModuleTree.DrawingPlanId"
                + " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId"
                + " inner join Customers on Projects.CustomerId=Customers.CustomerId"
                + " inner join Categories on Categories.CategoryId=ModuleTree.CategoryId"
                + " inner Join " + tree.CategoryName + " on " + tree.CategoryName + ".ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += string.Format(" where ModuleTree.ModuleTreeId={0}", tree.ModuleTreeId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            JobCard objJobCard = null;
            while (objReader.Read())
            {
                objJobCard = new JobCard()
                {
                    ODPNo = objReader["ODPNo"].ToString(),
                    BPONo = objReader["BPONo"].ToString(),
                    ProjectName = objReader["ProjectName"].ToString(),
                    CustomerName = objReader["CustomerName"].ToString(),
                    Item = objReader["Item"].ToString(),
                    Module = objReader["Module"].ToString(),
                    Model = objReader["Model"].ToString(),
                    ShippingTime = Convert.ToDateTime(objReader["ShippingTime"]),
                    Deepth = objReader["Deepth"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["Deepth"]),
                    SidePanel = objReader["SidePanel"].ToString(),
                    LabelImage = objReader["LabelImage"].ToString(),
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["Length"]),
                    HoodType = objReader["HoodType"].ToString(),
                    Height = objReader["Height"].ToString()
                };
            }
            objReader.Close();
            return objJobCard;
        }
    }
}
