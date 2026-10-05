BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectWbs] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Title] nvarchar(250) NOT NULL,
    [Code] nvarchar(100) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [ProjectId] bigint NOT NULL,
    [WbsTemplateId] bigint NOT NULL,
    [ParentId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectWbs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectWbs_ProjectWbs_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [engineer].[ProjectWbs] ([Id]),
    CONSTRAINT [FK_ProjectWbs_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]),
    CONSTRAINT [FK_ProjectWbs_WbsTemplates_WbsTemplateId] FOREIGN KEY ([WbsTemplateId]) REFERENCES [engineer].[WbsTemplates] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'Id';
SET @description = N'عنوان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'Title';
SET @description = N'کد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'Code';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbs', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectWbs_ParentId] ON [engineer].[ProjectWbs] ([ParentId]);
GO

CREATE INDEX [IX_ProjectWbs_ProjectId] ON [engineer].[ProjectWbs] ([ProjectId]);
GO

CREATE INDEX [IX_ProjectWbs_WbsTemplateId] ON [engineer].[ProjectWbs] ([WbsTemplateId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260616052141_CreateProjectWbs', N'8.0.8');
GO

COMMIT;
GO



