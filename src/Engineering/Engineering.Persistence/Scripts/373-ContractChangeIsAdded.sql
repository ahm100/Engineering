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



