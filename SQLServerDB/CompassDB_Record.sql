use CompassDB
go
insert into UserGroups(GroupName) values('admin')
select * from UserGroups
select * from Users
insert into Users(UserGroupId,UserAccount,UserPwd) values(1,'admin','admin')

select * from ProjectStatus
insert into ProjectStatus(ProjectStatusName,StatusDesc) 
values('GettingODP','�յ�ODP'),
('KickOff','��������'),
('DrawingMaking','��ͼ��'),
('InProduction','������'),
('ProductionCompleted','�������'),
('ProjectCompleted','��Ŀ���'),
('FollowUp','����'),
('Pending','δ����'),
('Cancel','ȡ��'),
('Import','����')

--�ձ���Ŀ����Ҫ��
use CompassDB
go
--
--ID��ͷ���ֹ���:0��/1��/2���/3�Ͳ�/4����/5��˿/6����/7ˮϴANSUL/8�����/9���Ӽ�
--����Ź���0�ձ���Ҫ�����1�ձ��������,2������������Ŀ�������3�����������4���ƺ��Ӽ������ǩ��
insert into CeilingAccessories(CeilingAccessoryId,ClassNo,PartDescription,PartNo,Remark,CountingRule) 
values('0001',0,'2�׽��ߺ�(Junction box)','���(assembly parts)','2900100010','')
update CeilingAccessories set CountingRule='һ����+1' where CeilingAccessoryId='0001'

insert into CeilingAccessories(CeilingAccessoryId,ClassNo,PartDescription,PartNo,Remark,CountingRule) 
values('0002',0,'3�׽��ߺ�(Junction box)','���(assembly parts)','2900100011','һ��Ŀ+1')


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



