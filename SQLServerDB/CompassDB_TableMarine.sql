use CompassDB
go

if exists (select * from sysobjects where name='ProjectsMarine')
drop table ProjectsMarine
go
create table ProjectsMarine
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
if exists (select * from sysobjects where name='pk_ProjectIdMarine')
    alter table ProjectsMarine drop constraint pk_ProjectIdMarine
GO
alter table ProjectsMarine add constraint pk_ProjectIdMarine primary key (ProjectId)
GO
if exists (select * from sysobjects where name='uq_ODPNoMarine')
    alter table ProjectsMarine drop constraint uq_ODPNoMarine
GO
alter table ProjectsMarine add constraint uq_ODPNoMarine unique (ODPNo)

if exists (select * from sysobjects where name='df_AddedDate_ProjectsMarine')
    alter table ProjectsMarine drop constraint df_AddedDate_ProjectsMarine
GO
alter table ProjectsMarine add constraint df_AddedDate_ProjectsMarine default (getdate()) for AddedDate
GO

if exists (select * from sysobjects where name='fk_CustomerIdMarine')
    alter table ProjectsMarine drop constraint fk_CustomerIdMarine
GO
alter table ProjectsMarine add constraint fk_CustomerIdMarine foreign key(CustomerId) references Customers (CustomerId)
GO
if exists (select * from sysobjects where name='fk_AddedBy_ProjectsMarine')
    alter table ProjectsMarine drop constraint fk_AddedBy_ProjectsMarine
GO
alter table ProjectsMarine add constraint fk_AddedBy_ProjectsMarine foreign key(UserId) references Users (UserId)
GO


if exists (select * from sysobjects where name='ProjectTrackingMarine')
drop table ProjectTrackingMarine
go
create table ProjectTrackingMarine
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
select * from ProjectTrackingMarine

if exists (select * from sysobjects where name='pk_ProjectTrackingIdMarine')
    alter table ProjectTrackingMarine drop constraint pk_ProjectTrackingIdMarine
GO
alter table ProjectTrackingMarine add constraint pk_ProjectTrackingIdMarine primary key (ProjectTrackingId)
GO
if exists (select * from sysobjects where name='uq_ProjectId_ProjectTrackingMarine')
    alter table ProjectTrackingMarine drop constraint uq_ProjectId_ProjectTrackingMarine
GO
alter table ProjectTrackingMarine add constraint uq_ProjectId_ProjectTrackingMarine unique (ProjectId)
GO
if exists (select * from sysobjects where name='df_AddedDate_ProjectTrackingMarine')
    alter table ProjectTrackingMarine drop constraint df_AddedDate_ProjectTrackingMarine
GO
alter table ProjectTrackingMarine add constraint df_AddedDate_ProjectTrackingMarine default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='fk_ProjectStatusIdMarine')
    alter table ProjectTrackingMarine drop constraint fk_ProjectStatusIdMarine
GO
alter table ProjectTrackingMarine add constraint fk_ProjectStatusIdMarine foreign key(ProjectStatusId) references ProjectStatus (ProjectStatusId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_ProjectTrackingMarine')
    alter table ProjectTrackingMarine drop constraint fk_ProjectId_ProjectTrackingMarine
GO
alter table ProjectTrackingMarine add constraint fk_ProjectId_ProjectTrackingMarine foreign key(ProjectId) references ProjectsMarine (ProjectId)



if exists (select * from sysobjects where name='GeneralRequirementsMarine')
drop table GeneralRequirementsMarine
go
create table GeneralRequirementsMarine
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

if exists (select * from sysobjects where name='pk_GeneralRequirementIdMarine')
    alter table GeneralRequirementsMarine drop constraint pk_GeneralRequirementIdMarine
GO
alter table GeneralRequirementsMarine add constraint pk_GeneralRequirementIdMarine primary key (GeneralRequirementId)
GO
if exists (select * from sysobjects where name='uq_ProjectId_GRMarine')
    alter table GeneralRequirementsMarine drop constraint uq_ProjectId_GRMarine
GO
alter table GeneralRequirementsMarine add constraint uq_ProjectId_GRMarine unique (ProjectId)
GO
if exists (select * from sysobjects where name='df_TypeIdMarineMarine')
    alter table GeneralRequirementsMarine drop constraint df_TypeIdMarineMarine
GO
alter table GeneralRequirementsMarine add constraint df_TypeIdMarineMarine default (1) for TypeId
GO
if exists (select * from sysobjects where name='df_InputPowerMarine')
    alter table GeneralRequirementsMarine drop constraint df_InputPowerMarine
GO
alter table GeneralRequirementsMarine add constraint df_InputPowerMarine default ('220/50Hz') for InputPower
GO
if exists (select * from sysobjects where name='df_MARVEL_GRMarine')
    alter table GeneralRequirementsMarine drop constraint df_MARVEL_GRMarine
GO
alter table GeneralRequirementsMarine add constraint df_MARVEL_GRMarine default ('NO') for MARVEL
GO
if exists (select * from sysobjects where name='df_ANSULPrePipe_GRMarine')
    alter table GeneralRequirementsMarine drop constraint df_ANSULPrePipe_GRMarine
GO
alter table GeneralRequirementsMarine add constraint df_ANSULPrePipe_GRMarine default ('NO') for ANSULPrePipe
GO
if exists (select * from sysobjects where name='df_ANSULSystem_GRMarine')
    alter table GeneralRequirementsMarine drop constraint df_ANSULSystem_GRMarine
GO
alter table GeneralRequirementsMarine add constraint df_ANSULSystem_GRMarine default ('NO') for ANSULSystem
GO
if exists (select * from sysobjects where name='df_RiskLevel_GRMarine')
    alter table GeneralRequirementsMarine drop constraint df_RiskLevel_GRMarine
GO
alter table GeneralRequirementsMarine add constraint df_RiskLevel_GRMarine default (3) for RiskLevel
GO
if exists (select * from sysobjects where name='fk_TypeIdMarine')
    alter table GeneralRequirementsMarine drop constraint fk_TypeIdMarine
GO
alter table GeneralRequirementsMarine add constraint fk_TypeIdMarine foreign key(TypeId) references ProjectTypes (TypeId)
GO

if exists (select * from sysobjects where name='fk_ProjectId_GRMarine')
    alter table GeneralRequirementsMarine drop constraint fk_ProjectId_GRMarine
GO
alter table GeneralRequirementsMarine add constraint fk_ProjectId_GRMarine foreign key(ProjectId) references ProjectsMarine (ProjectId)
GO

if exists (select * from sysobjects where name='SpecialRequirementsMarine')
drop table SpecialRequirementsMarine
go
create table SpecialRequirementsMarine
(
    SpecialRequirementId int identity(1,1),
    ProjectId int not null,
    Content varchar(500)
)
if exists (select * from sysobjects where name='pk_SpecialRequirementIdMarine')
    alter table SpecialRequirementsMarine drop constraint pk_SpecialRequirementIdMarine
GO
alter table SpecialRequirementsMarine add constraint pk_SpecialRequirementIdMarine primary key (SpecialRequirementId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_SRMarine')
    alter table SpecialRequirementsMarine drop constraint fk_ProjectId_SRMarine
GO
alter table SpecialRequirementsMarine add constraint fk_ProjectId_SRMarine foreign key(ProjectId) references ProjectsMarine (ProjectId)
GO



if exists (select * from sysobjects where name='DrawingPlanMarine')
    drop table DrawingPlanMarine
create table DrawingPlanMarine
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
if exists (select * from sysobjects where name='pk_DrawingPlanIdMarine')
    alter table DrawingPlanMarine drop constraint pk_DrawingPlanIdMarine
GO
alter table DrawingPlanMarine add constraint pk_DrawingPlanIdMarine primary key (DrawingPlanId)
GO
if exists (select * from sysobjects where name='df_AddedDate_DrawingPlanMarine')
    alter table DrawingPlanMarine drop constraint df_AddedDate_DrawingPlanMarine
GO
alter table DrawingPlanMarine add constraint df_AddedDate_DrawingPlanMarine default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='fk_ProjectId_DrawingPlanMarine')
    alter table DrawingPlanMarine drop constraint fk_ProjectId_DrawingPlanMarine
GO
alter table DrawingPlanMarine add constraint fk_ProjectId_DrawingPlanMarine foreign key(ProjectId) references ProjectsMarine (ProjectId)


if exists (select * from sysobjects where name='CategoriesMarine')
    drop table CategoriesMarine
create table CategoriesMarine
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
if exists (select * from sysobjects where name='pk_CategoryIdMarine')
    alter table CategoriesMarine drop constraint pk_CategoryIdMarine
GO
alter table CategoriesMarine add constraint pk_CategoryIdMarine primary key (CategoryId)
GO
if exists (select * from sysobjects where name='uq_CategoryNameMarine')
    alter table CategoriesMarine drop constraint uq_CategoryNameMarine
GO
alter table CategoriesMarine add constraint uq_CategoryNameMarine unique (CategoryName)
GO
if exists (select * from sysobjects where name='df_CategoryDescMarine')
    alter table CategoriesMarine drop constraint df_CategoryDescMarine
GO
alter table CategoriesMarine add constraint df_CategoryDescMarine default ('请添加描述') for CategoryDesc
GO
if exists (select * from sysobjects where name='df_LastSavedMarine')
    alter table CategoriesMarine drop constraint df_LastSavedMarine
GO
alter table CategoriesMarine add constraint df_LastSavedMarine default ('') for LastSaved
GO
if exists (select * from sysobjects where name='df_KMLink_CategotiesMarine')
    alter table CategoriesMarine drop constraint df_KMLink_CategotiesMarine
GO
alter table CategoriesMarine add constraint df_KMLink_CategotiesMarine default ('') for KMLink
GO
if exists (select * from sysobjects where name='df_ModelImageMarine')
    alter table CategoriesMarine drop constraint df_ModelImageMarine
GO
alter table CategoriesMarine add constraint df_ModelImageMarine default ('') for ModelImage
GO
if exists (select * from sysobjects where name='df_Model_CategotiesMarine')
    alter table CategoriesMarine drop constraint df_Model_CategotiesMarine
GO
alter table CategoriesMarine add constraint df_Model_CategotiesMarine default ('') for Model
GO
if exists (select * from sysobjects where name='df_SubType_CategotiesMarine')
    alter table CategoriesMarine drop constraint df_SubType_CategotiesMarine
GO
alter table CategoriesMarine add constraint df_SubType_CategotiesMarine default ('') for Model
GO
if exists (select * from sysobjects where name='fk_ParentIdMarine')
    alter table CategoriesMarine drop constraint fk_ParentIdMarine
GO
alter table CategoriesMarine add constraint fk_ParentIdMarine foreign key(ParentId) references CategoriesMarine (CategoryId)
GO




if exists (select * from sysobjects where name='ModuleTreeMarine')
drop table ModuleTreeMarine
go
create table ModuleTreeMarine
(
    ModuleTreeId int identity(1,1),
    DrawingPlanId int not null,
    CategoryId int not null,
    Module varchar(20) not null    
)

if exists (select * from sysobjects where name='pk_ModuleTreeIdMarine')
    alter table ModuleTreeMarine drop constraint pk_ModuleTreeIdMarine
GO
alter table ModuleTreeMarine add constraint pk_ModuleTreeIdMarine primary key (ModuleTreeId)
GO
if exists (select * from sysobjects where name='uq_ModuleTreeMarine')
    alter table ModuleTreeMarine drop constraint uq_ModuleTreeMarine
GO
alter table ModuleTreeMarine add constraint uq_ModuleTreeMarine unique (DrawingPlanId,Module)
GO
if exists (select * from sysobjects where name='fk_DrawingPlanId_ModuleTreeMarine')
    alter table ModuleTreeMarine drop constraint fk_DrawingPlanId_ModuleTreeMarine
GO
alter table ModuleTreeMarine add constraint fk_DrawingPlanId_ModuleTreeMarine foreign key(DrawingPlanId) references DrawingPlanMarine (DrawingPlanId)
GO
if exists (select * from sysobjects where name='fk_CategoryId_ModuleTreeMarine')
    alter table ModuleTreeMarine drop constraint fk_CategoryId_ModuleTreeMarine
GO
alter table ModuleTreeMarine add constraint fk_CategoryId_ModuleTreeMarine foreign key(CategoryId) references CategoriesMarine (CategoryId)
GO

if exists (select * from sysobjects where name='DesignWorkloadMarine')
    drop table DesignWorkloadMarine
create table DesignWorkloadMarine
(
    WorkloadId int identity(1,1),
    Model varchar(30),
    WorkloadValue decimal(4,2),
	ModelDesc varchar(100)
)
if exists (select * from sysobjects where name='pk_WorkloadIdMarine')
    alter table DesignWorkloadMarine drop constraint pk_WorkloadIdMarine
GO
alter table DesignWorkloadMarine add constraint pk_WorkloadIdMarine primary key (WorkloadId)
GO
if exists (select * from sysobjects where name='uq_Model_DesignWorkloadMarine')
    alter table DesignWorkloadMarine drop constraint uq_Model_DesignWorkloadMarine
GO
alter table DesignWorkloadMarine add constraint uq_Model_DesignWorkloadMarine unique (Model)
GO

select * from DXFCutList


if exists (select * from sysobjects where name='ProjectTypesMarine')
drop table ProjectTypesMarine
go
create table ProjectTypesMarine
(
    TypeId int identity(1,1),
    TypeName varchar(20),
    KMLink varchar(500)
)

if exists (select * from sysobjects where name='pk_TypeIdMarine')
    alter table ProjectTypesMarine drop constraint pk_TypeIdMarine
GO
alter table ProjectTypesMarine add constraint pk_TypeIdMarine primary key (TypeId)
GO
if exists (select * from sysobjects where name='uq_TypeNameMarine')
    alter table ProjectTypesMarine drop constraint uq_TypeNameMarine
GO
alter table ProjectTypesMarine add constraint uq_TypeNameMarine unique (TypeName)
GO
if exists (select * from sysobjects where name='fk_TypeIdMarine')
    alter table GeneralRequirementsMarine drop constraint fk_TypeIdMarine
GO
alter table GeneralRequirementsMarine add constraint fk_TypeIdMarine foreign key(TypeId) references ProjectTypesMarine (TypeId)
GO
select * from ProjectTypesMarine
select * from GeneralRequirementsMarine


--FinancialData
if exists (select * from sysobjects where name='FinancialDataMarine')
drop table FinancialDataMarine
go
create table FinancialDataMarine
(
    FinancialDataId int identity(1,1),
    ProjectId int not null,
    SalesValue decimal(8,0)
)
if exists (select * from sysobjects where name='pk_FinancialDataId_Marine')
    alter table FinancialDataMarine drop constraint pk_FinancialDataId_Marine
GO
alter table FinancialDataMarine add constraint pk_FinancialDataId_Marine primary key (FinancialDataId)

if exists (select * from sysobjects where name='df_SalesValueMarine')
    alter table FinancialDataMarine drop constraint df_SalesValueMarine
GO
alter table FinancialDataMarine add constraint df_SalesValueMarine default (0) for SalesValue
GO

if exists (select * from sysobjects where name='uq_ProjectId_FinancialDataMarine')
    alter table FinancialDataMarine drop constraint uq_ProjectId_FinancialDataMarine
GO
alter table FinancialDataMarine add constraint uq_ProjectId_FinancialDataMarine unique (ProjectId)
GO

if exists (select * from sysobjects where name='fk_ProjectId_FinancialData_Marine')
    alter table FinancialDataMarine drop constraint fk_ProjectId_FinancialData_Marine
GO
alter table FinancialDataMarine add constraint fk_ProjectId_FinancialData_Marine foreign key(ProjectId) references ProjectsMarine (ProjectId)
