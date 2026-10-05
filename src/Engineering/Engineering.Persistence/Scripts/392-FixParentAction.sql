BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperationDetailCommentDocuments] DROP CONSTRAINT [FK_ProjectOperationDetailCommentDocuments_ProjectOperationDetailComments_ProjectOperationDetailCommentId];
GO

ALTER TABLE [engineer].[ProjectOperationDetailComments] DROP CONSTRAINT [FK_ProjectOperationDetailComments_ProjectOperationDetailComments_ParentId];
GO

ALTER TABLE [engineer].[ProjectOperationDetailComments] DROP CONSTRAINT [FK_ProjectOperationDetailComments_ProjectOperationDetails_ProjectOperationDetailId];
GO

ALTER TABLE [engineer].[ProjectOperationDetailComments] DROP CONSTRAINT [PK_ProjectOperationDetailComments];
GO

EXEC sp_rename N'[engineer].[ProjectOperationDetailComments]', N'ProjectOperationDetailCmts';
GO

EXEC sp_rename N'[engineer].[ProjectOperationDetailCmts].[IX_ProjectOperationDetailComments_ProjectOperationDetailId]', N'IX_ProjectOperationDetailCmts_ProjectOperationDetailId', N'INDEX';
GO

EXEC sp_rename N'[engineer].[ProjectOperationDetailCmts].[IX_ProjectOperationDetailComments_ParentId]', N'IX_ProjectOperationDetailCmts_ParentId', N'INDEX';
GO

ALTER TABLE [engineer].[RequestGoodsSupplyTypeDetails] ADD [DescriptionEn] nvarchar(1500) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectScheduleTasks]') AND [c].[name] = N'IsSummary');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectScheduleTasks] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectScheduleTasks] ALTER COLUMN [IsSummary] bit NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘ü╪╣╪º┘ä█î╪¬ ╪¿╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'IsSummary';
GO

DECLARE @description AS sql_variant;
SET @description = N'┘ê╪╢╪╣█î╪¬ ╪»╪│╪¬█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'IsManuallyScheduled';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'ProjectManager');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[Projects] ALTER COLUMN [ProjectManager] bigint NULL;
GO

ALTER TABLE [engineer].[OperationInfos] ADD [YearId] bigint NULL;
GO

ALTER TABLE [engineer].[CostOvers] ADD [ApprovalStatus] int NOT NULL DEFAULT 0;
DECLARE @description AS sql_variant;
SET @description = N'┘ê╪╢╪╣█î╪¬ ╪¬╪ú█î█î╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostOvers', 'COLUMN', N'ApprovalStatus';
GO

ALTER TABLE [engineer].[CostOvers] ADD [CurrentApprovalAttemptId] uniqueidentifier NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç ╪¬┘ä╪º╪┤ ╪¼╪º╪▒█î ╪¬╪ú█î█î╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'CostOvers', 'COLUMN', N'CurrentApprovalAttemptId';
GO

ALTER TABLE [engineer].[ProjectOperationDetailCmts] ADD CONSTRAINT [PK_ProjectOperationDetailCmts] PRIMARY KEY ([Id]);
GO

CREATE TABLE [engineer].[WorkflowRequests] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [RequestId] uniqueidentifier NOT NULL,
    [CompanyId] bigint NOT NULL,
    [EntityType] nvarchar(100) NOT NULL,
    [BusinessKey] nvarchar(200) NOT NULL,
    [Purpose] nvarchar(100) NOT NULL,
    [WorkflowCode] nvarchar(100) NOT NULL,
    [RequestedByUserId] bigint NOT NULL,
    [VariablesJson] nvarchar(max) NOT NULL,
    [WorkflowInstanceId] bigint NULL,
    [DispatchStatus] int NOT NULL,
    [ExecutionStatus] int NOT NULL,
    [Outcome] nvarchar(100) NULL,
    [RequestedAtUtc] datetime2 NOT NULL,
    [AcceptedAtUtc] datetime2 NULL,
    [FinishedAtUtc] datetime2 NULL,
    [LastError] nvarchar(2000) NULL,
    [IsOpen] bit NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_WorkflowRequests] PRIMARY KEY ([Id]),
    CONSTRAINT [AK_WorkflowRequests_RequestId] UNIQUE ([RequestId])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç █î┌⌐╪¬╪º█î ╪»╪▒╪«┘ê╪º╪│╪¬ ╪»╪▒ ╪º╪▒╪¬╪¿╪º╪╖ ╪¿█î┘å ╪│╪▒┘ê█î╪│ ┘ç╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'RequestId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪┤╪▒┌⌐╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'CompanyId';
SET @description = N'┘å┘ê╪╣ ┘à┘ê╪¼┘ê╪»█î╪¬ ╪»╪▒╪«┘ê╪º╪│╪¬ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'EntityType';
SET @description = N'╪┤┘å╪º╪│┘ç ╪▒┌⌐┘ê╪▒╪» ┌⌐╪│╪¿ ┘ê ┌⌐╪º╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'BusinessKey';
SET @description = N'┘ç╪»┘ü ╪º╪¼╪▒╪º█î ┘ü╪▒╪º█î┘å╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'Purpose';
SET @description = N'┌⌐╪» ┘ü╪▒╪º█î┘å╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'WorkflowCode';
SET @description = N'╪┤┘å╪º╪│┘ç ┌⌐╪º╪▒╪¿╪▒ ╪»╪▒╪«┘ê╪º╪│╪¬ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'RequestedByUserId';
SET @description = N'╪¬╪╡┘ê█î╪▒ ╪½╪º╪¿╪¬ ╪º╪╖┘ä╪º╪╣╪º╪¬ ╪º┘ê┘ä█î┘ç ┘ü╪▒╪º█î┘å╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'VariablesJson';
SET @description = N'╪┤┘å╪º╪│┘ç ┘å┘à┘ê┘å┘ç ┘ü╪▒╪º█î┘å╪» ╪»╪▒ ╪│╪▒┘ê█î╪│ ┌»╪▒╪»╪┤ ┌⌐╪º╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'WorkflowInstanceId';
SET @description = N'┘ê╪╢╪╣█î╪¬ ╪º╪▒╪│╪º┘ä ╪»╪▒╪«┘ê╪º╪│╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'DispatchStatus';
SET @description = N'╪ó╪«╪▒█î┘å ┘ê╪╢╪╣█î╪¬ ╪»╪▒█î╪º┘ü╪¬ ╪┤╪»┘ç ╪º╪▓ ╪º╪¼╪▒╪º█î ┘ü╪▒╪º█î┘å╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'ExecutionStatus';
SET @description = N'┘å╪¬█î╪¼┘ç ┌⌐╪│╪¿ ┘ê ┌⌐╪º╪▒ ┘ü╪▒╪º█î┘å╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'Outcome';
SET @description = N'╪▓┘à╪º┘å ╪º█î╪¼╪º╪» ╪»╪▒╪«┘ê╪º╪│╪¬ ╪¿┘ç ┘ê┘é╪¬ UTC';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'RequestedAtUtc';
SET @description = N'╪▓┘à╪º┘å ┘╛╪░█î╪▒╪┤ ╪»╪▒╪«┘ê╪º╪│╪¬ ╪¬┘ê╪│╪╖ ╪│╪▒┘ê█î╪│ ┌»╪▒╪»╪┤ ┌⌐╪º╪▒ ╪¿┘ç ┘ê┘é╪¬ UTC';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'AcceptedAtUtc';
SET @description = N'╪▓┘à╪º┘å ┘╛╪º█î╪º┘å ╪º╪¼╪▒╪º█î ┘ü╪▒╪º█î┘å╪» ╪¿┘ç ┘ê┘é╪¬ UTC';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'FinishedAtUtc';
SET @description = N'╪ó╪«╪▒█î┘å ╪«╪╖╪º█î ╪½╪¿╪¬ ╪┤╪»┘ç ╪»╪▒ ┘╛█î┌»█î╪▒█î ╪»╪▒╪«┘ê╪º╪│╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'LastError';
SET @description = N'╪¿╪º╪▓ ╪¿┘ê╪»┘å ╪»╪▒╪«┘ê╪º╪│╪¬ ╪¿╪▒╪º█î ┘à┘ê╪¼┘ê╪»█î╪¬ ┘ê ┘ç╪»┘ü ┘à╪▒╪¿┘ê╪╖┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'IsOpen';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowRequests', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[WorkflowInbox] (
    [EventId] bigint NOT NULL,
    [CompanyId] bigint NOT NULL,
    [RequestId] uniqueidentifier NOT NULL,
    [PayloadHash] varchar(64) NOT NULL,
    [ProcessedAtUtc] datetime2 NOT NULL,
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_WorkflowInbox] PRIMARY KEY ([EventId]),
    CONSTRAINT [FK_WorkflowInbox_WorkflowRequests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [engineer].[WorkflowRequests] ([RequestId]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç █î┌⌐╪¬╪º█î ╪▒┘ê█î╪»╪º╪» ╪»╪▒ ╪│╪▒┘ê█î╪│ ┌»╪▒╪»╪┤ ┌⌐╪º╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'EventId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪┤╪▒┌⌐╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'CompanyId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪»╪▒╪«┘ê╪º╪│╪¬ ┌»╪▒╪»╪┤ ┌⌐╪º╪▒ ╪»╪▒ ┘à┘ç┘å╪»╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'RequestId';
SET @description = N'╪º╪½╪▒ ╪º┘å┌»╪┤╪¬ ┘à╪¡╪¬┘ê╪º█î ╪▒┘ê█î╪»╪º╪» ╪¿╪▒╪º█î ╪¬╪┤╪«█î╪╡ ╪¬┌⌐╪▒╪º╪▒ ┘à╪¬┘å╪º┘é╪╢';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'PayloadHash';
SET @description = N'╪▓┘à╪º┘å ╪½╪¿╪¬ ┘å╪¬█î╪¼┘ç ╪¿┘ç ┘ê┘é╪¬ UTC';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'ProcessedAtUtc';
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'Id';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowInbox', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[WorkflowOutboxes] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [MessageId] uniqueidentifier NOT NULL,
    [RequestId] uniqueidentifier NOT NULL,
    [CompanyId] bigint NOT NULL,
    [MessageType] varchar(100) NOT NULL,
    [PayloadJson] nvarchar(max) NOT NULL,
    [CreatedAtUtc] datetime2 NOT NULL,
    [Attempts] int NOT NULL,
    [NextAttemptAtUtc] datetime2 NOT NULL,
    [ProcessedAtUtc] datetime2 NULL,
    [LastError] nvarchar(2000) NULL,
    [LockId] uniqueidentifier NULL,
    [LockedUntilUtc] datetime2 NULL,
    [IsSuspended] bit NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_WorkflowOutboxes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WorkflowOutboxes_WorkflowRequests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [engineer].[WorkflowRequests] ([RequestId]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç █î┌⌐╪¬╪º█î ┘╛█î╪º┘à';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'MessageId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪»╪▒╪«┘ê╪º╪│╪¬ ┌»╪▒╪»╪┤ ┌⌐╪º╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'RequestId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪┤╪▒┌⌐╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'CompanyId';
SET @description = N'┘å┘ê╪╣ ┘ê ┘å╪│╪«┘ç ┘╛█î╪º┘à';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'MessageType';
SET @description = N'┘à╪¡╪¬┘ê╪º█î ╪½╪º╪¿╪¬ ┘╛█î╪º┘à';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'PayloadJson';
SET @description = N'╪▓┘à╪º┘å ╪º█î╪¼╪º╪» ┘╛█î╪º┘à ╪¿┘ç ┘ê┘é╪¬ UTC';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'CreatedAtUtc';
SET @description = N'╪¬╪╣╪»╪º╪» ╪¬┘ä╪º╪┤ ┘ç╪º█î ┘╛╪▒╪»╪º╪▓╪┤ ┘╛█î╪º┘à';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'Attempts';
SET @description = N'╪▓┘à╪º┘å ┘à╪¼╪º╪▓ ╪¬┘ä╪º╪┤ ╪¿╪╣╪»█î ╪¿┘ç ┘ê┘é╪¬ UTC';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'NextAttemptAtUtc';
SET @description = N'╪▓┘à╪º┘å ╪½╪¿╪¬ ┘╛╪░█î╪▒╪┤ ┘╛█î╪º┘à ╪¬┘ê╪│╪╖ ┘à┘é╪╡╪» ╪¿┘ç ┘ê┘é╪¬ UTC';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'ProcessedAtUtc';
SET @description = N'╪ó╪«╪▒█î┘å ╪«╪╖╪º█î ┘╛╪▒╪»╪º╪▓╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'LastError';
SET @description = N'╪┤┘å╪º╪│┘ç ┘à╪º┘ä┌⌐ ┘╛╪▒╪»╪º╪▓╪┤ ┘╛█î╪º┘à';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'LockId';
SET @description = N'╪▓┘à╪º┘å ╪º┘å┘é╪╢╪º█î ┘à╪º┘ä┌⌐█î╪¬ ┘╛╪▒╪»╪º╪▓╪┤ ╪¿┘ç ┘ê┘é╪¬ UTC';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'LockedUntilUtc';
SET @description = N'╪¬┘ê┘é┘ü ╪º╪▒╪│╪º┘ä ╪«┘ê╪»┌⌐╪º╪▒ ┘╛█î╪º┘à';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'IsSuspended';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WorkflowOutboxes', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_WorkflowInbox_RequestId] ON [engineer].[WorkflowInbox] ([RequestId]);
GO

CREATE UNIQUE INDEX [IX_WorkflowOutboxes_MessageId] ON [engineer].[WorkflowOutboxes] ([MessageId]);
GO

CREATE INDEX [IX_WorkflowOutboxes_NextAttemptAtUtc_CreatedAtUtc] ON [engineer].[WorkflowOutboxes] ([NextAttemptAtUtc], [CreatedAtUtc]) WHERE [ProcessedAtUtc] IS NULL AND [IsSuspended] = 0;
GO

CREATE UNIQUE INDEX [IX_WorkflowOutboxes_RequestId_MessageType] ON [engineer].[WorkflowOutboxes] ([RequestId], [MessageType]) WHERE [MessageType] = 'workflow.start.v1';
GO

CREATE UNIQUE INDEX [IX_WorkflowRequests_CompanyId_EntityType_BusinessKey_Purpose] ON [engineer].[WorkflowRequests] ([CompanyId], [EntityType], [BusinessKey], [Purpose]) WHERE [IsOpen] = 1;
GO

CREATE UNIQUE INDEX [IX_WorkflowRequests_CompanyId_WorkflowInstanceId] ON [engineer].[WorkflowRequests] ([CompanyId], [WorkflowInstanceId]) WHERE [WorkflowInstanceId] IS NOT NULL;
GO

ALTER TABLE [engineer].[ProjectOperationDetailCmts] ADD CONSTRAINT [FK_ProjectOperationDetailCmts_ProjectOperationDetailCmts_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [engineer].[ProjectOperationDetailCmts] ([Id]);
GO

ALTER TABLE [engineer].[ProjectOperationDetailCmts] ADD CONSTRAINT [FK_ProjectOperationDetailCmts_ProjectOperationDetails_ProjectOperationDetailId] FOREIGN KEY ([ProjectOperationDetailId]) REFERENCES [engineer].[ProjectOperationDetails] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [engineer].[ProjectOperationDetailCommentDocuments] ADD CONSTRAINT [FK_ProjectOperationDetailCommentDocuments_ProjectOperationDetailCmts_ProjectOperationDetailCommentId] FOREIGN KEY ([ProjectOperationDetailCommentId]) REFERENCES [engineer].[ProjectOperationDetailCmts] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260920105402_FixParentAction', N'8.0.8');
GO

COMMIT;
GO



