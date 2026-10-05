BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[OperationLocations]') AND [c].[name] = N'CostCenterId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[OperationLocations] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[OperationLocations] ALTER COLUMN [CostCenterId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260712095700_NullableCostCenterInOperationLocation', N'8.0.8');
GO

COMMIT;
GO



