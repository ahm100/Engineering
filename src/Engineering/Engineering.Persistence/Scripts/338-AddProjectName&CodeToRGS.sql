BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[RequestGoodsSupplyTypes]') AND [c].[name] = N'ReferenceId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[RequestGoodsSupplyTypes] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[RequestGoodsSupplyTypes] ALTER COLUMN [ReferenceId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplyTypes] ADD [ProjectCode] nvarchar(100) NULL;
DECLARE @description AS sql_variant;
SET @description = N'کد نوع پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'ProjectCode';
GO

ALTER TABLE [engineer].[RequestGoodsSupplyTypes] ADD [ProjectEnName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'نام پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'ProjectEnName';
GO

ALTER TABLE [engineer].[RequestGoodsSupplyTypes] ADD [ProjectName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'نام پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'ProjectName';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[RequestGoodsSupplyTypeHistories]') AND [c].[name] = N'ReferenceId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[RequestGoodsSupplyTypeHistories] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[RequestGoodsSupplyTypeHistories] ALTER COLUMN [ReferenceId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplyTypeHistories] ADD [ProjectCode] nvarchar(100) NULL;
DECLARE @description AS sql_variant;
SET @description = N'کد نوع پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'ProjectCode';
GO

ALTER TABLE [engineer].[RequestGoodsSupplyTypeHistories] ADD [ProjectEnName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'نام پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'ProjectEnName';
GO

ALTER TABLE [engineer].[RequestGoodsSupplyTypeHistories] ADD [ProjectName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'نام پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'ProjectName';
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[RequestGoodsSupplyTypeDetails]') AND [c].[name] = N'ReferenceId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[RequestGoodsSupplyTypeDetails] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[RequestGoodsSupplyTypeDetails] ALTER COLUMN [ReferenceId] bigint NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[RequestGoodsSupplyTypeDetailHistories]') AND [c].[name] = N'ReferenceId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[RequestGoodsSupplyTypeDetailHistories] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[RequestGoodsSupplyTypeDetailHistories] ALTER COLUMN [ReferenceId] bigint NULL;
GO

ALTER TABLE [engineer].[Projects] ADD [DescriptionEn] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'DescriptionEn';
GO

ALTER TABLE [engineer].[Projects] ADD [ProjectEnName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'نام پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'ProjectEnName';
GO

ALTER TABLE [engineer].[ProjectHistories] ADD [DescriptionEn] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'DescriptionEn';
GO

ALTER TABLE [engineer].[ProjectHistories] ADD [ProjectEnName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'نام پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'ProjectEnName';
GO

ALTER TABLE [engineer].[CostCenters] ADD [CostCenterEnName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'نام مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'CostCenterEnName';
GO

ALTER TABLE [engineer].[CostCenters] ADD [DescriptionEn] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'DescriptionEn';
GO

ALTER TABLE [engineer].[CostCenterHistories] ADD [CostCenterEnName] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'نام انگلیسی مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'CostCenterEnName';
GO

ALTER TABLE [engineer].[CostCenterHistories] ADD [DescriptionEn] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'DescriptionEn';
GO

ALTER TABLE [engineer].[Advertisements] ADD [CompanyId] bigint NULL;
GO

CREATE TABLE [engineer].[AdvertisementDocument] (
    [Id] bigint NOT NULL IDENTITY,
    [Url] nvarchar(max) NOT NULL,
    [AdvertisementId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AdvertisementDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AdvertisementDocument_Advertisements_AdvertisementId] FOREIGN KEY ([AdvertisementId]) REFERENCES [engineer].[Advertisements] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [engineer].[ServiceInfoDocument] (
    [Id] bigint NOT NULL IDENTITY,
    [Url] nvarchar(max) NOT NULL,
    [ServiceInfoId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ServiceInfoDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ServiceInfoDocument_EngineeringServices_ServiceInfoId] FOREIGN KEY ([ServiceInfoId]) REFERENCES [engineer].[EngineeringServices] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AdvertisementDocument_AdvertisementId] ON [engineer].[AdvertisementDocument] ([AdvertisementId]);
GO

CREATE INDEX [IX_ServiceInfoDocument_ServiceInfoId] ON [engineer].[ServiceInfoDocument] ([ServiceInfoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260729122919_AddProjectName&CodeToRGS', N'8.0.8');
GO

COMMIT;
GO



