BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[Projects]') AND [c].[name] = N'ApprovedBudget');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[Projects] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[Projects] ALTER COLUMN [ApprovedBudget] decimal(18,2) NULL;
GO

CREATE TABLE [engineer].[EmployerEmployees] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [EmployeeId] bigint NOT NULL,
    [EmployerId] bigint NOT NULL,
    [CompanyId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EmployerEmployees] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260610073848_CreateEmployerEmployee', N'8.0.8');
GO

COMMIT;
GO



