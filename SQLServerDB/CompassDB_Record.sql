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

--�����컨����cutlist�������
select * from CeilingCutList where SubAssyId=308

update CeilingCutList set Quantity=2 where CutListId=11719
update CeilingCutList set Quantity=4 where CutListId=11724
update CeilingCutList set Quantity=2 where CutListId=11734
update CeilingCutList set Quantity=6 where CutListId=11730



--����ģ�ͷ����
select * from Categories
select * from ModuleTree

update ModuleTree set CategoryId=1134  where CategoryId=1110
update Categories set CategoryId=1128 where CategoryId=1131
delete Categories where CategoryId=1110

insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values
		(1109,1100,'KVI450','KVI�߶�450','KVI','450','D:\halton\01 Tech Dept\05 Products Library\02 Hood\KVI450.SLDASM'),
		(1110,1100,'KVF450','KVF�߶�450','KVF','450','D:\halton\01 Tech Dept\05 Products Library\02 Hood\KVF450.SLDASM'),
		(1111,1100,'UVI450','UVI�߶�450','UVI','450','D:\halton\01 Tech Dept\05 Products Library\02 Hood\UVI450.SLDASM'),
		(1112,1100,'UVF450','UVF�߶�450','UVF','450','D:\halton\01 Tech Dept\05 Products Library\02 Hood\UVF450.SLDASM')





insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values(1133,1100,'KVIMR555','KVIMR�����·�M������','KVIM','R555','D:\halton\01 Tech Dept\05 Products Library\02 Hood\KVIMR555.SLDASM'),
	(1134,1100,'UVIMR555','UVIMR�����·�M������','UVIM','R555','D:\halton\01 Tech Dept\05 Products Library\02 Hood\UVIMR555.SLDASM')





insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values(1137,1100,'KVIR555','KVԲ�����ָ߶�555','KVIR','555','D:\halton\01 Tech Dept\05 Products Library\02 Hood\KVIR555.SLDASM')

insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values(1138,1100,'UVIR555','UVԲ�����ָ߶�555','UVIR','555','D:\halton\01 Tech Dept\05 Products Library\02 Hood\UVIR555.SLDASM')

insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values(1112,1100,'UVF450','UVF�߶�450','UVF','450','D:\halton\01 Tech Dept\05 Products Library\02 Hood\UVF450.SLDASM')


insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values(1149,1100,'CMODI555','CMODˮ�����ָ߶�555�·�I��','CMOD','I555','D:\halton\01 Tech Dept\05 Products Library\02 Hood\CMODI555.SLDASM')

insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values(1150,1100,'CMODF555','CMODˮ�����ָ߶�555�·�F��','CMOD','F555','D:\halton\01 Tech Dept\05 Products Library\02 Hood\CMODF555.SLDASM')


insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values(1142,1100,'HWUWF650','��ΪUWF�߶�650','UWF','650','')
insert into Categories (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath)
	values(1143,1100,'HWUVF555','��ΪUVF�߶�555','UVF','555',''),
		(1144,1100,'HWUWF555','��ΪUWF�߶�555','UWF','555','')


select * from users
insert into Users(UserGroupId,UserAccount,UserPwd,Email)
	values(4,'michael','123','michael.zhang@halton.com'),
	(4,'simon','123','simon.sang@halton.com'),


--�����컨����PackingList������� Z���ư�(Type Z Passive panel)
select * from CeilingPackingList where ProjectId=167
update CeilingPackingList set PartDescription='W�ư�(Type W Passive panel)' where ProjectId=167 and PartDescription='W���ư�(Type W Passive panel)'
update CeilingPackingList set PartDescription='Z�ư�(Type Z Passive panel)' where ProjectId=167 and PartDescription='Z���ư�(Type Z Passive panel)'
update CeilingPackingList set Material='SUS304' where ProjectId=167 and Material='AL'


select * from UVI450300
SELECT * from kvi450300

--�����컨���ַ����嵥����
select * from CeilingPackingList where ProjectId=215
update CeilingPackingList set Location='Kitchen-A' where ProjectId=209