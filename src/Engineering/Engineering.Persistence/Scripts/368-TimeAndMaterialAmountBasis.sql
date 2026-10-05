BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractTypeDetails] ADD [TimeAndMaterialAmountBasis] int NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪¿┘å╪º█î ┘à╪¡╪º╪│╪¿┘ç ┘à╪¿┘ä╪║ ┘å┘ü╪▒-╪▓┘à╪º┘å';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypeDetails', 'COLUMN', N'TimeAndMaterialAmountBasis';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260829053603_TimeAndMaterialAmountBasis', N'8.0.8');
GO

COMMIT;
GO



