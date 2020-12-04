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
select * from ProjectVaults
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

