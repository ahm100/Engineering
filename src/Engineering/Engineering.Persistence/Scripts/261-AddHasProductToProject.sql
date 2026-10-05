BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Projects] ADD [HasProduct] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'کالا دارد یا خیر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'HasProduct';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251214081146_AddHasProductToProject', N'8.0.8');
GO

COMMIT;
GO



