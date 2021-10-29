use CompassDB
GO
select * from UserGroups
select * from Users
select * from Categories
select * from DesignWorkload
select * from Projects
select * from DrawingPlan
select * from ProjectTracking
select * from ProjectStatus
select * from Projects
select * from ProjectTypes
select * from GeneralRequirements
select * from SpecialRequirements
select * from CheckComments
select * from QualityFeedbacks
select * from AfterSaleFeedbacks
select * from ProjectLearneds
select * from ModuleTree
select * from UVI555
go
select * from CeilingCutList order by cutlistid desc

select distinct ProjectTracking.ProjectId,ProjectTrackingId,ODPNo,ProjectTracking.ProjectStatusId,ProjectStatusName,DrReleaseTarget,DrReleaseActual,ProdFinishTarget,ProdFinishActual,ShippingTime,DeliverActual from ProjectTracking 
inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId 
inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId 
left join (select ProjectId,max(DrReleaseTarget)as DrReleaseTarget from DrawingPlan group by ProjectId) as PlanList on PlanList.ProjectId=Projects.ProjectId 
order by ShippingTime desc

select DrawingPlanId,UserAccount,DrawingPlan.ProjectId,ODPNo,Item,Model,ModuleNo,DrawingPlan.DrReleaseTarget,DrReleaseActual,SubTotalWorkload,DrawingPlan.AddedDate from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
inner join Users on Users.UserId=Projects.UserId
left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId 
order by DrawingPlan.DrReleasetarget desc

select DrawingPlanId,UserAccount,DrawingPlan.ProjectId,ODPNo,Item,Model,ModuleNo,DrReleaseTarget,SubTotalWorkload,DrawingPlan.AddedDate from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
inner join Users on Users.UserId=Projects.UserId 
where DrawingPlanId = 14

select DrawingPlanId,ODPNo,Item,LabelImage from DrawingPlan
inner join Projects on Projects.ProjectId=DrawingPlan.ProjectId

select DrawingPlanId,DrawingPlan.ProjectId,ODPNo,Item,LabelImage from DrawingPlan 
inner join Projects on Projects.ProjectId=DrawingPlan.ProjectId 
where DrawingPlan.ProjectId = 18

select * from ModuleTree

select UVI555Id,ModuleTreeId,Length,Deepth,ExRightDis,ExNo,ExLength,ExWidth,ExHeight,SidePanel,Outlet,LEDlogo,Bluetooth,BackToBack,WaterCollection,LEDSpotNo,LEDSpotDis,LightType,UVType,ANSUL,ANSide,ANDetector,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5,MARVEL,IRNo,IRDis1,IRDis2,IRDis3 from UVI555 
where ModuleTreeId=21

select GeneralRequirementId,GeneralRequirements.ProjectId,ODPNo,ProjectTypes.TypeId,TypeName,InputPower,MARVEL,ANSULPrePipe,ANSULSystem,RiskLevel from GeneralRequirements 
inner join Projects on Projects.ProjectId=GeneralRequirements.ProjectId 
inner join ProjectTypes on ProjectTypes.TypeId=GeneralRequirements.TypeId 
where ODPNo='FSO200228'

select Item,Module,Length,Deepth,ExRightDis,ExNo,ExLength,ExWidth,ExHeight,SidePanel,Outlet,LEDlogo,Bluetooth,BackToBack,WaterCollection,LEDSpotNo,LEDSpotDis,LightType,UVType,ANSUL,ANSide,ANDetector,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5,MARVEL,IRNo,IRDis1,IRDis2,IRDis3 from UVI555 
inner join ModuleTree on UVI555.ModuleTreeId=ModuleTree.ModuleTreeId
inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId
where ProjectId=8 
order by Item,Module

select Projects.ProjectId,ODPNo,BPONo,Projects.VaultId,VaultName,ProjectName,Projects.CustomerId,CustomerName,ShippingTime,Projects.UserId,UserAccount,Risklevel from Projects 
inner join ProjectVaults on Projects.VaultId=ProjectVaults.VaultId 
inner join Users on Projects.UserId=Users.UserId 
inner join Customers on Projects.CustomerId=Customers.CustomerId 
inner join GeneralRequirements on Projects.ProjectId=GeneralRequirements.ProjectId 
where ODPNo='FSO200314'

select DrawingPlanId,UserAccount,DrawingPlan.ProjectId,ODPNo,Item,Model,ModuleNo,DrawingPlan.DrReleaseTarget,DrReleaseActual,SubTotalWorkload,DrawingPlan.AddedDate from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
inner join Users on Users.UserId=Projects.UserId 
left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId 
where Projects.UserId=1 
order by DrawingPlan.DrReleasetarget desc

select ODPNo,BPONo,ProjectName,CustomerName,Item,Module,Model,ShippingTime,Length,Deepth,SidePanel,LabelImage from ModuleTree
inner join DrawingPlan on DrawingPlan.DrawingPlanId=ModuleTree.DrawingPlanId
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
inner join Customers on Projects.CustomerId=Customers.CustomerId
inner Join UVI555 on UVI555.ModuleTreeId=ModuleTree.ModuleTreeId
where ModuleTree.ModuleTreeId=43



select * from DrawingPlan
select ( select count(*) from DrawingPlan where ProjectId=8)as 'Id=8数量',
		( select count(*) from DrawingPlan where ProjectId=18)as 'Id=18数量'

select * from HoodCutList
delete from HoodCutList

--查询Item的数量
select count(*) from DrawingPlan
where ProjectId=8
--查询订单中烟罩台数
select count(*) from ModuleTree
inner join DrawingPlan on DrawingPlan.DrawingPlanId=ModuleTree.DrawingPlanId
where ProjectId=8

select CutListId,ModuleTreeId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo,AddedDate,HoodCutList.UserId,UserAccount from HoodCutList 
inner join Users on Users.UserId=HoodCutList.UserId 
where ModuleTreeId = '42' 
order by Thickness desc,Materials desc,Length desc,PartNo asc

delete from HoodCutList where CutListId=94

select CutListId,SubAssyId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo,AddedDate,CeilingCutList.UserId,UserAccount from CeilingCutList inner join Users on Users.UserId=CeilingCutList.UserId where SubAssyId = '24' order by Thickness desc,Materials desc,Length desc,PartNo asc

select * from GeneralRequirements
select * from ProjectTypes
select * from CeilingPackingList

select PartDescription,Quantity,PartNo,Unit,Length,Width,Height,Material,Remark,CountingRule,AddedDate,CeilingPackingList.UserId,UserAccount,ProjectId,CeilingAccessoryId,ClassNo,CeilingPackingListId,Location from CeilingPackingList inner join Users on Users.UserId=CeilingPackingList.UserId where CeilingPackingListId=2629

select * from UVIR555 


delete from Projects where ProjectId=57
select * from Projects  order by ShippingTime desc
select * from SpecialRequirements
delete from SpecialRequirements where ProjectId=104

select CutListId,SubAssyId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo,AddedDate,CeilingCutList.UserId,UserAccount from CeilingCutList inner join Users on Users.UserId=CeilingCutList.UserId where SubAssyId = '228' order by Thickness desc,Materials desc,PartNo asc








select Top 30 Projects.ProjectId,ODPNo,BPONo,ProjectName,CustomerName,ShippingTime,UserAccount,RiskLevel,ProjectStatusName,HoodType from Projects
  inner join Users on Projects.UserId=Users.UserId
   inner join Customers on Projects.CustomerId=Customers.CustomerId
    inner join ProjectTracking on Projects.ProjectId=ProjectTracking.ProjectId
	 inner join ProjectStatus on ProjectTracking.ProjectStatusId=ProjectStatus.ProjectStatusId
	  left join GeneralRequirements on Projects.ProjectId=GeneralRequirements.ProjectId
	   where ShippingTime>='2020/01/01' and ShippingTime<='2020/12/31' and Projects.ProjectId not in 
	   (select Top 0 Projects.ProjectId from Projects where ShippingTime>='2020/01/01' and ShippingTime<='2020/12/31' order by ShippingTime desc) 
	   order by ShippingTime desc;
select count(*) from Projects where ShippingTime>='2020/01/01' and ShippingTime<='2020/12/31'

select * from Projects
select *　from　Users
select SubTotalWorkload from DrawingPlan
select * from DrawingPlan order by DrReleaseTarget desc

select sum(SubTotalWorkload) from DrawingPlan where DrReleaseTarget>='2020/01/01' and DrReleaseTarget<='2020/12/31'

select sum(SubTotalWorkload) from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
inner join Users on Projects.UserId=Users.UserId
where DrReleaseTarget>='2020/01/01' and DrReleaseTarget<='2020/12/31'
 and UserAccount='hujian'

 
select sum(ModuleNo) from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
inner join Users on Projects.UserId=Users.UserId
where DrReleaseTarget>='2020/01/01' and DrReleaseTarget<='2020/12/31'
and HoodType='Hood'

 select DrawingPlan.DrReleaseTarget,DrReleaseActual,DrawingPlan.AddedDate,ProjectName,
 IIF(DATEDIFF(DAY,GETDATE(),DrawingPlan.DrReleaseTarget)<0,0,DATEDIFF(DAY,GETDATE(),DrawingPlan.DrReleaseTarget))as RemainingDays,
 IIF(DATEDIFF(DAY,GETDATE(),DrawingPlan.DrReleaseTarget)<0,100,100*DATEDIFF(DAY,GETDATE(),DrawingPlan.AddedDate)/DATEDIFF(DAY,DrawingPlan.DrReleaseTarget,DrawingPlan.AddedDate)) as ProgressValue
 
  from DrawingPlan
  inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
  inner join Users on Users.UserId=Projects.UserId
  left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId
  order by DrawingPlan.DrReleasetarget desc




--ProgressValue = Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(Convert.ToDateTime(objReader["AddedDate"])).Days<=0 ? 100:
--(int)(100 * (Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(Convert.ToDateTime(objReader["AddedDate"])).Days
--- Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(DateTime.Today).Days)
--/ (Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(Convert.ToDateTime(objReader["AddedDate"])).Days))


select Top 10000 UserAccount,ODPNo,Model,ModuleNo,DrawingPlan.DrReleaseTarget,
DrReleaseActual,SubTotalWorkload,HoodType from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
inner join Users on Users.UserId=Projects.UserId 
left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId 
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
and DrawingPlanId not in 
(select Top 0 DrawingPlanId from DrawingPlan 
where DrawingPlan.DrReleasetarget>='2020/01/01' 
and DrawingPlan.DrReleasetarget<='2020/12/31' 
order by DrawingPlan.DrReleasetarget desc) 
order by DrawingPlan.DrReleasetarget desc;
select count(*) from DrawingPlan inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31'




select * from DrawingPlan
order by DrawingPlan.DrReleasetarget desc

select Top 30 DrawingPlanId,UserAccount,ODPNo,Item,Model,ModuleNo,
DrawingPlan.DrReleaseTarget,DrReleaseActual,SubTotalWorkload,ProjectName,HoodType,
IIF(DATEDIFF(DAY,GETDATE(),DrawingPlan.DrReleaseTarget)<0,0,DATEDIFF(DAY,GETDATE(),DrawingPlan.DrReleaseTarget)) 
as RemainingDays,
IIF(DATEDIFF(DAY,GETDATE(),DrawingPlan.DrReleaseTarget)<=0,100,100*DATEDIFF(DAY,GETDATE(),DrawingPlan.AddedDate)/DATEDIFF(DAY,DrawingPlan.DrReleaseTarget,DrawingPlan.AddedDate)) as ProgressValue 
from DrawingPlan inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
inner join Users on Users.UserId=Projects.UserId 
left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId 
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' and DrawingPlanId 
not in (select Top 0 DrawingPlanId from DrawingPlan where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
order by DrawingPlan.DrReleasetarget desc) order by DrawingPlan.DrReleasetarget desc;
select count(*) from DrawingPlan inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31'


--统计年度机型数量
select distinct Model from DrawingPlan 

select Model,sum(SubTotalWorkload) from DrawingPlan

select Model,sum(ModuleNo) as TotalModuleNo from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
and HoodType='Hood'
group by Model
order by TotalModuleNo desc

--按月份统计数量
select month(DrawingPlan.DrReleasetarget) as Mon,sum(ModuleNo) as TotalModuleNo from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
and HoodType='Hood' and Model='UVF'
group by month(DrawingPlan.DrReleasetarget)
order by Mon asc



--统计年度天花机型工作量
select distinct Model from DrawingPlan 

select Model,sum(SubTotalWorkload) from DrawingPlan

select Model,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
and HoodType='Ceiling'
group by Model
order by TotalWorkload desc

--按月份统计单个机型天花工作量
select month(DrawingPlan.DrReleasetarget) as Mon,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
and HoodType='Ceiling' and Model='UCJ'
group by month(DrawingPlan.DrReleasetarget)
order by Mon asc

--统计年度天花机型工作量
select month(DrawingPlan.DrReleasetarget) as Mon,
sum(ModuleNo) as TotalModuleNo from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
and HoodType='Hood' and Model='UCW' 
group by month(DrawingPlan.DrReleasetarget) 
order by Mon asc



--统计年度人员工作量
select UserAccount,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
inner join Users on Users.UserId=Projects.UserId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
group by UserAccount
order by TotalWorkload desc

--按月份统计所有工作量
select month(DrawingPlan.DrReleasetarget) as Mon,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
group by month(DrawingPlan.DrReleasetarget)
order by Mon asc

--按月份统人员工作量
select month(DrawingPlan.DrReleasetarget) as Mon,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
inner join Users on Users.UserId=Projects.UserId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
and UserAccount='eric'
group by month(DrawingPlan.DrReleasetarget)
order by Mon asc

select * from ProjectTracking
--IIF(DATEDIFF(DAY,GETDATE(),DrawingPlan.DrReleaseTarget)<0,0,DATEDIFF(DAY,GETDATE(),DrawingPlan.DrReleaseTarget)) as RemainingDays
--按月份统人员delay/工作量
--/sum(SubTotalWorkload)
use CompassDB
go
select * from view_DelayQuery

select month(Drtarget) as Mon,
sum(DATEDIFF(DAY,Drtarget,DrActual)) as TotalDelay from view_DelayQuery
inner join Projects on view_DelayQuery.ProjectId=Projects.ProjectId
inner join Users on Users.UserId=Projects.UserId
where Drtarget>='2020/01/01' and Drtarget<='2020/12/31' 
and UserAccount='eric'
and DATEDIFF(DAY,Drtarget,DrActual)>0
group by month(Drtarget)
order by Mon asc


--按全年查询
select sum(DATEDIFF(DAY,Drtarget,DrActual)) as TotalDelay from view_DelayQuery
inner join Projects on view_DelayQuery.ProjectId=Projects.ProjectId
inner join Users on Users.UserId=Projects.UserId
where Drtarget>='2020/01/01' and Drtarget<='2020/12/31' 
and UserAccount='eric'
and DATEDIFF(DAY,Drtarget,DrActual)>0





--left join (select ProjectId,max(DrReleaseTarget)as DrReleaseTarget from DrawingPlan group by ProjectId) as PlanList on PlanList.ProjectId=Projects.ProjectId

select ODPNo,max(DrawingPlan.DrReleasetarget) as Drtarget,sum(DATEDIFF(DAY,DrawingPlan.DrReleaseTarget,DrReleaseActual)) as DelayDays
from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
inner join Users on Users.UserId=Projects.UserId
left join (select ProjectId,max(DrReleaseActual)as DrReleaseActual from ProjectTracking group by ProjectId) as PlanList on DrawingPlan.ProjectId=PlanList.ProjectId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
and UserAccount='eric'
and DATEDIFF(DAY,DrawingPlan.DrReleaseTarget,DrReleaseActual)>0
group by ODPNo
order by Drtarget desc

select ODPNo,max(DrReleasetarget) as Drtarget,max(DrReleaseActual) as DrActual from DrawingPlan
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
inner join Users on Users.UserId=Projects.UserId
left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId
where DrawingPlan.DrReleasetarget>='2020/01/01' and DrawingPlan.DrReleasetarget<='2020/12/31' 
 and UserAccount='eric'
 and DATEDIFF(DAY,DrReleaseTarget,DrReleaseActual)>0
group by ODPNo






--将查询delay定义为视图
use CompassDB
go
if exists(select * from sysobjects where name='view_DelayQuery')
drop procedure view_DelayQuery
go

create view view_DelayQuery
as
	select DrawingPlan.ProjectId,max(DrReleasetarget) as Drtarget,max(DrReleaseActual) as DrActual from DrawingPlan
	inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
	left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId
	group by DrawingPlan.ProjectId
go

--查询视图
select * from view_DelayQuery

--将查询delay定义为存储过程
use CompassDB
go
if exists(select * from sysobjects where name='usp_DelayQuery')
drop procedure usp_DelayQuery
go

create procedure usp_DelayQuery
 
@userAcc varchar(10), 
@StartDate datetime, 
@EndDate datetime

as
	select ODPNo,max(DrReleasetarget) as Drtarget,max(DrReleaseActual) as DrActual from DrawingPlan
	inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId
	inner join Users on Users.UserId=Projects.UserId
	left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId
	where DrawingPlan.DrReleasetarget>=@StartDate and DrawingPlan.DrReleasetarget<=@EndDate 
	 and UserAccount=@userAcc
	 and DATEDIFF(DAY,DrReleaseTarget,DrReleaseActual)>0
	group by ODPNo
go

--调用存储过程
exec usp_DelayQuery 'eric','2020/01/01','2020/12/31'



select month(Drtarget) as Mon,sum(DATEDIFF(DAY, Drtarget, DrActual))  as TotalDelay 
from view_DelayQuery 
inner join Projects on view_DelayQuery.ProjectId=Projects.ProjectId 
inner join Users on Users.UserId=Projects.UserId 
where Drtarget>='2020/01/01' and Drtarget<='2020/12/31' 
and UserAccount='eric' and DATEDIFF(DAY,Drtarget,DrActual)>0 
group by month(Drtarget) order by Mon asc


select sum(DATEDIFF(DAY, Drtarget, DrActual)) as TotalDelay from view_DelayQuery
where DATEDIFF(DAY,Drtarget,DrActual)>0


select sum(DATEDIFF(DAY, Drtarget, DrActual)) as TotalDelay from view_DelayQuery 
where Drtarget>='2021/01/01' and Drtarget<='2021/12/31' and DATEDIFF(DAY,Drtarget,DrActual)>0


select * from ProjectTracking

select Top 10000 ProjectsMarine.ProjectId,ODPNo,BPONo,ProjectName,CustomerName,ShippingTime,UserAccount,TypeName,RiskLevel,ProjectStatusName,HoodType from ProjectsMarine inner join Users on ProjectsMarine.UserId=Users.UserId inner join Customers on ProjectsMarine.CustomerId=Customers.CustomerId inner join ProjectTrackingMarine on ProjectsMarine.ProjectId=ProjectTrackingMarine.ProjectId inner join ProjectStatus on ProjectTrackingMarine.ProjectStatusId=ProjectStatus.ProjectStatusId left join GeneralRequirementsMarine on ProjectsMarine.ProjectId=GeneralRequirementsMarine.ProjectId left join ProjectTypesMarine on ProjectTypesMarine.TypeId=GeneralRequirementsMarine.TypeId where Projects.UserId = 2 and ShippingTime>='2021/01/01' and ShippingTime<='2021/12/31' and ProjectsMarine.ProjectId not in (select Top 0 ProjectsMarine.ProjectId from ProjectsMarine where Projects.UserId = 2 and ShippingTime>='2021/01/01' and ShippingTime<='2021/12/31' order by ShippingTime desc) order by ShippingTime desc;select count(*) from ProjectsMarine  where Projects.UserId = 2 and ShippingTime>='2021/01/01' and ShippingTime<='2021/12/31'
select * from ProjectsMarine

select * from Projects where ODPNo='FSO210343'
order by AddedDate desc

insert into Projects (ODPNo,BPONo,ProjectName,CustomerId,ShippingTime,UserId,HoodType) values('FSO210343','XX','xx',16,'2021/8/4 0:00:00',4,'Hood');select @@identity
insert into ProjectTracking (ProjectId,ProjectStatusId) values(@@IDENTITY,1)

select * from FinancialData where ProjectId=282
delete from Projects where ODPNo='FSO210343'



select month(DrawingPlan.DrReleasetarget) as Mon,
sum(SubTotalWorkload) as TotalWorkload from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
where DrawingPlan.DrReleasetarget>='2021/01/01' and DrawingPlan.DrReleasetarget<='2021/12/31' 
group by month(DrawingPlan.DrReleasetarget) order by Mon asc

select ODPNo,ProjectStatusName,
ODPReceiveDate,KickOffDate,
DrReleaseTarget,DrReleaseActual,
ShippingTime,ProdFinishActual,
DeliverActual
from ProjectTracking 
inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId 
inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId 
inner join Users on Projects.UserId=Users.UserId 
left join (select ProjectId,max(DrReleaseTarget)as DrReleaseTarget 
from DrawingPlan group by ProjectId) as PlanList on PlanList.ProjectId=Projects.ProjectId 
where ODPNo='FSO210343' 

select * from ProjectTracking

use compassdb
go
select * from ProjectStatus
--查询项目数量（月度）
select count(*) from Projects where ShippingTime like'2021%' and month(ShippingTime)='8'
--查询项目数量（年度）
select count(*) from Projects where ShippingTime like'2021%'

--统计项目状态数量（月度）
select ProjectStatusName,count(ProjectStatusName) from ProjectTracking
	inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId
	where ShippingTime like'2021%' and month(ShippingTime)='7'
	group by ProjectStatusName
--统计项目状态数量（年度）
select ProjectStatusName,count(ProjectStatusName) from ProjectTracking
	inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId
	where ShippingTime like'2021%'
	group by ProjectStatusName

select * from GeneralRequirements
--统计风险等级数量（月度）
select RiskLevel,count(RiskLevel) from GeneralRequirements	
	inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId
	where ShippingTime like'2021%' and month(ShippingTime)='8'
	group by RiskLevel
--统计风险等级数量（年度）
select RiskLevel,count(RiskLevel) from GeneralRequirements	
	inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId
	where ShippingTime like'2021%'
	group by RiskLevel

--统计项目类型数量（月度）
select TypeName,count(TypeName) from GeneralRequirements
	inner join ProjectTypes on ProjectTypes.TypeId=GeneralRequirements.TypeId
	inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId
	where ShippingTime like'2021%' and month(ShippingTime)='7'
	group by TypeName
--统计项目类型数量（年度）
select TypeName,count(TypeName) from GeneralRequirements
	inner join ProjectTypes on ProjectTypes.TypeId=GeneralRequirements.TypeId
	inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId
	where ShippingTime like'2021%' 
	group by TypeName

--按照金额计算
select * from FinancialData
--统计项目类型数量（月度）
select TypeName,sum(SalesValue) as SalesValue from GeneralRequirements
	inner join ProjectTypes on ProjectTypes.TypeId=GeneralRequirements.TypeId
	inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId
	inner join FinancialData on Projects.ProjectId=FinancialData.ProjectId
	where ShippingTime like'2021%' and month(ShippingTime)='7'
	group by TypeName

--统计项目类型数量（年度）
select TypeName,sum(SalesValue) as SalesValue from GeneralRequirements
	inner join ProjectTypes on ProjectTypes.TypeId=GeneralRequirements.TypeId
	inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId
	inner join FinancialData on Projects.ProjectId=FinancialData.ProjectId
	where ShippingTime like'2021%' 
	group by TypeName


select * from ProjectTypes

--统计销售额（月度）
select sum(SalesValue) from FinancialData
	inner join Projects on FinancialData.ProjectId=Projects.ProjectId
	where ShippingTime like'2021%' and month(ShippingTime)='7'

--统计销售额（年度）
select sum(SalesValue) from FinancialData
	inner join Projects on FinancialData.ProjectId=Projects.ProjectId
	where ShippingTime like'2021%'


select month(DrawingPlan.DrReleasetarget) as Mon,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
where ShippingTime like '2021%' 
group by month(DrawingPlan.DrReleasetarget) 
order by Mon asc

select month(ShippingTime) as Mon,SubTotalWorkload from DrawingPlan 
inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId 
where ShippingTime like '2021%' 

select sum(SalesValue) as TotalSalesValue from FinancialData 
inner join Projects on FinancialData.ProjectId=Projects.ProjectId 
where ShippingTime like '2020%'

select count(*) from Projects where ShippingTime like'2021%' and month(ShippingTime)='8'

select * from ProjectTracking
select * from ProjectStatus
select ODPNo from Projects
	inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId
	where ProjectStatusId between 1 and 4
	order by ShippingTime desc

select ODPNo from Projects
	where ShippingTime like'2021%' and month(ShippingTime) between 8 and 9



select * from ProjectTypes
select * from users
select * from ProjectTracking

--项目趋势
select day(ODPReceiveDate) as Day,sum(SubTotalWorkload) as Workload from DrawingPlan
	inner join ProjectTracking on ProjectTracking.ProjectId=DrawingPlan.ProjectId
	where ODPReceiveDate like '2021%' and month(ODPReceiveDate)='08'
	group by day(ODPReceiveDate) order by Day asc
select day(ProdFinishActual) as Day,sum(SubTotalWorkload) as Workload from DrawingPlan
	inner join ProjectTracking on ProjectTracking.ProjectId=DrawingPlan.ProjectId
	where ProdFinishActual like '2021%' and month(ProdFinishActual)='08'
	group by day(ProdFinishActual) order by Day asc
select day(DeliverActual) as Day,sum(SubTotalWorkload) as Workload from DrawingPlan
	inner join ProjectTracking on ProjectTracking.ProjectId=DrawingPlan.ProjectId
	where DeliverActual like '2021%' and month(DeliverActual)='08'
	group by day(DeliverActual)	order by Day asc

--项目及时完工,按月
select month(ShippingTime) as Mon, count(*) as OnTime from Projects
	inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId
	where ShippingTime like '2021%' and ProdFinishActual<=ShippingTime
	group by month(ShippingTime) order by Mon asc

select month(ShippingTime) as Mon, count(*) as Total from Projects
	where ShippingTime like '2021%'
	group by month(ShippingTime) order by Mon asc

select month(ShippingTime) as Mon, sum(SubTotalWorkload) as OnTime from Projects
	inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId
	inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId
	where ShippingTime like '2021%' and ProdFinishActual<=ShippingTime
	group by month(ShippingTime) order by Mon asc

select month(ShippingTime) as Mon, sum(SubTotalWorkload) as Total from Projects
	inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId
	where ShippingTime like '2021%'
	group by month(ShippingTime) order by Mon asc

select ODPNo from Projects
	inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId
	where ShippingTime like '2021%' and ProdFinishActual>ShippingTime
	order by ShippingTime desc
--项目及时完工,YTD
select count(*) as OnTime from Projects
	inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId
	where ShippingTime like '2021%' and ProdFinishActual<=ShippingTime	

select count(*) as Total from Projects
	where ShippingTime like '2021%'
	
select sum(SubTotalWorkload) as OnTime from Projects	
	inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId
	inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId
	where ShippingTime like '2021%' and ProdFinishActual<=ShippingTime

select sum(SubTotalWorkload) as Total from Projects
	inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId
	where ShippingTime like '2021%'


--项目周期
select month(ShippingTime) as Mon,sum(datediff(day,DrReleaseActual,ProdFinishActual))/count(*)+1 as CycleTime from ProjectTracking
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId	
	where ShippingTime like '2021%'	and DrReleaseActual<>'0001-01-01' and ProdFinishActual<>'0001-01-01'
	group by month(ShippingTime) order by Mon asc

select month(ShippingTime) as Mon,sum(datediff(day,ODPReceiveDate,ProdFinishActual))/count(*)+1 as CycleTime from ProjectTracking
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId	
	where ShippingTime like '2021%'	and ODPReceiveDate<>'0001-01-01' and ProdFinishActual<>'0001-01-01'
	group by month(ShippingTime) order by Mon asc

select month(ShippingTime) as Mon,sum(datediff(day,ODPReceiveDate,DeliverActual))/count(*)+1 as CycleTime from ProjectTracking
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId	
	where ShippingTime like '2021%'	and ODPReceiveDate<>'0001-01-01' and DeliverActual<>'0001-01-01'
	group by month(ShippingTime) order by Mon asc


select * from ProjectTracking
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId	
	where ShippingTime like '2021%' and month(ShippingTime)='09'
	and DrReleaseActual<>'0001-01-01' and ProdFinishActual<>'0001-01-01'

--项目周期，YTD
select sum(datediff(day,DrReleaseActual,ProdFinishActual))/count(*)+1 as CycleTime from ProjectTracking
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId	
	where ShippingTime like '2021%'	and DrReleaseActual<>'0001-01-01' and ProdFinishActual<>'0001-01-01'

select sum(datediff(day,ODPReceiveDate,ProdFinishActual))/count(*)+1 as CycleTime from ProjectTracking
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId	
	where ShippingTime like '2021%'	and ODPReceiveDate<>'0001-01-01' and ProdFinishActual<>'0001-01-01'

select sum(datediff(day,ODPReceiveDate,DeliverActual))/count(*)+1 as CycleTime from ProjectTracking
	inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId	
	where ShippingTime like '2021%'	and ODPReceiveDate<>'0001-01-01' and DeliverActual<>'0001-01-01'	


select * from Categories
use CompassDB
go
select * from DrawingNumMatrix order by DrawingNum asc


select Projects.ProjectId,ODPNo,BPONo,ProjectName,Projects.CustomerId,CustomerName,ShippingTime,Projects.UserId,UserAccount,RiskLevel,HoodType,SalesValue from Projects 
inner join Users on Projects.UserId=Users.UserId 
inner join Customers on Projects.CustomerId=Customers.CustomerId 
left join GeneralRequirements on Projects.ProjectId=GeneralRequirements.ProjectId 
left join FinancialData on Projects.ProjectId=FinancialData.ProjectId

select * from DrawingNumMatrix


select ProjectStatusId,count(ProjectStatusId) as Qty from ProjectTracking 
inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId 
where ShippingTime like'2021%' and month(ShippingTime) between 9 and 10  group by ProjectStatusId
order by ProjectStatusId asc

select * from ProjectStatus
select * from ProjectTypes


select ODPReceiveDate,sum(SubTotalWorkload) as Workload from DrawingPlan 
inner join ProjectTracking on ProjectTracking.ProjectId=DrawingPlan.ProjectId 
where ODPReceiveDate >='2021/9/15' and ODPReceiveDate <'2021/10/15' 
group by ODPReceiveDate order by ODPReceiveDate asc


select * from Users
select * from UserGroups order by usergroupid