BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerDocs]') AND [c].[name] = N'Description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerDocs] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[EmployerDocs] ALTER COLUMN [Description] nvarchar(1500) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerContracts]') AND [c].[name] = N'AdvancePayment');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerContracts] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[EmployerContracts] ALTER COLUMN [AdvancePayment] decimal(5,2) NOT NULL;
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'AdvancePayment';
SET @description = N'درصد پیش پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'AdvancePayment';
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'AdvancePayment';
SET @description = N'درصد پیش پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHistories', 'COLUMN', N'AdvancePayment';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251105095101_fixedAdvancePayment', N'8.0.8');
GO

COMMIT;
GO



