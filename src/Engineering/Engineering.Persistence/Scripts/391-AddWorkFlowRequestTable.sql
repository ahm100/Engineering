BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'Id';
GO

ALTER TABLE [engineer].[WorkflowOutboxes] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'Created';
GO

ALTER TABLE [engineer].[WorkflowOutboxes] ADD [CreatorId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
DECLARE @description AS sql_variant;
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'CreatorId';
GO

ALTER TABLE [engineer].[WorkflowOutboxes] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'IsDeleted';
GO

ALTER TABLE [engineer].[WorkflowOutboxes] ADD [Updated] datetime2 NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'Updated';
GO

ALTER TABLE [engineer].[WorkflowOutboxes] ADD [UpdaterId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'UpdaterId';
GO

ALTER TABLE [engineer].[WorkflowInbox] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'Created';
GO

ALTER TABLE [engineer].[WorkflowInbox] ADD [CreatorId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
DECLARE @description AS sql_variant;
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'CreatorId';
GO

ALTER TABLE [engineer].[WorkflowInbox] ADD [Id] bigint NOT NULL IDENTITY(2, 1);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'Id';
GO

ALTER TABLE [engineer].[WorkflowInbox] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'IsDeleted';
GO

ALTER TABLE [engineer].[WorkflowInbox] ADD [RowVersion] rowversion NOT NULL;
GO

ALTER TABLE [engineer].[WorkflowInbox] ADD [Updated] datetime2 NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'Updated';
GO

ALTER TABLE [engineer].[WorkflowInbox] ADD [UpdaterId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'UpdaterId';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260915131238_AddWorkFlowRequestTable', N'8.0.8');
GO

COMMIT;
GO



