using Compass.Shared.Const;
using Compass.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared;
public class LocalDb
{
    private List<ProjectDto> ProjectList;
    public LocalDb()
    {
        InitData();
    }
    /// <summary>
    /// 初始化模拟数据
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void InitData()
    {
        ProjectList=new List<ProjectDto>();
        ProjectList.Add(new ProjectDto()
        {
            QuoteNumber="SQFS220039",
            OdpNumber="FSO220118",
            ProdNumber="CMFP220071",
            ProjectName="南京东郊宾馆",
            OdpReleaseDate=Convert.ToDateTime("2022-05-24"),
            ProdFinishDate=Convert.ToDateTime("2022-06-10"),
            ProjectStatusRecords=new List<ProjectStatusRecordDto>
            {
                new ProjectStatusRecordDto() { ProjectStatus=ProjectStatus_e.GettingOdp, StartDate=Convert.ToDateTime("2022-05-20") },
                new ProjectStatusRecordDto() { ProjectStatus=ProjectStatus_e.DrwReleased, StartDate=Convert.ToDateTime("2022-05-24") },
                new ProjectStatusRecordDto() { ProjectStatus=ProjectStatus_e.ProdCompleted, StartDate=Convert.ToDateTime("2022-06-8") },
                new ProjectStatusRecordDto() { ProjectStatus=ProjectStatus_e.ProjCompleted, StartDate=Convert.ToDateTime("2022-06-10") }
            },
            ProductType=ProductType_e.烟罩,
            CustomerType=CustomerType_e.国内,
            RiskLevel=3,
            HoodQuantity=2,
            ItemLine=1,
            SemiProductStatus="OK",
            PurchaseStatus="OK",
            ModelSummary="KVI(2),UVF(6),KWF(12)",
            StdWorkload=2,
            VtaValue= 19734.51m,
            InvoiceMonth=Convert.ToDateTime("2022-06"),
            Customer=new CustomerDto { CustomerName="南京天邑商贸有限公司" },
            ProjectSpecialRequire="二层和三层(C和D开头)烟罩的所有安素内置箱及安素主副箱不用组装在一起，安装螺栓及螺母配好放在内置箱里即可",
            CriticalPath=new List<ActivityDto>
            {
                new ActivityDto()
                {
                    Order=1,
                    ActivityName=Activity_e.Drawing,
                    Start=Convert.ToDateTime("2022-5-20"),
                    End=Convert.ToDateTime("2022-5-24"),
                },
                new ActivityDto()
                {
                    Order=2,
                    ActivityName=Activity_e.Nesting,
                    Start=Convert.ToDateTime("2022-5-24"),
                    End=Convert.ToDateTime("2022-5-25"),
                },
                new ActivityDto()
                {
                    Order=3,
                    ActivityName=Activity_e.Cutting,
                    Start=Convert.ToDateTime("2022-5-25"),
                    End=Convert.ToDateTime("2022-5-26"),
                },
                 new ActivityDto()
                {
                     Order=4,
                    ActivityName=Activity_e.Bending,
                    Start=Convert.ToDateTime("2022-5-26"),
                    End=Convert.ToDateTime("2022-5-27"),
                },
                 new ActivityDto()
                {
                     Order=5,
                    ActivityName=Activity_e.Welding,
                    Start=Convert.ToDateTime("2022-5-27"),
                    End=Convert.ToDateTime("2022-5-29"),
                }                 ,
                 new ActivityDto()
                {
                     Order=7,
                    ActivityName=Activity_e.Packing,
                    Start=Convert.ToDateTime("2022-5-29"),
                    End=Convert.ToDateTime("2022-6-10"),
                },
                 new ActivityDto()
                {
                     Order=6,
                    ActivityName=Activity_e.Assembly,
                    Start=Convert.ToDateTime("2022-5-30"),
                    End=Convert.ToDateTime("2022-6-5"),
                }
            }
        });
        ProjectList.Add(new ProjectDto()
        {
            OdpNumber="FSO220072",
            ProjectName="2F Line Friends Cafe Macau",
            OdpReleaseDate=Convert.ToDateTime("2022-03-18"),
            ProdFinishDate=Convert.ToDateTime("2022-06-30"),
            ProjectStatusRecords=new List<ProjectStatusRecordDto>
            {
                new ProjectStatusRecordDto(){ProjectStatus=ProjectStatus_e.DrwReleased,StartDate=Convert.ToDateTime("2022-05-24")},
                new ProjectStatusRecordDto(){ProjectStatus=ProjectStatus_e.GettingOdp,StartDate=Convert.ToDateTime("2022-05-20")},
                new ProjectStatusRecordDto(){ProjectStatus=ProjectStatus_e.Abnormal,StartDate=Convert.ToDateTime("2022-05-30")},
            },
            ProductType=ProductType_e.天花,
            CustomerType=CustomerType_e.港澳台,
            RiskLevel=2,
            HoodQuantity=2,
            ItemLine=1,
            SemiProductStatus="OK",
            PurchaseStatus="OK",
            ModelSummary="KCW(1)",
            StdWorkload=26,
            Electricity=Electricity_e.AC230V50Hz,
            MarvelOption=MarvelOption_e.Yes,
            AnsulPrePipe=AnsulPrePipe_e.Piranha,
            AnsulSystem=AnsulSystem_e.Piranha,
            VtaValue=  1346658.85m,
            InvoiceMonth=Convert.ToDateTime("2022-06"),
            Customer=new CustomerDto { CustomerName="Halton Co.LTD" },
            ProjectSpecialRequire="二层和三层(C和D开头)烟罩的所有安素内置箱及安素主副箱不用组装在一起，安装螺栓及螺母配好放在内置箱里即可",
            CriticalPath= new List<ActivityDto>
            {
                new ActivityDto()
                {
                    Order=1,
                    ActivityName=Activity_e.Drawing,
                    Start=Convert.ToDateTime("2022-3-20"),
                    End=Convert.ToDateTime("2022-3-24"),
                },
                new ActivityDto()
                {
                    Order=2,
                    ActivityName=Activity_e.Nesting,
                    Start=Convert.ToDateTime("2022-3-24"),
                    End=Convert.ToDateTime("2022-3-24"),
                },
                new ActivityDto()
                {
                    Order=3,
                    ActivityName=Activity_e.Cutting,
                    Start=Convert.ToDateTime("2022-3-25"),
                    End=Convert.ToDateTime("2022-6-15"),
                },
                 new ActivityDto()
                {
                     Order=4,
                    ActivityName=Activity_e.Bending,
                    Start=Convert.ToDateTime("2022-6-16"),
                    End=Convert.ToDateTime("2022-6-16"),
                },
                 new ActivityDto()
                {
                     Order=5,
                    ActivityName=Activity_e.Collect,
                    Start=Convert.ToDateTime("2022-6-17"),
                    End=Convert.ToDateTime("2022-6-17"),
                },
                 new ActivityDto()
                {
                     Order=6,
                    ActivityName=Activity_e.Welding,
                    Start=Convert.ToDateTime("2022-6-18"),
                    End=Convert.ToDateTime("2022-6-18"),
                },
                 new ActivityDto()
                {
                     Order=7,
                    ActivityName=Activity_e.Polishing,
                    Start=Convert.ToDateTime("2022-6-18"),
                    End=Convert.ToDateTime("2022-6-18"),
                } ,
                 new ActivityDto()
                {
                     Order=8,
                    ActivityName=Activity_e.Assembly,
                    Start=Convert.ToDateTime("2022-6-18"),
                    End=Convert.ToDateTime("2022-6-27"),
                },
                 new ActivityDto()
                {
                     Order=9,
                    ActivityName=Activity_e.Electrical,
                    Start=Convert.ToDateTime("2022-6-26"),
                    End=Convert.ToDateTime("2022-6-26"),
                },
                 new ActivityDto()
                {
                     Order=10,
                    ActivityName=Activity_e.Piping,
                    Start=Convert.ToDateTime("2022-6-20"),
                    End=Convert.ToDateTime("2022-6-25"),
                },
                 new ActivityDto()
                {
                     Order=11,
                    ActivityName=Activity_e.Ansul,
                    Start=Convert.ToDateTime("2022-6-25"),
                    End=Convert.ToDateTime("2022-6-26"),
                },
                 new ActivityDto()
                {
                     Order=12,
                    ActivityName=Activity_e.Quality,
                    Start=Convert.ToDateTime("2022-6-27"),
                    End=Convert.ToDateTime("2022-6-27"),
                },
                 new ActivityDto()
                {
                     Order=13,
                    ActivityName=Activity_e.Packing,
                    Start=Convert.ToDateTime("2022-6-28"),
                    End=Convert.ToDateTime("2022-6-28"),
                }
            }
        }); 
    }

    public List<ProjectDto> GetProjectsBySearch(string search)
    {
        //先排好序
        return ProjectList.Where(x => x.ProjectName.Contains(search)).OrderByDescending(x=>x.ProdFinishDate).ToList();
    }
}
