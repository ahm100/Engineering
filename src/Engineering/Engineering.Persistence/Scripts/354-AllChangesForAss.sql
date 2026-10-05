BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignments]') AND [c].[name] = N'GoodsManagerThirdPartyId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[GoodsManagerAssignments] DROP COLUMN [GoodsManagerThirdPartyId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignments]') AND [c].[name] = N'ProductCategoryId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[GoodsManagerAssignments] DROP COLUMN [ProductCategoryId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignments]') AND [c].[name] = N'ProductGroupId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignments] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[GoodsManagerAssignments] DROP COLUMN [ProductGroupId];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignmentHistories]') AND [c].[name] = N'GoodsManagerThirdPartyId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP COLUMN [GoodsManagerThirdPartyId];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignmentHistories]') AND [c].[name] = N'ProductCategoryId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP COLUMN [ProductCategoryId];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignmentHistories]') AND [c].[name] = N'ProductGroupId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP COLUMN [ProductGroupId];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignments]') AND [c].[name] = N'ProductId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignments] DROP CONSTRAINT [' + @var6 + '];');
UPDATE [engineer].[GoodsManagerAssignments] SET [ProductId] = CAST(0 AS bigint) WHERE [ProductId] IS NULL;
ALTER TABLE [engineer].[GoodsManagerAssignments] ALTER COLUMN [ProductId] bigint NOT NULL;
ALTER TABLE [engineer].[GoodsManagerAssignments] ADD DEFAULT CAST(0 AS bigint) FOR [ProductId];
GO

ALTER TABLE [engineer].[GoodsManagerAssignments] ADD [OrganizationId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
DECLARE @description AS sql_variant;
SET @description = N'┘ê╪º╪¡╪» ╪│╪º╪▓┘à╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignments', 'COLUMN', N'OrganizationId';
GO

ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] ADD [OrganizationId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
DECLARE @description AS sql_variant;
SET @description = N'┘ê╪º╪¡╪» ╪│╪º╪▓┘à╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'OrganizationId';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260819134606_AllChangesForAss', N'8.0.8');
GO

COMMIT;
GO



