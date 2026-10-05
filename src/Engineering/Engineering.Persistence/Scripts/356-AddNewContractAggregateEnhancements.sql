BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Contracts]') AND [c].[name] = N'Title');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Contracts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Contracts] DROP COLUMN [Title];
GO

ALTER TABLE [engineer].[Contracts] ADD [Description] nvarchar(1500) NULL;
DECLARE @description AS sql_variant;
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'Description';
GO

ALTER TABLE [engineer].[Contracts] ADD [EnTitle] nvarchar(250) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'╪╣┘å┘ê╪º┘å ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'EnTitle';
GO

ALTER TABLE [engineer].[Contracts] ADD [FaTitle] nvarchar(250) NOT NULL DEFAULT N'';
DECLARE @description AS sql_variant;
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Contracts', 'COLUMN', N'FaTitle';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260825074814_AddNewContractAggregateEnhancements', N'8.0.8');
GO

COMMIT;
GO



