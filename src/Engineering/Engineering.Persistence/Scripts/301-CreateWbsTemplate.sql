BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[WbsTemplates] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Title] nvarchar(250) NOT NULL,
    [Code] nvarchar(100) NOT NULL,
    [CompanyId] bigint NOT NULL,
    [Description] nvarchar(1500) NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_WbsTemplates] PRIMARY KEY ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'Id';
SET @description = N'عنوان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'Title';
SET @description = N'کد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'Code';
SET @description = N'شناسه کمپانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'CompanyId';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'WbsTemplates', 'COLUMN', N'IsDeleted';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260615090130_CreateWbsTemplate', N'8.0.8');
GO

COMMIT;
GO



