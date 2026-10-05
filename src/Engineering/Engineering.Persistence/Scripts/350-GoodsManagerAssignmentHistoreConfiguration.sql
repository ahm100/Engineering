BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[GoodsManagerAssignmentHistory] DROP CONSTRAINT [FK_GoodsManagerAssignmentHistory_GoodsManagerAssignments_GoodsManagerAssignmentId];
GO

ALTER TABLE [engineer].[GoodsManagerAssignmentHistory] DROP CONSTRAINT [PK_GoodsManagerAssignmentHistory];
GO

EXEC sp_rename N'[engineer].[GoodsManagerAssignmentHistory]', N'GoodsManagerAssignmentHistories';
GO

EXEC sp_rename N'[engineer].[GoodsManagerAssignmentHistories].[IX_GoodsManagerAssignmentHistory_GoodsManagerAssignmentId]', N'IX_GoodsManagerAssignmentHistories_GoodsManagerAssignmentId', N'INDEX';
GO

DECLARE @description AS sql_variant;
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'UpdaterId';
GO

DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'Updated';
GO

DECLARE @description AS sql_variant;
SET @description = N'┘à╪¡╪╡┘ê┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'ProductId';
GO

DECLARE @description AS sql_variant;
SET @description = N'┌»╪▒┘ê┘ç ┘à╪¡╪╡┘ê┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'ProductGroupId';
GO

DECLARE @description AS sql_variant;
SET @description = N'╪»╪│╪¬┘ç ╪¿┘å╪»█î ┘à╪¡╪╡┘ê┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'ProductCategoryId';
GO

DECLARE @description AS sql_variant;
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'IsDeleted';
GO

DECLARE @description AS sql_variant;
SET @description = N'┘à╪»█î╪▒ ┌⌐╪º┘ä╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'GoodsManagerThirdPartyId';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignmentHistories]') AND [c].[name] = N'Description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] ALTER COLUMN [Description] nvarchar(1500) NULL;
GO

DECLARE @description AS sql_variant;
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'CreatorId';
GO

DECLARE @description AS sql_variant;
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'Created';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[GoodsManagerAssignmentHistories]') AND [c].[name] = N'Id');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] ALTER COLUMN [Id] bigint NOT NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'GoodsManagerAssignmentHistories', 'COLUMN', N'Id';
GO

ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] ADD CONSTRAINT [PK_GoodsManagerAssignmentHistories] PRIMARY KEY ([Id]);
GO

ALTER TABLE [engineer].[GoodsManagerAssignmentHistories] ADD CONSTRAINT [FK_GoodsManagerAssignmentHistories_GoodsManagerAssignments_GoodsManagerAssignmentId] FOREIGN KEY ([GoodsManagerAssignmentId]) REFERENCES [engineer].[GoodsManagerAssignments] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260815140940_GoodsManagerAssignmentHistoreConfiguration', N'8.0.8');
GO

COMMIT;
GO



