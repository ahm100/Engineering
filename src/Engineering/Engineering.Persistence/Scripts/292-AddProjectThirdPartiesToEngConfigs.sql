BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
GO

ALTER TABLE [engineer].[EngineeringConfigs] ADD [ProjectThirdParties] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'کلید تنظیمات دسترسی پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigs', 'COLUMN', N'ProjectThirdParties';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260523101854_AddProjectThirdPartiesToEngConfigs', N'8.0.8');
GO

COMMIT;
GO



