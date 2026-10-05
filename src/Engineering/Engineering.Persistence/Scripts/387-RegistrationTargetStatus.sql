BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[Contracts] ADD [RegistrationTargetStatus] int NULL;
DECLARE @description AS sql_variant;
SET @description = N'┘ê╪╢╪╣█î╪¬ ┘ç╪»┘ü ╪½╪¿╪¬ ┘é╪▒╪º╪▒╪»╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'RegistrationTargetStatus';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260913043215_RegistrationTargetStatus', N'8.0.8');
GO

COMMIT;
GO



