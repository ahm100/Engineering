BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Projects] ADD [ProjectTypeId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[OperationInfos]') AND [c].[name] = N'OperationInfoName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[OperationInfos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[OperationInfos] ALTER COLUMN [OperationInfoName] nvarchar(max) NOT NULL;
GO

CREATE TABLE [engineer].[CostCenterHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [CostCenterCode] nvarchar(100) NOT NULL,
    [CostCenterName] nvarchar(250) NOT NULL,
    [NoOperationDays] int NULL,
    [CityId] bigint NOT NULL,
    [Address] nvarchar(1500) NOT NULL,
    [PostalCode] nvarchar(10) NULL,
    [Latitude] decimal(18,9) NOT NULL,
    [Longitude] decimal(18,9) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [WeatherState] bit NULL DEFAULT CAST(0 AS bit),
    [CompanyId] bigint NULL,
    [PreferentialReferenceCode] uniqueidentifier NOT NULL,
    [CostCenterTypesId] bigint NOT NULL,
    [CostCenterId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_CostCenterHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CostCenterHistories_CostCenterTypes_CostCenterTypesId] FOREIGN KEY ([CostCenterTypesId]) REFERENCES [engineer].[CostCenterTypes] ([Id]),
    CONSTRAINT [FK_CostCenterHistories_CostCenters_CostCenterTypesId] FOREIGN KEY ([CostCenterTypesId]) REFERENCES [engineer].[CostCenters] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'Id';
SET @description = N'کد مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'CostCenterCode';
SET @description = N'نام مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'CostCenterName';
SET @description = N'روزهای بدون پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'NoOperationDays';
SET @description = N'شناسه شهر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'CityId';
SET @description = N'آدرس مرکز هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'Address';
SET @description = N'کد پستی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'PostalCode';
SET @description = N'عرض جغرافیایی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'Latitude';
SET @description = N'طول جغرافیایی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'Longitude';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'Description';
SET @description = N'وضعیت آب و هوا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'WeatherState';
SET @description = N'شناسه کمپانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'CompanyId';
SET @description = N'کد مرجع تفصیلی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'PreferentialReferenceCode';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_CostCenterHistories_CostCenterTypesId] ON [engineer].[CostCenterHistories] ([CostCenterTypesId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251130055052_CreateCostCenterHistoryEntity', N'8.0.8');
GO

COMMIT;
GO



