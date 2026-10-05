BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[OperationInfoHistories] ADD [BasePrice] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251120061736_AddBasePriceToHistory', N'8.0.8');
GO

COMMIT;
GO



