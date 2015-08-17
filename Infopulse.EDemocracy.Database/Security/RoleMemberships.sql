ALTER ROLE [db_owner] ADD MEMBER [enarod_app_admin];
GO
ALTER ROLE [db_owner] ADD MEMBER [enarod_app_dev];
GO


ALTER ROLE [db_accessadmin] ADD MEMBER [enarod_app_admin];
GO




ALTER ROLE [db_datawriter] ADD MEMBER [enarod_app_dev];
GO

ALTER ROLE [db_datareader] ADD MEMBER [enarod_app_dev];
GO
