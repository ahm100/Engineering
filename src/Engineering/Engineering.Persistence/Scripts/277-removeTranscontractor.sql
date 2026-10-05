BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationContractorPriceWeightHistories]') AND [c].[name] = N'TransportationContractorId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationContractorPriceWeightHistories] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[TransportationContractorPriceWeightHistories] DROP COLUMN [TransportationContractorId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260208134738_removeTranscontractor', N'8.0.8');
GO

COMMIT;
GO



