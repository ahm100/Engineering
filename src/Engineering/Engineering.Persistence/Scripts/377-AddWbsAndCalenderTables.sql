BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectWbses]') AND [c].[name] = N'Description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ProjectWbses] DROP COLUMN [Description];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectWbses]') AND [c].[name] = N'WbsTemplateId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ProjectWbses] ALTER COLUMN [WbsTemplateId] bigint NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [Code] nvarchar(250) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'┌⌐╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'Code';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [DescriptionEn] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬ ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'DescriptionEn';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [DescriptionFa] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬ ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'DescriptionFa';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [MppUid] int NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [OutlineLevel] int NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [OutlineNumber] nvarchar(max) NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [ProjectScheduleImportId] bigint NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [SortOrder] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [TitleEn] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪╣┘å┘ê╪º┘å ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'TitleEn';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [TitleFa] nvarchar(250) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'TitleFa';
GO

CREATE TABLE [engineer].[ProjectScheduleImports] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [FileId] uniqueidentifier NOT NULL,
    [FileName] nvarchar(250) NOT NULL,
    [Status] int NOT NULL,
    [ImportedAt] datetime2 NOT NULL,
    [ImportedBy] bigint NOT NULL,
    [ErrorMessage] nvarchar(2500) NULL,
    [ProjectId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectScheduleImports] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectScheduleImports_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'Id';
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'FileName';
SET @description = N'┘ê╪╢╪╣█î╪¬ ┘ç╪º█î ╪¿╪º╪▒┌»╪░╪º╪▒█î ┘ü╪º█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'Status';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê╪▒┘ê╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'ImportedAt';
SET @description = N'┌⌐╪º╪▒╪¿╪▒ ┘ê╪º╪▒╪»┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'ImportedBy';
SET @description = N'┘╛█î╪º┘à ╪«╪╖╪º█î ┘ê╪▒┘ê╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'ErrorMessage';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectScheduleTasks] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Title] nvarchar(250) NOT NULL,
    [MppUid] int NOT NULL,
    [MppId] int NOT NULL,
    [SortOrder] int NOT NULL,
    [OutlineLevel] int NOT NULL,
    [OutlineNumber] nvarchar(50) NULL,
    [PlannedStart] datetime2 NULL,
    [PlannedFinish] datetime2 NULL,
    [PlannedDurationMinutes] bigint NULL,
    [PercentComplete] decimal(5,2) NOT NULL,
    [BaselineStart] datetime2 NULL,
    [BaselineFinish] datetime2 NULL,
    [BaselineDurationMinutes] bigint NULL,
    [ActualStart] datetime2 NULL,
    [ActualFinish] datetime2 NULL,
    [ActualDurationMinutes] bigint NULL,
    [IsMilestone] bit NOT NULL,
    [IsCritical] bit NOT NULL,
    [ProjectScheduleImportId] bigint NOT NULL,
    [ProjectWbsId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectScheduleTasks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectScheduleTasks_ProjectScheduleImports_ProjectScheduleImportId] FOREIGN KEY ([ProjectScheduleImportId]) REFERENCES [engineer].[ProjectScheduleImports] ([Id]),
    CONSTRAINT [FK_ProjectScheduleTasks_ProjectWbses_ProjectWbsId] FOREIGN KEY ([ProjectWbsId]) REFERENCES [engineer].[ProjectWbses] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'Id';
SET @description = N'╪╣┘å┘ê╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'Title';
SET @description = N'╪┤┘å╪º╪│┘ç █î┌⌐╪¬╪º█î ┘ü╪╣╪º┘ä█î╪¬ ╪»╪▒ Microsoft Project';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'MppUid';
SET @description = N'╪┤┘å╪º╪│┘ç ┘ü╪╣╪º┘ä█î╪¬ ╪»╪▒ Microsoft Project';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'MppId';
SET @description = N'╪¬╪▒╪¬█î╪¿ ┘å┘à╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'SortOrder';
SET @description = N'╪│╪╖╪¡ ╪│╪º╪«╪¬╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'OutlineLevel';
SET @description = N'╪┤┘à╪º╪▒┘ç ╪│╪º╪«╪¬╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'OutlineNumber';
SET @description = N'╪¬╪º╪▒█î╪« ╪┤╪▒┘ê╪╣ ╪¿╪▒┘å╪º┘à┘çΓÇî╪▒█î╪▓█îΓÇî╪┤╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'PlannedStart';
SET @description = N'╪¬╪º╪▒█î╪« ┘╛╪º█î╪º┘å ╪¿╪▒┘å╪º┘à┘çΓÇî╪▒█î╪▓█îΓÇî╪┤╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'PlannedFinish';
SET @description = N'┘à╪»╪¬ ╪▓┘à╪º┘å ╪¿╪▒┘å╪º┘à┘çΓÇî╪▒█î╪▓█îΓÇî╪┤╪»┘ç ╪¿┘ç ╪»┘é█î┘é┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'PlannedDurationMinutes';
SET @description = N'╪»╪▒╪╡╪» ┘╛█î╪┤╪▒┘ü╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'PercentComplete';
SET @description = N'╪¬╪º╪▒█î╪« ╪┤╪▒┘ê╪╣ ╪«╪╖ ┘à╪¿┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'BaselineStart';
SET @description = N'╪¬╪º╪▒█î╪« ┘╛╪º█î╪º┘å ╪«╪╖ ┘à╪¿┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'BaselineFinish';
SET @description = N'┘à╪»╪¬ ╪▓┘à╪º┘å ╪«╪╖ ┘à╪¿┘å╪º ╪¿┘ç ╪»┘é█î┘é┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'BaselineDurationMinutes';
SET @description = N'╪¬╪º╪▒█î╪« ╪┤╪▒┘ê╪╣ ┘ê╪º┘é╪╣█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'ActualStart';
SET @description = N'╪¬╪º╪▒█î╪« ┘╛╪º█î╪º┘å ┘ê╪º┘é╪╣█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'ActualFinish';
SET @description = N'┘à╪»╪¬ ╪▓┘à╪º┘å ┘ê╪º┘é╪╣█î ╪¿┘ç ╪»┘é█î┘é┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'ActualDurationMinutes';
SET @description = N'┘å┘é╪╖┘ç ╪╣╪╖┘ü';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'IsMilestone';
SET @description = N'┘ü╪╣╪º┘ä█î╪¬ ╪¿╪¡╪▒╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'IsCritical';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectScheduleTaskDependencies] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Type] int NOT NULL,
    [LagMinutes] bigint NOT NULL,
    [PredecessorTaskId] bigint NOT NULL,
    [SuccessorTaskId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectScheduleTaskDependencies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectScheduleTaskDependencies_ProjectScheduleTasks_PredecessorTaskId] FOREIGN KEY ([PredecessorTaskId]) REFERENCES [engineer].[ProjectScheduleTasks] ([Id]),
    CONSTRAINT [FK_ProjectScheduleTaskDependencies_ProjectScheduleTasks_SuccessorTaskId] FOREIGN KEY ([SuccessorTaskId]) REFERENCES [engineer].[ProjectScheduleTasks] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'Id';
SET @description = N'┘å┘ê╪╣ ┘ç╪º█î ┘ê╪º╪¿╪│╪¬┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'Type';
SET @description = N'╪¬╪ú╪«█î╪▒ ╪▓┘à╪º┘å█î ╪¿┘ç ╪»┘é█î┘é┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'LagMinutes';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectScheduleTaskOperations] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProjectScheduleTaskId] bigint NOT NULL,
    [ProjectOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectScheduleTaskOperations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectScheduleTaskOperations_ProjectOperations_ProjectOperationId] FOREIGN KEY ([ProjectOperationId]) REFERENCES [engineer].[ProjectOperations] ([Id]),
    CONSTRAINT [FK_ProjectScheduleTaskOperations_ProjectScheduleTasks_ProjectScheduleTaskId] FOREIGN KEY ([ProjectScheduleTaskId]) REFERENCES [engineer].[ProjectScheduleTasks] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'Id';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectWbses_ProjectScheduleImportId] ON [engineer].[ProjectWbses] ([ProjectScheduleImportId]);
GO

CREATE INDEX [IX_ProjectScheduleImports_ProjectId] ON [engineer].[ProjectScheduleImports] ([ProjectId]);
GO

CREATE INDEX [IX_ProjectScheduleTaskDependencies_PredecessorTaskId] ON [engineer].[ProjectScheduleTaskDependencies] ([PredecessorTaskId]);
GO

CREATE INDEX [IX_ProjectScheduleTaskDependencies_SuccessorTaskId] ON [engineer].[ProjectScheduleTaskDependencies] ([SuccessorTaskId]);
GO

CREATE INDEX [IX_ProjectScheduleTaskOperations_ProjectOperationId] ON [engineer].[ProjectScheduleTaskOperations] ([ProjectOperationId]);
GO

CREATE INDEX [IX_ProjectScheduleTaskOperations_ProjectScheduleTaskId] ON [engineer].[ProjectScheduleTaskOperations] ([ProjectScheduleTaskId]);
GO

CREATE UNIQUE INDEX [IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid] ON [engineer].[ProjectScheduleTasks] ([ProjectScheduleImportId], [MppUid]);
GO

CREATE INDEX [IX_ProjectScheduleTasks_ProjectWbsId] ON [engineer].[ProjectScheduleTasks] ([ProjectWbsId]);
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_ProjectScheduleImports_ProjectScheduleImportId] FOREIGN KEY ([ProjectScheduleImportId]) REFERENCES [engineer].[ProjectScheduleImports] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260901125228_AddWbsAndCalenderTables', N'8.0.8');
GO

COMMIT;
GO



