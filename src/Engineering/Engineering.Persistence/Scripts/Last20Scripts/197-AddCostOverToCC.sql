BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorContracts] ADD [TotalCostOveredAmount] decimal(18,2) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorContractDetailCostOvers]') AND [c].[name] = N'ContractorContractDetailId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractorContractDetailCostOvers] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ContractorContractDetailCostOvers] ALTER COLUMN [ContractorContractDetailId] bigint NULL;
GO

ALTER TABLE [engineer].[ContractorContractDetailCostOvers] ADD [ContractorContractId] bigint NULL;
GO

CREATE INDEX [IX_ContractorContractDetailCostOvers_ContractorContractId] ON [engineer].[ContractorContractDetailCostOvers] ([ContractorContractId]);
GO

ALTER TABLE [engineer].[ContractorContractDetailCostOvers] ADD CONSTRAINT [FK_ContractorContractDetailCostOvers_ContractorContracts_ContractorContractId] FOREIGN KEY ([ContractorContractId]) REFERENCES [engineer].[ContractorContracts] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250430104746_AddCostOverToCC', N'8.0.8');
GO

COMMIT;
GO

