BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [DeviceCode] nvarchar(100) NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [DeviceEnName] nvarchar(250) NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [DeviceName] nvarchar(250) NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [DeviceNumber] nvarchar(250) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260801071533_AddDeviceName&Code', N'8.0.8');
GO

COMMIT;
GO



