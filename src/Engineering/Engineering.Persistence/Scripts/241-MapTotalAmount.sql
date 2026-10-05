BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperationDetails] ADD [FinalAmount] decimal(18,5) NOT NULL DEFAULT 1.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251117095211_MapTotalAmount', N'8.0.8');
GO

COMMIT;
GO



