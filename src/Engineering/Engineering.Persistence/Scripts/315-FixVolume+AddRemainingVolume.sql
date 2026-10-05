BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperationDetailContractorServices] ADD [RemainingVolume] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectOperationDetailContractorExperts]') AND [c].[name] = N'Volume');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectOperationDetailContractorExperts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectOperationDetailContractorExperts] ALTER COLUMN [Volume] decimal(18,2) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260630073316_FixVolume+AddRemainingVolume', N'8.0.8');
GO

COMMIT;
GO



