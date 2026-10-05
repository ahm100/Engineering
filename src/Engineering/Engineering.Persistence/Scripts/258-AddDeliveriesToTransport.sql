BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequests] ADD [DeliveryMethod] int NULL;
GO

ALTER TABLE [engineer].[TransportationRequests] ADD [DeliveryType] int NULL;
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [DeliveryMethod] int NULL;
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [DeliveryType] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251210122151_AddDeliveriesToTransport', N'8.0.8');
GO

COMMIT;
GO

--create synonym engineer.ViewPackingShippingDetail for warehouseDb.Dbo.PackingShippingDetails


