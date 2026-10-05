BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequests] ADD [TransportationContractorId] bigint NULL;
GO

DECLARE @description AS sql_variant;
SET @description = N'شروع قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'StartOfContract';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه قدیمی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'LegacyId';
GO

DECLARE @description AS sql_variant;
SET @description = N'پایان قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'EndOfContract';
GO

CREATE TABLE [engineer].[ShippingCosts] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TransportationContractorId] bigint NOT NULL,
    [MachineTypeId] bigint NOT NULL,
    [SourceCityId] bigint NOT NULL,
    [DestinationCityId] bigint NOT NULL,
    [Count] int NOT NULL,
    [LoadWeight] decimal(18,2) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Tax] decimal(18,2) NOT NULL,
    [LegacyId] bigint NULL,
    [Description] nvarchar(max) NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ShippingCosts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShippingCosts_MachineTypes_MachineTypeId] FOREIGN KEY ([MachineTypeId]) REFERENCES [engineer].[MachineTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ShippingCosts_TransportationContractors_TransportationContractorId] FOREIGN KEY ([TransportationContractorId]) REFERENCES [engineer].[TransportationContractors] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Id';
SET @description = N'تعداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Count';
SET @description = N'وزن بار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'LoadWeight';
SET @description = N'قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Price';
SET @description = N'مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Tax';
SET @description = N'شناسه قدیمی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'LegacyId';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'IsActive';
GO

CREATE TABLE [engineer].[TransportationRequestWarehouses] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TransportationRequestId] bigint NOT NULL,
    [WarehouseId] bigint NOT NULL,
    [ThirdPartyId] bigint NULL,
    [ThirdPartyName] nvarchar(max) NULL,
    [PackingId] bigint NOT NULL,
    [PackingNumber] bigint NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [ShippingCostId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TransportationRequestWarehouses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationRequestWarehouses_ShippingCosts_ShippingCostId] FOREIGN KEY ([ShippingCostId]) REFERENCES [engineer].[ShippingCosts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransportationRequestWarehouses_TransportationRequests_TransportationRequestId] FOREIGN KEY ([TransportationRequestId]) REFERENCES [engineer].[TransportationRequests] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [engineer].[TransportationRequestWarehouseProducts] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TransportationRequestWarehouseId] bigint NOT NULL,
    [Quantity] int NOT NULL,
    [PalletNumber] nvarchar(max) NULL,
    [ProductId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TransportationRequestWarehouseProducts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationRequestWarehouseProducts_TransportationRequestWarehouses_TransportationRequestWarehouseId] FOREIGN KEY ([TransportationRequestWarehouseId]) REFERENCES [engineer].[TransportationRequestWarehouses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_TransportationRequests_TransportationContractorId] ON [engineer].[TransportationRequests] ([TransportationContractorId]);
GO

CREATE INDEX [IX_ShippingCosts_DestinationCityId] ON [engineer].[ShippingCosts] ([DestinationCityId]);
GO

CREATE INDEX [IX_ShippingCosts_MachineTypeId] ON [engineer].[ShippingCosts] ([MachineTypeId]);
GO

CREATE INDEX [IX_ShippingCosts_SourceCityId] ON [engineer].[ShippingCosts] ([SourceCityId]);
GO

CREATE INDEX [IX_ShippingCosts_TransportationContractorId] ON [engineer].[ShippingCosts] ([TransportationContractorId]);
GO

CREATE INDEX [IX_TransportationRequestWarehouseProducts_ProductId] ON [engineer].[TransportationRequestWarehouseProducts] ([ProductId]);
GO

CREATE INDEX [IX_TransportationRequestWarehouseProducts_TransportationRequestWarehouseId] ON [engineer].[TransportationRequestWarehouseProducts] ([TransportationRequestWarehouseId]);
GO

CREATE INDEX [IX_TransportationRequestWarehouses_PackingId] ON [engineer].[TransportationRequestWarehouses] ([PackingId]);
GO

CREATE INDEX [IX_TransportationRequestWarehouses_ShippingCostId] ON [engineer].[TransportationRequestWarehouses] ([ShippingCostId]);
GO

CREATE INDEX [IX_TransportationRequestWarehouses_ThirdPartyId] ON [engineer].[TransportationRequestWarehouses] ([ThirdPartyId]);
GO

CREATE INDEX [IX_TransportationRequestWarehouses_TransportationRequestId] ON [engineer].[TransportationRequestWarehouses] ([TransportationRequestId]);
GO

CREATE INDEX [IX_TransportationRequestWarehouses_WarehouseId] ON [engineer].[TransportationRequestWarehouses] ([WarehouseId]);
GO

ALTER TABLE [engineer].[TransportationRequests] ADD CONSTRAINT [FK_TransportationRequests_TransportationContractors_TransportationContractorId] FOREIGN KEY ([TransportationContractorId]) REFERENCES [engineer].[TransportationContractors] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251109065635_Addshippingcost', N'8.0.8');
GO

COMMIT;
GO



