use CompassDB
go
--创建主键约束，primary key
if exists (select * from sysobjects where name='pk_UserGroupId')
    alter table UserGroups drop constraint pk_UserGroupId
GO
alter table UserGroups add constraint pk_UserGroupId primary key (UserGroupId)
GO
if exists (select * from sysobjects where name='pk_UserId')
    alter table Users drop constraint pk_UserId
GO
alter table Users add constraint pk_UserId primary key (UserId)
GO
if exists (select * from sysobjects where name='pk_CategoryId')
    alter table Categories drop constraint pk_CategoryId
GO
alter table Categories add constraint pk_CategoryId primary key (CategoryId)
GO
if exists (select * from sysobjects where name='pk_WorkloadId')
    alter table DesignWorkload drop constraint pk_WorkloadId
GO
alter table DesignWorkload add constraint pk_WorkloadId primary key (WorkloadId)
GO
if exists (select * from sysobjects where name='pk_DrawingPlanId')
    alter table DrawingPlan drop constraint pk_DrawingPlanId
GO
alter table DrawingPlan add constraint pk_DrawingPlanId primary key (DrawingPlanId)
GO
if exists (select * from sysobjects where name='pk_ProjectStatusId')
    alter table ProjectStatus drop constraint pk_ProjectStatusId
GO
alter table ProjectStatus add constraint pk_ProjectStatusId primary key (ProjectStatusId)
GO
if exists (select * from sysobjects where name='pk_ProjectTrackingId')
    alter table ProjectTracking drop constraint pk_ProjectTrackingId
GO
alter table ProjectTracking add constraint pk_ProjectTrackingId primary key (ProjectTrackingId)
GO

if exists (select * from sysobjects where name='pk_ProjectId')
    alter table Projects drop constraint pk_ProjectId
GO
alter table Projects add constraint pk_ProjectId primary key (ProjectId)
GO
if exists (select * from sysobjects where name='pk_TypeId')
    alter table ProjectTypes drop constraint pk_TypeId
GO
alter table ProjectTypes add constraint pk_TypeId primary key (TypeId)
GO
if exists (select * from sysobjects where name='pk_GeneralRequirementId')
    alter table GeneralRequirements drop constraint pk_GeneralRequirementId
GO
alter table GeneralRequirements add constraint pk_GeneralRequirementId primary key (GeneralRequirementId)
GO
if exists (select * from sysobjects where name='pk_SpecialRequirementId')
    alter table SpecialRequirements drop constraint pk_SpecialRequirementId
GO
alter table SpecialRequirements add constraint pk_SpecialRequirementId primary key (SpecialRequirementId)
GO
if exists (select * from sysobjects where name='pk_CheckCommentId')
    alter table CheckComments drop constraint pk_CheckCommentId
GO
alter table CheckComments add constraint pk_CheckCommentId primary key (CheckCommentId)
GO
if exists (select * from sysobjects where name='pk_QualityFeedbackId')
    alter table QualityFeedbacks drop constraint pk_QualityFeedbackId
GO
alter table QualityFeedbacks add constraint pk_QualityFeedbackId primary key (QualityFeedbackId)
GO
if exists (select * from sysobjects where name='pk_AfterSaleFeedbackId')
    alter table AfterSaleFeedbacks drop constraint pk_AfterSaleFeedbackId
GO
alter table AfterSaleFeedbacks add constraint pk_AfterSaleFeedbackId primary key (AfterSaleFeedbackId)
GO
if exists (select * from sysobjects where name='pk_ProjectLearnedId')
    alter table ProjectLearneds drop constraint pk_ProjectLearnedId
GO
alter table ProjectLearneds add constraint pk_ProjectLearnedId primary key (ProjectLearnedId)
GO
if exists (select * from sysobjects where name='pk_ModuleTreeId')
    alter table ModuleTree drop constraint pk_ModuleTreeId
GO
alter table ModuleTree add constraint pk_ModuleTreeId primary key (ModuleTreeId)
GO
if exists (select * from sysobjects where name='pk_CustomerId')
    alter table Customers drop constraint pk_CustomerId
GO
alter table Customers add constraint pk_CustomerId primary key (CustomerId)
GO
if exists (select * from sysobjects where name='pk_CutListId_Hood')
    alter table HoodCutList drop constraint pk_CutListId_Hood
GO
alter table HoodCutList add constraint pk_CutListId_Hood primary key (CutListId)
GO
if exists (select * from sysobjects where name='pk_CutListId_DXF')
    alter table DXFCutList drop constraint pk_CutListId_DXF
GO
alter table DXFCutList add constraint pk_CutListId_DXF primary key (CutListId)
GO
if exists (select * from sysobjects where name='pk_SubAssyId')
    alter table SubAssy drop constraint pk_SubAssyId
GO
alter table SubAssy add constraint pk_SubAssyId primary key (SubAssyId)
GO
if exists (select * from sysobjects where name='pk_CutListId_Ceiling')
    alter table CeilingCutList drop constraint pk_CutListId_Ceiling
GO
alter table CeilingCutList add constraint pk_CutListId_Ceiling primary key (CutListId)

if exists (select * from sysobjects where name='pk_CeilingAccessoryId')
    alter table CeilingAccessories drop constraint pk_CeilingAccessoryId
GO
alter table CeilingAccessories add constraint pk_CeilingAccessoryId primary key (CeilingAccessoryId)

if exists (select * from sysobjects where name='pk_CeilingPackingListId')
    alter table CeilingPackingList drop constraint pk_CeilingPackingListId
GO
alter table CeilingPackingList add constraint pk_CeilingPackingListId primary key (CeilingPackingListId)








--添加唯一约束，unique
if exists (select * from sysobjects where name='uq_GroupName')
    alter table UserGroups drop constraint uq_GroupName
GO
alter table UserGroups add constraint uq_GroupName unique (GroupName)
GO
if exists (select * from sysobjects where name='uq_UserAccount')
    alter table Users drop constraint uq_UserAccount
GO
alter table Users add constraint uq_UserAccount unique (UserAccount)
GO
if exists (select * from sysobjects where name='uq_CategoryName')
    alter table Categories drop constraint uq_CategoryName
GO
alter table Categories add constraint uq_CategoryName unique (CategoryName)
GO
if exists (select * from sysobjects where name='uq_Model_DesignWorkload')
    alter table DesignWorkload drop constraint uq_Model_DesignWorkload
GO
alter table DesignWorkload add constraint uq_Model_DesignWorkload unique (Model)
GO
if exists (select * from sysobjects where name='uq_ProjectStatus')
    alter table ProjectStatus drop constraint uq_ProjectStatus
GO
alter table ProjectStatus add constraint uq_ProjectStatus unique (ProjectStatus)
GO

if exists (select * from sysobjects where name='uq_ProjectId_ProjectTracking')
    alter table ProjectTracking drop constraint uq_ProjectId_ProjectTracking
GO
alter table ProjectTracking add constraint uq_ProjectId_ProjectTracking unique (ProjectId)
GO


if exists (select * from sysobjects where name='uq_ODPNo')
    alter table Projects drop constraint uq_ODPNo
GO
alter table Projects add constraint uq_ODPNo unique (ODPNo)
GO
if exists (select * from sysobjects where name='uq_TypeName')
    alter table ProjectTypes drop constraint uq_TypeName
GO
alter table ProjectTypes add constraint uq_TypeName unique (TypeName)
GO
if exists (select * from sysobjects where name='uq_CustomerName')
    alter table Customers drop constraint uq_CustomerName
GO
alter table Customers add constraint uq_CustomerName unique (CustomerName)
GO
if exists (select * from sysobjects where name='uq_ProjectId_GR')
    alter table GeneralRequirements drop constraint uq_ProjectId_GR
GO
alter table GeneralRequirements add constraint uq_ProjectId_GR unique (ProjectId)
GO
if exists (select * from sysobjects where name='uq_SubAssyPath')
    alter table SubAssy drop constraint uq_SubAssyPath
GO
alter table SubAssy add constraint uq_SubAssyPath unique (ProjectId,SubAssyName,SubAssyPath)
GO
select * from SubAssy

--多列组合约束
if exists (select * from sysobjects where name='uq_ModuleTree')
    alter table ModuleTree drop constraint uq_ModuleTree
GO
alter table ModuleTree add constraint uq_ModuleTree unique (DrawingPlanId,Module)
GO
select * from HoodCutList
if exists (select * from sysobjects where name='uq_HoodCutList')
    alter table HoodCutList drop constraint uq_HoodCutList
GO
alter table HoodCutList add constraint uq_HoodCutList unique (ModuleTreeId,PartNo)
GO
select * from CeilingCutList
if exists (select * from sysobjects where name='uq_CeilingCutList')
    alter table CeilingCutList drop constraint uq_CeilingCutList
GO
alter table CeilingCutList add constraint uq_CeilingCutList unique (SubAssyId,PartNo)
GO


--检查约束，check


--默认约束，default
if exists (select * from sysobjects where name='df_Email')
    alter table Users drop constraint df_Email
GO
alter table Users add constraint df_Email default ('xxx@halton.com') for Email
GO
if exists (select * from sysobjects where name='df_Contactl')
    alter table Users drop constraint df_Contactl
GO
alter table Users add constraint df_Contactl default ('10010001000') for Contact
GO
if exists (select * from sysobjects where name='df_CategoryDesc')
    alter table Categories drop constraint df_CategoryDesc
GO
alter table Categories add constraint df_CategoryDesc default ('请添加描述') for CategoryDesc
GO
if exists (select * from sysobjects where name='df_TypeId')
    alter table GeneralRequirements drop constraint df_TypeId
GO
alter table GeneralRequirements add constraint df_TypeId default (1) for TypeId
GO
if exists (select * from sysobjects where name='df_InputPower')
    alter table GeneralRequirements drop constraint df_InputPower
GO
alter table GeneralRequirements add constraint df_InputPower default ('220/50Hz') for InputPower
GO
if exists (select * from sysobjects where name='df_MARVEL_GR')
    alter table GeneralRequirements drop constraint df_MARVEL_GR
GO
alter table GeneralRequirements add constraint df_MARVEL_GR default ('NO') for MARVEL
GO
if exists (select * from sysobjects where name='df_ANSULPrePipe_GR')
    alter table GeneralRequirements drop constraint df_ANSULPrePipe_GR
GO
alter table GeneralRequirements add constraint df_ANSULPrePipe_GR default ('NO') for ANSULPrePipe
GO
if exists (select * from sysobjects where name='df_ANSULSystem_GR')
    alter table GeneralRequirements drop constraint df_ANSULSystem_GR
GO
alter table GeneralRequirements add constraint df_ANSULSystem_GR default ('NO') for ANSULSystem
GO
if exists (select * from sysobjects where name='df_AddedDate_DrawingPlan')
    alter table DrawingPlan drop constraint df_AddedDate_DrawingPlan
GO
alter table DrawingPlan add constraint df_AddedDate_DrawingPlan default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_AddedDate_ProjectTracking')
    alter table ProjectTracking drop constraint df_AddedDate_ProjectTracking
GO
alter table ProjectTracking add constraint df_AddedDate_ProjectTracking default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_AddedDate_Projects')
    alter table Projects drop constraint df_AddedDate_Projects
GO
alter table Projects add constraint df_AddedDate_Projects default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_AddedDate_CC')
    alter table CheckComments drop constraint df_AddedDate_CC
GO
alter table CheckComments add constraint df_AddedDate_CC default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_AddedDate_QF')
    alter table QualityFeedbacks drop constraint df_AddedDate_QF
GO
alter table QualityFeedbacks add constraint df_AddedDate_QF default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_AddedDate_AF')
    alter table AfterSaleFeedbacks drop constraint df_AddedDate_AF
GO
alter table AfterSaleFeedbacks add constraint df_AddedDate_AF default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_AddedDate_PL')
    alter table ProjectLearneds drop constraint df_AddedDate_PL
GO
alter table ProjectLearneds add constraint df_AddedDate_PL default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_LastSaved')
    alter table Categories drop constraint df_LastSaved
GO
alter table Categories add constraint df_LastSaved default ('') for LastSaved
GO
if exists (select * from sysobjects where name='df_KMLink_Categoties')
    alter table Categories drop constraint df_KMLink_Categoties
GO
alter table Categories add constraint df_KMLink_Categoties default ('') for KMLink
GO
if exists (select * from sysobjects where name='df_ModelImage')
    alter table Categories drop constraint df_ModelImage
GO
alter table Categories add constraint df_ModelImage default ('') for ModelImage
GO
if exists (select * from sysobjects where name='df_Model_Categoties')
    alter table Categories drop constraint df_Model_Categoties
GO
alter table Categories add constraint df_Model_Categoties default ('') for Model
GO
if exists (select * from sysobjects where name='df_SubType_Categoties')
    alter table Categories drop constraint df_SubType_Categoties
GO
alter table Categories add constraint df_SubType_Categoties default ('') for Model
GO
if exists (select * from sysobjects where name='df_RiskLevel')
    alter table GeneralRequirements drop constraint df_RiskLevel
GO
alter table GeneralRequirements add constraint df_RiskLevel default (3) for RiskLevel
GO
if exists (select * from sysobjects where name='df_AddedDate_HoodCutList')
    alter table HoodCutList drop constraint df_AddedDate_HoodCutList
GO
alter table HoodCutList add constraint df_AddedDate_HoodCutList default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_HoodType')
    alter table Projects drop constraint df_HoodType
GO
alter table Projects add constraint df_HoodType default ('Hood') for HoodType
GO
if exists (select * from sysobjects where name='df_AddedDate_CeilingCutList')
    alter table CeilingCutList drop constraint df_AddedDate_CeilingCutList
GO
alter table CeilingCutList add constraint df_AddedDate_CeilingCutList default (getdate()) for AddedDate
GO
if exists (select * from sysobjects where name='df_ClassNo_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_ClassNo_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_ClassNo_CeilingAccessory default (3) for ClassNo
GO
--Quantity
if exists (select * from sysobjects where name='df_Quantity_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_Quantity_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_Quantity_CeilingAccessory default (0) for Quantity
GO
if exists (select * from sysobjects where name='df_Unit_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_Unit_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_Unit_CeilingAccessory default ('PCS') for Unit
GO
if exists (select * from sysobjects where name='df_Length_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_Length_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_Length_CeilingAccessory default ('') for Length
GO
if exists (select * from sysobjects where name='df_Width_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_Width_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_Width_CeilingAccessory default ('') for Width
GO
if exists (select * from sysobjects where name='df_Height_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_Height_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_Height_CeilingAccessory default ('') for Height
GO
if exists (select * from sysobjects where name='df_Material_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_Material_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_Material_CeilingAccessory default ('') for Material
GO
if exists (select * from sysobjects where name='df_Remark_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_Remark_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_Remark_CeilingAccessory default ('') for Remark
GO
if exists (select * from sysobjects where name='df_CountingRule_CeilingAccessory')
    alter table CeilingAccessories drop constraint df_CountingRule_CeilingAccessory
GO
alter table CeilingAccessories add constraint df_CountingRule_CeilingAccessory default ('') for CountingRule
GO
if exists (select * from sysobjects where name='df_MainAssyPath')
    alter table GeneralRequirements drop constraint df_MainAssyPath
GO
alter table GeneralRequirements add constraint df_MainAssyPath default ('') for MainAssyPath
GO

if exists (select * from sysobjects where name='df_AddedDate_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_AddedDate_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_AddedDate_CeilingPackingList default (getdate()) for AddedDate
GO


if exists (select * from sysobjects where name='df_ClassNo_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_ClassNo_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_ClassNo_CeilingPackingList default (3) for ClassNo
GO
--Quantity
if exists (select * from sysobjects where name='df_Quantity_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_Quantity_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_Quantity_CeilingPackingList default (0) for Quantity
GO
if exists (select * from sysobjects where name='df_Unit_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_Unit_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_Unit_CeilingPackingList default ('PCS') for Unit
GO
if exists (select * from sysobjects where name='df_Length_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_Length_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_Length_CeilingPackingList default ('') for Length
GO
if exists (select * from sysobjects where name='df_Width_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_Width_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_Width_CeilingPackingList default ('') for Width
GO
if exists (select * from sysobjects where name='df_Height_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_Height_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_Height_CeilingPackingList default ('') for Height
GO
if exists (select * from sysobjects where name='df_Material_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_Material_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_Material_CeilingPackingList default ('') for Material
GO
if exists (select * from sysobjects where name='df_Remark_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_Remark_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_Remark_CeilingPackingList default ('') for Remark
GO
if exists (select * from sysobjects where name='df_CountingRule_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_CountingRule_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_CountingRule_CeilingPackingList default ('') for CountingRule
GO
if exists (select * from sysobjects where name='df_Location_CeilingPackingList')
    alter table CeilingPackingList drop constraint df_Location_CeilingPackingList
GO
alter table CeilingPackingList add constraint df_Location_CeilingPackingList default ('AREA1') for Location
GO




--主外键约束 foreign key
if exists (select * from sysobjects where name='fk_UserGroupId')
    alter table Users drop constraint fk_UserGroupId
GO
alter table Users add constraint fk_UserGroupId foreign key(UserGroupId) references UserGroups (UserGroupId)
GO
if exists (select * from sysobjects where name='fk_ParentId')
    alter table Categories drop constraint fk_ParentId
GO
alter table Categories add constraint fk_ParentId foreign key(ParentId) references Categories (CategoryId)
GO
if exists (select * from sysobjects where name='fk_UserId_DP')
    alter table DrawingPlan drop constraint fk_UserId_DP
GO
alter table DrawingPlan add constraint fk_UserId_DP foreign key(UserId) references Users (UserId)
GO
if exists (select * from sysobjects where name='fk_ProjectStatusId')
    alter table ProjectTracking drop constraint fk_ProjectStatusId
GO
alter table ProjectTracking add constraint fk_ProjectStatusId foreign key(ProjectStatusId) references ProjectStatus (ProjectStatusId)
GO

if exists (select * from sysobjects where name='fk_CustomerId')
    alter table Projects drop constraint fk_CustomerId
GO
alter table Projects add constraint fk_CustomerId foreign key(CustomerId) references Customers (CustomerId)
GO
if exists (select * from sysobjects where name='fk_AddedBy_Projects')
    alter table Projects drop constraint fk_AddedBy_Projects
GO
alter table Projects add constraint fk_AddedBy_Projects foreign key(UserId) references Users (UserId)
GO
if exists (select * from sysobjects where name='fk_TypeId')
    alter table GeneralRequirements drop constraint fk_TypeId
GO
alter table GeneralRequirements add constraint fk_TypeId foreign key(TypeId) references ProjectTypes (TypeId)
GO

if exists (select * from sysobjects where name='fk_ProjectId_DrawingPlan')
    alter table DrawingPlan drop constraint fk_ProjectId_DrawingPlan
GO
alter table DrawingPlan add constraint fk_ProjectId_DrawingPlan foreign key(ProjectId) references Projects (ProjectId)

if exists (select * from sysobjects where name='fk_ProjectId_ProjectTracking')
    alter table ProjectTracking drop constraint fk_ProjectId_ProjectTracking
GO
alter table ProjectTracking add constraint fk_ProjectId_ProjectTracking foreign key(ProjectId) references Projects (ProjectId)

if exists (select * from sysobjects where name='fk_ProjectId_GR')
    alter table GeneralRequirements drop constraint fk_ProjectId_GR
GO
alter table GeneralRequirements add constraint fk_ProjectId_GR foreign key(ProjectId) references Projects (ProjectId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_SR')
    alter table SpecialRequirements drop constraint fk_ProjectId_SR
GO
alter table SpecialRequirements add constraint fk_ProjectId_SR foreign key(ProjectId) references Projects (ProjectId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_CC')
    alter table CheckComments drop constraint fk_ProjectId_CC
GO
alter table CheckComments add constraint fk_ProjectId_CC foreign key(ProjectId) references Projects (ProjectId)
GO
if exists (select * from sysobjects where name='fk_AddedBy_CC')
    alter table CheckComments drop constraint fk_AddedBy_CC
GO
alter table CheckComments add constraint fk_AddedBy_CC foreign key(UserId) references Users (UserId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_QF')
    alter table QualityFeedbacks drop constraint fk_ProjectId_QF
GO
alter table QualityFeedbacks add constraint fk_ProjectId_QF foreign key(ProjectId) references Projects (ProjectId)
GO
if exists (select * from sysobjects where name='fk_AddedBy_QF')
    alter table QualityFeedbacks drop constraint fk_AddedBy_QF
GO
alter table QualityFeedbacks add constraint fk_AddedBy_QF foreign key(UserId) references Users (UserId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_AF')
    alter table AfterSaleFeedbacks drop constraint fk_ProjectId_AF
GO
alter table AfterSaleFeedbacks add constraint fk_ProjectId_AF foreign key(ProjectId) references Projects (ProjectId)
GO
if exists (select * from sysobjects where name='fk_AddedBy_AF')
    alter table AfterSaleFeedbacks drop constraint fk_AddedBy_AF
GO
alter table AfterSaleFeedbacks add constraint fk_AddedBy_AF foreign key(UserId) references Users (UserId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_PL')
    alter table ProjectLearneds drop constraint fk_ProjectId_PL
GO
alter table ProjectLearneds add constraint fk_ProjectId_PL foreign key(ProjectId) references Projects (ProjectId)
GO
if exists (select * from sysobjects where name='fk_AddedBy_PL')
    alter table ProjectLearneds drop constraint fk_AddedBy_PL
GO
alter table ProjectLearneds add constraint fk_AddedBy_PL foreign key(UserId) references Users (UserId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_Drawings')
    alter table Drawings drop constraint fk_ProjectId_Drawings
GO
alter table Drawings add constraint fk_ProjectId_Drawings foreign key(ProjectId) references Projects (ProjectId)
GO

if exists (select * from sysobjects where name='fk_DrawingPlanId_ModuleTree')
    alter table ModuleTree drop constraint fk_DrawingPlanId_ModuleTree
GO
alter table ModuleTree add constraint fk_DrawingPlanId_ModuleTree foreign key(DrawingPlanId) references DrawingPlan (DrawingPlanId)
GO

if exists (select * from sysobjects where name='fk_CategoryId_ModuleTree')
    alter table ModuleTree drop constraint fk_CategoryId_ModuleTree
GO
alter table ModuleTree add constraint fk_CategoryId_ModuleTree foreign key(CategoryId) references Categories (CategoryId)
GO

if exists (select * from sysobjects where name='fk_ModuleTreeId_HoodCutList')
    alter table HoodCutList drop constraint fk_ModuleTreeId_HoodCutList
GO
alter table HoodCutList add constraint fk_ModuleTreeId_HoodCutList foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='fk_CategoryId_DXFCutList')
    alter table DXFCutList drop constraint fk_CategoryId_DXFCutList
GO
alter table DXFCutList add constraint fk_CategoryId_DXFCutList foreign key(CategoryId) references Categories (CategoryId)
GO
if exists (select * from sysobjects where name='fk_ProjectId_SubAssy')
    alter table SubAssy drop constraint fk_ProjectId_SubAssy
GO
alter table SubAssy add constraint fk_ProjectId_SubAssy foreign key(ProjectId) references Projects (ProjectId)
GO
if exists (select * from sysobjects where name='fk_SubAssyId_CeilingCutList')
    alter table CeilingCutList drop constraint fk_SubAssyId_CeilingCutList
GO
alter table CeilingCutList add constraint fk_SubAssyId_CeilingCutList foreign key(SubAssyId) references SubAssy (SubAssyId)
GO


if exists (select * from sysobjects where name='fk_ProjectId_CeilingPackingList')
    alter table CeilingPackingList drop constraint fk_ProjectId_CeilingPackingList
GO
alter table CeilingPackingList add constraint fk_ProjectId_CeilingPackingList foreign key(ProjectId) references Projects (ProjectId)
GO
if exists (select * from sysobjects where name='fk_AddedBy_CeilingPackingList')
    alter table CeilingPackingList drop constraint fk_AddedBy_CeilingPackingList
GO
alter table CeilingPackingList add constraint fk_AddedBy_CeilingPackingList foreign key(UserId) references Users (UserId)
GO



























--Hood模型表格约束
--UVI555
if exists (select * from sysobjects where name='pk_UVI555Id')
    alter table UVI555 drop constraint pk_UVI555Id
GO
alter table UVI555 add constraint pk_UVI555Id primary key (UVI555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVI555')
    alter table UVI555 drop constraint fk_ModuleTreeId_UVI555
GO
alter table UVI555 add constraint fk_ModuleTreeId_UVI555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVI555')
    alter table UVI555 drop constraint df_Height_UVI555
GO
alter table UVI555 add constraint df_Height_UVI555 default ('555') for Height
GO
--HWUVI650，华为1.2mm板材
if exists (select * from sysobjects where name='pk_HWUVI650Id')
    alter table HWUVI650 drop constraint pk_HWUVI650Id
GO
alter table HWUVI650 add constraint pk_HWUVI650Id primary key (HWUVI650Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_HWUVI650')
    alter table HWUVI650 drop constraint fk_ModuleTreeId_HWUVI650
GO
alter table HWUVI650 add constraint fk_ModuleTreeId_HWUVI650 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_HWUVI650')
    alter table HWUVI650 drop constraint df_Height_HWUVI650
GO
alter table HWUVI650 add constraint df_Height_HWUVI650 default ('650') for Height
GO


--UVIMR555
if exists (select * from sysobjects where name='pk_UVIMR555Id')
    alter table UVIMR555 drop constraint pk_UVIMR555Id
GO
alter table UVIMR555 add constraint pk_UVIMR555Id primary key (UVIMR555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVIMR555')
    alter table UVIMR555 drop constraint fk_ModuleTreeId_UVIMR555
GO
alter table UVIMR555 add constraint fk_ModuleTreeId_UVIMR555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVIMR555')
    alter table UVIMR555 drop constraint df_Height_UVIMR555
GO
alter table UVIMR555 add constraint df_Height_UVIMR555 default ('555') for Height
GO
--UVIMT555
if exists (select * from sysobjects where name='pk_UVIMT555Id')
    alter table UVIMT555 drop constraint pk_UVIMT555Id
GO
alter table UVIMT555 add constraint pk_UVIMT555Id primary key (UVIMT555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVIMT555')
    alter table UVIMT555 drop constraint fk_ModuleTreeId_UVIMT555
GO
alter table UVIMT555 add constraint fk_ModuleTreeId_UVIMT555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVIMT555')
    alter table UVIMT555 drop constraint df_Height_UVIMT555
GO
alter table UVIMT555 add constraint df_Height_UVIMT555 default ('555') for Height
GO
--UVIR555
if exists (select * from sysobjects where name='pk_UVIR555Id')
    alter table UVIR555 drop constraint pk_UVIR555Id
GO
alter table UVIR555 add constraint pk_UVIR555Id primary key (UVIR555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVIR555')
    alter table UVIR555 drop constraint fk_ModuleTreeId_UVIR555
GO
alter table UVIR555 add constraint fk_ModuleTreeId_UVIR555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVIR555')
    alter table UVIR555 drop constraint df_Height_UVIR555
GO
alter table UVIR555 add constraint df_Height_UVIR555 default ('555') for Height
GO
if exists (select * from sysobjects where name='df_SidePanel_UVIR555')
    alter table UVIR555 drop constraint df_SidePanel_UVIR555
GO
alter table UVIR555 add constraint df_SidePanel_UVIR555 default ('BOTH') for SidePanel
GO

--KVV555
if exists (select * from sysobjects where name='pk_KVV555Id')
    alter table KVV555 drop constraint pk_KVV555Id
GO
alter table KVV555 add constraint pk_KVV555Id primary key (KVV555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVV555')
    alter table KVV555 drop constraint fk_ModuleTreeId_KVV555
GO
alter table KVV555 add constraint fk_ModuleTreeId_KVV555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVV555')
    alter table KVV555 drop constraint df_Height_KVV555
GO
alter table KVV555 add constraint df_Height_KVV555 default ('555') for Height
GO
if exists (select * from sysobjects where name='df_SidePanel_KVV555')
    alter table KVV555 drop constraint df_SidePanel_KVV555
GO
alter table KVV555 add constraint df_SidePanel_KVV555 default ('BOTH') for SidePanel
GO


--CH610
if exists (select * from sysobjects where name='pk_CH610Id')
    alter table CH610 drop constraint pk_CH610Id
GO
alter table CH610 add constraint pk_CH610Id primary key (CH610Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_CH610')
    alter table CH610 drop constraint fk_ModuleTreeId_CH610
GO
alter table CH610 add constraint fk_ModuleTreeId_CH610 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_CH610')
    alter table CH610 drop constraint df_Height_CH610
GO
alter table CH610 add constraint df_Height_CH610 default ('610') for Height
GO
if exists (select * from sysobjects where name='df_SidePanel_CH610')
    alter table CH610 drop constraint df_SidePanel_CH610
GO
alter table CH610 add constraint df_SidePanel_CH610 default ('BOTH') for SidePanel
GO

select * from ch610

select * from KVI555
--KVI555
if exists (select * from sysobjects where name='pk_KVI555Id')
    alter table KVI555 drop constraint pk_KVI555Id
GO
alter table KVI555 add constraint pk_KVI555Id primary key (KVI555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVI555')
    alter table KVI555 drop constraint fk_ModuleTreeId_KVI555
GO
alter table KVI555 add constraint fk_ModuleTreeId_KVI555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVI555')
    alter table KVI555 drop constraint df_Height_KVI555
GO
alter table KVI555 add constraint df_Height_KVI555 default ('555') for Height
GO

--KVI400
if exists (select * from sysobjects where name='pk_KVI400Id')
    alter table KVI400 drop constraint pk_KVI400Id
GO
alter table KVI400 add constraint pk_KVI400Id primary key (KVI400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVI400')
    alter table KVI400 drop constraint fk_ModuleTreeId_KVI400
GO
alter table KVI400 add constraint fk_ModuleTreeId_KVI400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVI400')
    alter table KVI400 drop constraint df_Height_KVI400
GO
alter table KVI400 add constraint df_Height_KVI400 default ('400') for Height
GO

--UVI450300
if exists (select * from sysobjects where name='pk_UVI450300Id')
    alter table UVI450300 drop constraint pk_UVF450400Id
GO
alter table UVI450300 add constraint pk_UVF450400Id primary key (UVI450300Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVI450300')
    alter table UVI450300 drop constraint fk_ModuleTreeId_UVF450400
GO
alter table UVI450300 add constraint fk_ModuleTreeId_UVI450300 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVI450300')
    alter table UVI450300 drop constraint df_Height_UVI450300
GO
alter table UVI450300 add constraint df_Height_UVI450300 default ('450') for Height
GO
--KVI450300
if exists (select * from sysobjects where name='pk_KVI450300Id')
    alter table KVI450300 drop constraint pk_KVF450400Id
GO
alter table KVI450300 add constraint pk_KVF450400Id primary key (KVI450300Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVI450300')
    alter table KVI450300 drop constraint fk_ModuleTreeId_KVF450400
GO
alter table KVI450300 add constraint fk_ModuleTreeId_KVI450300 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVI450300')
    alter table KVI450300 drop constraint df_Height_KVI450300
GO
alter table KVI450300 add constraint df_Height_KVI450300 default ('450') for Height
GO


--KCHI555
if exists (select * from sysobjects where name='pk_KCHI555Id')
    alter table KCHI555 drop constraint pk_KCHI555Id
GO
alter table KCHI555 add constraint pk_KCHI555Id primary key (KCHI555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCHI555')
    alter table KCHI555 drop constraint fk_ModuleTreeId_KCHI555
GO
alter table KCHI555 add constraint fk_ModuleTreeId_KCHI555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KCHI555')
    alter table KCHI555 drop constraint df_Height_KCHI555
GO
alter table KCHI555 add constraint df_Height_KCHI555 default ('555') for Height
GO
--KVIMR555
if exists (select * from sysobjects where name='pk_KVIMR555Id')
    alter table KVIMR555 drop constraint pk_KVIMR555Id
GO
alter table KVIMR555 add constraint pk_KVIMR555Id primary key (KVIMR555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVIMR555')
    alter table KVIMR555 drop constraint fk_ModuleTreeId_KVIMR555
GO
alter table KVIMR555 add constraint fk_ModuleTreeId_KVIMR555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVIMR555')
    alter table KVIMR555 drop constraint df_Height_KVIMR555
GO
alter table KVIMR555 add constraint df_Height_KVIMR555 default ('555') for Height
GO

--KVIR555
if exists (select * from sysobjects where name='pk_KVIR555Id')
    alter table KVIR555 drop constraint pk_KVIR555Id
GO
alter table KVIR555 add constraint pk_KVIR555Id primary key (KVIR555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVIR555')
    alter table KVIR555 drop constraint fk_ModuleTreeId_KVIR555
GO
alter table KVIR555 add constraint fk_ModuleTreeId_KVIR555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVIR555')
    alter table KVIR555 drop constraint df_Height_KVIR555
GO
alter table KVIR555 add constraint df_Height_KVIR555 default ('555') for Height
GO
if exists (select * from sysobjects where name='df_SidePanel_KVIR555')
    alter table KVIR555 drop constraint df_SidePanel_KVIR555
GO
alter table KVIR555 add constraint df_SidePanel_KVIR555 default ('BOTH') for SidePanel
GO

--UVF555
if exists (select * from sysobjects where name='pk_UVF555Id')
    alter table UVF555 drop constraint pk_UVF555Id
GO
alter table UVF555 add constraint pk_UVF555Id primary key (UVF555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVF555')
    alter table UVF555 drop constraint fk_ModuleTreeId_UVF555
GO
alter table UVF555 add constraint fk_ModuleTreeId_UVF555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVF555')
    alter table UVF555 drop constraint df_Height_UVF555
GO
alter table UVF555 add constraint df_Height_UVF555 default ('555') for Height
GO

--法国烟罩FRUVF555
if exists (select * from sysobjects where name='pk_FRUVF555Id')
    alter table FRUVF555 drop constraint pk_FRUVF555Id
GO
alter table FRUVF555 add constraint pk_FRUVF555Id primary key (FRUVF555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_FRUVF555')
    alter table FRUVF555 drop constraint fk_ModuleTreeId_FRUVF555
GO
alter table FRUVF555 add constraint fk_ModuleTreeId_FRUVF555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_FRUVF555')
    alter table FRUVF555 drop constraint df_Height_FRUVF555
GO
alter table FRUVF555 add constraint df_Height_FRUVF555 default ('555') for Height
GO

--HWUVF650，华为1.2mm板材
if exists (select * from sysobjects where name='pk_HWUVF650Id')
    alter table HWUVF650 drop constraint pk_HWUVF650Id
GO
alter table HWUVF650 add constraint pk_HWUVF650Id primary key (HWUVF650Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_HWUVF650')
    alter table HWUVF650 drop constraint fk_ModuleTreeId_HWUVF650
GO
alter table HWUVF650 add constraint fk_ModuleTreeId_HWUVF650 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_HWUVF650')
    alter table HWUVF650 drop constraint df_Height_HWUVF650
GO
alter table HWUVF650 add constraint df_Height_HWUVF650 default ('650') for Height
GO




--UVF450
if exists (select * from sysobjects where name='pk_UVF450Id')
    alter table UVF450 drop constraint pk_UVF450Id
GO
alter table UVF450 add constraint pk_UVF450Id primary key (UVF450Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVF450')
    alter table UVF450 drop constraint fk_ModuleTreeId_UVF450
GO
alter table UVF450 add constraint fk_ModuleTreeId_UVF450 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVF450')
    alter table UVF450 drop constraint df_Height_UVF450
GO
alter table UVF450 add constraint df_Height_UVF450 default ('450') for Height
GO


--UVF555400
if exists (select * from sysobjects where name='pk_UVF555400Id')
    alter table UVF555400 drop constraint pk_UVF555400Id
GO
alter table UVF555400 add constraint pk_UVF555400Id primary key (UVF555400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVF555400')
    alter table UVF555400 drop constraint fk_ModuleTreeId_UVF555400
GO
alter table UVF555400 add constraint fk_ModuleTreeId_UVF555400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVF555400')
    alter table UVF555400 drop constraint df_Height_UVF555400
GO
alter table UVF555400 add constraint df_Height_UVF555400 default ('555') for Height
GO

--UVF450400
if exists (select * from sysobjects where name='pk_UVF450400Id')
    alter table UVF450400 drop constraint pk_UVF450400Id
GO
alter table UVF450400 add constraint pk_UVF450400Id primary key (UVF450400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UVF450400')
    alter table UVF450400 drop constraint fk_ModuleTreeId_UVF450400
GO
alter table UVF450400 add constraint fk_ModuleTreeId_UVF450400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UVF450400')
    alter table UVF450400 drop constraint df_Height_UVF450400
GO
alter table UVF450400 add constraint df_Height_UVF450400 default ('450') for Height
GO





--KVF555
if exists (select * from sysobjects where name='pk_KVF555Id')
    alter table KVF555 drop constraint pk_KVF555Id
GO
alter table KVF555 add constraint pk_KVF555Id primary key (KVF555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVF555')
    alter table KVF555 drop constraint fk_ModuleTreeId_KVF555
GO
alter table KVF555 add constraint fk_ModuleTreeId_KVF555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVF555')
    alter table KVF555 drop constraint df_Height_KVF555
GO
alter table KVF555 add constraint df_Height_KVF555 default ('555') for Height
GO


--KVF400
if exists (select * from sysobjects where name='pk_KVF400Id')
    alter table KVF400 drop constraint pk_KVF400Id
GO
alter table KVF400 add constraint pk_KVF400Id primary key (KVF400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVF400')
    alter table KVF400 drop constraint fk_ModuleTreeId_KVF400
GO
alter table KVF400 add constraint fk_ModuleTreeId_KVF400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVF400')
    alter table KVF400 drop constraint df_Height_KVF400
GO
alter table KVF400 add constraint df_Height_KVF400 default ('400') for Height
GO


--KVF555400
if exists (select * from sysobjects where name='pk_KVF555400Id')
    alter table KVF555400 drop constraint pk_KVF555400Id
GO
alter table KVF555400 add constraint pk_KVF555400Id primary key (KVF555400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVF555400')
    alter table KVF555400 drop constraint fk_ModuleTreeId_KVF555400
GO
alter table KVF555400 add constraint fk_ModuleTreeId_KVF555400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVF555400')
    alter table KVF555400 drop constraint df_Height_KVF555400
GO
alter table KVF555400 add constraint df_Height_KVF555400 default ('555') for Height
GO

--KVF450400
if exists (select * from sysobjects where name='pk_KVF450400Id')
    alter table KVF450400 drop constraint pk_KVF450400Id
GO
alter table KVF450400 add constraint pk_KVF450400Id primary key (KVF450400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVF450400')
    alter table KVF450400 drop constraint fk_ModuleTreeId_KVF450400
GO
alter table KVF450400 add constraint fk_ModuleTreeId_KVF450400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KVF450400')
    alter table KVF450400 drop constraint df_Height_KVF450400
GO
alter table KVF450400 add constraint df_Height_KVF450400 default ('450') for Height
GO



--KCHF555
if exists (select * from sysobjects where name='pk_KCHF555Id')
    alter table KCHF555 drop constraint pk_KCHF555Id
GO
alter table KCHF555 add constraint pk_KCHF555Id primary key (KCHF555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCHF555')
    alter table KCHF555 drop constraint fk_ModuleTreeId_KCHF555
GO
alter table KCHF555 add constraint fk_ModuleTreeId_KCHF555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KCHF555')
    alter table KCHF555 drop constraint df_Height_KCHF555
GO
alter table KCHF555 add constraint df_Height_KCHF555 default ('555') for Height
GO

--UWF555
if exists (select * from sysobjects where name='pk_UWF555Id')
    alter table UWF555 drop constraint pk_UWF555Id
GO
alter table UWF555 add constraint pk_UWF555Id primary key (UWF555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UWF555')
    alter table UWF555 drop constraint fk_ModuleTreeId_UWF555
GO
alter table UWF555 add constraint fk_ModuleTreeId_UWF555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UWF555')
    alter table UWF555 drop constraint df_Height_UWF555
GO
alter table UWF555 add constraint df_Height_UWF555 default ('555') for Height
GO
--UWF555400
if exists (select * from sysobjects where name='pk_UWF555400Id')
    alter table UWF555400 drop constraint pk_UWF555400Id
GO
alter table UWF555400 add constraint pk_UWF555400Id primary key (UWF555400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UWF555400')
    alter table UWF555400 drop constraint fk_ModuleTreeId_UWF555400
GO
alter table UWF555400 add constraint fk_ModuleTreeId_UWF555400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UWF555400')
    alter table UWF555400 drop constraint df_Height_UWF555400
GO
alter table UWF555400 add constraint df_Height_UWF555400 default ('555') for Height
GO

--HWUWF555，华为1.2mm板材
if exists (select * from sysobjects where name='pk_HWUWF555Id')
    alter table HWUWF555 drop constraint pk_HWUWF555Id
GO
alter table HWUWF555 add constraint pk_HWUWF555Id primary key (HWUWF555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_HWUWF555')
    alter table HWUWF555 drop constraint fk_ModuleTreeId_HWUWF555
GO
alter table HWUWF555 add constraint fk_ModuleTreeId_HWUWF555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_HWUWF555')
    alter table HWUWF555 drop constraint df_Height_HWUWF555
GO
alter table HWUWF555 add constraint df_Height_HWUWF555 default ('555') for Height
GO

--HWUWF555400，华为1.2mm板材
if exists (select * from sysobjects where name='pk_HWUWF555400Id')
    alter table HWUWF555400 drop constraint pk_HWUWF555400Id
GO
alter table HWUWF555400 add constraint pk_HWUWF555400Id primary key (HWUWF555400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_HWUWF555400')
    alter table HWUWF555400 drop constraint fk_ModuleTreeId_HWUWF555400
GO
alter table HWUWF555400 add constraint fk_ModuleTreeId_HWUWF555400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_HWUWF555400')
    alter table HWUWF555400 drop constraint df_Height_HWUWF555400
GO
alter table HWUWF555400 add constraint df_Height_HWUWF555400 default ('555') for Height
GO


--UWI555
if exists (select * from sysobjects where name='pk_UWI555Id')
    alter table UWI555 drop constraint pk_UWI555Id
GO
alter table UWI555 add constraint pk_UWI555Id primary key (UWI555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UWI555')
    alter table UWI555 drop constraint fk_ModuleTreeId_UWI555
GO
alter table UWI555 add constraint fk_ModuleTreeId_UWI555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_UWI555')
    alter table UWI555 drop constraint df_Height_UWI555
GO
alter table UWI555 add constraint df_Height_UWI555 default ('555') for Height
GO
--KWF555
if exists (select * from sysobjects where name='pk_KWF555Id')
    alter table KWF555 drop constraint pk_KWF555Id
GO
alter table KWF555 add constraint pk_KWF555Id primary key (KWF555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KWF555')
    alter table KWF555 drop constraint fk_ModuleTreeId_KWF555
GO
alter table KWF555 add constraint fk_ModuleTreeId_KWF555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KWF555')
    alter table KWF555 drop constraint df_Height_KWF555
GO
alter table KWF555 add constraint df_Height_KWF555 default ('555') for Height
GO

--KWI555
if exists (select * from sysobjects where name='pk_KWI555Id')
    alter table KWI555 drop constraint pk_KWI555Id
GO
alter table KWI555 add constraint pk_KWI555Id primary key (KWI555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KWI555')
    alter table KWI555 drop constraint fk_ModuleTreeId_KWI555
GO
alter table KWI555 add constraint fk_ModuleTreeId_KWI555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_KWI555')
    alter table KWI555 drop constraint df_Height_KWI555
GO
alter table KWI555 add constraint df_Height_KWI555 default ('555') for Height
GO
--CMODI555
if exists (select * from sysobjects where name='pk_CMODI555Id')
    alter table CMODI555 drop constraint pk_CMODI555Id
GO
alter table CMODI555 add constraint pk_CMODI555Id primary key (CMODI555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_CMODI555')
    alter table CMODI555 drop constraint fk_ModuleTreeId_CMODI555
GO
alter table CMODI555 add constraint fk_ModuleTreeId_CMODI555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_CMODI555')
    alter table CMODI555 drop constraint df_Height_CMODI555
GO
alter table CMODI555 add constraint df_Height_CMODI555 default ('555') for Height
GO
--CMODF555
if exists (select * from sysobjects where name='pk_CMODF555Id')
    alter table CMODF555 drop constraint pk_CMODF555Id
GO
alter table CMODF555 add constraint pk_CMODF555Id primary key (CMODF555Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_CMODF555')
    alter table CMODF555 drop constraint fk_ModuleTreeId_CMODF555
GO
alter table CMODF555 add constraint fk_ModuleTreeId_CMODF555 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_CMODF555')
    alter table CMODF555 drop constraint df_Height_CMODF555
GO
alter table CMODF555 add constraint df_Height_CMODF555 default ('555') for Height
GO

--CMODF555400
if exists (select * from sysobjects where name='pk_CMODF555400Id')
    alter table CMODF555400 drop constraint pk_CMODF555400Id
GO
alter table CMODF555400 add constraint pk_CMODF555400Id primary key (CMODF555400Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_CMODF555400')
    alter table CMODF555400 drop constraint fk_ModuleTreeId_CMODF555400
GO
alter table CMODF555400 add constraint fk_ModuleTreeId_CMODF555400 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Height_CMODF555400')
    alter table CMODF555400 drop constraint df_Height_CMODF555400
GO
alter table CMODF555400 add constraint df_Height_CMODF555400 default ('555') for Height
GO

--KVS
if exists (select * from sysobjects where name='pk_KVSId')
    alter table KVS drop constraint pk_KVSId
GO
alter table KVS add constraint pk_KVSId primary key (KVSId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KVS')
    alter table KVS drop constraint fk_ModuleTreeId_KVS
GO
alter table KVS add constraint fk_ModuleTreeId_KVS foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_SidePanel_KVS')
    alter table KVS drop constraint df_SidePanel_KVS
GO
alter table KVS add constraint df_SidePanel_KVS default ('BOTH') for SidePanel
GO
if exists (select * from sysobjects where name='df_Height_KVS')
    alter table KVS drop constraint df_Height_KVS
GO
alter table KVS add constraint df_Height_KVS default ('555') for Height
GO
--LSDOST
if exists (select * from sysobjects where name='pk_LSDOSTId')
    alter table LSDOST drop constraint pk_LSDOSTId
GO
alter table LSDOST add constraint pk_LSDOSTId primary key (LSDOSTId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LSDOST')
    alter table LSDOST drop constraint fk_ModuleTreeId_LSDOST
GO
alter table LSDOST add constraint fk_ModuleTreeId_LSDOST foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Deepth_LSDOST')
    alter table LSDOST drop constraint df_Deepth_LSDOST
GO
alter table LSDOST add constraint df_Deepth_LSDOST default (350) for Deepth
GO
if exists (select * from sysobjects where name='df_SidePanel_LSDOST')
    alter table LSDOST drop constraint df_SidePanel_LSDOST
GO
alter table LSDOST add constraint df_SidePanel_LSDOST default ('BOTH') for SidePanel
GO
if exists (select * from sysobjects where name='df_Height_LSDOST')
    alter table LSDOST drop constraint df_Height_LSDOST
GO
alter table LSDOST add constraint df_Height_LSDOST default ('285') for Height
GO

--HOODBCJ
if exists (select * from sysobjects where name='pk_HOODBCJId')
    alter table HOODBCJ drop constraint pk_HOODBCJId
GO
alter table HOODBCJ add constraint pk_HOODBCJId primary key (HOODBCJId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_HOODBCJ')
    alter table HOODBCJ drop constraint fk_ModuleTreeId_HOODBCJ
GO
alter table HOODBCJ add constraint fk_ModuleTreeId_HOODBCJ foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--ABD200
if exists (select * from sysobjects where name='pk_ABD200Id')
    alter table ABD200 drop constraint pk_ABD200Id
GO
alter table ABD200 add constraint pk_ABD200Id primary key (ABD200Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_ABD200')
    alter table ABD200 drop constraint fk_ModuleTreeId_ABD200
GO
alter table ABD200 add constraint fk_ModuleTreeId_ABD200 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Deepth_ABD200')
    alter table ABD200 drop constraint df_Deepth_ABD200
GO
alter table ABD200 add constraint df_Deepth_ABD200 default (300) for Deepth
GO
if exists (select * from sysobjects where name='df_SidePanel_ABD200')
    alter table ABD200 drop constraint df_SidePanel_ABD200
GO
alter table ABD200 add constraint df_SidePanel_ABD200 default ('BOTH') for SidePanel
GO
if exists (select * from sysobjects where name='df_Height_ABD200')
    alter table ABD200 drop constraint df_Height_ABD200
GO
alter table ABD200 add constraint df_Height_ABD200 default ('200') for Height
GO
--ABD300
if exists (select * from sysobjects where name='pk_ABD300Id')
    alter table ABD300 drop constraint pk_ABD300Id
GO
alter table ABD300 add constraint pk_ABD300Id primary key (ABD300Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_ABD300')
    alter table ABD300 drop constraint fk_ModuleTreeId_ABD300
GO
alter table ABD300 add constraint fk_ModuleTreeId_ABD300 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Deepth_ABD300')
    alter table ABD300 drop constraint df_Deepth_ABD300
GO
alter table ABD300 add constraint df_Deepth_ABD300 default (300) for Deepth
GO
if exists (select * from sysobjects where name='df_SidePanel_ABD300')
    alter table ABD300 drop constraint df_SidePanel_ABD300
GO
alter table ABD300 add constraint df_SidePanel_ABD300 default ('BOTH') for SidePanel
GO
if exists (select * from sysobjects where name='df_Height_ABD300')
    alter table ABD300 drop constraint df_Height_ABD300
GO
alter table ABD300 add constraint df_Height_ABD300 default ('300') for Height
GO

















































































































--Ceiling模型表格约束
--KCJSB265
if exists (select * from sysobjects where name='pk_KCJSB265Id')
    alter table KCJSB265 drop constraint pk_KCJSB265Id
GO
alter table KCJSB265 add constraint pk_KCJSB265Id primary key (KCJSB265Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCJSB265')
    alter table KCJSB265 drop constraint fk_ModuleTreeId_KCJSB265
GO
alter table KCJSB265 add constraint fk_ModuleTreeId_KCJSB265 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--KCJSB290
if exists (select * from sysobjects where name='pk_KCJSB290Id')
    alter table KCJSB290 drop constraint pk_KCJSB290Id
GO
alter table KCJSB290 add constraint pk_KCJSB290Id primary key (KCJSB290Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCJSB290')
    alter table KCJSB290 drop constraint fk_ModuleTreeId_KCJSB290
GO
alter table KCJSB290 add constraint fk_ModuleTreeId_KCJSB290 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--KCJSB535
if exists (select * from sysobjects where name='pk_KCJSB535Id')
    alter table KCJSB535 drop constraint pk_KCJSB535Id
GO
alter table KCJSB535 add constraint pk_KCJSB535Id primary key (KCJSB535Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCJSB535')
    alter table KCJSB535 drop constraint fk_ModuleTreeId_KCJSB535
GO
alter table KCJSB535 add constraint fk_ModuleTreeId_KCJSB535 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--KCJDB800
if exists (select * from sysobjects where name='pk_KCJDB800Id')
    alter table KCJDB800 drop constraint pk_KCJDB800Id
GO
alter table KCJDB800 add constraint pk_KCJDB800Id primary key (KCJDB800Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCJDB800')
    alter table KCJDB800 drop constraint fk_ModuleTreeId_KCJDB800
GO
alter table KCJDB800 add constraint fk_ModuleTreeId_KCJDB800 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--UCJSB385
if exists (select * from sysobjects where name='pk_UCJSB385Id')
    alter table UCJSB385 drop constraint pk_UCJSB385Id
GO
alter table UCJSB385 add constraint pk_UCJSB385Id primary key (UCJSB385Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCJSB385')
    alter table UCJSB385 drop constraint fk_ModuleTreeId_UCJSB385
GO
alter table UCJSB385 add constraint fk_ModuleTreeId_UCJSB385 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--UCJSB535
if exists (select * from sysobjects where name='pk_UCJSB535Id')
    alter table UCJSB535 drop constraint pk_UCJSB535Id
GO
alter table UCJSB535 add constraint pk_UCJSB535Id primary key (UCJSB535Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCJSB535')
    alter table UCJSB535 drop constraint fk_ModuleTreeId_UCJSB535
GO
alter table UCJSB535 add constraint fk_ModuleTreeId_UCJSB535 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--UCJSB535
if exists (select * from sysobjects where name='pk_UCJDB800Id')
    alter table UCJDB800 drop constraint pk_UCJDB800Id
GO
alter table UCJDB800 add constraint pk_UCJDB800Id primary key (UCJDB800Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCJDB800')
    alter table UCJDB800 drop constraint fk_ModuleTreeId_UCJDB800
GO
alter table UCJDB800 add constraint fk_ModuleTreeId_UCJDB800 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--KCWSB265
if exists (select * from sysobjects where name='pk_KCWSB265Id')
    alter table KCWSB265 drop constraint pk_KCWSB265Id
GO
alter table KCWSB265 add constraint pk_KCWSB265Id primary key (KCWSB265Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCWSB265')
    alter table KCWSB265 drop constraint fk_ModuleTreeId_KCWSB265
GO
alter table KCWSB265 add constraint fk_ModuleTreeId_KCWSB265 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--KCWSB535
if exists (select * from sysobjects where name='pk_KCWSB535Id')
    alter table KCWSB535 drop constraint pk_KCWSB535Id
GO
alter table KCWSB535 add constraint pk_KCWSB535Id primary key (KCWSB535Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCWSB535')
    alter table KCWSB535 drop constraint fk_ModuleTreeId_KCWSB535
GO
alter table KCWSB535 add constraint fk_ModuleTreeId_KCWSB535 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--KCWDB800
if exists (select * from sysobjects where name='pk_KCWDB800Id')
    alter table KCWDB800 drop constraint pk_KCWDB800Id
GO
alter table KCWDB800 add constraint pk_KCWDB800Id primary key (KCWDB800Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_KCWDB800')
    alter table KCWDB800 drop constraint fk_ModuleTreeId_KCWDB800
GO
alter table KCWDB800 add constraint fk_ModuleTreeId_KCWDB800 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--UCWSB535
if exists (select * from sysobjects where name='pk_UCWSB535Id')
    alter table UCWSB535 drop constraint pk_UCWSB535Id
GO
alter table UCWSB535 add constraint pk_UCWSB535Id primary key (UCWSB535Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCWSB535')
    alter table UCWSB535 drop constraint fk_ModuleTreeId_UCWSB535
GO
alter table UCWSB535 add constraint fk_ModuleTreeId_UCWSB535 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--UCWDB800
if exists (select * from sysobjects where name='pk_UCWDB800Id')
    alter table UCWDB800 drop constraint pk_UCWDB800Id
GO
alter table UCWDB800 add constraint pk_UCWDB800Id primary key (UCWDB800Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCWDB800')
    alter table UCWDB800 drop constraint fk_ModuleTreeId_UCWDB800
GO
alter table UCWDB800 add constraint fk_ModuleTreeId_UCWDB800 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LFUSA
if exists (select * from sysobjects where name='pk_LFUSAId')
    alter table LFUSA drop constraint pk_LFUSAId
GO
alter table LFUSA add constraint pk_LFUSAId primary key (LFUSAId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUSA')
    alter table LFUSA drop constraint fk_ModuleTreeId_LFUSA
GO
alter table LFUSA add constraint fk_ModuleTreeId_LFUSA foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--LFUP
if exists (select * from sysobjects where name='pk_LFUPId')
    alter table LFUP drop constraint pk_LFUPId
GO
alter table LFUP add constraint pk_LFUPId primary key (LFUPId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUP')
    alter table LFUP drop constraint fk_ModuleTreeId_LFUP
GO
alter table LFUP add constraint fk_ModuleTreeId_LFUP foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--LFUSC
if exists (select * from sysobjects where name='pk_LFUSCId')
    alter table LFUSC drop constraint pk_LFUSCId
GO
alter table LFUSC add constraint pk_LFUSCId primary key (LFUSCId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUSC')
    alter table LFUSC drop constraint fk_ModuleTreeId_LFUSC
GO
alter table LFUSC add constraint fk_ModuleTreeId_LFUSC foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--LFUSS
if exists (select * from sysobjects where name='pk_LFUSSId')
    alter table LFUSS drop constraint pk_LFUSSId
GO
alter table LFUSS add constraint pk_LFUSSId primary key (LFUSSId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUSS')
    alter table LFUSS drop constraint fk_ModuleTreeId_LFUSS
GO
alter table LFUSS add constraint fk_ModuleTreeId_LFUSS foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--AN
if exists (select * from sysobjects where name='pk_ANId')
    alter table AN drop constraint pk_ANId
GO
alter table AN add constraint pk_ANId primary key (ANId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_AN')
    alter table AN drop constraint fk_ModuleTreeId_AN
GO
alter table AN add constraint fk_ModuleTreeId_AN foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--BF200
if exists (select * from sysobjects where name='pk_BF200Id')
    alter table BF200 drop constraint pk_BF200Id
GO
alter table BF200 add constraint pk_BF200Id primary key (BF200Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_BF200')
    alter table BF200 drop constraint fk_ModuleTreeId_BF200
GO
alter table BF200 add constraint fk_ModuleTreeId_BF200 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--SSPDOME
if exists (select * from sysobjects where name='pk_SSPDOMEId')
    alter table SSPDOME drop constraint pk_SSPDOMEId
GO
alter table SSPDOME add constraint pk_SSPDOMEId primary key (SSPDOMEId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_SSPDOME')
    alter table SSPDOME drop constraint fk_ModuleTreeId_SSPDOME
GO
alter table SSPDOME add constraint fk_ModuleTreeId_SSPDOME foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--SSPTBD
if exists (select * from sysobjects where name='pk_SSPTBDId')
    alter table SSPTBD drop constraint pk_SSPTBDId
GO
alter table SSPTBD add constraint pk_SSPTBDId primary key (SSPTBDId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_SSPTBD')
    alter table SSPTBD drop constraint fk_ModuleTreeId_SSPTBD
GO
alter table SSPTBD add constraint fk_ModuleTreeId_SSPTBD foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO


--SSPTSD
if exists (select * from sysobjects where name='pk_SSPTSDId')
    alter table SSPTSD drop constraint pk_SSPTSDId
GO
alter table SSPTSD add constraint pk_SSPTSDId primary key (SSPTSDId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_SSPTSD')
    alter table SSPTSD drop constraint fk_ModuleTreeId_SSPTSD
GO
alter table SSPTSD add constraint fk_ModuleTreeId_SSPTSD foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--SSPHFD
if exists (select * from sysobjects where name='pk_SSPHFDId')
    alter table SSPHFD drop constraint pk_SSPHFDId
GO
alter table SSPHFD add constraint pk_SSPHFDId primary key (SSPHFDId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_SSPHFD')
    alter table SSPHFD drop constraint fk_ModuleTreeId_SSPHFD
GO
alter table SSPHFD add constraint fk_ModuleTreeId_SSPHFD foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO


--SSPFLAT
if exists (select * from sysobjects where name='pk_SSPFLATId')
    alter table SSPFLAT drop constraint pk_SSPFLATId
GO
alter table SSPFLAT add constraint pk_SSPFLATId primary key (SSPFLATId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_SSPFLAT')
    alter table SSPFLAT drop constraint fk_ModuleTreeId_SSPFLAT
GO
alter table SSPFLAT add constraint fk_ModuleTreeId_SSPFLAT foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--LKS270
if exists (select * from sysobjects where name='pk_LKS270Id')
    alter table LKS270 drop constraint pk_LKS270Id
GO
alter table LKS270 add constraint pk_LKS270Id primary key (LKS270Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LKS270')
    alter table LKS270 drop constraint fk_ModuleTreeId_LKS270
GO
alter table LKS270 add constraint fk_ModuleTreeId_LKS270 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--LKST270
if exists (select * from sysobjects where name='pk_LKST270Id')
    alter table LKST270 drop constraint pk_LKST270Id
GO
alter table LKST270 add constraint pk_LKST270Id primary key (LKST270Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LKST270')
    alter table LKST270 drop constraint fk_ModuleTreeId_LKST270
GO
alter table LKST270 add constraint fk_ModuleTreeId_LKST270 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LKSSPEC
if exists (select * from sysobjects where name='pk_LKSSPECId')
    alter table LKSSPEC drop constraint pk_LKSSPECId
GO
alter table LKSSPEC add constraint pk_LKSSPECId primary key (LKSSPECId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LKSSPEC')
    alter table LKSSPEC drop constraint fk_ModuleTreeId_LKSSPEC
GO
alter table LKSSPEC add constraint fk_ModuleTreeId_LKSSPEC foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LKS258HCL
if exists (select * from sysobjects where name='pk_LKS258HCLId')
    alter table LKS258HCL drop constraint pk_LKS258HCLId
GO
alter table LKS258HCL add constraint pk_LKS258HCLId primary key (LKS258HCLId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LKS258HCL')
    alter table LKS258HCL drop constraint fk_ModuleTreeId_LKS258HCL
GO
alter table LKS258HCL add constraint fk_ModuleTreeId_LKS258HCL foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LKS270HCL
if exists (select * from sysobjects where name='pk_LKS270HCLId')
    alter table LKS270HCL drop constraint pk_LKS270HCLId
GO
alter table LKS270HCL add constraint pk_LKS270HCLId primary key (LKS270HCLId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LKS270HCL')
    alter table LKS270HCL drop constraint fk_ModuleTreeId_LKS270HCL
GO
alter table LKS270HCL add constraint fk_ModuleTreeId_LKS270HCL foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO


--LLEDS
if exists (select * from sysobjects where name='pk_LLEDSId')
    alter table LLEDS drop constraint pk_LLEDSId
GO
alter table LLEDS add constraint pk_LLEDSId primary key (LLEDSId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LLEDS')
    alter table LLEDS drop constraint fk_ModuleTreeId_LLEDS
GO
alter table LLEDS add constraint fk_ModuleTreeId_LLEDS foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LLKS
if exists (select * from sysobjects where name='pk_LLKSId')
    alter table LLKS drop constraint pk_LLKSId
GO
alter table LLKS add constraint pk_LLKSId primary key (LLKSId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LLKS')
    alter table LLKS drop constraint fk_ModuleTreeId_LLKS
GO
alter table LLKS add constraint fk_ModuleTreeId_LLKS foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LLKSJ
if exists (select * from sysobjects where name='pk_LLKSJId')
    alter table LLKSJ drop constraint pk_LLKSJId
GO
alter table LLKSJ add constraint pk_LLKSJId primary key (LLKSJId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LLKSJ')
    alter table LLKSJ drop constraint fk_ModuleTreeId_LLKSJ
GO
alter table LLKSJ add constraint fk_ModuleTreeId_LLKSJ foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LPZ
if exists (select * from sysobjects where name='pk_LPZId')
    alter table LPZ drop constraint pk_LPZId
GO
alter table LPZ add constraint pk_LPZId primary key (LPZId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LPZ')
    alter table LPZ drop constraint fk_ModuleTreeId_LPZ
GO
alter table LPZ add constraint fk_ModuleTreeId_LPZ foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LKA258
if exists (select * from sysobjects where name='pk_LKA258Id')
    alter table LKA258 drop constraint pk_LKA258Id
GO
alter table LKA258 add constraint pk_LKA258Id primary key (LKA258Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LKA258')
    alter table LKA258 drop constraint fk_ModuleTreeId_LKA258
GO
alter table LKA258 add constraint fk_ModuleTreeId_LKA258 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LKASPEC
if exists (select * from sysobjects where name='pk_LKASPECId')
    alter table LKASPEC drop constraint pk_LKASPECId
GO
alter table LKASPEC add constraint pk_LKASPECId primary key (LKASPECId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LKASPEC')
    alter table LKASPEC drop constraint fk_ModuleTreeId_LKASPEC
GO
alter table LKASPEC add constraint fk_ModuleTreeId_LKASPEC foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LLEDA
if exists (select * from sysobjects where name='pk_LLEDAId')
    alter table LLEDA drop constraint pk_LLEDAId
GO
alter table LLEDA add constraint pk_LLEDAId primary key (LLEDAId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LLEDA')
    alter table LLEDA drop constraint fk_ModuleTreeId_LLEDA
GO
alter table LLEDA add constraint fk_ModuleTreeId_LLEDA foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LLKA
if exists (select * from sysobjects where name='pk_LLKAId')
    alter table LLKA drop constraint pk_LLKAId
GO
alter table LLKA add constraint pk_LLKAId primary key (LLKAId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LLKA')
    alter table LLKA drop constraint fk_ModuleTreeId_LLKA
GO
alter table LLKA add constraint fk_ModuleTreeId_LLKA foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LLKAJ
if exists (select * from sysobjects where name='pk_LLKAJId')
    alter table LLKAJ drop constraint pk_LLKAJId
GO
alter table LLKAJ add constraint pk_LLKAJId primary key (LLKAJId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LLKAJ')
    alter table LLKAJ drop constraint fk_ModuleTreeId_LLKAJ
GO
alter table LLKAJ add constraint fk_ModuleTreeId_LLKAJ foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--INF
if exists (select * from sysobjects where name='pk_INFId')
    alter table INF drop constraint pk_INFId
GO
alter table INF add constraint pk_INFId primary key (INFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_INF')
    alter table INF drop constraint fk_ModuleTreeId_INF
GO
alter table INF add constraint fk_ModuleTreeId_INF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--CJ300
if exists (select * from sysobjects where name='pk_CJ300Id')
    alter table CJ300 drop constraint pk_CJ300Id
GO
alter table CJ300 add constraint pk_CJ300Id primary key (CJ300Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_CJ300')
    alter table CJ300 drop constraint fk_ModuleTreeId_CJ300
GO
alter table CJ300 add constraint fk_ModuleTreeId_CJ300 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--CJ330
if exists (select * from sysobjects where name='pk_CJ330Id')
    alter table CJ330 drop constraint pk_CJ330Id
GO
alter table CJ330 add constraint pk_CJ330Id primary key (CJ330Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_CJ330')
    alter table CJ330 drop constraint fk_ModuleTreeId_CJ330
GO
alter table CJ330 add constraint fk_ModuleTreeId_CJ330 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--BCJ300
if exists (select * from sysobjects where name='pk_BCJ300Id')
    alter table BCJ300 drop constraint pk_BCJ300Id
GO
alter table BCJ300 add constraint pk_BCJ300Id primary key (BCJ300Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_BCJ300')
    alter table BCJ300 drop constraint fk_ModuleTreeId_BCJ300
GO
alter table BCJ300 add constraint fk_ModuleTreeId_BCJ300 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--BCJ330
if exists (select * from sysobjects where name='pk_BCJ330Id')
    alter table BCJ330 drop constraint pk_BCJ330Id
GO
alter table BCJ330 add constraint pk_BCJ330Id primary key (BCJ330Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_BCJ330')
    alter table BCJ330 drop constraint fk_ModuleTreeId_BCJ330
GO
alter table BCJ330 add constraint fk_ModuleTreeId_BCJ330 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--NOCJ300
if exists (select * from sysobjects where name='pk_NOCJ300Id')
    alter table NOCJ300 drop constraint pk_NOCJ300Id
GO
alter table NOCJ300 add constraint pk_NOCJ300Id primary key (NOCJ300Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_NOCJ300')
    alter table NOCJ300 drop constraint fk_ModuleTreeId_NOCJ300
GO
alter table NOCJ300 add constraint fk_ModuleTreeId_NOCJ300 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--NOCJ330
if exists (select * from sysobjects where name='pk_NOCJ330Id')
    alter table NOCJ330 drop constraint pk_NOCJ330Id
GO
alter table NOCJ330 add constraint pk_NOCJ330Id primary key (NOCJ330Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_NOCJ330')
    alter table NOCJ330 drop constraint fk_ModuleTreeId_NOCJ330
GO
alter table NOCJ330 add constraint fk_ModuleTreeId_NOCJ330 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--NOCJ340
if exists (select * from sysobjects where name='pk_NOCJ340Id')
    alter table NOCJ340 drop constraint pk_NOCJ340Id
GO
alter table NOCJ340 add constraint pk_NOCJ340Id primary key (NOCJ340Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_NOCJ340')
    alter table NOCJ340 drop constraint fk_ModuleTreeId_NOCJ340
GO
alter table NOCJ340 add constraint fk_ModuleTreeId_NOCJ340 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--NOCJSPEC
if exists (select * from sysobjects where name='pk_NOCJSPECId')
    alter table NOCJSPEC drop constraint pk_NOCJSPECId
GO
alter table NOCJSPEC add constraint pk_NOCJSPECId primary key (NOCJSPECId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_NOCJSPEC')
    alter table NOCJSPEC drop constraint fk_ModuleTreeId_NOCJSPEC
GO
alter table NOCJSPEC add constraint fk_ModuleTreeId_NOCJSPEC foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--DP330
if exists (select * from sysobjects where name='pk_DP330Id')
    alter table DP330 drop constraint pk_DP330Id
GO
alter table DP330 add constraint pk_DP330Id primary key (DP330Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_DP330')
    alter table DP330 drop constraint fk_ModuleTreeId_DP330
GO
alter table DP330 add constraint fk_ModuleTreeId_DP330 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--DP340
if exists (select * from sysobjects where name='pk_DP340Id')
    alter table DP340 drop constraint pk_DP340Id
GO
alter table DP340 add constraint pk_DP340Id primary key (DP340Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_DP340')
    alter table DP340 drop constraint fk_ModuleTreeId_DP340
GO
alter table DP340 add constraint fk_ModuleTreeId_DP340 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
--DPCJ330
if exists (select * from sysobjects where name='pk_DPCJ330Id')
    alter table DPCJ330 drop constraint pk_DPCJ330Id
GO
alter table DPCJ330 add constraint pk_DPCJ330Id primary key (DPCJ330Id)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_DPCJ330')
    alter table DPCJ330 drop constraint fk_ModuleTreeId_DPCJ330
GO
alter table DPCJ330 add constraint fk_ModuleTreeId_DPCJ330 foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO











































--配件，DXF图纸

--UCPDXF
if exists (select * from sysobjects where name='pk_UCPDXFId')
    alter table UCPDXF drop constraint pk_UCPDXFId
GO
alter table UCPDXF add constraint pk_UCPDXFId primary key (UCPDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCPDXF')
    alter table UCPDXF drop constraint fk_ModuleTreeId_UCPDXF
GO
alter table UCPDXF add constraint fk_ModuleTreeId_UCPDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Length_UCPDXF')
    alter table UCPDXF drop constraint df_Length_UCPDXF
GO
alter table UCPDXF add constraint df_Length_UCPDXF default (400) for Length
GO
if exists (select * from sysobjects where name='df_Deepth_UCPDXF')
    alter table UCPDXF drop constraint df_Deepth_UCPDXF
GO
alter table UCPDXF add constraint df_Deepth_UCPDXF default (185) for Deepth
GO
if exists (select * from sysobjects where name='df_SidePanel_UCPDXF')
    alter table UCPDXF drop constraint df_SidePanel_UCPDXF
GO
alter table UCPDXF add constraint df_SidePanel_UCPDXF default ('BOTH') for SidePanel
GO
if exists (select * from sysobjects where name='df_Height_UCPDXF')
    alter table UCPDXF drop constraint df_Height_UCPDXF
GO
alter table UCPDXF add constraint df_Height_UCPDXF default ('350') for Height
GO

--MCPDXF
if exists (select * from sysobjects where name='pk_MCPDXFId')
    alter table MCPDXF drop constraint pk_MCPDXFId
GO
alter table MCPDXF add constraint pk_MCPDXFId primary key (MCPDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_MCPDXF')
    alter table MCPDXF drop constraint fk_ModuleTreeId_MCPDXF
GO
alter table MCPDXF add constraint fk_ModuleTreeId_MCPDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO
if exists (select * from sysobjects where name='df_Length_MCPDXF')
    alter table MCPDXF drop constraint df_Length_MCPDXF
GO
alter table MCPDXF add constraint df_Length_MCPDXF default (380) for Length
GO
if exists (select * from sysobjects where name='df_Deepth_MCPDXF')
    alter table MCPDXF drop constraint df_Deepth_MCPDXF
GO
alter table MCPDXF add constraint df_Deepth_MCPDXF default (205) for Deepth
GO
if exists (select * from sysobjects where name='df_SidePanel_MCPDXF')
    alter table MCPDXF drop constraint df_SidePanel_MCPDXF
GO
alter table MCPDXF add constraint df_SidePanel_MCPDXF default ('BOTH') for SidePanel
GO
if exists (select * from sysobjects where name='df_Height_MCPDXF')
    alter table MCPDXF drop constraint df_Height_MCPDXF
GO
alter table MCPDXF add constraint df_Height_MCPDXF default ('610') for Height
GO

--MU1BOXDXF
if exists (select * from sysobjects where name='pk_MU1BOXDXFId')
    alter table MU1BOXDXF drop constraint pk_MU1BOXDXFId
GO
alter table MU1BOXDXF add constraint pk_MU1BOXDXFId primary key (MU1BOXDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_MU1BOXDXF')
    alter table MU1BOXDXF drop constraint fk_ModuleTreeId_MU1BOXDXF
GO
alter table MU1BOXDXF add constraint fk_ModuleTreeId_MU1BOXDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--TCSBOXDXF
if exists (select * from sysobjects where name='pk_TCSBOXDXFId')
    alter table TCSBOXDXF drop constraint pk_TCSBOXDXFId
GO
alter table TCSBOXDXF add constraint pk_TCSBOXDXFId primary key (TCSBOXDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_TCSBOXDXF')
    alter table TCSBOXDXF drop constraint fk_ModuleTreeId_TCSBOXDXF
GO
alter table TCSBOXDXF add constraint fk_ModuleTreeId_TCSBOXDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO



--LFUMC150DXF
if exists (select * from sysobjects where name='pk_LFUMC150DXFId')
    alter table LFUMC150DXF drop constraint pk_LFUMC150DXFId
GO
alter table LFUMC150DXF add constraint pk_LFUMC150DXFId primary key (LFUMC150DXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUMC150DXF')
    alter table LFUMC150DXF drop constraint fk_ModuleTreeId_LFUMC150DXF
GO
alter table LFUMC150DXF add constraint fk_ModuleTreeId_LFUMC150DXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LFUMC200DXF
if exists (select * from sysobjects where name='pk_LFUMC200DXFId')
    alter table LFUMC200DXF drop constraint pk_LFUMC200DXFId
GO
alter table LFUMC200DXF add constraint pk_LFUMC200DXFId primary key (LFUMC200DXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUMC200DXF')
    alter table LFUMC200DXF drop constraint fk_ModuleTreeId_LFUMC200DXF
GO
alter table LFUMC200DXF add constraint fk_ModuleTreeId_LFUMC200DXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LFUMC250DXF
if exists (select * from sysobjects where name='pk_LFUMC250DXFId')
    alter table LFUMC250DXF drop constraint pk_LFUMC250DXFId
GO
alter table LFUMC250DXF add constraint pk_LFUMC250DXFId primary key (LFUMC250DXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUMC250DXF')
    alter table LFUMC250DXF drop constraint fk_ModuleTreeId_LFUMC250DXF
GO
alter table LFUMC250DXF add constraint fk_ModuleTreeId_LFUMC250DXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LFUMC150SUSDXF
if exists (select * from sysobjects where name='pk_LFUMC150SUSDXFId')
    alter table LFUMC150SUSDXF drop constraint pk_LFUMC150SUSDXFId
GO
alter table LFUMC150SUSDXF add constraint pk_LFUMC150SUSDXFId primary key (LFUMC150SUSDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUMC150SUSDXF')
    alter table LFUMC150SUSDXF drop constraint fk_ModuleTreeId_LFUMC150SUSDXF
GO
alter table LFUMC150SUSDXF add constraint fk_ModuleTreeId_LFUMC150SUSDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LFUMC200SUSDXF
if exists (select * from sysobjects where name='pk_LFUMC200SUSDXFId')
    alter table LFUMC200SUSDXF drop constraint pk_LFUMC200SUSDXFId
GO
alter table LFUMC200SUSDXF add constraint pk_LFUMC200SUSDXFId primary key (LFUMC200SUSDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUMC200SUSDXF')
    alter table LFUMC200SUSDXF drop constraint fk_ModuleTreeId_LFUMC200SUSDXF
GO
alter table LFUMC200SUSDXF add constraint fk_ModuleTreeId_LFUMC200SUSDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--LFUMC250SUSDXF
if exists (select * from sysobjects where name='pk_LFUMC250SUSDXFId')
    alter table LFUMC250SUSDXF drop constraint pk_LFUMC250SUSDXFId
GO
alter table LFUMC250SUSDXF add constraint pk_LFUMC250SUSDXFId primary key (LFUMC250SUSDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_LFUMC250SUSDXF')
    alter table LFUMC250SUSDXF drop constraint fk_ModuleTreeId_LFUMC250SUSDXF
GO
alter table LFUMC250SUSDXF add constraint fk_ModuleTreeId_LFUMC250SUSDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO


--UCWUVR4SDXF
if exists (select * from sysobjects where name='pk_UCWUVR4SDXFId')
    alter table UCWUVR4SDXF drop constraint pk_UCWUVR4SDXFId
GO
alter table UCWUVR4SDXF add constraint vpk_UCWUVR4SDXFId primary key (UCWUVR4SDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCWUVR4SDXF')
    alter table UCWUVR4SDXF drop constraint fk_ModuleTreeId_UCWUVR4SDXF
GO
alter table UCWUVR4SDXF add constraint fk_ModuleTreeId_UCWUVR4SDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--UCWUVR4LDXF
if exists (select * from sysobjects where name='pk_UCWUVR4LDXFId')
    alter table UCWUVR4LDXF drop constraint pk_UCWUVR4LDXFId
GO
alter table UCWUVR4LDXF add constraint pk_UCWUVR4LDXFId primary key (UCWUVR4LDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCWUVR4LDXF')
    alter table UCWUVR4LDXF drop constraint fk_ModuleTreeId_UCWUVR4LDXF
GO
alter table UCWUVR4LDXF add constraint fk_ModuleTreeId_UCWUVR4LDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO

--UCJFCCOMBIDXF
if exists (select * from sysobjects where name='pk_UCJFCCOMBIDXFId')
    alter table UCJFCCOMBIDXF drop constraint pk_UCJFCCOMBIDXFId
GO
alter table UCJFCCOMBIDXF add constraint pk_UCJFCCOMBIDXFId primary key (UCJFCCOMBIDXFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_UCJFCCOMBIDXF')
    alter table UCJFCCOMBIDXF drop constraint fk_ModuleTreeId_UCJFCCOMBIDXF
GO
alter table UCJFCCOMBIDXF add constraint fk_ModuleTreeId_UCJFCCOMBIDXF foreign key(ModuleTreeId) references ModuleTree (ModuleTreeId)
GO























-------------------------------------HME-----------------------------------------------------------
if exists (select * from sysobjects where name='pk_HMEId')
    alter table HME drop constraint pk_HMEId
GO
alter table HME add constraint pk_HMEId primary key (HMEId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_HME')
    alter table HME drop constraint fk_ModuleTreeId_HME
GO
alter table HME add constraint fk_ModuleTreeId_HME foreign key(ModuleTreeId) references ModuleTreeMarine (ModuleTreeId)
GO

-------------------------------------HMF-----------------------------------------------------------
if exists (select * from sysobjects where name='pk_HMFId')
    alter table HMF drop constraint pk_HMFId
GO
alter table HMF add constraint pk_HMFId primary key (HMFId)
GO
if exists (select * from sysobjects where name='fk_ModuleTreeId_HMF')
    alter table HMF drop constraint fk_ModuleTreeId_HMF
GO
alter table HMF add constraint fk_ModuleTreeId_HMF foreign key(ModuleTreeId) references ModuleTreeMarine (ModuleTreeId)
GO