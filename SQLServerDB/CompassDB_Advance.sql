--创建登陆账户
use master
go
exec sp_addlogin 'felix','123'
--删除登陆账户/与之关联的数据库用户不会同时删除
exec sp_droplogin 'felix'
--创建数据库用户-与登陆账户对应
use StudentManageDB
go
exec sp_grantdbaccess 'felix','felixuser01'
--删除数据库用户
exec sp_dropuser 'felixuser01'
--分配权限/查询/插入/修改
grant select,insert,update on Students to felixuser01
grant create table to felixuser01
--收回权限
revoke select,insert,update on Students to felixuser01
--给数据库用户赋予用户角色
exec sp_addrolemember 'db_owner','felixuser01'
--删除数据库角色
exec sp_droprolemember 'db_owner','felixuser01'

--全局变量
print @@version --版本
print @@MAX_CONNECTIONS --可创建的统统是连接的最大数
print @@ERROR --最后一个sql语句错误的错误号
print @@IDENTITY --最后一次插入标识值
print @@LANGUAGE --当前使用语言的名称
print @@ROWCOUNT --受上一个sql语句影响的行数
print @@SERVERNAME --服务器名称
print @@TRANCOUNT --当前打开的事务数

select @@SERVERNAME as '服务器名称'

--局部变量
use StudentManageDB
go
declare @stuid int,@stuname varchar(20)
--查询李明明的信息
set @stuname='李明明'
select StudentId,StudentName,Gender,StudentIdNo from Students
where StudentName=@stuname
--查询李明明的学号
select @stuid=StudentId from Students 
where StudentName=@stuname
--查询与李明明学号相邻的学员
select StudentId,StudentName,Gender,StudentIdNo from Students
where StudentId=(@stuid+1) or StudentId=(@stuid-1)

--查询学号等于100002学员的年龄
use StudentManageDB
go
declare @birthday datetime,@days int,@age int
select @birthday=Birthday from Students where StudentId=100002
set @days=datediff(dayofyear,@birthday,getdate())--计算两个日期差
set @age=floor(@days/365)--返回一个小于或等于当前值的最大整数
print '100002学员年龄：'+convert(varchar(20),@age)
select floor(datediff(dy,Birthday,getdate())/365)--年龄
from Students where StudentId=100002


--视图的创建
use StudentManageDB
go
select * from sys.objects --系统内置的视图，相当与一个数据表/虚拟表

--创建视图
--判断视图是否存在，存在就删除
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
--查询视图
select * from View_StuScore

--事务，模拟转账，写入存储过程
use StudentManageDB
go
create table CardAccount
(
	StudentId int not null,
	CurrentMoney money check(CurrentMoney>1)--账户余额必须大于1
)
go
insert into CardAccount (StudentId,CurrentMoney) 
values 
(100001,1000),
(100002,1500)

--带参数的存储过程
if exists(select * from sysobjects where name='usp_TransferAccounts')
	drop procedure usp_TransferAccounts
go

create procedure usp_TransferAccounts
@inputAccount int,--转入账户
@outoutAccount int,--转出账户
@transferMoney int--转账金额
as 
	declare @errorSum int --定义变量，用于累加事务中执行的错误
	set @errorSum=0 --初始化为0，代表没有错误
	begin transaction
		begin
			update CardAccount set CurrentMoney=CurrentMoney-@transferMoney where StudentId=@outoutAccount
			set @errorSum=@errorSum+@@ERROR --执行一条SQL语句及时捕获错误，累加
			update CardAccount set CurrentMoney=CurrentMoney+@transferMoney where StudentId=@inputAccount
			set @errorSum=@errorSum+@@ERROR
			if(@errorSum>0) --发生错误
				rollback transaction
			else
				commit transaction
		end
go
select * from CardAccount
go
--测试失败的转账
exec usp_TransferAccounts 100002,100001,1000
go
--测试成功的转账
exec usp_TransferAccounts 100002,100001,500
exec usp_TransferAccounts 100001,100002,500
go

--编写存储过程根据项目编号查询UVI555
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

--开启事务执行多条SQL语句
insert into ModuleTree(DrawingPlanId,CategoryId,Module) 
values(18,1107,'M1'); select @@IDENTITY
insert into UVI555 (ModuleTreeId) values(@@IDENTITY)

--查询统计存储过程
use StudentManageDB
go
if exists (select * from sysobjects where name='usp_ScoreQuery')
drop procedure usp_ScoreQuery
go
create procedure usp_ScoreQuery
@className varchar(20),
@stuCount int output,@absentCount int Output,@avgDB int Output,@avgCSharp int Output
as
	if(LEN(@className)=0)--查询全校
		begin
			--查询考试信息
			select Students.StudentId,StudentName,ClassName,CSharp,SQLServerDB from Students
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			inner join ScoreList on ScoreList.StudentId=Students.StudentId
			--查询考试统计信息
			select @stuCount=count(*),@avgCSharp=avg(CSharp),@avgDB=avg(SQLServerDB) from ScoreList
			select @absentCount=count(*) from Students where Students.StudentId not in (select StudentId from ScoreList)
			--查询没有参加考试的学员的姓名
			select StudentName from Students where Students.StudentId not in (select StudentId from ScoreList)
		end
	else --根据班级查询
		begin
			select Students.StudentId,StudentName,ClassName,CSharp,SQLServerDB from Students
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			inner join ScoreList on ScoreList.StudentId=Students.StudentId
			where ClassName=@className
			--查询考试统计信息
			select @stuCount=count(*),@avgCSharp=avg(CSharp),@avgDB=avg(SQLServerDB) from ScoreList
			inner join Students on ScoreList.StudentId=Students.StudentId
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			where ClassName=@className
			select @absentCount=count(*) from Students 
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			where Students.StudentId not in (select StudentId from ScoreList) and ClassName=@className
			--查询没有参加考试的学员的姓名
			select StudentName from Students
			inner join StudentClass on Students.ClassId=StudentClass.ClassId
			where Students.StudentId not in (select StudentId from ScoreList) and ClassName=@className
		end
go


--数据库连接池
use CompassDB
go
exec sp_who --查看连接

--Group by统计用法
select * from DrawingPlan where ProjectId=65
select Model as '机型',sum(ModuleNo) as '总台数' from DrawingPlan 
where ProjectId=65
group by Model


--分页查询
select * from Projects  order by ShippingTime desc
--查询完工日期在2020年的订单 where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1'
--第一页     
select top 10 * from Projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' order by ShippingTime desc
--第二页
select top 10 * from projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' and ProjectId not in
(select top 10 ProjectId from Projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' order by ShippingTime desc)
order by ShippingTime desc
--第三页（过滤条数=每页显示的条数*（显示的第几页-1））
select top 10 * from projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' and ProjectId not in
(select top 20 ProjectId from Projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1' order by ShippingTime desc)
order by ShippingTime desc


--查询符合条件的记录总数
select COUNT(*) from Projects where ShippingTime>'2019/12/31' and ShippingTime<'2021/1/1'

--计算符合条件的总页数（）

--------分页实现的基本思路----------
--每页显示的条数
--过滤掉的总数=每页显示的条数*（当前显示的页数-1）
--查询条件的确定
--排序条件

--获取满足条件的记录总数
--知道查询结果需要显示的页数=记录总数/每页显示条数+1（如果【记录总数%每页显示条数】取模后不为0【未除尽】，则+1）


