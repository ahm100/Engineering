BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectOperationActions] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [OperationInfoActionId] bigint NOT NULL,
    [ProjectOperationId] bigint NOT NULL,
    [Price] decimal(18,2) NULL DEFAULT 0.0,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectOperationActions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectOperationActions_OperationInfoActions_OperationInfoActionId] FOREIGN KEY ([OperationInfoActionId]) REFERENCES [engineer].[OperationInfoActions] ([Id]),
    CONSTRAINT [FK_ProjectOperationActions_ProjectOperations_ProjectOperationId] FOREIGN KEY ([ProjectOperationId]) REFERENCES [engineer].[ProjectOperations] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationActions', 'COLUMN', N'Id';
SET @description = N'هزینه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationActions', 'COLUMN', N'Price';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationActions', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationActions', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationActions', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationActions', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationActions', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectOperationActions_OperationInfoActionId] ON [engineer].[ProjectOperationActions] ([OperationInfoActionId]);
GO

CREATE INDEX [IX_ProjectOperationActions_ProjectOperationId] ON [engineer].[ProjectOperationActions] ([ProjectOperationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251228131317_AddProjectOperationActions', N'8.0.8');
GO

COMMIT;
GO



