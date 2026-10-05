BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerStatusStatementProjectOperationDetails]') AND [c].[name] = N'CalculatedAmount');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerStatusStatementProjectOperationDetails] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[EmployerStatusStatementProjectOperationDetails] DROP COLUMN [CalculatedAmount];
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperationDetails].[TotalPercentage]', N'SupervisorWorkVolume', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperationDetails].[StatusStatementWorkVolume]', N'EmployerRepresentativeWorkVolume', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperationDetails].[DoneWorkVolume]', N'ContractorWorkVolume', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerStatusStatementProjectOperationDetails].[DonePercentage]', N'ConsultantWorkVolume', N'COLUMN';
GO

ALTER TABLE [engineer].[EmployerStatusStatementProjectOperationDetails] ADD [Description] nvarchar(1500) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250421073333_NewChangesEntityESSPOD', N'8.0.8');
GO

COMMIT;
GO