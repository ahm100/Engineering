BEGIN TRANSACTION;
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperations].[TotalPercentage]', N'SupervisorWorkVolume', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperations].[StatusStatementWorkVolume]', N'EmployerRepresentativeWorkVolume', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperations].[StandardDeviation]', N'EmployerCommercialUnitPrice', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperations].[DoneWorkVolume]', N'EmployerCommercialTotalPrice', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperations].[DonePercentage]', N'ContractorWorkVolume', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperations].[CalculatedAmount]', N'ContractorUnitPrice', N'COLUMN';
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ADD [ConsultantWorkVolume] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ADD [ContractorConfirmeTotalPrice] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ADD [ContractorTotalPrice] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ADD [Description] nvarchar(max) NULL;
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperations] ADD [EmployerCommercialConfirmeTotalPrice] nvarchar(1500) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250421081712_NewChangesEntityESSPO', N'8.0.8');
GO

COMMIT;
GO