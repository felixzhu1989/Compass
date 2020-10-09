using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface IModelService
    {
        DataSet GetModelByDataSet(string projectId);
        IModel GetModelById(string id);
        IModel GetModelByModuleTreeId(string moduleTreeId);
        IModel GetModelByWhereSql(string whereSql);
        int EditModel(IModel model);
    }
}
