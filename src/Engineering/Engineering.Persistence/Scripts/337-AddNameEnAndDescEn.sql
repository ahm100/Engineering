BEGIN TRANSACTION;
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

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260728104221_AddNameEnAndDescEn', N'8.0.8');
GO

COMMIT;
GO



