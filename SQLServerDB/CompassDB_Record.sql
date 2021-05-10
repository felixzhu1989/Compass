use CompassDB
go
insert into UserGroups(GroupName) values('admin')
select * from UserGroups
select * from Users
insert into Users(UserGroupId,UserAccount,UserPwd) values(1,'admin','admin')

select * from ProjectStatus
insert into ProjectStatus(ProjectStatusName,StatusDesc) 
values('GettingODP','收到ODP'),
('KickOff','开工会议'),
('DrawingMaking','制图中'),
('InProduction','生产中'),
('ProductionCompleted','生产完成'),
('ProjectCompleted','项目完成'),
('FollowUp','跟踪'),
('Pending','未决定'),
('Cancel','取消'),
('Import','引进')

--日本项目不需要的
use CompassDB
go
--
--ID开头数字规则:0电/1灯/2风机/3型材/4油网/5螺丝/6控制/7水洗ANSUL/8折弯件/9焊接件
--分类号规则：0日本不要配件，1日本特有配件,2适用于所有项目的配件，3自制折弯件，4自制焊接件（打标签）
insert into CeilingAccessories(CeilingAccessoryId,ClassNo,PartDescription,PartNo,Remark,CountingRule) 
values('0001',0,'2孔接线盒(Junction box)','配件(assembly parts)','2900100010','')
update CeilingAccessories set CountingRule='一条灯+1' where CeilingAccessoryId='0001'

insert into CeilingAccessories(CeilingAccessoryId,ClassNo,PartDescription,PartNo,Remark,CountingRule) 
values('0002',0,'3孔接线盒(Junction box)','配件(assembly parts)','2900100011','一项目+1')


select * from CeilingAccessories
go






select * from ProjectTracking




select * from DrawingPlan

select * from ProjectVaults order by VaultName desc 

select * from Projects

select * from GeneralRequirements


select * from SpecialRequirements

select * from Categories
select * from Projects
select * from DrawingPlan
select * from Categories
select * from ModuleTree
select * from UVI555
select * from KVI555
select * from UVF555
select * from KVF555
select * from UWF555
select * from UWI555
select * from KWF555
select * from KWI555



select * from KCJSB265
select * from KCJSB290
select * from KCJSB535
select * from KCJDB800
select * from UCJSB385
select * from UCJSB535
select * from UCJDB800
select * from KCWSB265
select * from KCWSB535
select * from KCWDB800
select * from UCWSB535



delete from UVI555 where ModuleTreeId=16
delete from ModuleTree where ModuleTreeId=16


select * from DXFCutList
select * from CeilingCutList
select * from SubAssy



select distinct ProjectTracking.ProjectId,ProjectTrackingId,ODPNo,ProjectTracking.ProjectStatusId,ProjectStatusName,DrReleaseTarget,DrReleaseActual,ShippingTime,ProdFinishActual,DeliverActual,ProjectName,KickOffStatus,UserAccount from ProjectTracking inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId inner join Users on Projects.UserId=Users.UserId left join (select ProjectId,max(DrReleaseTarget)as DrReleaseTarget from DrawingPlan group by ProjectId) as PlanList on PlanList.ProjectId=Projects.ProjectId order by ShippingTime desc

--更改天花烟罩cutlist零件数量
select * from CeilingCutList where SubAssyId=308

update CeilingCutList set Quantity=2 where CutListId=11719
update CeilingCutList set Quantity=4 where CutListId=11724
update CeilingCutList set Quantity=2 where CutListId=11734
update CeilingCutList set Quantity=6 where CutListId=11730

--更改天花烟罩PackingList零件材质 Z铝灯板(Type Z Passive panel)
select * from CeilingPackingList where ProjectId=167
update CeilingPackingList set PartDescription='W灯板(Type W Passive panel)' where ProjectId=167 and PartDescription='W铝灯板(Type W Passive panel)'
update CeilingPackingList set PartDescription='Z灯板(Type Z Passive panel)' where ProjectId=167 and PartDescription='Z铝灯板(Type Z Passive panel)'
update CeilingPackingList set Material='SUS304' where ProjectId=167 and Material='AL'


select * from UVI450300
SELECT * from kvi450300

--更改天花烟罩发货清单区域
select * from CeilingPackingList where ProjectId=215
update CeilingPackingList set Location='Kitchen-A' where ProjectId=209