BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractTypes] ADD [AdjustmentCapPercentage] decimal(18,5) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪»╪▒╪╡╪» ╪│┘é┘ü ╪¬╪╣╪»█î┘ä';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ContractTypes', 'COLUMN', N'AdjustmentCapPercentage';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260826131717_AddContractTypeAdjustmentCap', N'8.0.8');
GO

COMMIT;
GO



