BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ContractTypeDetails] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ContractTypeId] bigint NOT NULL,
    [ConsumableVolumeProductId] bigint NULL,
    [ProjectOperationDetailId] bigint NULL,
    [ProjectOperationDetailContractorServiceId] bigint NULL,
    [Quantity] decimal(18,5) NOT NULL,
    [UnitOfMeasurementId] bigint NOT NULL,
    [UnitPrice] decimal(18,2) NULL,
    [FixedAmount] decimal(18,2) NULL,
    [TechnicalSpecifications] nvarchar(max) NULL,
    [ExpectedDeliverables] nvarchar(max) NULL,
    [Duration] decimal(18,2) NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ContractTypeDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_ContractTypeDetails_ExactlyOneSource] CHECK ((CASE WHEN [ConsumableVolumeProductId] IS NULL THEN 0 ELSE 1 END + CASE WHEN [ProjectOperationDetailId] IS NULL THEN 0 ELSE 1 END + CASE WHEN [ProjectOperationDetailContractorServiceId] IS NULL THEN 0 ELSE 1 END) = 1),
    CONSTRAINT [FK_ContractTypeDetails_ContractTypes_ContractTypeId] FOREIGN KEY ([ContractTypeId]) REFERENCES [engineer].[ContractTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContractTypeDetails_ProjectOperationDetailConsumableVolumeProducts_ConsumableVolumeProductId] FOREIGN KEY ([ConsumableVolumeProductId]) REFERENCES [engineer].[ProjectOperationDetailConsumableVolumeProducts] ([Id]),
    CONSTRAINT [FK_ContractTypeDetails_ProjectOperationDetailContractorServices_ProjectOperationDetailContractorServiceId] FOREIGN KEY ([ProjectOperationDetailContractorServiceId]) REFERENCES [engineer].[ProjectOperationDetailContractorServices] ([Id]),
    CONSTRAINT [FK_ContractTypeDetails_ProjectOperationDetails_ProjectOperationDetailId] FOREIGN KEY ([ProjectOperationDetailId]) REFERENCES [engineer].[ProjectOperationDetails] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'Id';
SET @description = N'╪┤┘å╪º╪│┘ç ┘å┘ê╪╣ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'ContractTypeId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪¡╪¼┘à ┘à╪╡╪▒┘ü█î ┌⌐╪º┘ä╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'ConsumableVolumeProductId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪¿╪▒╪ó┘ê╪▒╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'ProjectOperationDetailId';
SET @description = N'╪┤┘å╪º╪│┘ç ╪«╪»┘à╪¬ ┘╛█î┘à╪º┘å┌⌐╪º╪▒ ╪▒█î╪▓┘à╪¬╪▒┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'ProjectOperationDetailContractorServiceId';
SET @description = N'┘à┘é╪»╪º╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'Quantity';
SET @description = N'╪┤┘å╪º╪│┘ç ┘ê╪º╪¡╪» ╪º┘å╪»╪º╪▓┘çΓÇî┌»█î╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'UnitOfMeasurementId';
SET @description = N'┘à╪¿┘ä╪║ ┘ê╪º╪¡╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'UnitPrice';
SET @description = N'┘à╪¿┘ä╪║ ┘à┘é╪╖┘ê╪╣';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'FixedAmount';
SET @description = N'┘à╪┤╪«╪╡╪º╪¬ ┘ü┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'TechnicalSpecifications';
SET @description = N'╪«╪▒┘ê╪¼█îΓÇî┘ç╪º█î ┘à┘ê╪▒╪» ╪º┘å╪¬╪╕╪º╪▒';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'ExpectedDeliverables';
SET @description = N'┘à╪»╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'Duration';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ContractTypeDetails_ConsumableVolumeProductId] ON [engineer].[ContractTypeDetails] ([ConsumableVolumeProductId]);
GO

CREATE UNIQUE INDEX [IX_ContractTypeDetails_ContractTypeId_ConsumableVolumeProductId] ON [engineer].[ContractTypeDetails] ([ContractTypeId], [ConsumableVolumeProductId]) WHERE [IsDeleted] = 0 AND [ConsumableVolumeProductId] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_ContractTypeDetails_ContractTypeId_ProjectOperationDetailContractorServiceId] ON [engineer].[ContractTypeDetails] ([ContractTypeId], [ProjectOperationDetailContractorServiceId]) WHERE [IsDeleted] = 0 AND [ProjectOperationDetailContractorServiceId] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_ContractTypeDetails_ContractTypeId_ProjectOperationDetailId] ON [engineer].[ContractTypeDetails] ([ContractTypeId], [ProjectOperationDetailId]) WHERE [IsDeleted] = 0 AND [ProjectOperationDetailId] IS NOT NULL;
GO

CREATE INDEX [IX_ContractTypeDetails_ProjectOperationDetailContractorServiceId] ON [engineer].[ContractTypeDetails] ([ProjectOperationDetailContractorServiceId]);
GO

CREATE INDEX [IX_ContractTypeDetails_ProjectOperationDetailId] ON [engineer].[ContractTypeDetails] ([ProjectOperationDetailId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260826095529_AddContractTypeDetails', N'8.0.8');
GO

COMMIT;
GO



