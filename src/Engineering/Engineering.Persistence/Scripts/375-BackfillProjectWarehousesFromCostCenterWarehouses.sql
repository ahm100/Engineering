BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260831124225_BackfillProjectWarehousesFromCostCenterWarehouses', N'8.0.8');
GO

COMMIT;
GO



