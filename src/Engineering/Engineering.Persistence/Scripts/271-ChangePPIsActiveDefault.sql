BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectProducts]') AND [c].[name] = N'IsActive');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectProducts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectProducts] ADD DEFAULT CAST(1 AS bit) FOR [IsActive];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260110084212_ChangePPIsActiveDefault', N'8.0.8');
GO

COMMIT;
GO



