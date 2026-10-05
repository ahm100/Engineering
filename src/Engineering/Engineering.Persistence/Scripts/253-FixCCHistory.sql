BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[CostCenterHistories] DROP CONSTRAINT [FK_CostCenterHistories_CostCenters_CostCenterTypesId];
GO

CREATE INDEX [IX_CostCenterHistories_CostCenterId] ON [engineer].[CostCenterHistories] ([CostCenterId]);
GO

ALTER TABLE [engineer].[CostCenterHistories] ADD CONSTRAINT [FK_CostCenterHistories_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [engineer].[CostCenters] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251130140147_FixCCHistory', N'8.0.8');
GO

COMMIT;
GO



