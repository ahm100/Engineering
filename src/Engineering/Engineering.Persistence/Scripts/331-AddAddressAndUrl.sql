BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [ConsumptionAddress] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [ConsumptionRateAndInventoryUrl] nvarchar(1500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260719124224_AddAddressAndUrl', N'8.0.8');
GO

COMMIT;
GO



