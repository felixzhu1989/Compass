use master
go
if exists (select * from sysdatabases where name='CompassDB')
    drop database CompassDB
GO
create DATABASE CompassDB
on PRIMARY
(
    name='CompassDB_data',
    filename='D:\TechDB\CompassDB_data.mdf',
    size=100MB,
    filegrowth=20MB
)
log on
(
    name='CompassDB_log',
    filename='D:\TechDB\CompassDB_log.ldf',
    size=10MB,
    filegrowth=5MB
)
go