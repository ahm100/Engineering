BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[OperationInfos] ADD [IsPriceList] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'فهرست بها هست یا نه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfos', 'COLUMN', N'IsPriceList';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251126145748_AddBoolIsPriceList', N'8.0.8');
GO

COMMIT;
GO



