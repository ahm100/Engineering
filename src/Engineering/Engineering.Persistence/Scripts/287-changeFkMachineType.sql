BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[MachineTypes] DROP CONSTRAINT [FK_MachineTypes_CabinTypes_CabinTypeCode];
GO

DROP INDEX [IX_MachineTypes_CabinTypeCode] ON [engineer].[MachineTypes];
GO

ALTER TABLE [engineer].[CabinTypes] DROP CONSTRAINT [AK_CabinTypes_CabinTypeCode];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[MachineTypes]') AND [c].[name] = N'CabinTypeCode');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[MachineTypes] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[MachineTypes] DROP COLUMN [CabinTypeCode];
GO

ALTER TABLE [engineer].[MachineTypes] ADD [CabinTypeId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

CREATE INDEX [IX_MachineTypes_CabinTypeId] ON [engineer].[MachineTypes] ([CabinTypeId]);
GO

ALTER TABLE [engineer].[MachineTypes] ADD CONSTRAINT [FK_MachineTypes_CabinTypes_CabinTypeId] FOREIGN KEY ([CabinTypeId]) REFERENCES [engineer].[CabinTypes] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260422060452_changeFkMachineType', N'8.0.8');
GO

COMMIT;
GO



