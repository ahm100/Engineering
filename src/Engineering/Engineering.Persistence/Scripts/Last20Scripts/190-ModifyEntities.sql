BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerStatusStatementProjectOperations]') AND [c].[name] = N'EmployerCommercialConfirmeTotalPrice');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ALTER COLUMN [EmployerCommercialConfirmeTotalPrice] decimal(18,2) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerStatusStatementProjectOperations]') AND [c].[name] = N'Description');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ALTER COLUMN [Description] nvarchar(1500) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerStatusStatementProjectOperationDetails]') AND [c].[name] = N'Description');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerStatusStatementProjectOperationDetails] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[EmployerStatusStatementProjectOperationDetails] ALTER COLUMN [Description] nvarchar(1500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250422100020_ModifyEntities', N'8.0.8');
GO

COMMIT;
GO
