BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EngineeringServices] ADD [DescriptionEn] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات انگلیسی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'DescriptionEn';
GO

ALTER TABLE [engineer].[EngineeringServices] ADD [DescriptionFa] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات فارسی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'DescriptionFa';
GO

ALTER TABLE [engineer].[EngineeringServices] ADD [ServiceInfoEnName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'عنوان انگلیس';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'ServiceInfoEnName';
GO

ALTER TABLE [engineer].[EngineeringServices] ADD [Type] int NOT NULL DEFAULT 1;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260718082717_AlterServiceInfo', N'8.0.8');
GO

COMMIT;
GO



