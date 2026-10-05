BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractTypeDetailAdjustments]') AND [c].[name] = N'PriceIndex');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractTypeDetailAdjustments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ContractTypeDetailAdjustments] DROP COLUMN [PriceIndex];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractTypeDetailAdjustments]') AND [c].[name] = N'PriceIndexReference');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractTypeDetailAdjustments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ContractTypeDetailAdjustments] DROP COLUMN [PriceIndexReference];
GO

ALTER TABLE [engineer].[ContractTypeDetailAdjustments] ADD [PriceIndexId] bigint NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç ╪┤╪º╪«╪╡ ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'PriceIndexId';
GO

CREATE TABLE [engineer].[ContractAdjustmentReferences] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [FaTitle] nvarchar(250) NOT NULL,
    [EnTitle] nvarchar(250) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [IsActive] bit NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractAdjustmentReferences] PRIMARY KEY ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'Id';
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'FaTitle';
SET @description = N'╪╣┘å┘ê╪º┘å ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'EnTitle';
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'Description';
SET @description = N'┘ê╪╢╪╣█î╪¬ ┘ü╪╣╪º┘ä █î╪º ╪║█î╪▒ ┘ü╪╣╪º┘ä ╪¿┘ê╪»┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'IsActive';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ContractAdjustmentIndexes] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractAdjustmentReferenceId] bigint NOT NULL,
    [FaTitle] nvarchar(250) NOT NULL,
    [EnTitle] nvarchar(250) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [IsActive] bit NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractAdjustmentIndexes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractAdjustmentIndexes_ContractAdjustmentReferences_ContractAdjustmentReferenceId] FOREIGN KEY ([ContractAdjustmentReferenceId]) REFERENCES [engineer].[ContractAdjustmentReferences] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç ┘à╪▒╪¼╪╣ ╪┤╪º╪«╪╡ ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'ContractAdjustmentReferenceId';
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'FaTitle';
SET @description = N'╪╣┘å┘ê╪º┘å ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'EnTitle';
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'Description';
SET @description = N'┘ê╪╢╪╣█î╪¬ ┘ü╪╣╪º┘ä █î╪º ╪║█î╪▒ ┘ü╪╣╪º┘ä ╪¿┘ê╪»┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'IsActive';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ContractTypeDetailAdjustments_PriceIndexId] ON [engineer].[ContractTypeDetailAdjustments] ([PriceIndexId]);
GO

CREATE INDEX [IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId] ON [engineer].[ContractAdjustmentIndexes] ([ContractAdjustmentReferenceId]);
GO

ALTER TABLE [engineer].[ContractTypeDetailAdjustments] ADD CONSTRAINT [FK_ContractTypeDetailAdjustments_ContractAdjustmentIndexes_PriceIndexId] FOREIGN KEY ([PriceIndexId]) REFERENCES [engineer].[ContractAdjustmentIndexes] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260829134608_ContractTypeDetailAdjustment', N'8.0.8');
GO

COMMIT;
GO



