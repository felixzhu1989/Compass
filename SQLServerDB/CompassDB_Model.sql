use CompassDB
go
if exists (select * from sysobjects where name='UVI555')
drop table UVI555
go
create table UVI555
(
    UVI555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)
if exists (select * from sysobjects where name='UVIMR555')
drop table UVIMR555
go
create table UVIMR555
(
    UVIMR555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),    
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)
if exists (select * from sysobjects where name='UVIMT555')
drop table UVIMT555
go
create table UVIMT555
(
    UVIMT555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),    
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)
if exists (select * from sysobjects where name='UVIR555')
drop table UVIR555
go
create table UVIR555
(
    UVIR555Id int identity(1,1),
    ModuleTreeId int,
	ExBeamLength decimal(6,2),
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
	SidePanel varchar(6),    
    Outlet varchar(9), 
	   
    Bluetooth varchar(3),    
    
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)

--KVV555
if exists (select * from sysobjects where name='KVV555')
drop table KVV555
go
create table KVV555
(
    KVV555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
	SidePanel varchar(6),   
    LightType varchar(7)    
)


if exists (select * from sysobjects where name='KVI555')
drop table KVI555
go
create table KVI555
(
    KVI555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),    
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)





if exists (select * from sysobjects where name='KVI400')
drop table KVI400
go
create table KVI400
(
    KVI400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),    
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)

if exists (select * from sysobjects where name='KVIMR555')
drop table KVIMR555
go
create table KVIMR555
(
    KVIMR555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),       
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)

if exists (select * from sysobjects where name='KVIR555')
drop table KVIR555
go
create table KVIR555
(
    KVIR555Id int identity(1,1),
    ModuleTreeId int,
	ExBeamLength decimal(6,2),
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
	SidePanel varchar(6),    
    Outlet varchar(9),       
    
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)

if exists (select * from sysobjects where name='KCHI555')
drop table KCHI555
go
create table KCHI555
(
    KCHI555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),    
    BackToBack varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),	
)

if exists (select * from sysobjects where name='UVF555')
drop table UVF555
go
create table UVF555
(
    UVF555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)
if exists (select * from sysobjects where name='UVF555400')
drop table UVF555400
go
create table UVF555400
(
    UVF555400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)
if exists (select * from sysobjects where name='UVF450400')
drop table UVF450400
go
create table UVF450400
(
    UVF450400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)

if exists (select * from sysobjects where name='KVF450400')
drop table KVF450400
go
create table KVF450400
(
    KVF450400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),   
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)

if exists (select * from sysobjects where name='UVI450300')
drop table UVI450300
go
create table UVI450300
(
    UVI450300Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)

if exists (select * from sysobjects where name='KVI450300')
drop table KVI450300
go
create table KVI450300
(
    KVI450300Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2)
)

if exists (select * from sysobjects where name='KVF555')
drop table KVF555
go
create table KVF555
(
    KVF555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),    
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)

if exists (select * from sysobjects where name='KVF400')
drop table KVF400
go
create table KVF400
(
    KVF400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),    
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)


if exists (select * from sysobjects where name='KVF555400')
drop table KVF555400
go
create table KVF555400
(
    KVF555400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),    
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)
if exists (select * from sysobjects where name='KCHF555')
drop table KCHF555
go
create table KCHF555
(
    KCHF555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
    LEDLogo varchar(3),    
    BackToBack varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)


if exists (select * from sysobjects where name='UWF555')
drop table UWF555
go
create table UWF555
(
    UWF555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)

if exists (select * from sysobjects where name='UWF555400')
drop table UWF555400
go
create table UWF555400
(
    UWF555400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)

--HWUWF555400£¬»ªÎª1.2mm°å²Ä
if exists (select * from sysobjects where name='HWUWF555400')
drop table HWUWF555400
go
create table HWUWF555400
(
    HWUWF555400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
	LightYDis decimal(6,2),
    UVType varchar(6),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)






if exists (select * from sysobjects where name='UWI555')
drop table UWI555
go
create table UWI555
(
    UWI555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    Bluetooth varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    UVType varchar(5),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),	
)
if exists (select * from sysobjects where name='KWF555')
drop table KWF555
go
create table KWF555
(
    KWF555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)
if exists (select * from sysobjects where name='KWI555')
drop table KWI555
go
create table KWI555
(
    KWI555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    BackToBack varchar(3),
    WaterCollection varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
)

if exists (select * from sysobjects where name='CMODI555')
drop table CMODI555
go
create table CMODI555
(
    CMODI555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    BackToBack varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
)

if exists (select * from sysobjects where name='CMODF555')
drop table CMODF555
go
create table CMODF555
(
    CMODF555Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    BackToBack varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)

if exists (select * from sysobjects where name='CMODF555400')
drop table CMODF555400
go
create table CMODF555400
(
    CMODF555400Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExRightDis decimal(6,2),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    SidePanel varchar(6),
    Outlet varchar(9),
	Inlet varchar(9),
    LEDLogo varchar(3),
    BackToBack varchar(3),
    LEDSpotNo int,
    LEDSpotDis decimal(6,2),
    LightType varchar(7),
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
	SuNo int,
	SuDis decimal(6,2)
)


if exists (select * from sysobjects where name='KVS')
drop table KVS
go
create table KVS
(
    KVSId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Deepth decimal(6,2),
	Height varchar(10),
    ExNo int,
    ExDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    LightType varchar(7),
	SidePanel varchar(6)    
)

--LSDOST
if exists (select * from sysobjects where name='LSDOST')
drop table LSDOST
go
create table LSDOST
(
    LSDOSTId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    SuNo int,
	SuDis decimal(6,2),
	Deepth decimal(6,2),
	Height varchar(10),
	SidePanel varchar(6)

)

--HOODBCJ
if exists (select * from sysobjects where name='HOODBCJ')
drop table HOODBCJ
go
create table HOODBCJ
(
    HOODBCJId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Height decimal(6,2),	
	SuDis decimal(6,2)	
)

--ABD200
if exists (select * from sysobjects where name='ABD200')
drop table ABD200
go
create table ABD200
(
    ABD200Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Deepth decimal(6,2),
	Height varchar(10),
	SidePanel varchar(6)    
)
--ABD300
if exists (select * from sysobjects where name='ABD300')
drop table ABD300
go
create table ABD300
(
    ABD300Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Deepth decimal(6,2),
	Height varchar(10),
	SidePanel varchar(6)    
)





































--Ceiling

if exists (select * from sysobjects where name='KCJSB265')
drop table KCJSB265
go
create table KCJSB265
(
    KCJSB265Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),
	FCType varchar(6),
	FCBlindNo int,
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    MARVEL varchar(3),
    Japan varchar(3)
)
if exists (select * from sysobjects where name='KCJSB290')
drop table KCJSB290
go
create table KCJSB290
(
    KCJSB290Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),
	FCType varchar(6),
	FCBlindNo int,
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    MARVEL varchar(3),
    Japan varchar(3)
)
if exists (select * from sysobjects where name='KCJSB535')
drop table KCJSB535
go
create table KCJSB535
(
    KCJSB535Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),
	FCType varchar(6),
	FCBlindNo int,
	LightType varchar(7),
	LightCable varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    MARVEL varchar(3),
    Japan varchar(3)
)
if exists (select * from sysobjects where name='KCJDB800')
drop table KCJDB800
go
create table KCJDB800
(
    KCJDB800Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),
	FCType varchar(6),
	FCBlindNo int,
	LightType varchar(7),
	LightCable varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    Japan varchar(3)
)
if exists (select * from sysobjects where name='UCJSB385')
drop table UCJSB385
go
create table UCJSB385
(
    UCJSB385Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),	
	FCBlindNo int,
	UVType varchar(6),
	LightType varchar(7),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    MARVEL varchar(3),
    Japan varchar(3)
)
if exists (select * from sysobjects where name='UCJSB535')
drop table UCJSB535
go
create table UCJSB535
(
    UCJSB535Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),	
	FCBlindNo int,
	UVType varchar(6),
	LightType varchar(7),
	LightCable varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetector varchar(5),
    MARVEL varchar(3),
    Japan varchar(3)
)

if exists (select * from sysobjects where name='UCJDB800')
drop table UCJDB800
go
create table UCJDB800
(
    UCJDB800Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),	
	FCBlindNo int,
	UVType varchar(6),
	LightType varchar(7),
	LightCable varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),
    ANDetectorEnd varchar(5),
	ANDetectorNo int,
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    Japan varchar(3)
)
--KCWSB265
if exists (select * from sysobjects where name='KCWSB265')
drop table KCWSB265
go
create table KCWSB265
(
    KCWSB265Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),	
	FCBlindNo int,
	SidePanel varchar(6),
	Inlet varchar(6),
	DPSide varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),   
    MARVEL varchar(3),
    Japan varchar(3)	
)
--KCWSB535
if exists (select * from sysobjects where name='KCWSB535')
drop table KCWSB535
go
create table KCWSB535
(
    KCWSB535Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),	
	FCBlindNo int,
	SidePanel varchar(6),
	LightType varchar(6),
	DPSide varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),   
    MARVEL varchar(3),
    Japan varchar(3)	
)
--KCWDB800
if exists (select * from sysobjects where name='KCWDB800')
drop table KCWDB800
go
create table KCWDB800
(
    KCWDB800Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),	
	FCBlindNo int,
	SidePanel varchar(6),
	LightType varchar(6),
	DPSide varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),   
    MARVEL varchar(3),
    Japan varchar(3)	
)
--UCWSB535
if exists (select * from sysobjects where name='UCWSB535')
drop table UCWSB535
go
create table UCWSB535
(
    UCWSB535Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),	
	FCBlindNo int,
	SidePanel varchar(6),
	LightType varchar(6),
	UVType varchar(6),
	SensorNo int,
	SensorDis1 decimal(6,2),
	SensorDis2 decimal(6,2),
	DPSide varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),   
    MARVEL varchar(3),
    Japan varchar(3),
	HCLSide varchar(6),
	HCLSideLeft decimal(6,2),
	HCLSideRight decimal(6,2),	
)
--UCWDB800
if exists (select * from sysobjects where name='UCWDB800')
drop table UCWDB800
go
create table UCWDB800
(
    UCWDB800Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    ExRightDis decimal(6,2),
    ExLength decimal(6,2),
    ExWidth decimal(6,2),
    ExHeight decimal(6,2),
    FCSideLeft decimal(6,2),
	FCSideRight decimal(6,2),
	FCSide varchar(6),	
	FCBlindNo int,
	SidePanel varchar(6),
	LightType varchar(6),
	UVType varchar(6),
	SensorNo int,
	SensorDis1 decimal(6,2),
	SensorDis2 decimal(6,2),
	DPSide varchar(6),
	SSPType varchar(4),
	Gutter varchar(3),
	GutterWidth decimal(6,2),    
    ANSUL varchar(3),
    ANSide varchar(5),   
    MARVEL varchar(3),
    Japan varchar(3)	
)
--LFUSA
if exists (select * from sysobjects where name='LFUSA')
drop table LFUSA
go
create table LFUSA
(
    LFUSAId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Width decimal(6,2),
	SuNo int,
	SuDia decimal(6,2),
	SuDis decimal(6,2),
	SidePanel varchar(6),
	Japan varchar(3)	
)

--LFUP
if exists (select * from sysobjects where name='LFUP')
drop table LFUP
go
create table LFUP
(
    LFUPId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Width decimal(6,2)
)
--LFUSC
if exists (select * from sysobjects where name='LFUSC')
drop table LFUSC
go
create table LFUSC
(
    LFUSCId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    SuNo int,
	SuDia decimal(6,2),
	SuDis decimal(6,2),	
	Japan varchar(3)	
)
--LFUSS
if exists (select * from sysobjects where name='LFUSS')
drop table LFUSS
go
create table LFUSS
(
    LFUSSId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Width decimal(6,2),
	SuNo int,
	SuDia decimal(6,2),
	SuDis decimal(6,2),
	SidePanel varchar(6),
	Japan varchar(3)	
)

if exists (select * from sysobjects where name='AN')
drop table AN
go
create table AN
(
    ANId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    Width decimal(6,2),
    ANSUL varchar(3),       
    ANYDis decimal(6,2),
    ANDropNo int,
    ANDropDis1 decimal(6,2),
    ANDropDis2 decimal(6,2),
    ANDropDis3 decimal(6,2),
    ANDropDis4 decimal(6,2),
    ANDropDis5 decimal(6,2),
	ANDetectorNo int,
	ANDetectorEnd varchar(5),
    ANDetectorDis1 decimal(6,2),
    ANDetectorDis2 decimal(6,2),
    ANDetectorDis3 decimal(6,2),
    ANDetectorDis4 decimal(6,2),
    ANDetectorDis5 decimal(6,2),
    MARVEL varchar(3),
    IRNo int,
    IRDis1 decimal(6,2),
    IRDis2 decimal(6,2),
    IRDis3 decimal(6,2),
)

--BF200
if exists (select * from sysobjects where name='BF200')
drop table BF200
go
create table BF200
(
    BF200Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
    LeftLength decimal(6,2),
	RightLength decimal(6,2),	
	MPanelLength decimal(6,2),
	WPanelLength decimal(6,2),	
	MPanelNo int,
	UVType varchar(6)	
)

--SSPDOME
if exists (select * from sysobjects where name='SSPDOME')
drop table SSPDOME
go
create table SSPDOME
(
    SSPDOMEId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LeftType varchar(2),
	RightType varchar(2),
    LeftLength decimal(6,2),
	RightLength decimal(6,2),	
	MPanelNo int,
	LightType varchar(6)	
)


--SSPTBD
if exists (select * from sysobjects where name='SSPTBD')
drop table SSPTBD
go
create table SSPTBD
(
    SSPTBDId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LeftType varchar(2),
	RightType varchar(2),
    LeftLength decimal(6,2),
	RightLength decimal(6,2),	
	MPanelNo int,
	LightType varchar(6)	
)

--SSPTSD
if exists (select * from sysobjects where name='SSPTSD')
drop table SSPTSD
go
create table SSPTSD
(
    SSPTSDId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LeftType varchar(2),
	RightType varchar(2),
    LeftLength decimal(6,2),
	RightLength decimal(6,2),	
	MPanelNo int,
	LightType varchar(6)	
)

--SSPHFD
if exists (select * from sysobjects where name='SSPHFD')
drop table SSPHFD
go
create table SSPHFD
(
    SSPHFDId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LeftType varchar(2),
	RightType varchar(2),
    LeftLength decimal(6,2),
	RightLength decimal(6,2),	
	MPanelNo int,
	LightType varchar(6)	
)

--SSPFLAT
if exists (select * from sysobjects where name='SSPFLAT')
drop table SSPFLAT
go
create table SSPFLAT
(
    SSPFLATId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LeftType varchar(2),
	RightType varchar(2),
    LeftLength decimal(6,2),
	RightLength decimal(6,2),	
	MPanelNo int,
	LightType varchar(6)	
)

--LKS270
if exists (select * from sysobjects where name='LKS270')
drop table LKS270
go
create table LKS270
(
    LKS270Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	WBeam varchar(3),
	SidePanel varchar(6),	
	LightType varchar(6),
	Japan varchar(3)	
)
--LKST270
if exists (select * from sysobjects where name='LKST270')
drop table LKST270
go
create table LKST270
(
    LKST270Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	WBeam varchar(3),
	SidePanel varchar(6),	
	LightType varchar(6),
	Japan varchar(3)	
)

--LKSSPEC
if exists (select * from sysobjects where name='LKSSPEC')
drop table LKSSPEC
go
create table LKSSPEC
(
    LKSSPECId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Height decimal(6,2),
	WBeam varchar(3),
	SidePanel varchar(6),	
	LightType varchar(6),
	Japan varchar(3)	
)

--LKS258HCL
if exists (select * from sysobjects where name='LKS258HCL')
drop table LKS258HCL
go
create table LKS258HCL
(
    LKS258HCLId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	HCLSide varchar(6),
	HCLSideLeft decimal(6,2),
	HCLSideRight decimal(6,2),		
)


--LLEDS
if exists (select * from sysobjects where name='LLEDS')
drop table LLEDS
go
create table LLEDS
(
    LLEDSId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2)		
)
--LLKS
if exists (select * from sysobjects where name='LLKS')
drop table LLKS
go
create table LLKS
(
    LLKSId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LongGlassNo int,
	ShortGlassNo int	
)

--LLKSJ
if exists (select * from sysobjects where name='LLKSJ')
drop table LLKSJ
go
create table LLKSJ
(
    LLKSJId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LongGlassNo int,
	ShortGlassNo int,
	LeftLength decimal(6,2),
	RightLength decimal(6,2)
)

--LPZ
if exists (select * from sysobjects where name='LPZ')
drop table LPZ
go
create table LPZ
(
    LPZId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Width decimal(6,2),	
	ZPanelNo int,
	LightType varchar(6)
)
select * from LPZ


--LKA258
if exists (select * from sysobjects where name='LKA258')
drop table LKA258
go
create table LKA258
(
    LKA258Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LightType varchar(6),
	Japan varchar(3)	
)

--LKASPEC
if exists (select * from sysobjects where name='LKASPEC')
drop table LKASPEC
go
create table LKASPEC
(
    LKASPECId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Height decimal(6,2),	
	SidePanel varchar(6),	
	LightType varchar(6),
	Japan varchar(3)	
)

--LLEDA
if exists (select * from sysobjects where name='LLEDA')
drop table LLEDA
go
create table LLEDA
(
    LLEDAId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2)		
)
--LLKA
if exists (select * from sysobjects where name='LLKA')
drop table LLKA
go
create table LLKA
(
    LLKAId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LongGlassNo int,
	ShortGlassNo int	
)

--LLKAJ
if exists (select * from sysobjects where name='LLKAJ')
drop table LLKAJ
go
create table LLKAJ
(
    LLKAJId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	LongGlassNo int,
	ShortGlassNo int,
	LeftLength decimal(6,2),
	RightLength decimal(6,2)
)

--INF
if exists (select * from sysobjects where name='INF')
drop table INF
go
create table INF
(
    INFId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Width decimal(6,2)	
)

--CJ300
if exists (select * from sysobjects where name='CJ300')
drop table CJ300
go
create table CJ300
(
    CJ300Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	SidePanel varchar(6),
	SuType varchar(6),
	SuDis decimal(6,2),
	BackCJSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)

--CJ330
if exists (select * from sysobjects where name='CJ330')
drop table CJ330
go
create table CJ330
(
    CJ330Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	SidePanel varchar(6),
	SuType varchar(6),
	SuDis decimal(6,2),
	BackCJSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)


--BCJ300
if exists (select * from sysobjects where name='BCJ300')
drop table BCJ300
go
create table BCJ300
(
    BCJ300Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	SidePanel varchar(6),
	SuType varchar(6),
	SuDis decimal(6,2)	
)
--BCJ330
if exists (select * from sysobjects where name='BCJ330')
drop table BCJ330
go
create table BCJ330
(
    BCJ330Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	SidePanel varchar(6),
	SuType varchar(6),
	SuDis decimal(6,2)	
)

--NOCJ300
if exists (select * from sysobjects where name='NOCJ300')
drop table NOCJ300
go
create table NOCJ300
(
    NOCJ300Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Width decimal(6,2),
	SidePanel varchar(10),	
	BackCJSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)


--NOCJ330
if exists (select * from sysobjects where name='NOCJ330')
drop table NOCJ330
go
create table NOCJ330
(
    NOCJ330Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Width decimal(6,2),
	SidePanel varchar(10),	
	BackCJSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)


--NOCJ340
if exists (select * from sysobjects where name='NOCJ340')
drop table NOCJ340
go
create table NOCJ340
(
    NOCJ340Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Width decimal(6,2),
	SidePanel varchar(10),	
	BackCJSide varchar(6),
	DPSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)
--NOCJSPEC
if exists (select * from sysobjects where name='NOCJSPEC')
drop table NOCJSPEC
go
create table NOCJSPEC
(
    NOCJSPECId int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	Width decimal(6,2),
	Height decimal(6,2),
	TopBend decimal(6,2),
	BackBend decimal(6,2),
	SidePanel varchar(10),	
	BackCJSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)

--DP330
if exists (select * from sysobjects where name='DP330')
drop table DP330
go
create table DP330
(
    DP330Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	SidePanel varchar(10),	
	Outlet Varchar(6),
	BackCJSide varchar(6),
	DPSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)
--DP340
if exists (select * from sysobjects where name='DP340')
drop table DP340
go
create table DP340
(
    DP340Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	SidePanel varchar(10),	
	Outlet Varchar(6),
	BackCJSide varchar(6),
	DPSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)

--DPCJ330
if exists (select * from sysobjects where name='DPCJ330')
drop table DPCJ330
go
create table DPCJ330
(
    DPCJ330Id int identity(1,1),
    ModuleTreeId int,
    Length decimal(6,2),
	SidePanel varchar(10),
	SuType varchar(6),
	SuDis decimal(6,2),	
	Outlet Varchar(6),
	BackCJSide varchar(6),
	DPSide varchar(6),
	LeftDis decimal(6,2),
	RightDis decimal(6,2),
	LeftBeamType varchar(8),
	LeftBeamDis decimal(6,2),
	RightBeamType varchar(8),
	RightBeamDis decimal(6,2),
	LKSide varchar(6),
	GutterSide varchar(6),
	GutterWidth decimal(6,2)	
)
















--Åä¼þ£¬DXFÍ¼Ö½

--UCPDXF
if exists (select * from sysobjects where name='UCPDXF')
drop table UCPDXF
go
create table UCPDXF
(
    UCPDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int,
    Length decimal(6,2),
	Deepth decimal(6,2),
	Height varchar(10),
	SidePanel varchar(6)    
)

--MCPDXF
if exists (select * from sysobjects where name='MCPDXF')
drop table MCPDXF
go
create table MCPDXF
(
    MCPDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int,
    Length decimal(6,2),
	Deepth decimal(6,2),
	Height varchar(10),
	SidePanel varchar(6)    
)

--MU1BOXDXF
if exists (select * from sysobjects where name='MU1BOXDXF')
drop table MU1BOXDXF
go
create table MU1BOXDXF
(
    MU1BOXDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)

--TCSBOXDXF
if exists (select * from sysobjects where name='TCSBOXDXF')
drop table TCSBOXDXF
go
create table TCSBOXDXF
(
    TCSBOXDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)



--LFUMC150DXF
if exists (select * from sysobjects where name='LFUMC150DXF')
drop table LFUMC150DXF
go
create table LFUMC150DXF
(
    LFUMC150DXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)


--LFUMC200DXF
if exists (select * from sysobjects where name='LFUMC200DXF')
drop table LFUMC200DXF
go
create table LFUMC200DXF
(
    LFUMC200DXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)

--LFUMC250DXF
if exists (select * from sysobjects where name='LFUMC250DXF')
drop table LFUMC250DXF
go
create table LFUMC250DXF
(
    LFUMC250DXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)

--LFUMC150SUSDXF
if exists (select * from sysobjects where name='LFUMC150SUSDXF')
drop table LFUMC150SUSDXF
go
create table LFUMC150SUSDXF
(
    LFUMC150SUSDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)


--LFUMC200SUSDXF
if exists (select * from sysobjects where name='LFUMC200SUSDXF')
drop table LFUMC200SUSDXF
go
create table LFUMC200SUSDXF
(
    LFUMC200SUSDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)

--LFUMC250SUSDXF
if exists (select * from sysobjects where name='LFUMC250SUSDXF')
drop table LFUMC250SUSDXF
go
create table LFUMC250SUSDXF
(
    LFUMC250SUSDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)




--UCWUVR4SDXF
if exists (select * from sysobjects where name='UCWUVR4SDXF')
drop table UCWUVR4SDXF
go
create table UCWUVR4SDXF
(
    UCWUVR4SDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)

--UCWUVR4LDXF
if exists (select * from sysobjects where name='UCWUVR4LDXF')
drop table UCWUVR4LDXF
go
create table UCWUVR4LDXF
(
    UCWUVR4LDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)

--UCJFCCOMBIDXF
if exists (select * from sysobjects where name='UCJFCCOMBIDXF')
drop table UCJFCCOMBIDXF
go
create table UCJFCCOMBIDXF
(
    UCJFCCOMBIDXFId int identity(1,1),
    ModuleTreeId int,
	Quantity int    
)