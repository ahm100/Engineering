BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Contracts]') AND [c].[name] = N'EnTitle');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Contracts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Contracts] ALTER COLUMN [EnTitle] nvarchar(250) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractLegalSnapshots]') AND [c].[name] = N'EnTitle');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractLegalSnapshots] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ContractLegalSnapshots] ALTER COLUMN [EnTitle] nvarchar(250) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260912140006_ContractFinalize', N'8.0.8');
GO

COMMIT;
GO



