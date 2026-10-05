BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractTypeDetails] ADD [DurationUnit] int NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘ê╪º╪¡╪» ┘à╪»╪¬ ╪▓┘à╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'DurationUnit';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260902113119_ContractDurationUnitIsAddedToCTD', N'8.0.8');
GO

COMMIT;
GO



