BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[DisciplineDocs] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(255) NOT NULL,
    [Title] nvarchar(255) NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_DisciplineDocs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [engineer].[Disciplines] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(255) NOT NULL,
    [Name] nvarchar(1500) NOT NULL,
    [EnglishName] nvarchar(1500) NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Disciplines] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [engineer].[DisciplineDocTypes] (
    [Id] bigint NOT NULL IDENTITY,
    [DisciplineId] bigint NOT NULL,
    [DisciplineDocId] bigint NOT NULL,
    [Code] nvarchar(255) NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_DisciplineDocTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DisciplineDocTypes_DisciplineDocs_DisciplineDocId] FOREIGN KEY ([DisciplineDocId]) REFERENCES [engineer].[DisciplineDocs] ([Id]),
    CONSTRAINT [FK_DisciplineDocTypes_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [engineer].[Disciplines] ([Id])
);
GO

CREATE TABLE [engineer].[ProjectDocs] (
    [Id] bigint NOT NULL IDENTITY,
    [ProjectId] bigint NOT NULL,
    [DisciplineId] bigint NOT NULL,
    [DisciplineDocId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectDocs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectDocs_DisciplineDocs_DisciplineDocId] FOREIGN KEY ([DisciplineDocId]) REFERENCES [engineer].[DisciplineDocs] ([Id]),
    CONSTRAINT [FK_ProjectDocs_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [engineer].[Disciplines] ([Id]),
    CONSTRAINT [FK_ProjectDocs_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
GO

CREATE INDEX [IX_DisciplineDocTypes_DisciplineDocId] ON [engineer].[DisciplineDocTypes] ([DisciplineDocId]);
GO

CREATE INDEX [IX_DisciplineDocTypes_DisciplineId] ON [engineer].[DisciplineDocTypes] ([DisciplineId]);
GO

CREATE INDEX [IX_ProjectDocs_DisciplineDocId] ON [engineer].[ProjectDocs] ([DisciplineDocId]);
GO

CREATE INDEX [IX_ProjectDocs_DisciplineId] ON [engineer].[ProjectDocs] ([DisciplineId]);
GO

CREATE INDEX [IX_ProjectDocs_ProjectId] ON [engineer].[ProjectDocs] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260906072725_CreateEngineeringDocs', N'8.0.8');
GO

COMMIT;
GO



