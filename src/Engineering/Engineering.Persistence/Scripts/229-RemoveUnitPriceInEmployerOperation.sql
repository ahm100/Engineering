BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251104125303_RemoveUnitPriceInEmployerOperation', N'8.0.8');
GO

COMMIT;
GO



