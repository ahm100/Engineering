BEGIN TRANSACTION;
GO

CREATE SEQUENCE [engineer].[Contract_ContractNumber] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [engineer].[Contracts] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractNumber] bigint NULL DEFAULT (NEXT VALUE FOR engineer.Contract_ContractNumber),
    [Title] nvarchar(max) NOT NULL,
    [ProjectId] bigint NOT NULL,
    [ContractPartyId] bigint NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [Duration] int NOT NULL,
    [DurationUnit] int NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [CompanyId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Contracts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Contracts_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'Id';
SET @description = N'╪┤┘à╪º╪▒┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'ContractNumber';
SET @description = N'╪╣┘å┘ê╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'Title';
SET @description = N'╪┤┘å╪º╪│┘ç ┘╛╪▒┘ê┌ÿ┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'ProjectId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪╖╪▒┘ü ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'ContractPartyId';
SET @description = N'╪¬╪º╪▒█î╪« ╪┤╪▒┘ê╪╣';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'StartDate';
SET @description = N'┘à╪»╪¬ ╪▓┘à╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'Duration';
SET @description = N'┘ê╪º╪¡╪» ┘à╪»╪¬ ╪▓┘à╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'DurationUnit';
SET @description = N'╪¬╪º╪▒█î╪« ┘╛╪º█î╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'EndDate';
SET @description = N'┘ê╪╢╪╣█î╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'Status';
SET @description = N'╪┤┘å╪º╪│┘ç ┌⌐┘à┘╛╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'CompanyId';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'IsDeleted';
GO

CREATE UNIQUE INDEX [IX_Contracts_ContractNumber] ON [engineer].[Contracts] ([ContractNumber]) WHERE [ContractNumber] IS NOT NULL;
GO

CREATE INDEX [IX_Contracts_ProjectId] ON [engineer].[Contracts] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260824110132_AddNewContractAggregate', N'8.0.8');
GO

COMMIT;
GO



