BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [BasePrice] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'هزینه اولیه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'BasePrice';
GO

ALTER TABLE [engineer].[ProjectOperationHistories] ADD [ChangedPrice] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N' آخریم هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'ChangedPrice';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251126143701_AddBase&ChangePriceToPOHistory', N'8.0.8');
GO

COMMIT;
GO



