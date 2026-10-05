BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectProducts] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [RequestQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [RemainingQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [CompletedQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [InProgressQuantity] decimal(18,2) NOT NULL DEFAULT 0.0,
    [ProductGroupId] bigint NOT NULL,
    [CompanyId] bigint NULL,
    [ProjectId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectProducts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectProducts_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'Id';
SET @description = N'شناسه گروه کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'ProductGroupId';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectProducts', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_ProjectProducts_ProjectId] ON [engineer].[ProjectProducts] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251213083227_CreateProjectProduct', N'8.0.8');
GO

COMMIT;
GO



