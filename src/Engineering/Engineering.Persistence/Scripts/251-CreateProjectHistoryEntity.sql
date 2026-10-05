BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
SET @description = N'مهندس ناظر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'SupervisorEngineer';
GO

DECLARE @description AS sql_variant;
SET @description = N'وضعیت پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'Status';
GO

DECLARE @description AS sql_variant;
SET @description = N'نام پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'ProjectName';
GO

DECLARE @description AS sql_variant;
SET @description = N'مدیر پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'ProjectManager';
GO

DECLARE @description AS sql_variant;
SET @description = N'کد نوع پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'ProjectCode';
GO

DECLARE @description AS sql_variant;
SET @description = N'مسئول برنامه ریزی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'PlanningAssistant';
GO

DECLARE @description AS sql_variant;
SET @description = N'کد کارفرما';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'EmployerId';
GO

DECLARE @description AS sql_variant;
SET @description = N'نوع قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'Contractual';
GO

DECLARE @description AS sql_variant;
SET @description = N'شناسه کمپانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'CompanyId';
GO

DECLARE @description AS sql_variant;
SET @description = N'مشاور پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'Advisor';
GO

DECLARE @description AS sql_variant;
SET @description = N'افزوده‌شده خودکار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'Projects', 'COLUMN', N'AddAutomated';
GO

CREATE TABLE [engineer].[ProjectHistories] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProjectName] nvarchar(250) NOT NULL,
    [ProjectCode] nvarchar(100) NULL,
    [EmployerId] bigint NOT NULL,
    [SupervisorEngineer] bigint NULL,
    [Advisor] bigint NULL,
    [ProjectManager] bigint NOT NULL,
    [PlanningAssistant] bigint NULL,
    [Status] int NOT NULL,
    [AddAutomated] bit NOT NULL,
    [Contractual] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CollectiveService] bit NOT NULL,
    [CompanyId] bigint NULL,
    [PreferentialReferenceCode] uniqueidentifier NOT NULL,
    [ProjectId] bigint NOT NULL,
    [ProjectTypesId] bigint NOT NULL,
    [ProjectTypeId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectHistories_EngineeringProjectTypes_ProjectTypesId] FOREIGN KEY ([ProjectTypesId]) REFERENCES [engineer].[EngineeringProjectTypes] ([Id]),
    CONSTRAINT [FK_ProjectHistories_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'Id';
SET @description = N'نام پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'ProjectName';
SET @description = N'کد نوع پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'ProjectCode';
SET @description = N'کد کارفرما';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'EmployerId';
SET @description = N'مهندس ناظر';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'SupervisorEngineer';
SET @description = N'مشاور پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'Advisor';
SET @description = N'مدیر پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'ProjectManager';
SET @description = N'مسئول برنامه ریزی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'PlanningAssistant';
SET @description = N'وضعیت پروژه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'Status';
SET @description = N'افزوده‌شده خودکار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'AddAutomated';
SET @description = N'نوع قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'Contractual';
SET @description = N'شناسه کمپانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'CompanyId';
SET @description = N'کد مرجع تفصیلی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'PreferentialReferenceCode';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectHistories', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_ProjectHistories_ProjectId] ON [engineer].[ProjectHistories] ([ProjectId]);
GO

CREATE INDEX [IX_ProjectHistories_ProjectTypesId] ON [engineer].[ProjectHistories] ([ProjectTypesId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251130063827_CreateProjectHistoryEntity', N'8.0.8');
GO

COMMIT;
GO



