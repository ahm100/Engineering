BEGIN TRANSACTION;
GO


                INSERT INTO EngineeringDB.engineer.ProjectCategories
                    (ProjectId, CategoryId, Created, CreatorId, Updated, UpdaterId, IsDeleted)
                SELECT 
                    p.Id,
                    p.CategoryId,
                    p.Created,   -- Created date/time
                    p.CreatorId, -- CreatorId = p.CreatorId
                    p.Updated,   -- Updated = p.Updated
                    p.UpdaterId, -- UpdaterId = p.UpdaterId
                    p.IsDeleted  -- IsDeleted = p.IsDeleted
                FROM EngineeringDB.engineer.Projects p
                LEFT JOIN EngineeringDB.engineer.ProjectCategories pc ON pc.ProjectId = p.Id AND pc.CategoryId = p.CategoryId
                WHERE pc.Id IS NULL  
                  AND p.CategoryId IS NOT NULL 
                  AND p.IsDeleted != 1;
            
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260118104455_UpdatePCategoriesRows', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [FK_Projects_EngineeringCategories_CategoryId];
GO

DROP INDEX [IX_Projects_CategoryId] ON [engineer].[Projects];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'CategoryId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Projects] DROP COLUMN [CategoryId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260118130745_DeleteCategoryIdFromProjects', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectProducts] ADD [DefaultManagerSet] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'تایید دیفالت مدیرپروژه هست یا نه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'DefaultManagerSet';
GO

CREATE TABLE [engineer].[ProjectProductHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [RequestQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [RemainingQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CompletedQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InProgressQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [ProductGroupId] bigint NULL,
    [ProductCategoryId] bigint NULL,
    [TolerancePercentage] decimal(18,5) NOT NULL DEFAULT 0.0,
    [DefaultManagerSet] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ProjectProductId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_ProjectProductHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectProductHistories_ProjectProducts_ProjectProductId] FOREIGN KEY ([ProjectProductId]) REFERENCES [engineer].[ProjectProducts] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'Id';
SET @description = N'شناسه گروه کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'ProductGroupId';
SET @description = N'شناسه دسته بندی کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'ProductCategoryId';
SET @description = N'درصد تلورانس';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'TolerancePercentage';
SET @description = N'تایید دیفالت مدیرپروژه هست یا نه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'DefaultManagerSet';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_ProjectProductHistories_ProjectProductId] ON [engineer].[ProjectProductHistories] ([ProjectProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260126123905_AddDefaultManagerSetToPP&&AddPPHistory', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationContractorPriceWeightHistories]') AND [c].[name] = N'TransportationContractorId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationContractorPriceWeightHistories] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[TransportationContractorPriceWeightHistories] DROP COLUMN [TransportationContractorId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260208134738_removeTranscontractor', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[FixAssetMachineryRates] ADD [VolumeRate] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[FixAssetMachineries] ADD [VolumeRate] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260209074505_AddVolumeUnit', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] ADD [Quantity] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260217140727_AddQuantityToWarehouseTransportation', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO


                CREATE SYNONYM [engineer].[ViewGroup] FOR [WarehouseDb].[dbo].[Group];
                CREATE SYNONYM [engineer].[ViewCategory] FOR [WarehouseDb].[dbo].[Categories];
                CREATE SYNONYM [engineer].[ViewDocument] FOR [WarehouseDb].[dbo].[Documents];
                CREATE SYNONYM [engineer].[ViewDocumentGroup] FOR [WarehouseDb].[dbo].[DocumentGroups];
                CREATE SYNONYM [engineer].[ViewDocumentProduct] FOR [WarehouseDb].[dbo].[DocumentProducts];
                CREATE SYNONYM [engineer].[ViewWarehouseAsset] FOR [WarehouseDb].[dbo].[WarehouseAssets];
                CREATE SYNONYM [engineer].[ViewBrand] FOR [MetaDataDb].[meta].[Brands];
                CREATE SYNONYM [engineer].[ViewBrandModel] FOR [MetaDataDb].[meta].[BrandModels];
            
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260225091426_AddMultipleSynonyms', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD [OtherCost] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'هزینه  سایر هزینه ها';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'OtherCost';
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD [OtherCostPercent] decimal(5,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'درصد سایر هزینه ها';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'OtherCostPercent';
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD [ProfitCost] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'هزینه سود';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'ProfitCost';
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD [ProfitCostPercent] decimal(5,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'درصد سود';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'ProfitCostPercent';
GO

ALTER TABLE [engineer].[EmployerOperationProducts] ADD [OtherCost] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'هزینه  سایر هزینه ها';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'OtherCost';
GO

ALTER TABLE [engineer].[EmployerOperationProducts] ADD [OtherCostPercent] decimal(5,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'درصد سایر هزینه ها';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'OtherCostPercent';
GO

ALTER TABLE [engineer].[EmployerOperationProducts] ADD [ProfitCost] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'هزینه سود';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'ProfitCost';
GO

ALTER TABLE [engineer].[EmployerOperationProducts] ADD [ProfitCostPercent] decimal(5,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'درصد سود';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'ProfitCostPercent';
GO

CREATE TABLE [engineer].[EmployerOperationProductHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProductGroupId] bigint NOT NULL,
    [MinPrice] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MaxPrice] decimal(18,2) NOT NULL,
    [Tax] decimal(18,2) NOT NULL,
    [TaxPercent] decimal(5,2) NOT NULL,
    [TransportationCost] decimal(18,2) NOT NULL,
    [TransportationCostPercent] decimal(5,2) NOT NULL,
    [ProfitCost] decimal(18,2) NOT NULL,
    [ProfitCostPercent] decimal(5,2) NOT NULL,
    [OtherCost] decimal(18,2) NOT NULL,
    [OtherCostPercent] decimal(5,2) NOT NULL,
    [IsStandard] bit NOT NULL DEFAULT CAST(1 AS bit),
    [Description] nvarchar(1500) NULL,
    [EmployerOperationProductId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerOperationProductHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerOperationProductHistories_EmployerOperationProducts_EmployerOperationProductId] FOREIGN KEY ([EmployerOperationProductId]) REFERENCES [engineer].[EmployerOperationProducts] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'Id';
SET @description = N'حداقل قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'MinPrice';
SET @description = N'حداکثر قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'MaxPrice';
SET @description = N'مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'Tax';
SET @description = N'درصد مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'TaxPercent';
SET @description = N'هزینه حمل و نقل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'TransportationCost';
SET @description = N'درصد هزینه حمل و نقل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'TransportationCostPercent';
SET @description = N'هزینه سود';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'ProfitCost';
SET @description = N'درصد سود';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'ProfitCostPercent';
SET @description = N'هزینه  سایر هزینه ها';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'OtherCost';
SET @description = N'درصد سایر هزینه ها';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'OtherCostPercent';
SET @description = N'استاندارد بودن کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'IsStandard';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProductHistories', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[EmployerOperationServicesHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [MinPrice] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MaxPrice] decimal(18,2) NOT NULL,
    [Tax] decimal(18,2) NOT NULL,
    [TaxPercent] decimal(5,2) NOT NULL,
    [TransportationCost] decimal(18,2) NOT NULL,
    [TransportationCostPercent] decimal(5,2) NOT NULL,
    [ProfitCost] decimal(18,2) NOT NULL,
    [ProfitCostPercent] decimal(5,2) NOT NULL,
    [OtherCost] decimal(18,2) NOT NULL,
    [OtherCostPercent] decimal(5,2) NOT NULL,
    [IsStandard] bit NOT NULL DEFAULT CAST(1 AS bit),
    [Description] nvarchar(1500) NULL,
    [EmployerOperationServiceId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerOperationServicesHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerOperationServicesHistories_EmployerOperationServices_EmployerOperationServiceId] FOREIGN KEY ([EmployerOperationServiceId]) REFERENCES [engineer].[EmployerOperationServices] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'Id';
SET @description = N'حداقل قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'MinPrice';
SET @description = N'حداکثر قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'MaxPrice';
SET @description = N'مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'Tax';
SET @description = N'درصد مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'TaxPercent';
SET @description = N'هزینه حمل و نقل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'TransportationCost';
SET @description = N'درصد هزینه حمل و نقل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'TransportationCostPercent';
SET @description = N'هزینه سود';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'ProfitCost';
SET @description = N'درصد سود';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'ProfitCostPercent';
SET @description = N'هزینه  سایر هزینه ها';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'OtherCost';
SET @description = N'درصد سایر هزینه ها';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'OtherCostPercent';
SET @description = N'استاندارد بودن کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'IsStandard';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServicesHistories', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_EmployerOperationProductHistories_EmployerOperationProductId] ON [engineer].[EmployerOperationProductHistories] ([EmployerOperationProductId]);
GO

CREATE INDEX [IX_EmployerOperationServicesHistories_EmployerOperationServiceId] ON [engineer].[EmployerOperationServicesHistories] ([EmployerOperationServiceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260303045854_EOServiceHistoryANdEoProductAdd', N'8.0.8');
GO

COMMIT;
GO

