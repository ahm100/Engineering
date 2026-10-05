BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatements] ADD [MultiPayment] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'Updated';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[BillOfLadings]') AND [c].[name] = N'IsDeleted');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[BillOfLadings] DROP CONSTRAINT [' + @var0 + '];');
DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'IsActive';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'نام ترابری';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'BillOfLadingName';
GO

DECLARE @description AS sql_variant;
SET @description = N'کد ترابری';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'BillOfLadingCode';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'BillOfLadings', 'COLUMN', N'Id';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251004044407_AddMultiPayment', N'8.0.8');
GO

COMMIT;
GO



