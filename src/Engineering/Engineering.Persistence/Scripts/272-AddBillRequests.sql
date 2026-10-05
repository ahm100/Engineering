BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequestDocuments] ADD [IsBill] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260118080241_AddBillRequests', N'8.0.8');
GO

COMMIT;
GO



