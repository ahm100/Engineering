BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [PurchaseLocation] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260719060540_AddPurchaseLocationToRGS', N'8.0.8');
GO

COMMIT;
GO



