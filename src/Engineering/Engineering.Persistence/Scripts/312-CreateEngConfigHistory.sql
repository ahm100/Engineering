BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[EngineeringConfigHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [SendTelegramMessage] bit NOT NULL,
    [ProjectThirdParties] bit NOT NULL DEFAULT CAST(0 AS bit),
    [EngineeringConfigId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EngineeringConfigHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EngineeringConfigHistories_EngineeringConfigs_EngineeringConfigId] FOREIGN KEY ([EngineeringConfigId]) REFERENCES [engineer].[EngineeringConfigs] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'Id';
SET @description = N'کلید تنظیمات برای کنترل ارسال پیام به تلگرام';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'SendTelegramMessage';
SET @description = N'کلید تنظیمات دسترسی پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'ProjectThirdParties';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigHistories', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_EngineeringConfigHistories_EngineeringConfigId] ON [engineer].[EngineeringConfigHistories] ([EngineeringConfigId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260627061626_CreateEngConfigHistory', N'8.0.8');
GO

COMMIT;
GO



