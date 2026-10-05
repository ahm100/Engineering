BEGIN TRANSACTION;
GO

DROP TABLE [engineer].[ProjectDocumentFiles];
GO

DROP TABLE [engineer].[ProjectDocuments];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260908065626_delete-old-project-documents', N'8.0.8');
GO

COMMIT;
GO



