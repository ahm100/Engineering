BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'Description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Projects] ALTER COLUMN [Description] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[Projects] ADD [AddressDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'آدرس پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'AddressDescription';
GO

ALTER TABLE [engineer].[ProjectHistories] ADD [AddressDescription] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'آدرس پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'AddressDescription';
GO

ALTER TABLE [engineer].[ProjectHistories] ADD [CityId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'شناسه شهر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'CityId';
GO

ALTER TABLE [engineer].[ProjectHistories] ADD [Description] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'Description';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260712092430_AddAddressDescription+History', N'8.0.8');
GO

COMMIT;
GO



