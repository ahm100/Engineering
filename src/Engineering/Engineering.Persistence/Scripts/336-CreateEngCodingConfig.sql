BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EngineeringConfigs] ADD [HaveCodingAlgorithm] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'کلید تنظیمات دسترسی کدگذاری پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringConfigs', 'COLUMN', N'HaveCodingAlgorithm';
GO

CREATE TABLE [engineer].[EngineeringCodingConfigs] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [EngineeringConfigId] bigint NOT NULL,
    [Type] int NOT NULL,
    [Prefix] nvarchar(max) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EngineeringCodingConfigs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EngineeringCodingConfigs_EngineeringConfigs_EngineeringConfigId] FOREIGN KEY ([EngineeringConfigId]) REFERENCES [engineer].[EngineeringConfigs] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'Id';
SET @description = N'نوع موجودیت الگوریتم کد گذاری';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'Type';
SET @description = N'پیشوند';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'Prefix';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringCodingConfigs', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_EngineeringCodingConfigs_EngineeringConfigId] ON [engineer].[EngineeringCodingConfigs] ([EngineeringConfigId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260728135621_CreateEngCodingConfig', N'8.0.8');
GO

COMMIT;
GO



