BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperationDetails] ADD [CreatedProductId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507094937_AddCreatedProductId', N'8.0.8');
GO

COMMIT;
GO
