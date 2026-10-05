BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[DailyProjectOperationHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [LegacyId] bigint NULL,
    [Status] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Length] decimal(18,5) NOT NULL DEFAULT 0.0,
    [Width] decimal(18,5) NOT NULL DEFAULT 0.0,
    [Height] decimal(18,5) NOT NULL DEFAULT 0.0,
    [Weight] decimal(18,5) NOT NULL DEFAULT 0.0,
    [Number] decimal(18,5) NOT NULL DEFAULT 0.0,
    [CompanyId] bigint NULL,
    [Description] nvarchar(1500) NULL,
    [DailyProjectOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DailyProjectOperationHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DailyProjectOperationHistories_DailyProjectOperations_DailyProjectOperationId] FOREIGN KEY ([DailyProjectOperationId]) REFERENCES [engineer].[DailyProjectOperations] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'DailyProjectOperationHistories', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'DailyProjectOperationHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'DailyProjectOperationHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'DailyProjectOperationHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'DailyProjectOperationHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'DailyProjectOperationHistories', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_DailyProjectOperationHistories_DailyProjectOperationId] ON [engineer].[DailyProjectOperationHistories] ([DailyProjectOperationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251207110636_AddDailyProjectOperationHistory', N'8.0.8');
GO

COMMIT;
GO



