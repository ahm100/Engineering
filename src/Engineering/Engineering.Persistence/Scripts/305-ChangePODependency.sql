BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectOperationDependencies] DROP CONSTRAINT [FK_ProjectOperationDependencies_ProjectOperations_ProjectOperationId];
GO

EXEC sp_rename N'[engineer].[ProjectOperationDependencies].[RelationId]', N'SuccessorId', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[ProjectOperationDependencies].[RelationDays]', N'LagDays', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[ProjectOperationDependencies].[ProjectOperationId]', N'PredecessorId', N'COLUMN';
GO

EXEC sp_rename N'[engineer].[ProjectOperationDependencies].[IX_ProjectOperationDependencies_ProjectOperationId]', N'IX_ProjectOperationDependencies_PredecessorId', N'INDEX';
GO

DECLARE @description AS sql_variant;
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDependencies', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDependencies', 'COLUMN', N'Updated';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectOperationDependencies]') AND [c].[name] = N'IsDeleted');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectOperationDependencies] DROP CONSTRAINT [' + @var0 + '];');
DECLARE @description AS sql_variant;
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDependencies', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDependencies', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDependencies', 'COLUMN', N'Created';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDependencies', 'COLUMN', N'Id';
GO

CREATE INDEX [IX_ProjectOperationDependencies_SuccessorId] ON [engineer].[ProjectOperationDependencies] ([SuccessorId]);
GO

ALTER TABLE [engineer].[ProjectOperationDependencies] ADD CONSTRAINT [FK_ProjectOperationDependencies_ProjectOperations_PredecessorId] FOREIGN KEY ([PredecessorId]) REFERENCES [engineer].[ProjectOperations] ([Id]);
GO

ALTER TABLE [engineer].[ProjectOperationDependencies] ADD CONSTRAINT [FK_ProjectOperationDependencies_ProjectOperations_SuccessorId] FOREIGN KEY ([SuccessorId]) REFERENCES [engineer].[ProjectOperations] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260617093730_ChangePODependency', N'8.0.8');
GO

COMMIT;
GO



