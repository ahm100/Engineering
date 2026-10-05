BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperations] ADD [GoodsInProgress] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507085825_AddGoodsInProgress', N'8.0.8');
GO

COMMIT;
GO
