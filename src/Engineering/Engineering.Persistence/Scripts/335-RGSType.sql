BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[RequestGoodsSupplyTypes] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Importance] int NOT NULL,
    [Status] int NOT NULL DEFAULT 1,
    [DelivaryDeadLine] datetime2 NULL,
    [ReferenceId] bigint NOT NULL,
    [Type] int NOT NULL,
    [PackageId] bigint NULL,
    [RequestedCount] decimal(18,2) NOT NULL,
    [UnitPrice] decimal(18,2) NULL,
    [TotalPrice] decimal(18,2) NULL,
    [FinalPrice] decimal(18,2) NULL,
    [PackageCount] decimal(18,2) NULL,
    [PackageUnitPrice] decimal(18,2) NULL,
    [PackingPrice] decimal(18,2) NULL,
    [CheckGroup] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ContractorId] bigint NULL,
    [SerialNumber] nvarchar(250) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [ManagementDescription] nvarchar(1500) NULL,
    [LastDescription] nvarchar(1500) NULL,
    [RequestGoodsSupplyId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RequestGoodsSupplyTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestGoodsSupplyTypes_RequestGoodsSupplies_RequestGoodsSupplyId] FOREIGN KEY ([RequestGoodsSupplyId]) REFERENCES [engineer].[RequestGoodsSupplies] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'Id';
SET @description = N'شناسه محصول سفارش داده شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'ReferenceId';
SET @description = N'نوع محصول سفارش داده شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'Type';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypes', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[RequestGoodsSupplyTypeDetails] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Importance] int NULL,
    [Status] int NOT NULL DEFAULT 1,
    [ReferenceId] bigint NOT NULL,
    [Type] int NOT NULL,
    [RequestedCount] decimal(18,2) NOT NULL,
    [DelivaryDeadLine] datetime2 NULL,
    [UnitPrice] decimal(18,2) NULL,
    [TotalPrice] decimal(18,2) NULL,
    [PackingPrice] decimal(18,2) NULL,
    [FinalPrice] decimal(18,2) NULL,
    [Description] nvarchar(1500) NULL,
    [ManagementDescription] nvarchar(1500) NULL,
    [CheckGroup] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ContractorId] bigint NULL,
    [PackageId] bigint NULL,
    [PackageCount] decimal(18,2) NULL,
    [PackageUnitPrice] decimal(18,2) NULL,
    [LastDescription] nvarchar(1500) NULL,
    [ProjectProductId] bigint NULL,
    [RequestGoodsSupplyId] bigint NOT NULL,
    [RequestGoodsSupplyTypeId] bigint NULL,
    [CostCenterId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RequestGoodsSupplyTypeDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestGoodsSupplyTypeDetails_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [engineer].[CostCenters] ([Id]),
    CONSTRAINT [FK_RequestGoodsSupplyTypeDetails_ProjectProducts_ProjectProductId] FOREIGN KEY ([ProjectProductId]) REFERENCES [engineer].[ProjectProducts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RequestGoodsSupplyTypeDetails_RequestGoodsSupplies_RequestGoodsSupplyId] FOREIGN KEY ([RequestGoodsSupplyId]) REFERENCES [engineer].[RequestGoodsSupplies] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId] FOREIGN KEY ([RequestGoodsSupplyTypeId]) REFERENCES [engineer].[RequestGoodsSupplyTypes] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetails', 'COLUMN', N'Id';
SET @description = N'شناسه محصول سفارش داده شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetails', 'COLUMN', N'ReferenceId';
SET @description = N'نوع محصول سفارش داده شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetails', 'COLUMN', N'Type';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetails', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetails', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetails', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetails', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetails', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[RequestGoodsSupplyTypeDocument] (
    [Id] bigint NOT NULL IDENTITY,
    [Url] nvarchar(max) NOT NULL,
    [RequestGoodsSupplyTypeId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RequestGoodsSupplyTypeDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestGoodsSupplyTypeDocument_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId] FOREIGN KEY ([RequestGoodsSupplyTypeId]) REFERENCES [engineer].[RequestGoodsSupplyTypes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [engineer].[RequestGoodsSupplyTypeHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Importance] int NOT NULL,
    [Status] int NOT NULL DEFAULT 1,
    [DelivaryDeadLine] datetime2 NULL,
    [ReferenceId] bigint NOT NULL,
    [Type] int NOT NULL,
    [PackageId] bigint NULL,
    [RequestedCount] decimal(18,2) NOT NULL,
    [UnitPrice] decimal(18,2) NULL,
    [TotalPrice] decimal(18,2) NULL,
    [TaxPercentage] decimal(5,2) NULL,
    [TaxNumber] decimal(18,2) NULL,
    [DiscountByPercentage] decimal(5,2) NULL,
    [DiscountByNumber] decimal(18,2) NULL,
    [DiscountedPrice] decimal(18,2) NULL,
    [TransferPrice] decimal(18,2) NULL,
    [FinalPrice] decimal(18,2) NULL,
    [PackageCount] decimal(18,2) NULL,
    [PackageUnitPrice] decimal(18,2) NULL,
    [PackingPrice] decimal(18,2) NULL,
    [CheckGroup] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ContractorId] bigint NULL,
    [DestinationWarehouseId] bigint NULL,
    [CustomerInvoiceNumber] nvarchar(50) NULL,
    [SerialNumber] nvarchar(250) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [ManagementDescription] nvarchar(1500) NULL,
    [LastDescription] nvarchar(1500) NULL,
    [RequestGoodsSupplyTypeId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RequestGoodsSupplyTypeHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestGoodsSupplyTypeHistories_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId] FOREIGN KEY ([RequestGoodsSupplyTypeId]) REFERENCES [engineer].[RequestGoodsSupplyTypes] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'Id';
SET @description = N'شناسه محصول سفارش داده شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'ReferenceId';
SET @description = N'نوع محصول سفارش داده شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'Type';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeHistories', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[RequestGoodsSupplyTypeDetailDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(1500) NOT NULL,
    [RequestGoodsSupplyTypeDetailId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RequestGoodsSupplyTypeDetailDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestGoodsSupplyTypeDetailDocuments_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyTypeDetailId] FOREIGN KEY ([RequestGoodsSupplyTypeDetailId]) REFERENCES [engineer].[RequestGoodsSupplyTypeDetails] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailDocuments', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailDocuments', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailDocuments', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailDocuments', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailDocuments', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailDocuments', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[RequestGoodsSupplyTypeDetailHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Importance] int NULL,
    [Status] int NOT NULL DEFAULT 1,
    [ReferenceId] bigint NOT NULL,
    [Type] int NOT NULL,
    [RequestedCount] decimal(18,2) NOT NULL,
    [DelivaryDeadLine] datetime2 NULL,
    [UnitPrice] decimal(18,2) NULL,
    [TotalPrice] decimal(18,2) NULL,
    [PackingPrice] decimal(18,2) NULL,
    [FinalPrice] decimal(18,2) NULL,
    [Description] nvarchar(1500) NULL,
    [ManagementDescription] nvarchar(1500) NULL,
    [CheckGroup] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ContractorId] bigint NULL,
    [PackageId] bigint NULL,
    [PackageCount] decimal(18,2) NULL,
    [PackageUnitPrice] decimal(18,2) NULL,
    [LastDescription] nvarchar(1500) NULL,
    [RequestGoodsSupplyTypeDetailId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RequestGoodsSupplyTypeDetailHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestGoodsSupplyTypeDetailHistories_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyTypeDetailId] FOREIGN KEY ([RequestGoodsSupplyTypeDetailId]) REFERENCES [engineer].[RequestGoodsSupplyTypeDetails] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailHistories', 'COLUMN', N'Id';
SET @description = N'شناسه محصول سفارش داده شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailHistories', 'COLUMN', N'ReferenceId';
SET @description = N'نوع محصول سفارش داده شده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailHistories', 'COLUMN', N'Type';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyTypeDetailHistories', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_RequestGoodsSupplyTypeDetailDocuments_RequestGoodsSupplyTypeDetailId] ON [engineer].[RequestGoodsSupplyTypeDetailDocuments] ([RequestGoodsSupplyTypeDetailId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyTypeDetailHistories_RequestGoodsSupplyTypeDetailId] ON [engineer].[RequestGoodsSupplyTypeDetailHistories] ([RequestGoodsSupplyTypeDetailId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyTypeDetails_CostCenterId] ON [engineer].[RequestGoodsSupplyTypeDetails] ([CostCenterId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyTypeDetails_ProjectProductId] ON [engineer].[RequestGoodsSupplyTypeDetails] ([ProjectProductId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyId] ON [engineer].[RequestGoodsSupplyTypeDetails] ([RequestGoodsSupplyId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyTypeId] ON [engineer].[RequestGoodsSupplyTypeDetails] ([RequestGoodsSupplyTypeId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyTypeDocument_RequestGoodsSupplyTypeId] ON [engineer].[RequestGoodsSupplyTypeDocument] ([RequestGoodsSupplyTypeId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyTypeHistories_RequestGoodsSupplyTypeId] ON [engineer].[RequestGoodsSupplyTypeHistories] ([RequestGoodsSupplyTypeId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyTypes_RequestGoodsSupplyId] ON [engineer].[RequestGoodsSupplyTypes] ([RequestGoodsSupplyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260725120246_RGSType', N'8.0.8');
GO

COMMIT;
GO



