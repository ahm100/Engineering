BEGIN TRANSACTION;
GO

DROP TABLE [engineer].[RequestGoodsSupplyProductScales];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260722055939_RemoveRGSProductScale', N'8.0.8');
GO

COMMIT;
GO



