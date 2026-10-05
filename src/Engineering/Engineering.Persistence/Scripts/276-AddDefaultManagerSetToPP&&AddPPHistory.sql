BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectProducts] ADD [DefaultManagerSet] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'تایید دیفالت مدیرپروژه هست یا نه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'DefaultManagerSet';
GO

CREATE TABLE [engineer].[ProjectProductHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [RequestQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [RemainingQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CompletedQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InProgressQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [ProductGroupId] bigint NULL,
    [ProductCategoryId] bigint NULL,
    [TolerancePercentage] decimal(18,5) NOT NULL DEFAULT 0.0,
    [DefaultManagerSet] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ProjectProductId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_ProjectProductHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectProductHistories_ProjectProducts_ProjectProductId] FOREIGN KEY ([ProjectProductId]) REFERENCES [engineer].[ProjectProducts] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'Id';
SET @description = N'شناسه گروه کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'ProductGroupId';
SET @description = N'شناسه دسته بندی کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'ProductCategoryId';
SET @description = N'درصد تلورانس';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'TolerancePercentage';
SET @description = N'تایید دیفالت مدیرپروژه هست یا نه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'DefaultManagerSet';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProductHistories', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_ProjectProductHistories_ProjectProductId] ON [engineer].[ProjectProductHistories] ([ProjectProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260126123905_AddDefaultManagerSetToPP&&AddPPHistory', N'8.0.8');
GO

COMMIT;
GO



