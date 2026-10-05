BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ContractTypes]') AND [c].[name] = N'AdjustmentCapPercentage');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ContractTypes] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[ContractTypes] DROP COLUMN [AdjustmentCapPercentage];
GO

CREATE TABLE [engineer].[ContractFinancialInformations] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractId] bigint NOT NULL,
    [InitialAmount] decimal(18,2) NULL,
    [CurrencyId] bigint NOT NULL,
    [HasPrepayment] bit NOT NULL,
    [ContractCeilingAmount] decimal(18,2) NULL,
    [AdjustmentLimitValue] decimal(18,2) NULL,
    [AdjustmentLimitType] int NULL,
    [PrepaymentPercentage] decimal(5,2) NULL,
    [PrepaymentAmortizationMethod] int NULL,
    [PrepaymentAmortizationValue] decimal(18,2) NULL,
    [PrepaymentStartStatusStatementNumber] int NULL,
    [PrepaymentStartProgressPercentage] decimal(5,2) NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractFinancialInformations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractFinancialInformations_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [engineer].[Contracts] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'ContractId';
SET @description = N'┘à╪¿┘ä╪║ ╪º┘ê┘ä█î┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'InitialAmount';
SET @description = N'╪º╪▒╪▓';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'CurrencyId';
SET @description = N'┘à╪┤┘à┘ê┘ä ┘╛█î╪┤ΓÇî┘╛╪▒╪»╪º╪«╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'HasPrepayment';
SET @description = N'╪│┘é┘ü ┘à╪¿┘ä╪║ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'ContractCeilingAmount';
SET @description = N'╪│┘é┘ü ╪º┘ü╪▓╪º█î╪┤/┌⌐╪º┘ç╪┤ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'AdjustmentLimitValue';
SET @description = N'┘å┘ê╪╣ ╪│┘é┘ü ╪º┘ü╪▓╪º█î╪┤/┌⌐╪º┘ç╪┤ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'AdjustmentLimitType';
SET @description = N'╪»╪▒╪╡╪» ┘╛█î╪┤ΓÇî┘╛╪▒╪»╪º╪«╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'PrepaymentPercentage';
SET @description = N'╪▒┘ê╪┤ ╪º╪│╪¬┘ç┘ä╪º┌⌐ ┘╛█î╪┤ΓÇî┘╛╪▒╪»╪º╪«╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'PrepaymentAmortizationMethod';
SET @description = N'┘à┘é╪»╪º╪▒/╪»╪▒╪╡╪» ╪º╪│╪¬┘ç┘ä╪º┌⌐ ┘╛█î╪┤ΓÇî┘╛╪▒╪»╪º╪«╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'PrepaymentAmortizationValue';
SET @description = N'╪┤┘à╪º╪▒┘ç ╪╡┘ê╪▒╪¬ΓÇî┘ê╪╢╪╣█î╪¬ ╪┤╪▒┘ê╪╣ ╪º╪│╪¬┘ç┘ä╪º┌⌐';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'PrepaymentStartStatusStatementNumber';
SET @description = N'╪»╪▒╪╡╪» ┘╛█î╪┤╪▒┘ü╪¬ ╪┤╪▒┘ê╪╣ ╪º╪│╪¬┘ç┘ä╪º┌⌐';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'PrepaymentStartProgressPercentage';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'IsDeleted';
GO

CREATE UNIQUE INDEX [IX_ContractFinancialInformations_ContractId] ON [engineer].[ContractFinancialInformations] ([ContractId]) WHERE [IsDeleted] = 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260829044232_FinancialInformationIsAdded', N'8.0.8');
GO

COMMIT;
GO



