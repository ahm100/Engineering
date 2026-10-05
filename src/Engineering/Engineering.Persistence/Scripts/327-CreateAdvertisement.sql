BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[Advertisements] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TitleFa] nvarchar(250) NOT NULL,
    [TitleEn] nvarchar(250) NOT NULL,
    [DescriptionFa] nvarchar(1500) NOT NULL,
    [DescriptionEn] nvarchar(1500) NOT NULL,
    [TechnicalCode] nvarchar(100) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Advertisements] PRIMARY KEY ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'Id';
SET @description = N'عنوان فارسی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'TitleFa';
SET @description = N'عنوان انگلیس';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'TitleEn';
SET @description = N'توضیحات فارسی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'DescriptionFa';
SET @description = N'توضیحات انگلیسی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'DescriptionEn';
SET @description = N'کد فنی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'TechnicalCode';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Advertisements', 'COLUMN', N'IsActive';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260718060944_CreateAdvertisement', N'8.0.8');
GO

COMMIT;
GO



