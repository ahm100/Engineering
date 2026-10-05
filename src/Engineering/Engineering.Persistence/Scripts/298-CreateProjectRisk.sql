BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectRisks] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Code] nvarchar(100) NOT NULL,
    [Title] nvarchar(250) NOT NULL,
    [ProjectId] bigint NOT NULL,
    [RiskProbability] int NOT NULL,
    [RiskImpact] int NOT NULL,
    [RiskStatus] int NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectRisks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectRisks_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'Id';
SET @description = N'کد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'Code';
SET @description = N'عنوان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'Title';
SET @description = N'احتمال ریسک';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'RiskProbability';
SET @description = N'تاثیر ریسک';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'RiskImpact';
SET @description = N'وضعیت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'RiskStatus';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectRisks', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ProjectRisks_ProjectId] ON [engineer].[ProjectRisks] ([ProjectId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260614044032_CreateProjectRisk', N'8.0.8');
GO

COMMIT;
GO



