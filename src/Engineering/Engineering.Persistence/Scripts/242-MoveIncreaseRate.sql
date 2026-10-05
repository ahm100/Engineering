BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerOperationServices]') AND [c].[name] = N'IncreaseRate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerOperationServices] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[EmployerOperationServices] DROP COLUMN [IncreaseRate];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerOperationProducts]') AND [c].[name] = N'IncreaseRate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerOperationProducts] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[EmployerOperationProducts] DROP COLUMN [IncreaseRate];
GO

ALTER TABLE [engineer].[EmployerOperations] ADD [IncreaseRate] decimal(5,2) NOT NULL DEFAULT 1.0;
DECLARE @description AS sql_variant;
SET @description = N'ضریب افزایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'IncreaseRate';
GO

ALTER TABLE [engineer].[EmployerOperationHistories] ADD [IncreaseRate] decimal(5,2) NOT NULL DEFAULT 1.0;
DECLARE @description AS sql_variant;
SET @description = N'ضریب افزایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'IncreaseRate';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251117134436_MoveIncreaseRate', N'8.0.8');
GO

COMMIT;
GO



