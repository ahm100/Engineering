BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ShippingCostHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ShippingCostId] bigint NOT NULL,
    [TransportationContractorId] bigint NOT NULL,
    [MachineTypeId] bigint NOT NULL,
    [SourceCityId] bigint NOT NULL,
    [RegionId] bigint NULL,
    [DestinationCityId] bigint NOT NULL,
    [Count] int NULL,
    [LoadWeight] decimal(18,2) NULL,
    [Price] decimal(18,2) NOT NULL,
    [Tax] decimal(18,2) NULL,
    [ThirdPartyId] bigint NULL,
    [Latitude] decimal(18,9) NULL,
    [Longitude] decimal(18,9) NULL,
    [FromDate] datetime2 NULL,
    [ToDate] datetime2 NULL,
    [Description] nvarchar(max) NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ShippingCostHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShippingCostHistories_MachineTypes_MachineTypeId] FOREIGN KEY ([MachineTypeId]) REFERENCES [engineer].[MachineTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ShippingCostHistories_ShippingCosts_ShippingCostId] FOREIGN KEY ([ShippingCostId]) REFERENCES [engineer].[ShippingCosts] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Id';
SET @description = N'تعداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Count';
SET @description = N'وزن بار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'LoadWeight';
SET @description = N'قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Price';
SET @description = N'مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Tax';
SET @description = N'عرض جغرافیایی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Latitude';
SET @description = N'طول جغرافیایی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Longitude';
SET @description = N'از تاریخ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'FromDate';
SET @description = N'تا تاریخ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'ToDate';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCostHistories', 'COLUMN', N'IsActive';
GO

CREATE TABLE [engineer].[TransportationContractorPriceWeightHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TransportationContractorPriceWeightId] bigint NOT NULL,
    [TransportationContractorId] bigint NOT NULL,
    [UntilWeight] decimal(18,2) NOT NULL,
    [IsFixed] bit NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TransportationContractorPriceWeightHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationContractorPriceWeightHistories_TransportationContractorPriceWeights_TransportationContractorPriceWeightId] FOREIGN KEY ([TransportationContractorPriceWeightId]) REFERENCES [engineer].[TransportationContractorPriceWeights] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'Id';
SET @description = N'تا وزن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'UntilWeight';
SET @description = N'هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'IsFixed';
SET @description = N'هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'Price';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeightHistories', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ShippingCostHistories_CreatorId] ON [engineer].[ShippingCostHistories] ([CreatorId]);
GO

CREATE INDEX [IX_ShippingCostHistories_DestinationCityId] ON [engineer].[ShippingCostHistories] ([DestinationCityId]);
GO

CREATE INDEX [IX_ShippingCostHistories_MachineTypeId] ON [engineer].[ShippingCostHistories] ([MachineTypeId]);
GO

CREATE INDEX [IX_ShippingCostHistories_RegionId] ON [engineer].[ShippingCostHistories] ([RegionId]);
GO

CREATE INDEX [IX_ShippingCostHistories_ShippingCostId] ON [engineer].[ShippingCostHistories] ([ShippingCostId]);
GO

CREATE INDEX [IX_ShippingCostHistories_SourceCityId] ON [engineer].[ShippingCostHistories] ([SourceCityId]);
GO

CREATE INDEX [IX_ShippingCostHistories_ThirdPartyId] ON [engineer].[ShippingCostHistories] ([ThirdPartyId]);
GO

CREATE INDEX [IX_TransportationContractorPriceWeightHistories_CreatorId] ON [engineer].[TransportationContractorPriceWeightHistories] ([CreatorId]);
GO

CREATE INDEX [IX_TransportationContractorPriceWeightHistories_TransportationContractorPriceWeightId] ON [engineer].[TransportationContractorPriceWeightHistories] ([TransportationContractorPriceWeightId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260105095410_AddHistoryEntities', N'8.0.8');
GO

COMMIT;
GO



