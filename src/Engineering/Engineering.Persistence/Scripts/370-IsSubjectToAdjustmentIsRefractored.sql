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



