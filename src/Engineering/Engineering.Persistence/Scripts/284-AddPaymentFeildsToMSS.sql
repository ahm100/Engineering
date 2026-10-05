BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestMachineryStatusStatements] ADD [CostCategoryId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestMachineryStatusStatements] ADD [CostGroupId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestMachineryStatusStatements] ADD [DocumentTypeId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestMachineryStatusStatements] ADD [PreferentialTypeId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260405110041_AddPaymentFeildsToMSS', N'8.0.8');
GO

COMMIT;
GO



