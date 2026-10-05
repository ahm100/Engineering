BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectProducts] ADD [TolerancePercentage] decimal(18,5) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'درصد تلورانس';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'TolerancePercentage';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251215064131_AddTolerancePercentageToProjectProduct', N'8.0.8');
GO

COMMIT;
GO



