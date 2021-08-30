using System.Data;
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
