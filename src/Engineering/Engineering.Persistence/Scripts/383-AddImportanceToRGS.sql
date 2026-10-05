BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [Importance] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260908065843_AddImportanceToRGS', N'8.0.8');
GO

COMMIT;
GO



