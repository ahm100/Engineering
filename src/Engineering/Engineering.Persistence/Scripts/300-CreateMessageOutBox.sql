BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[MessageOutboxEntities] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Status] int NOT NULL,
    [ErrorMessage] nvarchar(max) NULL,
    [ErrorRetries] int NOT NULL,
    [ErrorTime] datetime2 NULL,
    [BrokerAudit] nvarchar(32) NULL,
    [QueuedAt] datetime2 NOT NULL,
    [Envelope] nvarchar(max) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_MessageOutboxEntities] PRIMARY KEY ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MessageOutboxEntities', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MessageOutboxEntities', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MessageOutboxEntities', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MessageOutboxEntities', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MessageOutboxEntities', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'MessageOutboxEntities', 'COLUMN', N'IsDeleted';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260615062335_CreateMessageOutBox', N'8.0.8');
GO

COMMIT;
GO



