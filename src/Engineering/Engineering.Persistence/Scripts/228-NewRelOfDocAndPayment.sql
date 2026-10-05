BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorStatusStatementDocuments] ADD [ContractorStatusStatementPaymentId] bigint NULL;
GO

CREATE INDEX [IX_ContractorStatusStatementDocuments_ContractorStatusStatementPaymentId] ON [engineer].[ContractorStatusStatementDocuments] ([ContractorStatusStatementPaymentId]);
GO

ALTER TABLE [engineer].[ContractorStatusStatementDocuments] ADD CONSTRAINT [FK_ContractorStatusStatementDocuments_ContractorStatusStatementPayments_ContractorStatusStatementPaymentId] FOREIGN KEY ([ContractorStatusStatementPaymentId]) REFERENCES [engineer].[ContractorStatusStatementPayments] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251029114428_NewRelOfDocAndPayment', N'8.0.8');
GO

COMMIT;
GO



