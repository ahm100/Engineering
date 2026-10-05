BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[EmployerOperationHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Price] decimal(18,2) NOT NULL,
    [Workload] decimal(18,5) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [EmployerOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerOperationHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerOperationHistories_EmployerOperations_EmployerOperationId] FOREIGN KEY ([EmployerOperationId]) REFERENCES [engineer].[EmployerOperations] ([Id])
    );
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'Id';
SET @description = N'قیمت واحد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'Price';
SET @description = N'حجم کار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'Workload';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationHistories', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectOperationHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Price] decimal(18,2) NOT NULL,
    [Workload] decimal(18,5) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [ProjectOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectOperationHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectOperationHistories_ProjectOperations_ProjectOperationId] FOREIGN KEY ([ProjectOperationId]) REFERENCES [engineer].[ProjectOperations] ([Id])
    );
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'Id';
SET @description = N'هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'Price';
SET @description = N'حجم شرح عملیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'Workload';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationHistories', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_EmployerOperationHistories_EmployerOperationId] ON [engineer].[EmployerOperationHistories] ([EmployerOperationId]);
GO

CREATE INDEX [IX_ProjectOperationHistories_ProjectOperationId] ON [engineer].[ProjectOperationHistories] ([ProjectOperationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251106125106_AddPOAndEOHistory', N'8.0.8');
GO

COMMIT;
GO