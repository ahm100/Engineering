  BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequests] ADD [IsCredit] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [engineer].[TransportationRequests] ADD [TransportationPaymentType] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250413114442_AddIsCreditAndPaymentType', N'8.0.8');
GO

COMMIT;
GO