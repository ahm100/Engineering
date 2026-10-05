BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[OperationInfos] ADD [YearId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260912082519_AddYearIdToOpertaionInfo', N'8.0.8');
GO

COMMIT;
GO



