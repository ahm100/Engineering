BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractorStatusStatementPayments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ManagerConfirmedAmount] decimal(18,2) NOT NULL,
    [PrimaryManagerConfirmedAmount] decimal(18,2) NULL,
    [FinalManagerConfirmedAmount] decimal(18,2) NULL,
    [PaymentOrderId] bigint NULL,
    [IsPaid] bit NOT NULL DEFAULT CAST(0 AS bit),
    [PaymentDate] datetime2 NULL,
    [TreasuryPaid] decimal(18,2) NULL,
    [ContractorStatusStatementId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_ContractorStatusStatementPayments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractorStatusStatementPayments_ContractorStatusStatements_ContractorStatusStatementId] FOREIGN KEY ([ContractorStatusStatementId]) REFERENCES [engineer].[ContractorStatusStatements] ([Id])
);
GO

CREATE INDEX [IX_ContractorStatusStatementPayments_ContractorStatusStatementId] ON [engineer].[ContractorStatusStatementPayments] ([ContractorStatusStatementId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251019133003_AddCSSPayment', N'8.0.8');
GO

COMMIT;
GO



