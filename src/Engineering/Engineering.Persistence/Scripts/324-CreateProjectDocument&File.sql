BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Description] nvarchar(1500) NOT NULL,
    [Date] datetime2 NOT NULL,
    [Type] int NOT NULL DEFAULT 999,
    [ProjectId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectDocuments_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'Id';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'Description';
SET @description = N'تاریخ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'Date';
SET @description = N'نوع اسناد پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'Type';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocuments', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectDocumentFiles] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(1500) NOT NULL,
    [ProjectDocumentId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectDocumentFiles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectDocumentFiles_ProjectDocuments_ProjectDocumentId] FOREIGN KEY ([ProjectDocumentId]) REFERENCES [engineer].[ProjectDocuments] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocumentFiles', 'COLUMN', N'Id';
SET @description = N'مسیر فایل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocumentFiles', 'COLUMN', N'Url';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocumentFiles', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocumentFiles', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocumentFiles', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocumentFiles', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectDocumentFiles', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectDocumentFiles_ProjectDocumentId] ON [engineer].[ProjectDocumentFiles] ([ProjectDocumentId]);
GO

CREATE INDEX [IX_ProjectDocuments_ProjectId] ON [engineer].[ProjectDocuments] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260713064959_CreateProjectDocument&File', N'8.0.8');
GO

COMMIT;
GO



