BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [FK_TransportationRequestWarehouses_ShippingCosts_ShippingCostId];
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [FK_TransportationRequestWarehouses_TransportationRequests_TransportationRequestId];
GO

DROP INDEX [IX_TransportationRequestWarehouses_PackingId] ON [engineer].[TransportationRequestWarehouses];
GO

DROP INDEX [IX_TransportationRequestWarehouses_ShippingCostId] ON [engineer].[TransportationRequestWarehouses];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'PackingId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP COLUMN [PackingId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'PackingNumber');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP COLUMN [PackingNumber];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'RefrenceId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP COLUMN [RefrenceId];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'ShippingCostId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP COLUMN [ShippingCostId];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'ThirdPartyName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP COLUMN [ThirdPartyName];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'TransferPrice');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP COLUMN [TransferPrice];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationRequestWarehouses]') AND [c].[name] = N'Weight');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [engineer].[TransportationRequestWarehouses] DROP COLUMN [Weight];
GO

EXEC sp_rename N'[engineer].[TransportationRequestWarehouses].[WarehouseId]', N'PackingSourceAddressId', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[TransportationRequestWarehouses].[TransportationRequestId]', N'TransportationCargoPalletId', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[TransportationRequestWarehouses].[ThirdPartyId]', N'PackingDestinationAddressId', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[TransportationRequestWarehouses].[IX_TransportationRequestWarehouses_WarehouseId]', N'IX_TransportationRequestWarehouses_PackingSourceAddressId', N'INDEX';
GO

EXEC sp_rename N'[engineer].[TransportationRequestWarehouses].[IX_TransportationRequestWarehouses_TransportationRequestId]', N'IX_TransportationRequestWarehouses_TransportationCargoPalletId', N'INDEX';
GO

EXEC sp_rename N'[engineer].[TransportationRequestWarehouses].[IX_TransportationRequestWarehouses_ThirdPartyId]', N'IX_TransportationRequestWarehouses_PackingDestinationAddressId', N'INDEX';
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationContractors]') AND [c].[name] = N'ThirdPartyId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationContractors] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [engineer].[TransportationContractors] ALTER COLUMN [ThirdPartyId] bigint NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationContractors]') AND [c].[name] = N'StartOfContract');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationContractors] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [engineer].[TransportationContractors] ALTER COLUMN [StartOfContract] datetime2 NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationContractors]') AND [c].[name] = N'PercentageValue');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationContractors] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [engineer].[TransportationContractors] ALTER COLUMN [PercentageValue] decimal(18,2) NULL;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationContractors]') AND [c].[name] = N'EndOfContract');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationContractors] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [engineer].[TransportationContractors] ALTER COLUMN [EndOfContract] datetime2 NULL;
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [Title] nvarchar(max) NULL;
DECLARE @description AS sql_variant;
SET @description = N'عنوان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'Title';
GO

CREATE TABLE [engineer].[TransportationCargos] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [PackingNumber] bigint NOT NULL,
    [PackingId] bigint NOT NULL,
    [ThirdPartyId] bigint NULL,
    [ThirdPartyName] nvarchar(max) NULL,
    [PackingShippingId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TransportationCargos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [engineer].[TransportationCargoPallets] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TransportationCargoId] bigint NOT NULL,
    [TransportationRequestId] bigint NULL,
    [PalletNumber] nvarchar(max) NULL,
    [PackingPalletId] bigint NOT NULL,
    [PackingSourceAddressId] bigint NULL,
    [PackingDestinationAddressId] bigint NULL,
    [Price] decimal(18,2) NULL,
    [TransferPrice] decimal(18,2) NULL,
    [Weight] decimal(18,2) NULL,
    [Quantity] decimal(18,2) NULL,
    [ShippingCostId] bigint NULL,
    [RefrenceId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TransportationCargoPallets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationCargoPallets_ShippingCosts_ShippingCostId] FOREIGN KEY ([ShippingCostId]) REFERENCES [engineer].[ShippingCosts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransportationCargoPallets_TransportationCargos_TransportationCargoId] FOREIGN KEY ([TransportationCargoId]) REFERENCES [engineer].[TransportationCargos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransportationCargoPallets_TransportationRequests_TransportationRequestId] FOREIGN KEY ([TransportationRequestId]) REFERENCES [engineer].[TransportationRequests] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_TransportationCargoPallets_PackingDestinationAddressId] ON [engineer].[TransportationCargoPallets] ([PackingDestinationAddressId]);
GO

CREATE INDEX [IX_TransportationCargoPallets_PackingPalletId] ON [engineer].[TransportationCargoPallets] ([PackingPalletId]);
GO

CREATE INDEX [IX_TransportationCargoPallets_PackingSourceAddressId] ON [engineer].[TransportationCargoPallets] ([PackingSourceAddressId]);
GO

CREATE INDEX [IX_TransportationCargoPallets_ShippingCostId] ON [engineer].[TransportationCargoPallets] ([ShippingCostId]);
GO

CREATE INDEX [IX_TransportationCargoPallets_TransportationCargoId] ON [engineer].[TransportationCargoPallets] ([TransportationCargoId]);
GO

CREATE INDEX [IX_TransportationCargoPallets_TransportationRequestId] ON [engineer].[TransportationCargoPallets] ([TransportationRequestId]);
GO

CREATE INDEX [IX_TransportationCargos_PackingId] ON [engineer].[TransportationCargos] ([PackingId]);
GO

CREATE INDEX [IX_TransportationCargos_PackingShippingId] ON [engineer].[TransportationCargos] ([PackingShippingId]);
GO

CREATE INDEX [IX_TransportationCargos_ThirdPartyId] ON [engineer].[TransportationCargos] ([ThirdPartyId]);
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] ADD CONSTRAINT [FK_TransportationRequestWarehouses_TransportationCargoPallets_TransportationCargoPalletId] FOREIGN KEY ([TransportationCargoPalletId]) REFERENCES [engineer].[TransportationCargoPallets] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260613052433_AddLogesticCargos', N'8.0.8');
GO

COMMIT;
GO



