BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorContracts]
ADD [ContractorContractType] int NOT NULL DEFAULT 0;
GO

UPDATE CC
SET CC.ContractorContractType = TRY_CAST(CCT.ContractorContractTypeCode AS INT)
FROM engineer.ContractorContracts CC
INNER JOIN engineer.ContractorContractTypes CCT
    ON CC.ContractorContractTypeId = CCT.Id;
GO

ALTER TABLE [engineer].[ContractorContracts] 
DROP CONSTRAINT [FK_ContractorContracts_ContractorContractTypes_ContractorContractTypeId];
GO

DROP INDEX [IX_ContractorContracts_ContractorContractTypeId] 
ON [engineer].[ContractorContracts];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] 
    ON [d].[parent_column_id] = [c].[column_id] 
    AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractorContracts]')
       AND [c].[name] = N'ContractorContractTypeId');

IF @var0 IS NOT NULL 
    EXEC(N'ALTER TABLE [engineer].[ContractorContracts] DROP CONSTRAINT [' + @var0 + '];');
GO

ALTER TABLE [engineer].[ContractorContracts] 
DROP COLUMN [ContractorContractTypeId];
GO

DROP TABLE [engineer].[ContractorContractTypes];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251208061756_RemoveContractorContractType', N'8.0.8');
GO

COMMIT;
GO
