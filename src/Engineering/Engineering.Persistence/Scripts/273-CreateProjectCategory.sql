BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectCategories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProjectId] bigint NOT NULL,
    [CategoryId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectCategories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCategories_EngineeringCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [engineer].[EngineeringCategories] ([Id]),
    CONSTRAINT [FK_ProjectCategories_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCategories', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCategories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCategories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCategories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCategories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCategories', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectCategories_CategoryId] ON [engineer].[ProjectCategories] ([CategoryId]);
GO

CREATE INDEX [IX_ProjectCategories_ProjectId] ON [engineer].[ProjectCategories] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260118100511_CreateProjectCategory', N'8.0.8');
GO

COMMIT;
GO



