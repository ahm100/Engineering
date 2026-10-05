BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EmployerOperationProducts] ADD [Count] int NULL;
DECLARE @description AS sql_variant;
SET @description = N'تعداد کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'Count';
GO

ALTER TABLE [engineer].[EmployerOperationProducts] ADD [ProductId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'شناسه کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'ProductId';
GO

ALTER TABLE [engineer].[EmployerOperationProductHistories] ADD [Count] int NULL;
GO

ALTER TABLE [engineer].[EmployerOperationProductHistories] ADD [ProductId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260306192127_ProductIdAddedToEOProduct', N'8.0.8');
GO

COMMIT;
GO



