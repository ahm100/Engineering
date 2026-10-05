BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(1500) NOT NULL,
    [ContractId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContractDocuments_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [engineer].[Contracts] ([Id]) ON DELETE CASCADE
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractDocuments', 'COLUMN', N'Id';
SET @description = N'┘à╪│█î╪▒ ┘ü╪º█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractDocuments', 'COLUMN', N'Url';
SET @description = N'╪┤┘å╪º╪│┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractDocuments', 'COLUMN', N'ContractId';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractDocuments', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractDocuments', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractDocuments', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractDocuments', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractDocuments', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ContractDocuments_ContractId] ON [engineer].[ContractDocuments] ([ContractId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260825132758_AddContractDocuments', N'8.0.8');
GO

COMMIT;
GO



