BEGIN TRANSACTION;
GO

DROP TABLE [engineer].[TransportationRequestWarehouseProducts];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'ShippingCostId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] ALTER COLUMN [ShippingCostId] bigint NULL;
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] ADD [PackingProductId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] ADD [PalletNumber] nvarchar(max) NULL;
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] ADD [TransferPrice] decimal(18,2) NULL;
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] ADD [Weight] decimal(18,2) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequests]') AND [c].[name] = N'TripId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequests] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[TransportationRequests] ALTER COLUMN [TripId] bigint NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequests]') AND [c].[name] = N'TransportationId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequests] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[TransportationRequests] ALTER COLUMN [TransportationId] bigint NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequests]') AND [c].[name] = N'StartDate');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequests] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[TransportationRequests] ALTER COLUMN [StartDate] datetime2 NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequests]') AND [c].[name] = N'EndDate');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequests] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [engineer].[TransportationRequests] ALTER COLUMN [EndDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[TransportationRequests] ADD [IsAggregate] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [FirstPrefix] nvarchar(10) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'مقدار اول بارنامه داخلی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'FirstPrefix';
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [FixedNumber] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'عدد ثابت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'FixedNumber';
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [PercentageValue] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'درصد محاسبه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'PercentageValue';
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [SecondPrefix] nvarchar(10) NULL;
DECLARE @description AS sql_variant;
SET @description = N'مقدار دوم بارنامه داخلی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'SecondPrefix';
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [Type] int NOT NULL DEFAULT 0;
DECLARE @description AS sql_variant;
SET @description = N'نوع محاسبه پیمانکار حمل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'Type';
GO

ALTER TABLE [engineer].[ShippingCosts] ADD [RegionId] bigint NULL;
GO

CREATE TABLE [engineer].[TransportationContractorPriceWeights] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
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
    CONSTRAINT [PK_TransportationContractorPriceWeights] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationContractorPriceWeights_TransportationContractors_TransportationContractorId] FOREIGN KEY ([TransportationContractorId]) REFERENCES [engineer].[TransportationContractors] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'Id';
SET @description = N'تا وزن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'UntilWeight';
SET @description = N'هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'IsFixed';
SET @description = N'هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'Price';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorPriceWeights', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[TransportationRequestDetails] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [GlobalFreightNumber] nvarchar(50) NULL,
    [ClassifiedFreightNumber] nvarchar(50) NULL,
    [Tax] decimal(18,2) NULL,
    [TransferPrice] decimal(18,2) NULL,
    [ServicePrice] decimal(18,2) NULL,
    [InsuranceNumber] nvarchar(50) NULL,
    [InsurancePrice] decimal(18,2) NULL,
    [ShippingCost] decimal(18,2) NULL,
    [ProductTotalPrice] decimal(18,2) NULL,
    [OutofRange] decimal(18,2) NULL,
    [OrderNumber] nvarchar(50) NULL,
    [TransportationRequestId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_TransportationRequestDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationRequestDetails_TransportationRequests_TransportationRequestId] FOREIGN KEY ([TransportationRequestId]) REFERENCES [engineer].[TransportationRequests] ([Id])
);
GO

CREATE INDEX [IX_TransportationRequestWarehouses_PackingProductId] ON [engineer].[TransportationRequestWarehouses] ([PackingProductId]);
GO

CREATE INDEX [IX_TransportationRequests_DestinationCityId] ON [engineer].[TransportationRequests] ([DestinationCityId]);
GO

CREATE INDEX [IX_TransportationRequests_DriverId] ON [engineer].[TransportationRequests] ([DriverId]);
GO

CREATE INDEX [IX_TransportationRequests_StartingCityId] ON [engineer].[TransportationRequests] ([StartingCityId]);
GO

CREATE INDEX [IX_ShippingCosts_RegionId] ON [engineer].[ShippingCosts] ([RegionId]);
GO

CREATE INDEX [IX_TransportationContractorPriceWeights_TransportationContractorId] ON [engineer].[TransportationContractorPriceWeights] ([TransportationContractorId]);
GO

CREATE INDEX [IX_TransportationRequestDetails_TransportationRequestId] ON [engineer].[TransportationRequestDetails] ([TransportationRequestId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251119061829_AddDetailAndNewPropsOfContractor', N'8.0.8');
GO

COMMIT;
GO



