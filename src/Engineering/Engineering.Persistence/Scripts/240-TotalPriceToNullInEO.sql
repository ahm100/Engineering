BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerOperations]') AND [c].[name] = N'TotalPrice');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerOperations] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[EmployerOperations] ALTER COLUMN [TotalPrice] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251115054014_TotalPriceToNullInEO', N'8.0.8');
GO

COMMIT;
GO



