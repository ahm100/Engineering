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



