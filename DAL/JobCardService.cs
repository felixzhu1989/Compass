using System;
using System.Data.SqlClient;
using Models;
using Models.Model;

namespace DAL
{
    public class JobCardService
    {
        public JobCard GetJobCard(ModuleTree tree)
        {
            string sql =
                $"select ODPNo,BPONo,ProjectName,CustomerName,Item,Module,Categories{tree.SBU}.Model,ShippingTime,Length,Deepth,Height,SidePanel,LabelImage,HoodType from ModuleTree{tree.SBU}";
            sql += $" inner join DrawingPlan{tree.SBU} on DrawingPlan{tree.SBU}.DrawingPlanId=ModuleTree{tree.SBU}.DrawingPlanId";
            sql += $" inner join Projects{tree.SBU} on DrawingPlan{tree.SBU}.ProjectId=Projects{tree.SBU}.ProjectId";
            sql += $" inner join Customers on Projects{tree.SBU}.CustomerId=Customers.CustomerId";
            sql += $" inner join Categories{tree.SBU} on Categories{tree.SBU}.CategoryId=ModuleTree{tree.SBU}.CategoryId";
            sql += " inner Join " + tree.CategoryName + " on " + tree.CategoryName + $".ModuleTreeId=ModuleTree{tree.SBU}.ModuleTreeId";
            sql += $" where ModuleTree.ModuleTreeId={tree.ModuleTreeId}";
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
