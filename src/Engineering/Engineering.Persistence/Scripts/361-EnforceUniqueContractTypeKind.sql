BEGIN TRANSACTION;
GO

DROP INDEX [IX_ContractTypes_ContractId] ON [engineer].[ContractTypes];
GO

CREATE UNIQUE INDEX [IX_ContractTypes_ContractId_Kind] ON [engineer].[ContractTypes] ([ContractId], [Kind]) WHERE [IsDeleted] = 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260826080020_EnforceUniqueContractTypeKind', N'8.0.8');
GO

COMMIT;
GO



