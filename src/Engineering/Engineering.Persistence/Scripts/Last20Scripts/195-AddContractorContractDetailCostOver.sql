BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractorContractDetailCostOvers] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractorId] bigint NOT NULL,
    [Percentage] decimal(5,2) NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [ContractorContractDetailId] bigint NOT NULL,
    [CostOverId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_ContractorContractDetailCostOvers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractorContractDetailCostOvers_ContractorContractDetails_ContractorContractDetailId] FOREIGN KEY ([ContractorContractDetailId]) REFERENCES [engineer].[ContractorContractDetails] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContractorContractDetailCostOvers_CostOvers_CostOverId] FOREIGN KEY ([CostOverId]) REFERENCES [engineer].[CostOvers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_ContractorContractDetailCostOvers_ContractorContractDetailId] ON [engineer].[ContractorContractDetailCostOvers] ([ContractorContractDetailId]);
GO

CREATE INDEX [IX_ContractorContractDetailCostOvers_CostOverId] ON [engineer].[ContractorContractDetailCostOvers] ([CostOverId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250430060305_AddContractorContractDetailCostOver', N'8.0.8');
GO

COMMIT;
GO
