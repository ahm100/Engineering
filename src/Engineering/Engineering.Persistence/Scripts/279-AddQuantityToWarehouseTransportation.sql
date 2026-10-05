BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequestWarehouses] ADD [Quantity] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260217140727_AddQuantityToWarehouseTransportation', N'8.0.8');
GO

COMMIT;
GO



