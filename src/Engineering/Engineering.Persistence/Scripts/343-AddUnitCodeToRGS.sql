BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [UnitCode] nvarchar(100) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260803074921_AddUnitCodeToRGS', N'8.0.8');
GO

COMMIT;
GO



