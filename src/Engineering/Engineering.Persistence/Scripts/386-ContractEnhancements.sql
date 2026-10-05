BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractFinancialInformations] ADD [RegisteredInitialAmount] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘à╪¿┘ä╪║ ╪º┘ê┘ä█î┘ç ╪½╪¿╪¬ΓÇî╪┤╪»┘ç ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractFinancialInformations', 'COLUMN', N'RegisteredInitialAmount';
GO

ALTER TABLE [engineer].[ContractChanges] ADD [Mode] int NOT NULL DEFAULT 0;
DECLARE @description AS sql_variant;
SET @description = N'╪┤█î┘ê┘ç ╪½╪¿╪¬ ╪¬╪║█î█î╪▒ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractChanges', 'COLUMN', N'Mode';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260912170514_ContractEnhancements', N'8.0.8');
GO

COMMIT;
GO



