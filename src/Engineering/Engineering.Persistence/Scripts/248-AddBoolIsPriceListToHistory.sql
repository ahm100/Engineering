BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[OperationInfoHistories] ADD [IsPriceList] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251126152337_AddBoolIsPriceListToHistory', N'8.0.8');
GO

COMMIT;
GO



