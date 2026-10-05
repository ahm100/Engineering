BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ShippingCosts]') AND [c].[name] = N'SourceCityId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ShippingCosts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ShippingCosts] ALTER COLUMN [SourceCityId] bigint NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ShippingCosts]') AND [c].[name] = N'DestinationCityId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ShippingCosts] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ShippingCosts] ALTER COLUMN [DestinationCityId] bigint NULL;
GO

ALTER TABLE [engineer].[ShippingCosts] ADD [ThirdPartyCompanyId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'کمپانی طرف حساب';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'ThirdPartyCompanyId';
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ShippingCostHistories]') AND [c].[name] = N'SourceCityId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ShippingCostHistories] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[ShippingCostHistories] ALTER COLUMN [SourceCityId] bigint NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ShippingCostHistories]') AND [c].[name] = N'DestinationCityId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ShippingCostHistories] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[ShippingCostHistories] ALTER COLUMN [DestinationCityId] bigint NULL;
GO

ALTER TABLE [engineer].[ShippingCostHistories] ADD [ThirdPartyCompanyId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'کمپانی طرف حساب';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'ThirdPartyCompanyId';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260630103156_AddThirdPartyCompanyId', N'8.0.8');
GO

COMMIT;
GO



