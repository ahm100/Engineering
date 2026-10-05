BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectProducts] ADD [ProjectProductType] int NOT NULL DEFAULT 1;
DECLARE @description AS sql_variant;
SET @description = N'نوع کالای پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'ProjectProductType';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251225093911_AddProjectProductType', N'8.0.8');
GO

COMMIT;
GO



