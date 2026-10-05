BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'CategoryId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Projects] ALTER COLUMN [CategoryId] bigint NULL;
GO

ALTER TABLE [engineer].[FiduciaryProducts] ADD [LastDescription] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[FiduciaryProducts] ADD [RequestNumber] bigint NULL;
GO

ALTER TABLE [engineer].[FiduciaryProducts] ADD [StatusDescription] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[FiduciaryProductHistories] ADD [LastDescription] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[FiduciaryProductHistories] ADD [StatusDescription] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[FiduciaryProductDetails] ADD [LastDescription] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[FiduciaryProductDetails] ADD [StatusDescription] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[FiduciaryProductDetailHistories] ADD [LastDescription] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[FiduciaryProductDetailHistories] ADD [StatusDescription] nvarchar(1500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250811063837_MakeNullCategoryInProject', N'8.0.8');
GO

COMMIT;
GO



