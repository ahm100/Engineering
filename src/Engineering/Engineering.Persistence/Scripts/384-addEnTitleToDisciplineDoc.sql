BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[DisciplineDocs] ADD [EnTitle] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260913063600_addEnTitleToDisciplineDoc', N'8.0.8');
GO

COMMIT;
GO



