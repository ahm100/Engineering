BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectCostCenterRequest] DROP CONSTRAINT [FK_ProjectCostCenterRequest_CostCenters_CostCenterId];
GO

ALTER TABLE [engineer].[ProjectCostCenterRequest] DROP CONSTRAINT [FK_ProjectCostCenterRequest_Projects_ProjectId];
GO

ALTER TABLE [engineer].[ProjectCostCenterRequest] DROP CONSTRAINT [PK_ProjectCostCenterRequest];
GO

EXEC sp_rename N'[engineer].[ProjectCostCenterRequest]', N'ProjectCostCenterRequests';
GO

EXEC sp_rename N'[engineer].[ProjectCostCenterRequests].[IX_ProjectCostCenterRequest_ProjectId]', N'IX_ProjectCostCenterRequests_ProjectId', N'INDEX';
GO

EXEC sp_rename N'[engineer].[ProjectCostCenterRequests].[IX_ProjectCostCenterRequest_CostCenterId]', N'IX_ProjectCostCenterRequests_CostCenterId', N'INDEX';
GO

DECLARE @description AS sql_variant;
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenterRequests', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenterRequests', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenterRequests', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenterRequests', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenterRequests', 'COLUMN', N'Created';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectCostCenterRequests]') AND [c].[name] = N'Id');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectCostCenterRequests] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectCostCenterRequests] ALTER COLUMN [Id] bigint NOT NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCostCenterRequests', 'COLUMN', N'Id';
GO

ALTER TABLE [engineer].[ProjectCostCenterRequests] ADD CONSTRAINT [PK_ProjectCostCenterRequests] PRIMARY KEY ([Id]);
GO

ALTER TABLE [engineer].[ProjectCostCenterRequests] ADD CONSTRAINT [FK_ProjectCostCenterRequests_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [engineer].[CostCenters] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [engineer].[ProjectCostCenterRequests] ADD CONSTRAINT [FK_ProjectCostCenterRequests_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260810110805_ProjectCostCenterRequestTableBugFix', N'8.0.8');
GO

COMMIT;
GO



