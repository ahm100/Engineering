BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatementCostOvers] DROP CONSTRAINT [FK_ContractorStatusStatementCostOvers_ContractorContractDetailCostOvers_RequestRewardId];
GO

EXEC sp_rename N'[engineer].[ContractorStatusStatementCostOvers].[RequestRewardId]', N'ContractorContractDetailCostOverId', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[ContractorStatusStatementCostOvers].[IX_ContractorStatusStatementCostOvers_RequestRewardId]', N'IX_ContractorStatusStatementCostOvers_ContractorContractDetailCostOverId', N'INDEX';
GO

ALTER TABLE [engineer].[ContractorStatusStatementCostOvers] ADD CONSTRAINT [FK_ContractorStatusStatementCostOvers_ContractorContractDetailCostOvers_ContractorContractDetailCostOverId] FOREIGN KEY ([ContractorContractDetailCostOverId]) REFERENCES [engineer].[ContractorContractDetailCostOvers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251014104713_FixedNameCostOver', N'8.0.8');
GO

COMMIT;
GO



