--������½�˻�
use master
go
exec sp_addlogin 'felix','123'
--ɾ����½�˻�/��֮���������ݿ��û�����ͬʱɾ��
exec sp_droplogin 'felix'
--�������ݿ��û�-���½�˻���Ӧ
use StudentManageDB
go
exec sp_grantdbaccess 'felix','felixuser01'
--ɾ�����ݿ��û�
exec sp_dropuser 'felixuser01'
--����Ȩ��/��ѯ/����/�޸�
grant select,insert,update on Students to felixuser01
grant create table to felixuser01
--�ջ�Ȩ��
revoke select,insert,update on Students to felixuser01
--�����ݿ��û������û���ɫ
exec sp_addrolemember 'db_owner','felixuser01'
--ɾ�����ݿ��ɫ
exec sp_droprolemember 'db_owner','felixuser01'

--ȫ�ֱ���
print @@version --�汾
print @@MAX_CONNECTIONS --�ɴ�����ͳͳ�����ӵ������
print @@ERROR --���һ��sql������Ĵ����
print @@IDENTITY --���һ�β����ʶֵ
print @@LANGUAGE --��ǰʹ�����Ե�����
print @@ROWCOUNT --����һ��sql���Ӱ�������
print @@SERVERNAME --����������
print @@TRANCOUNT --��ǰ�򿪵�������

select @@SERVERNAME as '����������'

--�ֲ�����
use StudentManageDB
go
declare @stuid int,@stuname varchar(20)
--��ѯ����������Ϣ
set @stuname='������'
select StudentId,StudentName,Gender,StudentIdNo from Students
where StudentName=@stuname
--��ѯ��������ѧ��
select @stuid=StudentId from Students 
where StudentName=@stuname
--��ѯ��������ѧ�����ڵ�ѧԱ
select StudentId,StudentName,Gender,StudentIdNo from Students
where StudentId=(@stuid+1) or StudentId=(@stuid-1)

--��ѯѧ�ŵ���100002ѧԱ������
use StudentManageDB
go
declare @birthday datetime,@days int,@age int
select @birthday=Birthday from Students where StudentId=100002
set @days=datediff(dayofyear,@birthday,getdate())--�����������ڲ�
set @age=floor(@days/365)--����һ��С�ڻ���ڵ�ǰֵ���������
print '100002ѧԱ���䣺'+convert(varchar(20),@age)
select floor(datediff(dy,Birthday,getdate())/365)--����
from Students where StudentId=100002


--��ͼ�Ĵ���
use StudentManageDB
go
select * from sys.objects --ϵͳ���õ���ͼ���൱��һ�����ݱ�/�����

--������ͼ
--�ж���ͼ�Ƿ���ڣ����ھ�ɾ��
if exists (select * from sysobjects where name='View_StuScore')
	drop view View_StuScore
go
create view View_StuScore 
as
	select Students.StudentId,StudentName,ClassName,C#=CSharp,SQLDB=SQLServerDB,
	ScoreSum=(CSharp+SQLServerDB) from Students
	inner join ScoreList on ScoreList.StudentId=Students.StudentId
	inner join StudentClass on StudentClass.ClassId=Students.ClassId	
go
--��ѯ��ͼ
select * from View_StuScore

--����ģ��ת�ˣ�д��洢����
use StudentManageDB
go
create table CardAccount
(
	StudentId int not null,
	CurrentMoney money check(CurrentMoney>1)--�˻����������1
)
go
insert into CardAccount (StudentId,CurrentMoney) 
values 
(100001,1000),
(100002,1500)

--�������Ĵ洢����
if exists(select * from sysobjects where name='usp_TransferAccounts')
	drop procedure usp_TransferAccounts
go

create procedure usp_TransferAccounts
@inputAccount int,--ת���˻�
@outoutAccount int,--ת���˻�
@transferMoney int--ת�˽��
as 
	declare @errorSum int --��������������ۼ�������ִ�еĴ���
	set @errorSum=0 --��ʼ��Ϊ0������û�д���
	begin transaction
		begin
			update CardAccount set CurrentMoney=CurrentMoney-@transferMoney where StudentId=@outoutAccount
			set @errorSum=@errorSum+@@ERROR --ִ��һ��SQL��估ʱ��������ۼ�
			update CardAccount set CurrentMoney=CurrentMoney+@transferMoney where StudentId=@inputAccount
			set @errorSum=@errorSum+@@ERROR
			if(@errorSum>0) --��������
				rollback transaction
			else
				commit transaction
		end
go
select * from CardAccount
go
--����ʧ�ܵ�ת��
exec usp_TransferAccounts 100002,100001,1000
go
--���Գɹ���ת��
exec usp_TransferAccounts 100002,100001,500
exec usp_TransferAccounts 100001,100002,500
go

--��д�洢���̸�����Ŀ��Ų�ѯUVI555
use CompassDB
go
if exists (select * from sysobjects where name='usp_QuickBrowseUVI555')
drop procedure usp_QuickBrowseUVI555
go
create procedure usp_QuickBrowseUVI555
@ProjectId int
as
	select Item,Module,Length,Deepth,ExRightDis,ExNo,ExLength,ExWidth,ExHeight,SidePanel,Outlet,LEDlogo,Bluetooth,BackToBack,WaterCollection,LEDSpotNo,LEDSpotDis,LightType,UVType,ANSUL,ANSide,ANDetector,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5,MARVEL,IRNo,IRDis1,IRDis2,IRDis3 from UVI555 
	inner join ModuleTree on UVI555.ModuleTreeId=ModuleTree.ModuleTreeId
	inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId
	where ProjectId=@ProjectId 
	order by Item,Module
go

--��������ִ�ж���SQL���
insert into ModuleTree(DrawingPlanId,CategoryId,Module) 
values(18,1107,'M1'); select @@IDENTITY
insert into UVI555 (ModuleTreeId) values(@@IDENTITY)

--��ѯͳ�ƴ洢����
use StudentManageDB
go
if exists (select * from sysobjects where name='usp_ScoreQuery')
drop procedure usp_ScoreQuery
go
create procedure usp_ScoreQuery
@className varchar(20),
@stuCount int output,@absentCount int Output,@avgDB int Output,@avgCSharp int Output
as
	if(LEN(@className)=0)--��ѯȫУ
		begin
			--��ѯ������Ϣ
			select Students.StudentId,StudentName,ClassName,CSharp,SQLServerDB from Students
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			inner join ScoreList on ScoreList.StudentId=Students.StudentId
			--��ѯ����ͳ����Ϣ
			select @stuCount=count(*),@avgCSharp=avg(CSharp),@avgDB=avg(SQLServerDB) from ScoreList
			select @absentCount=count(*) from Students where Students.StudentId not in (select StudentId from ScoreList)
			--��ѯû�вμӿ��Ե�ѧԱ������
			select StudentName from Students where Students.StudentId not in (select StudentId from ScoreList)
		end
	else --���ݰ༶��ѯ
		begin
			select Students.StudentId,StudentName,ClassName,CSharp,SQLServerDB from Students
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			inner join ScoreList on ScoreList.StudentId=Students.StudentId
			where ClassName=@className
			--��ѯ����ͳ����Ϣ
			select @stuCount=count(*),@avgCSharp=avg(CSharp),@avgDB=avg(SQLServerDB) from ScoreList
			inner join Students on ScoreList.StudentId=Students.StudentId
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			where ClassName=@className
			select @absentCount=count(*) from Students 
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			where Students.StudentId not in (select StudentId from ScoreList) and ClassName=@className
			--��ѯû�вμӿ��Ե�ѧԱ������
			select StudentName from Students
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			where Students.StudentId not in (select StudentId from ScoreList) and ClassName=@className
		end
go


--���ݿ����ӳ�
use CompassDB
go
exec sp_who --�鿴����

--Group byͳ���÷�
select * from DrawingPlan where ProjectId=65
select Model as '����',sum(ModuleNo) as '��̨��' from DrawingPlan 
where ProjectId=65
group by Model


--��ҳ��ѯ
select * from Projects  order by ShippingTime desc
--��ѯ�깤������2020��Ķ��� where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1'
--��һҳ     
select top 10 * from Projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' order by ShippingTime desc
--�ڶ�ҳ
select top 10 * from projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' and ProjectId not in
(select top 10 ProjectId from Projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' order by ShippingTime desc)
order by ShippingTime desc
--����ҳ����������=ÿҳ��ʾ������*����ʾ�ĵڼ�ҳ-1����
select top 10 * from projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' and ProjectId not in
(select top 20 ProjectId from Projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' order by ShippingTime desc)
order by ShippingTime desc


--��ѯ���������ļ�¼����
select COUNT(*) from Projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1'

--���������������ҳ������

--------��ҳʵ�ֵĻ���˼·----------
--ÿҳ��ʾ������
--���˵�������=ÿҳ��ʾ������*����ǰ��ʾ��ҳ��-1��
--��ѯ������ȷ��
--��������

--��ȡ���������ļ�¼����
--֪����ѯ�����Ҫ��ʾ��ҳ��=��¼����/ÿҳ��ʾ����+1���������¼����%ÿҳ��ʾ������ȡģ��Ϊ0��δ����������+1��


