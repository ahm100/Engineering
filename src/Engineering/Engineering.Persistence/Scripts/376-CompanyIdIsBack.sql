BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Contracts] ADD [CompanyId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç ┌⌐┘à┘╛╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'CompanyId';
GO

ALTER TABLE [engineer].[ContractorContracts] ADD [CompanyId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç ┌⌐┘à┘╛╪º┘å█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractorContracts', 'COLUMN', N'CompanyId';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260901122206_CompanyIdIsBack', N'8.0.8');
GO

COMMIT;
GO



