BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260513145211_RemovePreferentialComment', N'8.0.8');
GO

COMMIT;
GO



