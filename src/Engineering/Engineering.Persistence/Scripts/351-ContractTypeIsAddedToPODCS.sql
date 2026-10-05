BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperationDetailContractorServices] ADD [Type] int NOT NULL DEFAULT 2;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260818042145_ContractTypeIsAddedToPODCS', N'8.0.8');
GO

COMMIT;
GO



