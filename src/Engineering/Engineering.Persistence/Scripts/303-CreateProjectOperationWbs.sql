BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectWbs] DROP CONSTRAINT [FK_ProjectWbs_ProjectWbs_ParentId];
GO

ALTER TABLE [engineer].[ProjectWbs] DROP CONSTRAINT [FK_ProjectWbs_Projects_ProjectId];
GO

ALTER TABLE [engineer].[ProjectWbs] DROP CONSTRAINT [FK_ProjectWbs_WbsTemplates_WbsTemplateId];
GO

ALTER TABLE [engineer].[ProjectWbs] DROP CONSTRAINT [PK_ProjectWbs];
GO

EXEC sp_rename N'[engineer].[ProjectWbs]', N'ProjectWbses';
GO

EXEC sp_rename N'[engineer].[ProjectWbses].[IX_ProjectWbs_WbsTemplateId]', N'IX_ProjectWbses_WbsTemplateId', N'INDEX';
GO

EXEC sp_rename N'[engineer].[ProjectWbses].[IX_ProjectWbs_ProjectId]', N'IX_ProjectWbses_ProjectId', N'INDEX';
GO

EXEC sp_rename N'[engineer].[ProjectWbses].[IX_ProjectWbs_ParentId]', N'IX_ProjectWbses_ParentId', N'INDEX';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [PK_ProjectWbses] PRIMARY KEY ([Id]);
GO

CREATE TABLE [engineer].[ProjectOperationWbses] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProjectOperationId] bigint NOT NULL,
    [ProjectWbsId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectOperationWbses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectOperationWbses_ProjectOperations_ProjectOperationId] FOREIGN KEY ([ProjectOperationId]) REFERENCES [engineer].[ProjectOperations] ([Id]),
    CONSTRAINT [FK_ProjectOperationWbses_ProjectWbses_ProjectWbsId] FOREIGN KEY ([ProjectWbsId]) REFERENCES [engineer].[ProjectWbses] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationWbses', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationWbses', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationWbses', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationWbses', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationWbses', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationWbses', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectOperationWbses_ProjectOperationId] ON [engineer].[ProjectOperationWbses] ([ProjectOperationId]);
GO

CREATE INDEX [IX_ProjectOperationWbses_ProjectWbsId] ON [engineer].[ProjectOperationWbses] ([ProjectWbsId]);
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_ProjectWbses_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [engineer].[ProjectWbses] ([Id]);
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]);
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_WbsTemplates_WbsTemplateId] FOREIGN KEY ([WbsTemplateId]) REFERENCES [engineer].[WbsTemplates] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260616085649_CreateProjectOperationWbs', N'8.0.8');
GO

COMMIT;
GO



