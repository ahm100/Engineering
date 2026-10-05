BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractTypes] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Kind] int NOT NULL,
    [PricingMethod] int NOT NULL,
    [PaymentBasis] int NOT NULL,
    [IsSubjectToAmendment] bit NOT NULL,
    [ContractId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractTypes_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [engineer].[Contracts] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'Id';
SET @description = N'┘å┘ê╪╣ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'Kind';
SET @description = N'╪▒┘ê╪┤ ┘é█î┘à╪¬ΓÇî┌»╪░╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'PricingMethod';
SET @description = N'┘à╪¿┘å╪º█î ┘╛╪▒╪»╪º╪«╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'PaymentBasis';
SET @description = N'┘à╪┤┘à┘ê┘ä ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'IsSubjectToAmendment';
SET @description = N'╪┤┘å╪º╪│┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'ContractId';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ContractTypes_ContractId] ON [engineer].[ContractTypes] ([ContractId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260825094424_AddContractType', N'8.0.8');
GO

COMMIT;
GO



