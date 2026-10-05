BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EmployerStatusStatements] DROP CONSTRAINT [FK_EmployerStatusStatements_EmployerContracts_EmployerContractId];
GO

ALTER TABLE [engineer].[ProjectOperations] DROP CONSTRAINT [FK_ProjectOperations_EmployerContracts_EmployerContractId];
GO

DROP TABLE [engineer].[ConsiderationDependencies];
GO

DROP TABLE [engineer].[ContractCostOverImpacts];
GO

DROP TABLE [engineer].[DocumentDetailUrls];
GO

DROP TABLE [engineer].[EmployerConsiderations];
GO

DROP TABLE [engineer].[ContractCostOvers];
GO

DROP TABLE [engineer].[DocumentDetails];
GO

DROP TABLE [engineer].[EmployerContracts];
GO

DROP INDEX [IX_ProjectOperations_EmployerContractId] ON [engineer].[ProjectOperations];
GO

DROP INDEX [IX_EmployerStatusStatements_EmployerContractId] ON [engineer].[EmployerStatusStatements];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectOperations]') AND [c].[name] = N'EmployerContractId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectOperations] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectOperations] DROP COLUMN [EmployerContractId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[EmployerStatusStatements]') AND [c].[name] = N'EmployerContractId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[EmployerStatusStatements] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[EmployerStatusStatements] DROP COLUMN [EmployerContractId];
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
SET @description = N'کد مرجع تفص?ل?';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251014122833_RemoveEmployerContract', N'8.0.8');
GO

COMMIT;
GO



