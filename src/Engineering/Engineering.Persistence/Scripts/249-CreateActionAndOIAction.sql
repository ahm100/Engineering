BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[Actions] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ActionName] nvarchar(250) NOT NULL,
    [ActionCode] nvarchar(100) NOT NULL,
    [CompanyId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Actions] PRIMARY KEY ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'Id';
SET @description = N'نام عملیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'ActionName';
SET @description = N'کد عملیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'ActionCode';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Actions', 'COLUMN', N'IsActive';
GO

CREATE TABLE [engineer].[OperationInfoActions] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [OperationInfoId] bigint NOT NULL,
    [ActionId] bigint NOT NULL,
    [Price] decimal(18,2) NULL DEFAULT 0.0,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_OperationInfoActions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OperationInfoActions_Actions_ActionId] FOREIGN KEY ([ActionId]) REFERENCES [engineer].[Actions] ([Id]),
    CONSTRAINT [FK_OperationInfoActions_OperationInfos_OperationInfoId] FOREIGN KEY ([OperationInfoId]) REFERENCES [engineer].[OperationInfos] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfoActions', 'COLUMN', N'Id';
SET @description = N'هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfoActions', 'COLUMN', N'Price';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfoActions', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfoActions', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfoActions', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfoActions', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'OperationInfoActions', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_OperationInfoActions_ActionId] ON [engineer].[OperationInfoActions] ([ActionId]);
GO

CREATE INDEX [IX_OperationInfoActions_OperationInfoId] ON [engineer].[OperationInfoActions] ([OperationInfoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251129085045_CreateActionAndOIAction', N'8.0.8');
GO

COMMIT;
GO



