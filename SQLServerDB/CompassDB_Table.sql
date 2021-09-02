use CompassDB
go
if exists (select * from sysobjects where name='UserGroups')
    drop table UserGroups
GO
create table UserGroups
(
    UserGroupId int identity(1,1),
    GroupName varchar(15) not null
)
if exists (select * from sysobjects where name='Users')
    drop table Users
create table Users
(
    UserId int identity(1,1),
    UserGroupId int not null,
    UserAccount varchar(15) not null,
    UserPwd varchar(15) not null,
    Email varchar(50),
    Contact char(11)
) 
if exists (select * from sysobjects where name='Categories')
    drop table Categories
create table Categories
(
    CategoryId int not null,--不能自增，这个后面要手动定义分类号
    ParentId int not null,--类型的父类，引用CategoryId，约束后面再添加
    CategoryName varchar(20) not null,
    CategoryDesc varchar(30),    
    Model varchar(30),
    SubType varchar(20),
    ModelImage text,
    LastSaved text,
	KMLink varchar(500),
    ModelPath varchar(500)
)
if exists (select * from sysobjects where name='DesignWorkload')
    drop table DesignWorkload
create table DesignWorkload
(
    WorkloadId int identity(1,1),
    Model varchar(30),
    WorkloadValue decimal(4,2),
	ModelDesc varchar(100)
)

if exists (select * from sysobjects where name='DrawingPlan')
    drop table DrawingPlan
create table DrawingPlan
(
    DrawingPlanId int identity(1,1),    
    ProjectId int not null,
    Item varchar(30),
    Model varchar(30) not null,
    ModuleNo int not null,
    DrReleaseTarget date,
    SubTotalWorkload decimal(6,2),
    AddedDate datetime,
	LabelImage text
)
if exists (select * from sysobjects where name='ProjectStatus')
drop table ProjectStatus
go
create table ProjectStatus
(
    ProjectStatusId int identity(1,1),
    ProjectStatusName varchar(25) not null,
    StatusDesc varchar(100)
)
if exists (select * from sysobjects where name='ProjectTracking')
drop table ProjectTracking
go
create table ProjectTracking
(
    ProjectTrackingId int identity(1,1),
    ProjectId int not null,
    ProjectStatusId int not null,    
    DrReleaseActual date,    
    ProdFinishActual date,    
    DeliverActual date,
    AddedDate datetime,
	ODPReceiveDate date,
	KickOffDate date
)

if exists (select * from sysobjects where name='Customers')
drop table Customers
go
create table Customers
(
    CustomerId int identity(1,1),
    CustomerName varchar(100) not null,   
)

if exists (select * from sysobjects where name='Projects')
drop table Projects
go
create table Projects
(
    ProjectId int identity(1,1),
    ODPNo varchar(10) not null,
    BPONo varchar(10),    
    ProjectName varchar(100),
    CustomerId int,
    ShippingTime date,
    AddedDate datetime,--添加一个默认时间
    UserId int,
	HoodType varchar(10)
)

if exists (select * from sysobjects where name='ProjectTypes')
drop table ProjectTypes
go
create table ProjectTypes
(
    TypeId int identity(1,1),
    TypeName varchar(20),
    KMLink varchar(500)
)






select * from ProjectTypesMarine


if exists (select * from sysobjects where name='GeneralRequirements')
drop table GeneralRequirements
go
create table GeneralRequirements
(
    GeneralRequirementId int identity(1,1),
    ProjectId int not null,
    TypeId int not null,
    InputPower varchar(10),
    MARVEL varchar(20),
    ANSULPrePipe varchar(20),
    ANSULSystem varchar(20),
	RiskLevel int,
	MainAssyPath varchar(500)
)

if exists (select * from sysobjects where name='SpecialRequirements')
drop table SpecialRequirements
go
create table SpecialRequirements
(
    SpecialRequirementId int identity(1,1),
    ProjectId int not null,
    Content varchar(500)
)

if exists (select * from sysobjects where name='CheckComments')
drop table CheckComments
go
create table CheckComments
(
    CheckCommentId int identity(1,1),
    ProjectId int not null,
    Content varchar(500),
    CheckStatus int not null,
    AddedDate datetime,
    UserId int not null
)
if exists (select * from sysobjects where name='QualityFeedbacks')
drop table QualityFeedbacks
go
create table QualityFeedbacks
(
    QualityFeedbackId int identity(1,1),
    ProjectId int not null,
    Content varchar(500),
    AddedDate datetime,
    UserId int not null
)
if exists (select * from sysobjects where name='AfterSaleFeedbacks')
drop table AfterSaleFeedbacks
go
create table AfterSaleFeedbacks
(
    AfterSaleFeedbackId int identity(1,1),
    ProjectId int not null,
    Content varchar(500),
    AddedDate datetime,
    UserId int not null
)
if exists (select * from sysobjects where name='ProjectLearneds')
drop table ProjectLearneds
go
create table ProjectLearneds
(
    ProjectLearnedId int identity(1,1),
    ProjectId int not null,
    Content varchar(500),
    KMLink varchar(500),
    AddedDate datetime,
    UserId int not null
)
if exists (select * from sysobjects where name='ModuleTree')
drop table ModuleTree
go
create table ModuleTree
(
    ModuleTreeId int identity(1,1),
    DrawingPlanId int not null,
    CategoryId int not null,
    Module varchar(20) not null    
)
if exists (select * from sysobjects where name='HoodCutList')
drop table HoodCutList
go
create table HoodCutList
(
    CutListId int identity(1,1),
	ModuleTreeId int not null,
    PartDescription varchar(100),
	Length decimal(6,2),
	Width decimal(6,2),
	Thickness decimal(4,2),
	Quantity int,
	Materials varchar(25),
	PartNo varchar(100),
	AddedDate datetime,
    UserId int not null
)
if exists (select * from sysobjects where name='DXFCutList')
drop table DXFCutList
go
create table DXFCutList
(
    CutListId int identity(1,1),
	CategoryId int not null,--fk
    PartDescription varchar(100),
	Length decimal(6,2),
	Width decimal(6,2),
	Thickness decimal(4,2),
	Quantity int,
	Materials varchar(25),
	PartNo varchar(100)
)




if exists (select * from sysobjects where name='SubAssy')
drop table SubAssy
go
create table SubAssy
(
    SubAssyId int identity(1,1),
	ProjectId int not null,
	SubAssyName varchar(50),
    SubAssyPath varchar(500)
)

if exists (select * from sysobjects where name='CeilingCutList')
drop table CeilingCutList
go
create table CeilingCutList
(
    CutListId int identity(1,1),
	SubAssyId int not null,
    PartDescription varchar(100),
	Length decimal(6,2),
	Width decimal(6,2),
	Thickness decimal(4,2),
	Quantity int,
	Materials varchar(25),
	PartNo varchar(100),
	AddedDate datetime,
    UserId int not null
)


if exists (select * from sysobjects where name='CeilingAccessories')
drop table CeilingAccessories
go
create table CeilingAccessories
(
    CeilingAccessoryId varchar(4) not null,	
	ClassNo int not null,--df 3  --分类号规则：0日本不要配件，1日本特有配件,2适用于所有项目的配件，3自制折弯件，4自制焊接件（打标签）
    PartDescription varchar(150),
	Quantity int,--df 0
	PartNo varchar(50),	
	Unit varchar(10), --df 'PCS'	
	Length varchar(5),--df ''
	Width varchar(5),--df ''
	Height varchar(5),--df ''
	Material varchar(25),--df ''
	Remark varchar(100),--df ''
	CountingRule varchar(200)--df ''
)
if exists (select * from sysobjects where name='CeilingPackingList')
drop table CeilingPackingList
go
create table CeilingPackingList
(
    CeilingPackingListId int identity(1,1),--pk,PartDescription的tag
	ProjectId int not null,--fk
	CeilingAccessoryId varchar(4) not null,	
	ClassNo int not null,--df 3  --分类号规则：0日本不要配件，1日本特有配件,2适用于所有项目的配件，3自制折弯件，4自制焊接件（打标签）
    PartDescription varchar(150),
	Quantity int,--df 0
	PartNo varchar(50),	
	Unit varchar(10), --df 'PCS'	
	Length varchar(6),--df ''
	Width varchar(6),--df ''
	Height varchar(6),--df ''
	Material varchar(25),--df ''
	Remark varchar(100),--df ''
	CountingRule varchar(200),--df ''
	AddedDate datetime, --df
	UserId int not null, --fk
	Location varchar(50) --df
) 



--FinancialData
if exists (select * from sysobjects where name='FinancialData')
drop table FinancialData
go
create table FinancialData
(
    FinancialDataId int identity(1,1),
    ProjectId int not null,
    SalesValue decimal(8,0)
)
if exists (select * from sysobjects where name='pk_FinancialDataId')
    alter table FinancialData drop constraint pk_FinancialDataId
GO
alter table FinancialData add constraint pk_FinancialDataId primary key (FinancialDataId)

if exists (select * from sysobjects where name='df_SalesValue')
    alter table FinancialData drop constraint df_SalesValue
GO
alter table FinancialData add constraint df_SalesValue default (0) for SalesValue
GO

if exists (select * from sysobjects where name='uq_ProjectId_FinancialData')
    alter table FinancialData drop constraint uq_ProjectId_FinancialData
GO
alter table FinancialData add constraint uq_ProjectId_FinancialData unique (ProjectId)
GO

if exists (select * from sysobjects where name='fk_ProjectId_FinancialData')
    alter table FinancialData drop constraint fk_ProjectId_FinancialData
GO
alter table FinancialData add constraint fk_ProjectId_FinancialData foreign key(ProjectId) references Projects (ProjectId)


select * from FinancialData


select FinancialDataId,ProjectId,SalesValue from FinancialData where ProjectId=61

--DrawingNumCodeRule
if exists (select * from sysobjects where name='DrawingNumCodeRule')
drop table DrawingNumCodeRule
go
create table DrawingNumCodeRule
(
    CodeId int not null,--不能自增，这个后面要手动定义分类号
    ParentId int not null,--类型的父类，引用CodeId，约束后面再添加
    Code char(1) not null,
    CodeName varchar(50) not null
)

if exists (select * from sysobjects where name='pk_CodeId')
    alter table DrawingNumCodeRule drop constraint pk_CodeId
GO
alter table DrawingNumCodeRule add constraint pk_CodeId primary key (CodeId)
GO

if exists (select * from sysobjects where name='fk_ParentId_CodeRule')
    alter table DrawingNumCodeRule drop constraint fk_ParentId_CodeRule
GO
alter table DrawingNumCodeRule add constraint fk_ParentId_CodeRule foreign key(ParentId) references DrawingNumCodeRule (CodeId)
GO

--DrawingNumMatrix
if exists (select * from sysobjects where name='DrawingNumMatrix')
drop table DrawingNumMatrix
go
create table DrawingNumMatrix
(
    DrawingId int identity(1,1),
	DrawingNum varchar(8) not null,
    DrawingDesc varchar(100) not null,
	DrawingType varchar(10) not null,	
	Mark varchar(100),
	AddedDate datetime,
    UserId int not null,
	DrawingImage text
)
if exists (select * from sysobjects where name='pk_DrawingId')
    alter table DrawingNumMatrix drop constraint pk_DrawingId
GO
alter table DrawingNumMatrix add constraint pk_DrawingId primary key (DrawingId)
GO
if exists (select * from sysobjects where name='df_AddedDate_DrawingNumMatrix')
    alter table DrawingNumMatrix drop constraint df_AddedDate_DrawingNumMatrix
GO
alter table DrawingNumMatrix add constraint df_AddedDate_DrawingNumMatrix default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='uq_DrawingNum')
    alter table DrawingNumMatrix drop constraint uq_DrawingNum
GO
alter table DrawingNumMatrix add constraint uq_DrawingNum unique (DrawingNum)
GO
if exists (select * from sysobjects where name='fk_UserId_DrawingNumMatrix')
    alter table DrawingNumMatrix drop constraint fk_UserId_DrawingNumMatrix
GO
alter table DrawingNumMatrix add constraint fk_UserId_DrawingNumMatrix foreign key(UserId) references Users (UserId)
GO
