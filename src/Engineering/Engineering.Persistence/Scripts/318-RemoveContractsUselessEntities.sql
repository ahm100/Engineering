BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatementServices] DROP CONSTRAINT [FK_ContractorStatusStatementServices_ContractorContractDetailSkills_ContractorContractDetailSkillId];
GO

DROP TABLE [engineer].[ContractorContractDetailThirdParties];
GO

DROP TABLE [engineer].[ContractorContractDetailSkills];
GO

DROP INDEX [IX_ContractorStatusStatementServices_ContractorContractDetailSkillId] ON [engineer].[ContractorStatusStatementServices];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorStatusStatementServices]') AND [c].[name] = N'ContractorContractDetailSkillId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorStatusStatementServices] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ContractorStatusStatementServices] DROP COLUMN [ContractorContractDetailSkillId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260701063146_RemoveContractsUselessEntities', N'8.0.8');
GO

COMMIT;
GO



