BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractTypeDetails]') AND [c].[name] = N'UnitOfMeasurementId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractTypeDetails] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ContractTypeDetails] ALTER COLUMN [UnitOfMeasurementId] bigint NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractTypeDetails]') AND [c].[name] = N'Quantity');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractTypeDetails] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ContractTypeDetails] ALTER COLUMN [Quantity] decimal(18,5) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260826104928_MakeContractTypeDetailOptionalFieldsNullable', N'8.0.8');
GO

COMMIT;
GO



