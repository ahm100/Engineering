BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectCalendars] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TitleFa] nvarchar(250) NOT NULL,
    [TitleEn] nvarchar(250) NULL,
    [MppUid] int NULL,
    [IsDefault] bit NOT NULL,
    [MinutesPerDay] int NOT NULL,
    [ProjectId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectCalendars] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCalendars_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'Id';
SET @description = N'╪╣┘å┘ê╪º┘å ┘ü╪º╪▒╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'TitleFa';
SET @description = N'╪╣┘å┘ê╪º┘å ╪º┘å┌»┘ä█î╪│█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'TitleEn';
SET @description = N'╪┤┘å╪º╪│┘ç ╪¬┘é┘ê█î┘à ╪»╪▒ Microsoft Project';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'MppUid';
SET @description = N'╪¬┘é┘ê█î┘à ┘╛█î╪┤ΓÇî┘ü╪▒╪╢ ┘╛╪▒┘ê┌ÿ┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'IsDefault';
SET @description = N'╪¬╪╣╪»╪º╪» ╪»┘é╪º█î┘é ┌⌐╪º╪▒█î ╪º╪│╪¬╪º┘å╪»╪º╪▒╪» ╪»╪▒ ╪▒┘ê╪▓';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'MinutesPerDay';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendars', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectCalendarExceptions] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Date] datetime2 NOT NULL,
    [IsWorking] bit NOT NULL,
    [From] time NULL,
    [To] time NULL,
    [Description] nvarchar(1500) NULL,
    [ProjectCalendarId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectCalendarExceptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCalendarExceptions_ProjectCalendars_ProjectCalendarId] FOREIGN KEY ([ProjectCalendarId]) REFERENCES [engineer].[ProjectCalendars] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Id';
SET @description = N'╪¬╪º╪▒█î╪« ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Date';
SET @description = N'┘ê╪╢╪╣█î╪¬ ┌⌐╪º╪▒█î ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'IsWorking';
SET @description = N'╪▓┘à╪º┘å ╪┤╪▒┘ê╪╣ ┌⌐╪º╪▒ ╪»╪▒ ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'From';
SET @description = N'╪▓┘à╪º┘å ┘╛╪º█î╪º┘å ┌⌐╪º╪▒ ╪»╪▒ ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'To';
SET @description = N'╪¬┘ê╪╢█î╪¡╪º╪¬ ╪º╪│╪¬╪½┘å╪º█î ╪¬┘é┘ê█î┘à';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Description';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarExceptions', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectCalendarWorkingDaies] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [DayOfWeek] int NOT NULL,
    [IsWorking] bit NOT NULL,
    [ProjectCalendarId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectCalendarWorkingDaies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCalendarWorkingDaies_ProjectCalendars_ProjectCalendarId] FOREIGN KEY ([ProjectCalendarId]) REFERENCES [engineer].[ProjectCalendars] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'Id';
SET @description = N'╪▒┘ê╪▓ ┘ç┘ü╪¬┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'DayOfWeek';
SET @description = N'╪▒┘ê╪▓ ┌⌐╪º╪▒█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'IsWorking';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingDaies', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[ProjectCalendarWorkingTimes] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [From] time NOT NULL,
    [To] time NOT NULL,
    [ProjectCalendarWorkingDayId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectCalendarWorkingTimes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDaies_ProjectCalendarWorkingDayId] FOREIGN KEY ([ProjectCalendarWorkingDayId]) REFERENCES [engineer].[ProjectCalendarWorkingDaies] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'╪┤┘å╪º╪│┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'Id';
SET @description = N'╪▓┘à╪º┘å ╪┤╪▒┘ê╪╣ ┌⌐╪º╪▒ ╪»╪▒ ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'From';
SET @description = N'╪▓┘à╪º┘å ┘╛╪º█î╪º┘å ┌⌐╪º╪▒ ╪»╪▒ ╪▒┘ê╪▓ ╪º╪│╪¬╪½┘å╪º';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'To';
SET @description = N'╪¬╪º╪▒█î╪« ╪º█î╪¼╪º╪»';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'Created';
SET @description = N'╪º█î╪¼╪º╪» ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'CreatorId';
SET @description = N'╪¬╪º╪▒█î╪« ┘ê█î╪▒╪º█î╪┤';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'Updated';
SET @description = N'┘ê█î╪▒╪º█î╪┤ ┌⌐┘å┘å╪»┘ç';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'UpdaterId';
SET @description = N'╪¡╪░┘ü ╪┤╪»┌»█î';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectCalendarWorkingTimes', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectCalendarExceptions_ProjectCalendarId] ON [engineer].[ProjectCalendarExceptions] ([ProjectCalendarId]);
GO

CREATE INDEX [IX_ProjectCalendars_ProjectId] ON [engineer].[ProjectCalendars] ([ProjectId]);
GO

CREATE INDEX [IX_ProjectCalendarWorkingDaies_ProjectCalendarId] ON [engineer].[ProjectCalendarWorkingDaies] ([ProjectCalendarId]);
GO

CREATE INDEX [IX_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDayId] ON [engineer].[ProjectCalendarWorkingTimes] ([ProjectCalendarWorkingDayId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260902060956_AddCalenderTables', N'8.0.8');
GO

COMMIT;
GO



