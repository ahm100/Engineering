BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectOperationHistories]') AND [c].[name] = N'Price');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectOperationHistories] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectOperationHistories] DROP COLUMN [Price];
GO

EXEC sp_rename N'[engineer].[EmployerOperationHistories].[Price]', N'UnitPrice', N'COLUMN';
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [GoodsInProgress] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'کالاهای در حال پیشرفت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'GoodsInProgress';
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [Priority] int NULL;
DECLARE @description AS sql_variant;
SET @description = N'اولویت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'Priority';
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [ProjectOperationStatus] int NOT NULL DEFAULT 1;
DECLARE @description AS sql_variant;
SET @description = N'وضعیت شرح عملیات های پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'ProjectOperationStatus';
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [TolerancePercentage] decimal(18,5) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'درصد تحمل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'TolerancePercentage';
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [UnitOfMeasurementId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [UnitPrice] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'هزینه واحد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'UnitPrice';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251108062406_AddUnitPrice', N'8.0.8');
GO

COMMIT;
GO



