BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260510105100_AddCurrencySynonym', N'8.0.8');
CREATE SYNONYM engineer.ViewCurrency FOR [MetaDataDb].[meta].[Currencies]
GO
COMMIT;
GO



