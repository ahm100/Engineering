BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EngineeringStandardProducts] DROP CONSTRAINT [FK_EngineeringStandardProducts_EmployerOperations_EmployerOperationId];
GO

DROP INDEX [IX_EngineeringStandardProducts_EmployerOperationId] ON [engineer].[EngineeringStandardProducts];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EngineeringStandardProducts]') AND [c].[name] = N'EmployerOperationId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EngineeringStandardProducts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[EngineeringStandardProducts] DROP COLUMN [EmployerOperationId];
GO

ALTER TABLE [engineer].[EmployerOperationProducts] ADD [ConsumptionStandardProductId] bigint NULL;
GO

CREATE INDEX [IX_EmployerOperationProducts_ConsumptionStandardProductId] ON [engineer].[EmployerOperationProducts] ([ConsumptionStandardProductId]);
GO

ALTER TABLE [engineer].[EmployerOperationProducts] ADD CONSTRAINT [FK_EmployerOperationProducts_EngineeringStandardProducts_ConsumptionStandardProductId] FOREIGN KEY ([ConsumptionStandardProductId]) REFERENCES [engineer].[EngineeringStandardProducts] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251110081436_AddRelationEOperationToProduct', N'8.0.8');
GO

COMMIT;
GO



