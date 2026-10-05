BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[OperationInfos] ADD [HasChanged] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'تغییر داده شده یا نه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'HasChanged';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260622072002_AddHasChangedToOI', N'8.0.8');
GO

COMMIT;
GO



