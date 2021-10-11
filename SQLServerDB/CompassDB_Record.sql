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
	values(1151,1100,'CMODI555-2','CMODˮ�����ָ߶�555�·�I��','CMOD','I555','D:\halton\01 Tech Dept\05 Products Library\02 Hood\CMODI555.SLDASM')




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

--�����컨����PackingList�����������

update CeilingPackingList set PartDescription=replace(PartDescription,'228','230') 
	where ProjectId=209 and Remark in ('2200600015','2200600032')
select * from CeilingPackingList where ProjectId=215

--����
select * from CeilingAccessories order by CeilingAccessoryId asc
update CeilingAccessories set CeilingAccessoryId='9069'
	where CeilingAccessoryId='9'
update CeilingAccessories set PartDescription='M6��ͨ��Ƭ(M6 Washer)'
	where CeilingAccessoryId='5007'

update CeilingAccessories set CeilingAccessoryId='0005' where PartDescription like '�¿�%'

select * from GeneralRequirements
update GeneralRequirements set RiskLevel=3
	where RiskLevel=4
select * from GeneralRequirementsMarine
update GeneralRequirementsMarine set RiskLevel=3
	where RiskLevel=4


select * from ProjectTracking 
where ProjectTrackingId=229

select * from ProjectTrackingMarine

select * from GeneralRequirements
select * from ProjectTypes

use CompassDB
go
select * from Categories

select * from DrawingNumCodeRule order by ParentId,CodeId asc
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(00000,00000,'H','Halton')
--SBU
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(10000,00000,'F','FoodService'),
	(20000,00000,'M','Marine'),
	(30000,00000,'S','SBA')

--Product name : FoodService	
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(10100,10000,'S','Show Kitchens'),
	(10200,10000,'P','Air Purification'),
	(10300,10000,'L','Supply Air Unit'),
	(10400,10000,'C','Ventilated Ceiling'),
	(10500,10000,'H','Hood')

--sub assembly : Show Kitchens	
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(10101,10100,'C','Casing'),
	(10102,10100,'B','Bracket'),
	(10103,10100,'D','Duct'),
	(10104,10100,'O','Components')
--sub assembly : Air Purification
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(10201,10200,'F','Frame'),
	(10202,10200,'B','Bracket'),
	(10203,10200,'P','Side Plate'),
	(10204,10200,'O','Components')	

--sub assembly : Supply Air Unit
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(10301,10300,'C','Casing'),
	(10302,10300,'B','Bracket'),
	(10303,10300,'D','Diffuser'),
	(10304,10300,'O','Components')
--sub assembly : Ventilated Ceiling
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(10401,10400,'E','Exhaust Beam'),
	(10402,10400,'B','Bracket'),
	(10403,10400,'J','Capture Jet Beam'),
	(10404,10400,'S','Side Panel / Channel'),
	(10405,10400,'M','Middle Roof'),
	(10406,10400,'O','Components'),
	(10407,10400,'L','Light Box / Plate'),
	(10408,10400,'A','Air Supply Unit'),
	(10409,10400,'L','Ceil Plate-AL'),
	(10410,10400,'P','Profile')
--sub assembly : Hood
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(10501,10500,'E','Exhaust Beam'),
	(10502,10500,'B','Bracket'),
	(10503,10500,'A','Air Supply Beam'),
	(10504,10500,'S','Side Panel'),
	(10505,10500,'M','Middle Roof'),
	(10506,10500,'O','Components')	

--Product name : Marine
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(20100,20000,'G','Valve & Grill / Fancoils / Air Flow Unit'),
	(20200,20000,'B','Blast Valve / Galley Diffuser'),
	(20300,20000,'H','Galley Hood'),
	(20400,20000,'S','DSH'),
	(20500,20000,'T','Cabin Diffuser'),
	(20600,20000,'C','Cabin'),
	(20700,20000,'D','Damper')

--sub assembly : Valve & Grill / Fancoils / Air Flow Unit
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(20101,20100,'F','Frame'),
	(20102,20100,'B','Blade'),
	(20103,20100,'S','Support'),
	(20104,20100,'O','Components')

--sub assembly : Blast Valve / Galley Diffuser
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(20201,20200,'F','Frame'),
	(20202,20200,'B','Blade'),
	(20203,20200,'L','Linkage'),
	(20204,20200,'S','Support'),
	(20205,20200,'O','Components')

--sub assembly : Galley Hood
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(20301,20300,'E','Exhaust Beam'),
	(20302,20300,'B','Bracket'),
	(20303,20300,'A','Air Supply Beam'),
	(20304,20300,'S','Side Panel'),
	(20305,20300,'M','Middle Roof'),
	(20306,20300,'O','Components')

--sub assembly : DSH
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(20401,20400,'F','Frame'),
	(20402,20400,'D','Droplet Seperator'),
	(20403,20400,'S','Support'),
	(20404,20400,'O','Components')

--sub assembly : Cabin Diffuser	
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(20501,20500,'C','Casing'),
	(20502,20500,'O','Components')

--sub assembly : Cabin	
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(20601,20600,'C','Casing'),
	(20602,20600,'S','Support'),
	(20603,20600,'V','Valve'),
	(20604,20600,'I','Insulation'),
	(20605,20600,'O','Components')

--sub assembly : Damper	
insert into DrawingNumCodeRule(CodeId,ParentId,Code,CodeName) 
values(20701,20700,'F','Frame'),
	(20702,20700,'B','Blade'),
	(20703,20700,'L','Linkage'),
	(20704,20700,'S','Support'),
	(20705,20700,'O','Components')

select CodeId,ParentId,Code,CodeName from DrawingNumCodeRule order by ParentId,CodeId asc



select DrawingId,DrawingNum,DrawingDesc,DrawingType,Mark,DrawingNumMatrix.UserId,UserAccount,AddedDate,DrawingImage from DrawingNumMatrix
	inner join Users on Users.UserId=DrawingNumMatrix.UserId
	order by DrawingNum asc


select * from DrawingNumMatrix
select * from Users
select * from UserGroups

select * from DrawingPlan
select * from Categories


---ɾ��һ��������Ҫɾ����Щ�������
select * from projects where projectId=62
select * from GeneralRequirements where projectId=62
select * from FinancialData where projectId=62
select * from ProjectTracking where  projectId=62

delete from projects where projectId=62

select ProjectStatusId from ProjectTracking
select * from ProjectStatus

select * from drawingnummatrix where drawingid between 488 and 516
update drawingnummatrix set DrawingType='Standard' where drawingid between 488 and 516

select * from Users
update Users set UserGroupId=1 where UserAccount='jack'

select * from CMODI555
select * from ModuleTree where CategoryId=1149

select * from Categories where CategoryName='CMODI555'

select * from ModuleTree where CategoryId=1151
select * from ModuleTree where CategoryId=1149
update ModuleTree set CategoryId=1170 where CategoryId=1173

update Categories set CategoryId=1174 where CategoryName='KCD'