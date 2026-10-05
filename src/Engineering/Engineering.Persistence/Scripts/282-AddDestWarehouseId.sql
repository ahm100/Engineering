BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[FiduciaryProductDetailManagements] ADD [DestinationWarehouseId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260308082704_AddDestWarehouseId', N'8.0.8');
GO

COMMIT;
GO



