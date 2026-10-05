BEGIN TRANSACTION;
GO

DROP INDEX [IX_TransportationCargos_PackingShippingId] ON [engineer].[TransportationCargos];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationCargos]') AND [c].[name] = N'PackingShippingId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationCargos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[TransportationCargos] DROP COLUMN [PackingShippingId];
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [DeliveryMethod] int NULL;
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [DeliveryType] int NULL;
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [Driver] nvarchar(250) NULL;
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [DriverPhoneNumber] nvarchar(250) NULL;
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [NumberPlate] nvarchar(250) NULL;
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [PackingShippingType] int NULL;
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [PostageDate] datetime2 NULL;
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [TransportationContractorId] bigint NULL;
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD [VehicleName] nvarchar(250) NULL;
GO

CREATE INDEX [IX_TransportationCargoPallets_TransportationContractorId] ON [engineer].[TransportationCargoPallets] ([TransportationContractorId]);
GO

ALTER TABLE [engineer].[TransportationCargoPallets] ADD CONSTRAINT [FK_TransportationCargoPallets_TransportationContractors_TransportationContractorId] FOREIGN KEY ([TransportationContractorId]) REFERENCES [engineer].[TransportationContractors] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260618085419_AddShippingDetailToPallets', N'8.0.8');
GO

COMMIT;
GO



