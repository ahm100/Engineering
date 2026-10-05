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



