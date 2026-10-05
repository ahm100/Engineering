BEGIN TRANSACTION;
GO

DROP TABLE [engineer].[RequestGoodsSupplyTemporaries];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260721130303_RemoveRGSTemporary', N'8.0.8');
GO

COMMIT;
GO



