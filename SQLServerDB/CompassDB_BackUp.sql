BACKUP DATABASE [CompassDB] TO  
DISK = N'D:\Halton Help BackUp\CompassDB_BackUp\CompassDB.bak' 
WITH NOFORMAT, NOINIT,  
NAME = N'CompassDB-Full Database Backup', 
SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO
