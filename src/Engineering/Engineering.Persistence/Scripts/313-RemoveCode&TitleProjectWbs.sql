BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectWbses]') AND [c].[name] = N'Code');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectWbses] DROP COLUMN [Code];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectWbses]') AND [c].[name] = N'Title');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ProjectWbses] DROP COLUMN [Title];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260627092852_RemoveCode&TitleProjectWbs', N'8.0.8');
GO

COMMIT;
GO



