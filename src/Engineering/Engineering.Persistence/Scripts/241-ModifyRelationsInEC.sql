BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EmployerContractHeads] DROP CONSTRAINT [FK_EmployerContractHeads_Projects_ProjectId];
GO

EXEC sp_rename N'[engineer].[EmployerContractHeads].[ProjectId]', N'CostCenterId', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[EmployerContractHeads].[IX_EmployerContractHeads_ProjectId]', N'IX_EmployerContractHeads_CostCenterId', N'INDEX';
GO

ALTER TABLE [engineer].[EmployerContracts] ADD [ProjectId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

CREATE INDEX [IX_EmployerContracts_ProjectId] ON [engineer].[EmployerContracts] ([ProjectId]);
GO

ALTER TABLE [engineer].[EmployerContractHeads] ADD CONSTRAINT [FK_EmployerContractHeads_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [engineer].[CostCenters] ([Id]);
GO

ALTER TABLE [engineer].[EmployerContracts] ADD CONSTRAINT [FK_EmployerContracts_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251117083718_ModifyRelationsInEC', N'8.0.8');
GO

COMMIT;
GO



