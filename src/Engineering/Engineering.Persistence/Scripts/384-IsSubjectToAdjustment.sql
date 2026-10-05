BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractTypes]') AND [c].[name] = N'IsSubjectToAdjustment');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractTypes] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ContractTypes] DROP COLUMN [IsSubjectToAdjustment];
GO

CREATE TABLE [engineer].[ContractTypeDetailAdjustments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractTypeDetailId] bigint NOT NULL,
    [Type] int NOT NULL,
    [PriceIndexBaseYear] int NULL,
    [PriceIndexBasePeriod] int NULL,
    [PriceIndexReference] nvarchar(250) NULL,
    [PriceIndex] nvarchar(250) NULL,
    [CurrencyBaseDate] datetime2 NULL,
    [CurrencyBaseRate] decimal(18,6) NULL,
    [CurrencyId] bigint NULL,
    [CurrencyReferenceType] int NULL,
    [CurrencyCustomReference] nvarchar(250) NULL,
    [OtherBasis] nvarchar(250) NULL,
    [OtherReference] nvarchar(250) NULL,
    [OtherIndex] nvarchar(250) NULL,
    [Description] nvarchar(1500) NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractTypeDetailAdjustments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractTypeDetailAdjustments_ContractTypeDetails_ContractTypeDetailId] FOREIGN KEY ([ContractTypeDetailId]) REFERENCES [engineer].[ContractTypeDetails] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'Id';
SET @description = N'╪¼╪▓╪ª█î╪º╪¬ ┘å┘ê╪╣ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'ContractTypeDetailId';
SET @description = N'┘å┘ê╪╣ ╪¬╪╣╪»█î┘ä ╪¼╪▓╪ª█î╪º╪¬ ┘å┘ê╪╣ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'Type';
SET @description = N'╪│╪º┘ä ┘à╪¿┘å╪º█î ╪┤╪º╪«╪╡ ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'PriceIndexBaseYear';
SET @description = N'╪»┘ê╪▒┘ç ┘à╪¿┘å╪º█î ╪┤╪º╪«╪╡ ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'PriceIndexBasePeriod';
SET @description = N'┘à╪▒╪¼╪╣ ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'PriceIndexReference';
SET @description = N'╪┤╪º╪«╪╡ ┘à╪▒╪¿┘ê╪╖┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'PriceIndex';
SET @description = N'╪¬╪º╪▒█î╪« ┘à╪¿┘å╪º█î ╪º╪▒╪▓';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'CurrencyBaseDate';
SET @description = N'┘å╪▒╪« ╪º╪▒╪▓ ╪»╪▒ ╪¬╪º╪▒█î╪« ┘à╪¿┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'CurrencyBaseRate';
SET @description = N'╪º╪▒╪▓';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'CurrencyId';
SET @description = N'┘å┘ê╪╣ ┘à╪▒╪¼╪╣ ┘å╪▒╪« ╪º╪▒╪▓';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'CurrencyReferenceType';
SET @description = N'┘à╪▒╪¼╪╣ ╪│┘ü╪º╪▒╪┤█î ┘å╪▒╪« ╪º╪▒╪▓';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'CurrencyCustomReference';
SET @description = N'┘à╪¿┘å╪º█î ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'OtherBasis';
SET @description = N'┘à╪▒╪¼╪╣ ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'OtherReference';
SET @description = N'╪┤╪º╪«╪╡ █î╪º ┘à╪╣█î╪º╪▒ ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'OtherIndex';
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'Description';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetailAdjustments', 'COLUMN', N'IsDeleted';
GO

CREATE UNIQUE INDEX [IX_ContractTypeDetailAdjustments_ContractTypeDetailId] ON [engineer].[ContractTypeDetailAdjustments] ([ContractTypeDetailId]) WHERE [IsDeleted] = 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260829103500_IsSubjectToAdjustmentIsRefractored', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractFinancialInformations]') AND [c].[name] = N'InitialAmount');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractFinancialInformations] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ContractFinancialInformations] DROP COLUMN [InitialAmount];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260829111909_financeEnhancements', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractTypeDetailAdjustments]') AND [c].[name] = N'PriceIndex');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractTypeDetailAdjustments] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[ContractTypeDetailAdjustments] DROP COLUMN [PriceIndex];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractTypeDetailAdjustments]') AND [c].[name] = N'PriceIndexReference');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractTypeDetailAdjustments] DROP CONSTRAINT [' + @var3 + '];');
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

BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractChanges] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractId] bigint NOT NULL,
    [Type] int NOT NULL,
    [Number] nvarchar(250) NOT NULL,
    [Date] datetime2 NOT NULL,
    [Subject] nvarchar(250) NOT NULL,
    [DurationChange] int NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractChanges] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractChanges_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [engineer].[Contracts] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'Id';
SET @description = N'┘å┘ê╪╣ ╪¬╪║█î█î╪▒ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'Type';
SET @description = N'╪┤┘à╪º╪▒┘ç ╪¬╪║█î█î╪▒ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'Number';
SET @description = N'╪¬╪º╪▒█î╪«';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'Date';
SET @description = N'┘à┘ê╪╢┘ê╪╣ ╪¬╪║█î█î╪▒ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'Subject';
SET @description = N'╪¬╪║█î█î╪▒ ┘à╪»╪¬ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'DurationChange';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ContractChangeDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractChangeId] bigint NOT NULL,
    [Url] nvarchar(1500) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractChangeDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractChangeDocuments_ContractChanges_ContractChangeId] FOREIGN KEY ([ContractChangeId]) REFERENCES [engineer].[ContractChanges] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeDocuments', 'COLUMN', N'Id';
SET @description = N'┘à╪│█î╪▒ ┘ü╪º█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeDocuments', 'COLUMN', N'Url';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeDocuments', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeDocuments', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeDocuments', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeDocuments', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeDocuments', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ContractChangeItems] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractChangeId] bigint NOT NULL,
    [ContractTypeDetailId] bigint NULL,
    [ContractTypeId] bigint NULL,
    [ProjectOperationDetailId] bigint NULL,
    [PricingMethod] int NOT NULL,
    [PreviousValue] decimal(23,5) NOT NULL,
    [NewValue] decimal(23,5) NOT NULL,
    [UnitOfMeasurementId] bigint NULL,
    [UnitPrice] decimal(18,2) NULL,
    [ChangeAmount] decimal(18,2) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractChangeItems] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_ContractChangeItems_ExactlyOneOrigin] CHECK (([ContractTypeDetailId] IS NOT NULL AND [ContractTypeId] IS NULL AND [ProjectOperationDetailId] IS NULL) OR ([ContractTypeDetailId] IS NULL AND [ContractTypeId] IS NOT NULL AND [ProjectOperationDetailId] IS NOT NULL)),
    CONSTRAINT [FK_ContractChangeItems_ContractChanges_ContractChangeId] FOREIGN KEY ([ContractChangeId]) REFERENCES [engineer].[ContractChanges] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContractChangeItems_ContractTypeDetails_ContractTypeDetailId] FOREIGN KEY ([ContractTypeDetailId]) REFERENCES [engineer].[ContractTypeDetails] ([Id]),
    CONSTRAINT [FK_ContractChangeItems_ContractTypes_ContractTypeId] FOREIGN KEY ([ContractTypeId]) REFERENCES [engineer].[ContractTypes] ([Id]),
    CONSTRAINT [FK_ContractChangeItems_ProjectOperationDetails_ProjectOperationDetailId] FOREIGN KEY ([ProjectOperationDetailId]) REFERENCES [engineer].[ProjectOperationDetails] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'Id';
SET @description = N'╪▒┘ê╪┤ ┘é█î┘à╪¬ΓÇî┌»╪░╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'PricingMethod';
SET @description = N'┘à┘é╪»╪º╪▒ ┘é╪¿┘ä█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'PreviousValue';
SET @description = N'┘à┘é╪»╪º╪▒ ╪¼╪»█î╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'NewValue';
SET @description = N'┘à╪¿┘ä╪║ ┘ê╪º╪¡╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'UnitPrice';
SET @description = N'┘à╪¿┘ä╪║ ╪¬╪║█î█î╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'ChangeAmount';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChangeItems', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ContractChangeDocuments_ContractChangeId] ON [engineer].[ContractChangeDocuments] ([ContractChangeId]);
GO

CREATE UNIQUE INDEX [IX_ContractChangeItems_ContractChangeId_ContractTypeDetailId] ON [engineer].[ContractChangeItems] ([ContractChangeId], [ContractTypeDetailId]) WHERE [IsDeleted] = 0 AND [ContractTypeDetailId] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_ContractChangeItems_ContractChangeId_ContractTypeId_ProjectOperationDetailId] ON [engineer].[ContractChangeItems] ([ContractChangeId], [ContractTypeId], [ProjectOperationDetailId]) WHERE [IsDeleted] = 0 AND [ContractTypeId] IS NOT NULL AND [ProjectOperationDetailId] IS NOT NULL;
GO

CREATE INDEX [IX_ContractChangeItems_ContractTypeDetailId] ON [engineer].[ContractChangeItems] ([ContractTypeDetailId]);
GO

CREATE INDEX [IX_ContractChangeItems_ContractTypeId] ON [engineer].[ContractChangeItems] ([ContractTypeId]);
GO

CREATE INDEX [IX_ContractChangeItems_ProjectOperationDetailId] ON [engineer].[ContractChangeItems] ([ProjectOperationDetailId]);
GO

CREATE UNIQUE INDEX [IX_ContractChanges_ContractId_Number] ON [engineer].[ContractChanges] ([ContractId], [Number]) WHERE [IsDeleted] = 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260831060447_ContractChangeIsAdded', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectWarehouses] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProjectId] bigint NOT NULL,
    [WarehouseId] bigint NOT NULL,
    [IsDefault] bit NOT NULL DEFAULT CAST(0 AS bit),
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectWarehouses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectWarehouses_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWarehouses', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç ╪º┘å╪¿╪º╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWarehouses', 'COLUMN', N'WarehouseId';
SET @description = N'┘╛█î╪┤ΓÇî┘ü╪▒╪╢ ╪¿┘ê╪»┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWarehouses', 'COLUMN', N'IsDefault';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWarehouses', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWarehouses', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWarehouses', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWarehouses', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWarehouses', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectWarehouses_ProjectId] ON [engineer].[ProjectWarehouses] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260831113433_AddProjectWarehouse', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

IF OBJECT_ID('tempdb..#ProjectWarehouseBackfill') IS NOT NULL
    DROP TABLE #ProjectWarehouseBackfill;

CREATE TABLE #ProjectWarehouseBackfill
(
    ProjectId   BIGINT        NOT NULL,
    WarehouseId BIGINT        NOT NULL,
    IsDefault   BIT           NOT NULL,
    Created     DATETIME2     NOT NULL,
    CreatorId   BIGINT        NOT NULL,
    Updated     DATETIME2     NULL,
    UpdaterId   BIGINT        NULL,

    CONSTRAINT PK_ProjectWarehouseBackfill
        PRIMARY KEY (ProjectId, WarehouseId)
);

/* ============================================================
   Build Project -> Warehouse snapshot from the current
   ProjectCostCenter -> CostCenterWarehouse graph.

   When one Project reaches the same Warehouse through more
   than one CostCenter, keep only one ProjectWarehouse row.

   A Warehouse is Project-default only when BOTH:
   - the ProjectCostCenter is default
   - the CostCenterWarehouse is default
   ============================================================ */

;WITH SourceRows AS
(
    SELECT
        pcc.ProjectId,
        ccw.WarehouseId,

        CAST(
            CASE
                WHEN pcc.IsDefault = 1
                 AND ccw.IsDefault = 1
                    THEN 1
                ELSE 0
            END
            AS BIT
        ) AS IsDefault,

        ccw.Created,
        ccw.CreatorId,
        ccw.Updated,
        ccw.UpdaterId,

        ROW_NUMBER() OVER
        (
            PARTITION BY
                pcc.ProjectId,
                ccw.WarehouseId

            ORDER BY
                /* Prefer the row that represents Project default */
                CASE
                    WHEN pcc.IsDefault = 1
                     AND ccw.IsDefault = 1
                        THEN 0
                    ELSE 1
                END,

                /* Then prefer the Warehouse default relation */
                CASE
                    WHEN ccw.IsDefault = 1
                        THEN 0
                    ELSE 1
                END,

                ccw.Created,
                ccw.Id
        ) AS RowNumber
    FROM [engineer].[ProjectCostCenters] AS pcc

    INNER JOIN [engineer].[CostCenterWarehouses] AS ccw
        ON ccw.CostCenterId = pcc.CostCenterId
       AND ccw.IsDeleted = 0

    INNER JOIN [engineer].[Projects] AS p
        ON p.Id = pcc.ProjectId
       AND p.IsDeleted = 0

    INNER JOIN [engineer].[CostCenters] AS cc
        ON cc.Id = pcc.CostCenterId
       AND cc.IsDeleted = 0

    WHERE pcc.IsDeleted = 0
)
INSERT INTO #ProjectWarehouseBackfill
(
    ProjectId,
    WarehouseId,
    IsDefault,
    Created,
    CreatorId,
    Updated,
    UpdaterId
)
SELECT
    ProjectId,
    WarehouseId,
    IsDefault,
    Created,
    CreatorId,
    Updated,
    UpdaterId
FROM SourceRows
WHERE RowNumber = 1;


/* ============================================================
   Safety check #1
   Every Project that will be migrated must resolve to exactly
   one default ProjectWarehouse.

   We do NOT silently choose a fallback default because that
   would invent business data instead of mirroring legacy data.
   ============================================================ */

IF EXISTS
(
    SELECT 1
    FROM #ProjectWarehouseBackfill
    GROUP BY ProjectId
    HAVING
        SUM(
            CASE
                WHEN IsDefault = 1 THEN 1
                ELSE 0
            END
        ) <> 1
)
BEGIN
    THROW 50001,
          'ProjectWarehouse backfill aborted: one or more Projects do not resolve to exactly one default Warehouse.',
          1;
END;


/* ============================================================
   Safety check #2
   Do not mix the migration snapshot with already-active
   ProjectWarehouse data.

   This is intentional. If the new table already contains live
   data for a Project being migrated, that state must be reviewed
   instead of silently merged.
   ============================================================ */

IF EXISTS
(
    SELECT 1
    FROM [engineer].[ProjectWarehouses] AS pw
    INNER JOIN
    (
        SELECT DISTINCT ProjectId
        FROM #ProjectWarehouseBackfill
    ) AS sourceProjects
        ON sourceProjects.ProjectId = pw.ProjectId
    WHERE pw.IsDeleted = 0
)
BEGIN
    THROW 50002,
          'ProjectWarehouse backfill aborted: active ProjectWarehouse data already exists for one or more source Projects.',
          1;
END;


/* ============================================================
   Insert snapshot

   Id is identity.
   RowVersion is generated by SQL Server.
   Only current/active legacy relations are copied.
   ============================================================ */

INSERT INTO [engineer].[ProjectWarehouses]
(
    ProjectId,
    WarehouseId,
    IsDefault,
    Created,
    CreatorId,
    Updated,
    UpdaterId,
    IsDeleted
)
SELECT
    ProjectId,
    WarehouseId,
    IsDefault,
    Created,
    CreatorId,
    Updated,
    UpdaterId,
    0
FROM #ProjectWarehouseBackfill;


DROP TABLE #ProjectWarehouseBackfill;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260831124225_BackfillProjectWarehousesFromCostCenterWarehouses', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Contracts] ADD [CompanyId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç ┌⌐┘à┘╛╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'CompanyId';
GO

ALTER TABLE [engineer].[ContractorContracts] ADD [CompanyId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç ┌⌐┘à┘╛╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContracts', 'COLUMN', N'CompanyId';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260901122206_CompanyIdIsBack', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectWbses]') AND [c].[name] = N'Description');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [engineer].[ProjectWbses] DROP COLUMN [Description];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ProjectWbses]') AND [c].[name] = N'WbsTemplateId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ProjectWbses] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [engineer].[ProjectWbses] ALTER COLUMN [WbsTemplateId] bigint NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [Code] nvarchar(250) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'┌⌐╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'Code';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [DescriptionEn] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬ ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'DescriptionEn';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [DescriptionFa] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬ ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'DescriptionFa';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [MppUid] int NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [OutlineLevel] int NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [OutlineNumber] nvarchar(max) NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [ProjectScheduleImportId] bigint NULL;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [SortOrder] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [TitleEn] nvarchar(250) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪╣┘å┘ê╪º┘å ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'TitleEn';
GO

ALTER TABLE [engineer].[ProjectWbses] ADD [TitleFa] nvarchar(250) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectWbses', 'COLUMN', N'TitleFa';
GO

CREATE TABLE [engineer].[ProjectScheduleImports] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [FileId] uniqueidentifier NOT NULL,
    [FileName] nvarchar(250) NOT NULL,
    [Status] int NOT NULL,
    [ImportedAt] datetime2 NOT NULL,
    [ImportedBy] bigint NOT NULL,
    [ErrorMessage] nvarchar(2500) NULL,
    [ProjectId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectScheduleImports] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectScheduleImports_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'Id';
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'FileName';
SET @description = N'┘ê╪╢╪╣█î╪¬ ┘ç╪º█î ╪¿╪º╪▒┌»╪░╪º╪▒█î ┘ü╪º█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'Status';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê╪▒┘ê╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'ImportedAt';
SET @description = N'┌⌐╪º╪▒╪¿╪▒ ┘ê╪º╪▒╪»┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'ImportedBy';
SET @description = N'┘╛█î╪º┘à ╪«╪╖╪º█î ┘ê╪▒┘ê╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'ErrorMessage';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleImports', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectScheduleTasks] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Title] nvarchar(250) NOT NULL,
    [MppUid] int NOT NULL,
    [MppId] int NOT NULL,
    [SortOrder] int NOT NULL,
    [OutlineLevel] int NOT NULL,
    [OutlineNumber] nvarchar(50) NULL,
    [PlannedStart] datetime2 NULL,
    [PlannedFinish] datetime2 NULL,
    [PlannedDurationMinutes] bigint NULL,
    [PercentComplete] decimal(5,2) NOT NULL,
    [BaselineStart] datetime2 NULL,
    [BaselineFinish] datetime2 NULL,
    [BaselineDurationMinutes] bigint NULL,
    [ActualStart] datetime2 NULL,
    [ActualFinish] datetime2 NULL,
    [ActualDurationMinutes] bigint NULL,
    [IsMilestone] bit NOT NULL,
    [IsCritical] bit NOT NULL,
    [ProjectScheduleImportId] bigint NOT NULL,
    [ProjectWbsId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectScheduleTasks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectScheduleTasks_ProjectScheduleImports_ProjectScheduleImportId] FOREIGN KEY ([ProjectScheduleImportId]) REFERENCES [engineer].[ProjectScheduleImports] ([Id]),
    CONSTRAINT [FK_ProjectScheduleTasks_ProjectWbses_ProjectWbsId] FOREIGN KEY ([ProjectWbsId]) REFERENCES [engineer].[ProjectWbses] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'Id';
SET @description = N'╪╣┘å┘ê╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'Title';
SET @description = N'╪┤┘å╪º╪│┘ç █î┌⌐╪¬╪º█î ┘ü╪╣╪º┘ä█î╪¬ ╪»╪▒ Microsoft Project';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'MppUid';
SET @description = N'╪┤┘å╪º╪│┘ç ┘ü╪╣╪º┘ä█î╪¬ ╪»╪▒ Microsoft Project';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'MppId';
SET @description = N'╪¬╪▒╪¬█î╪¿ ┘å┘à╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'SortOrder';
SET @description = N'╪│╪╖╪¡ ╪│╪º╪«╪¬╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'OutlineLevel';
SET @description = N'╪┤┘à╪º╪▒┘ç ╪│╪º╪«╪¬╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'OutlineNumber';
SET @description = N'╪¬╪º╪▒█î╪« ╪┤╪▒┘ê╪╣ ╪¿╪▒┘å╪º┘à┘çΓÇî╪▒█î╪▓█îΓÇî╪┤╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'PlannedStart';
SET @description = N'╪¬╪º╪▒█î╪« ┘╛╪º█î╪º┘å ╪¿╪▒┘å╪º┘à┘çΓÇî╪▒█î╪▓█îΓÇî╪┤╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'PlannedFinish';
SET @description = N'┘à╪»╪¬ ╪▓┘à╪º┘å ╪¿╪▒┘å╪º┘à┘çΓÇî╪▒█î╪▓█îΓÇî╪┤╪»┘ç ╪¿┘ç ╪»┘é█î┘é┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'PlannedDurationMinutes';
SET @description = N'╪»╪▒╪╡╪» ┘╛█î╪┤╪▒┘ü╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'PercentComplete';
SET @description = N'╪¬╪º╪▒█î╪« ╪┤╪▒┘ê╪╣ ╪«╪╖ ┘à╪¿┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'BaselineStart';
SET @description = N'╪¬╪º╪▒█î╪« ┘╛╪º█î╪º┘å ╪«╪╖ ┘à╪¿┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'BaselineFinish';
SET @description = N'┘à╪»╪¬ ╪▓┘à╪º┘å ╪«╪╖ ┘à╪¿┘å╪º ╪¿┘ç ╪»┘é█î┘é┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'BaselineDurationMinutes';
SET @description = N'╪¬╪º╪▒█î╪« ╪┤╪▒┘ê╪╣ ┘ê╪º┘é╪╣█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'ActualStart';
SET @description = N'╪¬╪º╪▒█î╪« ┘╛╪º█î╪º┘å ┘ê╪º┘é╪╣█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'ActualFinish';
SET @description = N'┘à╪»╪¬ ╪▓┘à╪º┘å ┘ê╪º┘é╪╣█î ╪¿┘ç ╪»┘é█î┘é┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'ActualDurationMinutes';
SET @description = N'┘å┘é╪╖┘ç ╪╣╪╖┘ü';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'IsMilestone';
SET @description = N'┘ü╪╣╪º┘ä█î╪¬ ╪¿╪¡╪▒╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'IsCritical';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTasks', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectScheduleTaskDependencies] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Type] int NOT NULL,
    [LagMinutes] bigint NOT NULL,
    [PredecessorTaskId] bigint NOT NULL,
    [SuccessorTaskId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectScheduleTaskDependencies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectScheduleTaskDependencies_ProjectScheduleTasks_PredecessorTaskId] FOREIGN KEY ([PredecessorTaskId]) REFERENCES [engineer].[ProjectScheduleTasks] ([Id]),
    CONSTRAINT [FK_ProjectScheduleTaskDependencies_ProjectScheduleTasks_SuccessorTaskId] FOREIGN KEY ([SuccessorTaskId]) REFERENCES [engineer].[ProjectScheduleTasks] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'Id';
SET @description = N'┘å┘ê╪╣ ┘ç╪º█î ┘ê╪º╪¿╪│╪¬┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'Type';
SET @description = N'╪¬╪ú╪«█î╪▒ ╪▓┘à╪º┘å█î ╪¿┘ç ╪»┘é█î┘é┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'LagMinutes';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskDependencies', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectScheduleTaskOperations] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProjectScheduleTaskId] bigint NOT NULL,
    [ProjectOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectScheduleTaskOperations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectScheduleTaskOperations_ProjectOperations_ProjectOperationId] FOREIGN KEY ([ProjectOperationId]) REFERENCES [engineer].[ProjectOperations] ([Id]),
    CONSTRAINT [FK_ProjectScheduleTaskOperations_ProjectScheduleTasks_ProjectScheduleTaskId] FOREIGN KEY ([ProjectScheduleTaskId]) REFERENCES [engineer].[ProjectScheduleTasks] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'Id';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectScheduleTaskOperations', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectWbses_ProjectScheduleImportId] ON [engineer].[ProjectWbses] ([ProjectScheduleImportId]);
GO

CREATE INDEX [IX_ProjectScheduleImports_ProjectId] ON [engineer].[ProjectScheduleImports] ([ProjectId]);
GO

CREATE INDEX [IX_ProjectScheduleTaskDependencies_PredecessorTaskId] ON [engineer].[ProjectScheduleTaskDependencies] ([PredecessorTaskId]);
GO

CREATE INDEX [IX_ProjectScheduleTaskDependencies_SuccessorTaskId] ON [engineer].[ProjectScheduleTaskDependencies] ([SuccessorTaskId]);
GO

CREATE INDEX [IX_ProjectScheduleTaskOperations_ProjectOperationId] ON [engineer].[ProjectScheduleTaskOperations] ([ProjectOperationId]);
GO

CREATE INDEX [IX_ProjectScheduleTaskOperations_ProjectScheduleTaskId] ON [engineer].[ProjectScheduleTaskOperations] ([ProjectScheduleTaskId]);
GO

CREATE UNIQUE INDEX [IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid] ON [engineer].[ProjectScheduleTasks] ([ProjectScheduleImportId], [MppUid]);
GO

CREATE INDEX [IX_ProjectScheduleTasks_ProjectWbsId] ON [engineer].[ProjectScheduleTasks] ([ProjectWbsId]);
GO

ALTER TABLE [engineer].[ProjectWbses] ADD CONSTRAINT [FK_ProjectWbses_ProjectScheduleImports_ProjectScheduleImportId] FOREIGN KEY ([ProjectScheduleImportId]) REFERENCES [engineer].[ProjectScheduleImports] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260901125228_AddWbsAndCalenderTables', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE contract
SET contract.CompanyId = project.CompanyId
FROM [engineer].[Contracts] AS contract
INNER JOIN [engineer].[Projects] AS project
    ON project.Id = contract.ProjectId
WHERE contract.CompanyId <= 0
  AND project.CompanyId IS NOT NULL
  AND project.CompanyId > 0;

IF EXISTS
(
    SELECT 1
    FROM [engineer].[Contracts] AS contract
    WHERE contract.CompanyId <= 0
)
BEGIN
    THROW 51000, 'CompanyId backfill failed for one or more Contracts.', 1;
END;
GO

UPDATE contractorContract
SET contractorContract.CompanyId = project.CompanyId
FROM [engineer].[ContractorContracts] AS contractorContract
INNER JOIN [engineer].[Projects] AS project
    ON project.Id = contractorContract.ProjectId
WHERE contractorContract.CompanyId <= 0
  AND project.CompanyId IS NOT NULL
  AND project.CompanyId > 0;

IF EXISTS
(
    SELECT 1
    FROM [engineer].[ContractorContracts] AS contractorContract
    WHERE contractorContract.CompanyId <= 0
)
BEGIN
    THROW 51001, 'CompanyId backfill failed for one or more ContractorContracts.', 1;
END;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260902045919_CompanyIdBackfill', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectCalendars] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TitleFa] nvarchar(250) NOT NULL,
    [TitleEn] nvarchar(250) NULL,
    [MppUid] int NULL,
    [IsDefault] bit NOT NULL,
    [MinutesPerDay] int NOT NULL,
    [ProjectId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectCalendars] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCalendars_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'Id';
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'TitleFa';
SET @description = N'╪╣┘å┘ê╪º┘å ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'TitleEn';
SET @description = N'╪┤┘å╪º╪│┘ç ╪¬┘é┘ê█î┘à ╪»╪▒ Microsoft Project';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'MppUid';
SET @description = N'╪¬┘é┘ê█î┘à ┘╛█î╪┤ΓÇî┘ü╪▒╪╢ ┘╛╪▒┘ê┌ÿ┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'IsDefault';
SET @description = N'╪¬╪╣╪»╪º╪» ╪»┘é╪º█î┘é ┌⌐╪º╪▒█î ╪º╪│╪¬╪º┘å╪»╪º╪▒╪» ╪»╪▒ ╪▒┘ê╪▓';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'MinutesPerDay';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectCalendarExceptions] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Date] datetime2 NOT NULL,
    [IsWorking] bit NOT NULL,
    [From] time NULL,
    [To] time NULL,
    [Description] nvarchar(1500) NULL,
    [ProjectCalendarId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectCalendarExceptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCalendarExceptions_ProjectCalendars_ProjectCalendarId] FOREIGN KEY ([ProjectCalendarId]) REFERENCES [engineer].[ProjectCalendars] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Id';
SET @description = N'╪¬╪º╪▒█î╪« ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Date';
SET @description = N'┘ê╪╢╪╣█î╪¬ ┌⌐╪º╪▒█î ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'IsWorking';
SET @description = N'╪▓┘à╪º┘å ╪┤╪▒┘ê╪╣ ┌⌐╪º╪▒ ╪»╪▒ ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'From';
SET @description = N'╪▓┘à╪º┘å ┘╛╪º█î╪º┘å ┌⌐╪º╪▒ ╪»╪▒ ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'To';
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬ ╪º╪│╪¬╪½┘å╪º█î ╪¬┘é┘ê█î┘à';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Description';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectCalendarWorkingDaies] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [DayOfWeek] int NOT NULL,
    [IsWorking] bit NOT NULL,
    [ProjectCalendarId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectCalendarWorkingDaies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCalendarWorkingDaies_ProjectCalendars_ProjectCalendarId] FOREIGN KEY ([ProjectCalendarId]) REFERENCES [engineer].[ProjectCalendars] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'Id';
SET @description = N'╪▒┘ê╪▓ ┘ç┘ü╪¬┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'DayOfWeek';
SET @description = N'╪▒┘ê╪▓ ┌⌐╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'IsWorking';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectCalendarWorkingTimes] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [From] time NOT NULL,
    [To] time NOT NULL,
    [ProjectCalendarWorkingDayId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectCalendarWorkingTimes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDaies_ProjectCalendarWorkingDayId] FOREIGN KEY ([ProjectCalendarWorkingDayId]) REFERENCES [engineer].[ProjectCalendarWorkingDaies] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'Id';
SET @description = N'╪▓┘à╪º┘å ╪┤╪▒┘ê╪╣ ┌⌐╪º╪▒ ╪»╪▒ ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'From';
SET @description = N'╪▓┘à╪º┘å ┘╛╪º█î╪º┘å ┌⌐╪º╪▒ ╪»╪▒ ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'To';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectCalendarExceptions_ProjectCalendarId] ON [engineer].[ProjectCalendarExceptions] ([ProjectCalendarId]);
GO

CREATE INDEX [IX_ProjectCalendars_ProjectId] ON [engineer].[ProjectCalendars] ([ProjectId]);
GO

CREATE INDEX [IX_ProjectCalendarWorkingDaies_ProjectCalendarId] ON [engineer].[ProjectCalendarWorkingDaies] ([ProjectCalendarId]);
GO

CREATE INDEX [IX_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDayId] ON [engineer].[ProjectCalendarWorkingTimes] ([ProjectCalendarWorkingDayId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260902060956_AddCalenderTables', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS
(
    SELECT 1
    FROM [engineer].[ContractChanges]
)
BEGIN
    THROW 51020,
        'Existing ContractChanges require legal amount backfill before ContractHistoryIsAdded can be applied.',
        1;
END;
GO

DROP INDEX [IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId] ON [engineer].[ContractAdjustmentIndexes];
GO

ALTER TABLE [engineer].[ContractChanges] ADD [FinalContractAmount] decimal(18,2) NOT NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪¿┘ä╪║ ┘å┘ç╪º█î█î ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'FinalContractAmount';
GO

ALTER TABLE [engineer].[ContractChanges] ADD [FinancialChangeAmount] decimal(18,2) NOT NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪¿┘ä╪║ ┘à╪º┘ä█î ╪¬╪║█î█î╪▒ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'FinancialChangeAmount';
GO

ALTER TABLE [engineer].[ContractChanges] ADD [PreviousContractAmount] decimal(18,2) NOT NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪¿┘ä╪║ ┘é╪▒╪º╪▒╪»╪º╪» ┘╛█î╪┤ ╪º╪▓ ╪¬╪║█î█î╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'PreviousContractAmount';
GO

ALTER TABLE [engineer].[ContractAdjustmentReferences] ADD [Code] nvarchar(100) NULL;
DECLARE @description AS sql_variant;
SET @description = N'┌⌐╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'Code';
GO

ALTER TABLE [engineer].[ContractAdjustmentIndexes] ADD [Code] nvarchar(100) NULL;
DECLARE @description AS sql_variant;
SET @description = N'┌⌐╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'Code';
GO

UPDATE [engineer].[ContractAdjustmentReferences]
SET [Code] = CONCAT(N'LEGACY-REF-', [Id])
WHERE [Code] IS NULL
   OR LTRIM(RTRIM([Code])) = N'';

UPDATE [engineer].[ContractAdjustmentIndexes]
SET [Code] = CONCAT(N'LEGACY-IDX-', [Id])
WHERE [Code] IS NULL
   OR LTRIM(RTRIM([Code])) = N'';
GO

IF EXISTS
(
    SELECT 1
    FROM [engineer].[ContractAdjustmentReferences]
    WHERE [Code] IS NULL
       OR LTRIM(RTRIM([Code])) = N''
)
BEGIN
    THROW 51010,
        'ContractAdjustmentReference Code backfill failed.',
        1;
END;

IF EXISTS
(
    SELECT [Code]
    FROM [engineer].[ContractAdjustmentReferences]
    WHERE [IsDeleted] = 0
    GROUP BY [Code]
    HAVING COUNT(*) > 1
)
BEGIN
    THROW 51011,
        'Duplicate ContractAdjustmentReference Code detected after backfill.',
        1;
END;

IF EXISTS
(
    SELECT 1
    FROM [engineer].[ContractAdjustmentIndexes]
    WHERE [Code] IS NULL
       OR LTRIM(RTRIM([Code])) = N''
)
BEGIN
    THROW 51012,
        'ContractAdjustmentIndex Code backfill failed.',
        1;
END;

IF EXISTS
(
    SELECT
        [ContractAdjustmentReferenceId],
        [Code]
    FROM [engineer].[ContractAdjustmentIndexes]
    WHERE [IsDeleted] = 0
    GROUP BY
        [ContractAdjustmentReferenceId],
        [Code]
    HAVING COUNT(*) > 1
)
BEGIN
    THROW 51013,
        'Duplicate ContractAdjustmentIndex Code detected after backfill.',
        1;
END;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractAdjustmentReferences]') AND [c].[name] = N'Code');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractAdjustmentReferences] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [engineer].[ContractAdjustmentReferences] ALTER COLUMN [Code] nvarchar(100) NOT NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractAdjustmentIndexes]') AND [c].[name] = N'Code');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractAdjustmentIndexes] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [engineer].[ContractAdjustmentIndexes] ALTER COLUMN [Code] nvarchar(100) NOT NULL;
GO

CREATE TABLE [engineer].[ContractLegalSnapshots] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractId] bigint NOT NULL,
    [ProjectId] bigint NOT NULL,
    [ContractPartyId] bigint NOT NULL,
    [FaTitle] nvarchar(250) NOT NULL,
    [EnTitle] nvarchar(250) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [StartDate] datetime2 NOT NULL,
    [Duration] int NOT NULL,
    [DurationUnit] int NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [CurrencyId] bigint NULL,
    [InitialAmount] decimal(18,2) NOT NULL,
    [FinalAmount] decimal(18,2) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractLegalSnapshots] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractLegalSnapshots_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [engineer].[Contracts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContractLegalSnapshots_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'ContractId';
SET @description = N'╪┤┘å╪º╪│┘ç ┘╛╪▒┘ê┌ÿ┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'ProjectId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪╖╪▒┘ü ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'ContractPartyId';
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'FaTitle';
SET @description = N'╪╣┘å┘ê╪º┘å ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'EnTitle';
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'Description';
SET @description = N'╪¬╪º╪▒█î╪« ╪┤╪▒┘ê╪╣';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'StartDate';
SET @description = N'┘à╪»╪¬ ╪▓┘à╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'Duration';
SET @description = N'┘ê╪º╪¡╪» ┘à╪»╪¬ ╪▓┘à╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'DurationUnit';
SET @description = N'╪¬╪º╪▒█î╪« ┘╛╪º█î╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'EndDate';
SET @description = N'┘ê╪╢╪╣█î╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'Status';
SET @description = N'╪º╪▒╪▓';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'CurrencyId';
SET @description = N'┘à╪¿┘ä╪║ ╪º┘ê┘ä█î┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'InitialAmount';
SET @description = N'┘à╪¿┘ä╪║ ┘å┘ç╪º█î█î ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'FinalAmount';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractLegalSnapshots', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ContractStatusHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractId] bigint NOT NULL,
    [FromStatus] int NOT NULL,
    [ToStatus] int NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractStatusHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractStatusHistories_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [engineer].[Contracts] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'ContractId';
SET @description = N'┘ê╪╢╪╣█î╪¬ ┘é╪¿┘ä█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'FromStatus';
SET @description = N'┘ê╪╢╪╣█î╪¬ ╪¼╪»█î╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'ToStatus';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractStatusHistories', 'COLUMN', N'IsDeleted';
GO

CREATE UNIQUE INDEX [IX_ContractAdjustmentReferences_Code] ON [engineer].[ContractAdjustmentReferences] ([Code]) WHERE [IsDeleted] = 0;
GO

CREATE UNIQUE INDEX [IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId_Code] ON [engineer].[ContractAdjustmentIndexes] ([ContractAdjustmentReferenceId], [Code]) WHERE [IsDeleted] = 0;
GO

CREATE UNIQUE INDEX [IX_ContractLegalSnapshots_ContractId] ON [engineer].[ContractLegalSnapshots] ([ContractId]);
GO

CREATE INDEX [IX_ContractLegalSnapshots_ProjectId] ON [engineer].[ContractLegalSnapshots] ([ProjectId]);
GO

CREATE INDEX [IX_ContractStatusHistories_ContractId] ON [engineer].[ContractStatusHistories] ([ContractId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260902064238_ContractHistoryIsAdded', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractGuarantees] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractId] bigint NOT NULL,
    [Type] int NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Percentage] decimal(5,2) NULL,
    [Number] nvarchar(250) NOT NULL,
    [IssueDate] datetime2 NOT NULL,
    [ExpiryDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [FileUrl] nvarchar(1500) NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractGuarantees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractGuarantees_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [engineer].[Contracts] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'ContractId';
SET @description = N'┘å┘ê╪╣ ╪¬╪╢┘à█î┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'Type';
SET @description = N'┘à╪¿┘ä╪║ ╪¬╪╢┘à█î┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'Amount';
SET @description = N'╪»╪▒╪╡╪» ╪¬╪╢┘à█î┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'Percentage';
SET @description = N'╪┤┘à╪º╪▒┘ç ╪╢┘à╪º┘å╪¬ΓÇî┘å╪º┘à┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'Number';
SET @description = N'╪¬╪º╪▒█î╪« ╪╡╪»┘ê╪▒ ╪╢┘à╪º┘å╪¬ΓÇî┘å╪º┘à┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'IssueDate';
SET @description = N'╪¬╪º╪▒█î╪« ╪º┘å┘é╪╢╪º█î ╪╢┘à╪º┘å╪¬ΓÇî┘å╪º┘à┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'ExpiryDate';
SET @description = N'┘ê╪╢╪╣█î╪¬ ╪¬╪╢┘à█î┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'Status';
SET @description = N'┘ü╪º█î┘ä ╪╢┘à╪º┘å╪¬ΓÇî┘å╪º┘à┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'FileUrl';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractGuarantees', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ContractGuarantees_ContractId] ON [engineer].[ContractGuarantees] ([ContractId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260902075125_ContractGuaranteeIsAdded', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractTypeDetails] ADD [DurationUnit] int NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘ê╪º╪¡╪» ┘à╪»╪¬ ╪▓┘à╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'DurationUnit';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260902113119_ContractDurationUnitIsAddedToCTD', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[DisciplineDocs] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(255) NOT NULL,
    [Title] nvarchar(255) NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_DisciplineDocs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [engineer].[Disciplines] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(255) NOT NULL,
    [Name] nvarchar(1500) NOT NULL,
    [EnglishName] nvarchar(1500) NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Disciplines] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [engineer].[DisciplineDocTypes] (
    [Id] bigint NOT NULL IDENTITY,
    [DisciplineId] bigint NOT NULL,
    [DisciplineDocId] bigint NOT NULL,
    [Code] nvarchar(255) NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_DisciplineDocTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DisciplineDocTypes_DisciplineDocs_DisciplineDocId] FOREIGN KEY ([DisciplineDocId]) REFERENCES [engineer].[DisciplineDocs] ([Id]),
    CONSTRAINT [FK_DisciplineDocTypes_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [engineer].[Disciplines] ([Id])
);
GO

CREATE TABLE [engineer].[ProjectDocs] (
    [Id] bigint NOT NULL IDENTITY,
    [ProjectId] bigint NOT NULL,
    [DisciplineId] bigint NOT NULL,
    [DisciplineDocId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectDocs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectDocs_DisciplineDocs_DisciplineDocId] FOREIGN KEY ([DisciplineDocId]) REFERENCES [engineer].[DisciplineDocs] ([Id]),
    CONSTRAINT [FK_ProjectDocs_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [engineer].[Disciplines] ([Id]),
    CONSTRAINT [FK_ProjectDocs_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
GO

CREATE INDEX [IX_DisciplineDocTypes_DisciplineDocId] ON [engineer].[DisciplineDocTypes] ([DisciplineDocId]);
GO

CREATE INDEX [IX_DisciplineDocTypes_DisciplineId] ON [engineer].[DisciplineDocTypes] ([DisciplineId]);
GO

CREATE INDEX [IX_ProjectDocs_DisciplineDocId] ON [engineer].[ProjectDocs] ([DisciplineDocId]);
GO

CREATE INDEX [IX_ProjectDocs_DisciplineId] ON [engineer].[ProjectDocs] ([DisciplineId]);
GO

CREATE INDEX [IX_ProjectDocs_ProjectId] ON [engineer].[ProjectDocs] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260906072725_CreateEngineeringDocs', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Code] nvarchar(256) NOT NULL DEFAULT N'';
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Description] nvarchar(1500) NOT NULL DEFAULT N'';
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Revision] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Sequence] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Status] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [ThirdPartyId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

ALTER TABLE [engineer].[ProjectDocs] ADD [Url] nvarchar(256) NOT NULL DEFAULT N'';
GO

CREATE TABLE [engineer].[ProjectDocHistories] (
    [Id] bigint NOT NULL IDENTITY,
    [ProjectId] bigint NOT NULL,
    [DisciplineId] bigint NOT NULL,
    [DisciplineDocId] bigint NOT NULL,
    [Url] nvarchar(256) NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [ThirdPartyId] bigint NOT NULL,
    [Code] nvarchar(256) NOT NULL,
    [Revision] int NOT NULL,
    [Sequence] int NOT NULL,
    [Status] int NOT NULL,
    [ProjectDocId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ProjectDocHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectDocHistories_DisciplineDocs_DisciplineDocId] FOREIGN KEY ([DisciplineDocId]) REFERENCES [engineer].[DisciplineDocs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectDocHistories_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [engineer].[Disciplines] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectDocHistories_ProjectDocs_ProjectDocId] FOREIGN KEY ([ProjectDocId]) REFERENCES [engineer].[ProjectDocs] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectDocHistories_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ProjectDocHistories_DisciplineDocId] ON [engineer].[ProjectDocHistories] ([DisciplineDocId]);
GO

CREATE INDEX [IX_ProjectDocHistories_DisciplineId] ON [engineer].[ProjectDocHistories] ([DisciplineId]);
GO

CREATE INDEX [IX_ProjectDocHistories_ProjectDocId] ON [engineer].[ProjectDocHistories] ([ProjectDocId]);
GO

CREATE INDEX [IX_ProjectDocHistories_ProjectId] ON [engineer].[ProjectDocHistories] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260907112429_AddProjectDocumentHistory', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [engineer].[ProjectDocumentFiles];
GO

DROP TABLE [engineer].[ProjectDocuments];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260908065626_delete-old-project-documents', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [Importance] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260908065843_AddImportanceToRGS', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractTypeDetails] ADD [IsSubjectToAdjustment] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'┘à╪┤┘à┘ê┘ä ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'IsSubjectToAdjustment';
GO

ALTER TABLE [engineer].[ContractFinancialInformations] ADD [IsSubjectToAdjustment] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @description AS sql_variant;
SET @description = N'┘à╪┤┘à┘ê┘ä ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'IsSubjectToAdjustment';
GO

CREATE TABLE [engineer].[ContractAdjustmentConfigurations] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractId] bigint NOT NULL,
    [Type] int NOT NULL,
    [PriceIndexBaseYear] int NULL,
    [PriceIndexBasePeriod] int NULL,
    [PriceIndexId] bigint NULL,
    [CurrencyBaseDate] datetime2 NULL,
    [CurrencyBaseRate] decimal(18,6) NULL,
    [CurrencyId] bigint NULL,
    [CurrencyReferenceType] int NULL,
    [CurrencyCustomReference] nvarchar(250) NULL,
    [OtherBasis] nvarchar(250) NULL,
    [OtherReference] nvarchar(250) NULL,
    [OtherIndex] nvarchar(250) NULL,
    [Description] nvarchar(1500) NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractAdjustmentConfigurations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractAdjustmentConfigurations_ContractAdjustmentIndexes_PriceIndexId] FOREIGN KEY ([PriceIndexId]) REFERENCES [engineer].[ContractAdjustmentIndexes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContractAdjustmentConfigurations_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [engineer].[Contracts] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentConfigurations', 'COLUMN', N'Id';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentConfigurations', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentConfigurations', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentConfigurations', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentConfigurations', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentConfigurations', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ContractAdjustmentScopes] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractAdjustmentConfigurationId] bigint NOT NULL,
    [ScopeType] int NOT NULL,
    [ContractTypeKind] int NULL,
    [ContractTypeDetailId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractAdjustmentScopes] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_ContractAdjustmentScopes_Shape] CHECK (([ScopeType] = 1 AND [ContractTypeKind] IS NULL AND [ContractTypeDetailId] IS NULL) OR ([ScopeType] = 2 AND [ContractTypeKind] IS NOT NULL AND [ContractTypeDetailId] IS NULL) OR ([ScopeType] = 3 AND [ContractTypeKind] IS NULL AND [ContractTypeDetailId] IS NOT NULL)),
    CONSTRAINT [FK_ContractAdjustmentScopes_ContractAdjustmentConfigurations_ContractAdjustmentConfigurationId] FOREIGN KEY ([ContractAdjustmentConfigurationId]) REFERENCES [engineer].[ContractAdjustmentConfigurations] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContractAdjustmentScopes_ContractTypeDetails_ContractTypeDetailId] FOREIGN KEY ([ContractTypeDetailId]) REFERENCES [engineer].[ContractTypeDetails] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentScopes', 'COLUMN', N'Id';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentScopes', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentScopes', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentScopes', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentScopes', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentScopes', 'COLUMN', N'IsDeleted';
GO

CREATE UNIQUE INDEX [IX_ContractAdjustmentConfigurations_ContractId] ON [engineer].[ContractAdjustmentConfigurations] ([ContractId]) WHERE [IsDeleted] = 0;
GO

CREATE INDEX [IX_ContractAdjustmentConfigurations_PriceIndexId] ON [engineer].[ContractAdjustmentConfigurations] ([PriceIndexId]);
GO

CREATE INDEX [IX_ContractAdjustmentScopes_ContractAdjustmentConfigurationId] ON [engineer].[ContractAdjustmentScopes] ([ContractAdjustmentConfigurationId]);
GO

CREATE INDEX [IX_ContractAdjustmentScopes_ContractTypeDetailId] ON [engineer].[ContractAdjustmentScopes] ([ContractTypeDetailId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260912122237_IsSubjectToAdjustment', N'8.0.8');
GO

COMMIT;
GO



