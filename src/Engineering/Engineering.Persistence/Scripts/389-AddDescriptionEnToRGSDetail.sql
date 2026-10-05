BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplyTypeDetails] ADD [DescriptionEn] nvarchar(1500) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'ProjectManager');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Projects] ALTER COLUMN [ProjectManager] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260914094949_AddDescriptionEnToRGSDetail', N'8.0.8');
GO

COMMIT;
GO



