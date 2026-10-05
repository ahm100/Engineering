BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[CostCenters] ADD [IsDefault] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'پیش‌فرض بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenters', 'COLUMN', N'IsDefault';
GO

ALTER TABLE [engineer].[CostCenterHistories] ADD [IsDefault] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'پیش‌فرض بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostCenterHistories', 'COLUMN', N'IsDefault';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260513060923_AddIsDefaultToCostCenter', N'8.0.8');
GO

COMMIT;
GO



