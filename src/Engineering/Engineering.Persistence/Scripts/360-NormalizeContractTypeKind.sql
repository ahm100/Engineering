BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260826073659_NormalizeContractTypeKind', N'8.0.8');
GO

COMMIT;
GO



