BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractorStatusStatementCostOvers] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Amount] decimal(18,2) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [RequestRewardId] bigint NOT NULL,
    [ContractorStatusStatementId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_ContractorStatusStatementCostOvers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractorStatusStatementCostOvers_ContractorContractDetailCostOvers_RequestRewardId] FOREIGN KEY ([RequestRewardId]) REFERENCES [engineer].[ContractorContractDetailCostOvers] ([Id]),
    CONSTRAINT [FK_ContractorStatusStatementCostOvers_ContractorStatusStatements_ContractorStatusStatementId] FOREIGN KEY ([ContractorStatusStatementId]) REFERENCES [engineer].[ContractorStatusStatements] ([Id])
);
GO

CREATE INDEX [IX_ContractorStatusStatementCostOvers_ContractorStatusStatementId] ON [engineer].[ContractorStatusStatementCostOvers] ([ContractorStatusStatementId]);
GO

CREATE INDEX [IX_ContractorStatusStatementCostOvers_RequestRewardId] ON [engineer].[ContractorStatusStatementCostOvers] ([RequestRewardId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250503072936_AddContractorStatusStatementCostOver', N'8.0.8');
GO

COMMIT;
GO