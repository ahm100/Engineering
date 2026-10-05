BEGIN TRANSACTION;
GO

DROP INDEX [IX_ContractAdjustmentIndexes_ContractAdjustmentReferenceId] ON [engineer].[ContractAdjustmentIndexes];
GO

ALTER TABLE [engineer].[ContractChanges] ADD [FinalContractAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪¿┘ä╪║ ┘å┘ç╪º█î█î ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'FinalContractAmount';
GO

ALTER TABLE [engineer].[ContractChanges] ADD [FinancialChangeAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪¿┘ä╪║ ┘à╪º┘ä█î ╪¬╪║█î█î╪▒ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'FinancialChangeAmount';
GO

ALTER TABLE [engineer].[ContractChanges] ADD [PreviousContractAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪¿┘ä╪║ ┘é╪▒╪º╪▒╪»╪º╪» ┘╛█î╪┤ ╪º╪▓ ╪¬╪║█î█î╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'PreviousContractAmount';
GO

ALTER TABLE [engineer].[ContractAdjustmentReferences] ADD [Code] nvarchar(100) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'┌⌐╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentReferences', 'COLUMN', N'Code';
GO

ALTER TABLE [engineer].[ContractAdjustmentIndexes] ADD [Code] nvarchar(100) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'┌⌐╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractAdjustmentIndexes', 'COLUMN', N'Code';
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



