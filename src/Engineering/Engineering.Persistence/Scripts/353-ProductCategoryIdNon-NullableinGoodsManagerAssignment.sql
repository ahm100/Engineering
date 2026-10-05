BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignments]') AND [c].[name] = N'ProductCategoryId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignments] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [engineer].[GoodsManagerAssignments] SET [ProductCategoryId] = CAST(0 AS bigint) WHERE [ProductCategoryId] IS NULL;
ALTER TABLE [engineer].[GoodsManagerAssignments] ALTER COLUMN [ProductCategoryId] bigint NOT NULL;
ALTER TABLE [engineer].[GoodsManagerAssignments] ADD DEFAULT CAST(0 AS bigint) FOR [ProductCategoryId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignmentHistories]') AND [c].[name] = N'ProductCategoryId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [engineer].[GoodsManagerAssignmentHistories] SET [ProductCategoryId] = CAST(0 AS bigint) WHERE [ProductCategoryId] IS NULL;
ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] ALTER COLUMN [ProductCategoryId] bigint NOT NULL;
ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] ADD DEFAULT CAST(0 AS bigint) FOR [ProductCategoryId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260819081244_ProductCategoryIdNon-NullableinGoodsManagerAssignment', N'8.0.8');
GO

COMMIT;
GO



