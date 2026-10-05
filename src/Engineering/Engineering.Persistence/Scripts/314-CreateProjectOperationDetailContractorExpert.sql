BEGIN TRANSACTION;
GO

CREATE TABLE [engineer].[ProjectOperationDetailContractorExperts] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Volume] bigint NOT NULL,
    [HaveContract] bit NOT NULL,
    [ConsumableVolumeExpertId] bigint NOT NULL,
    [ProjectOperationDetailContractorServiceId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProjectOperationDetailContractorExperts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectOperationDetailContractorExperts_ProjectOperationDetailConsumableVolumeExperts_ConsumableVolumeExpertId] FOREIGN KEY ([ConsumableVolumeExpertId]) REFERENCES [engineer].[ProjectOperationDetailConsumableVolumeExperts] ([Id]),
    CONSTRAINT [FK_ProjectOperationDetailContractorExperts_ProjectOperationDetailContractorServices_ProjectOperationDetailContractorServiceId] FOREIGN KEY ([ProjectOperationDetailContractorServiceId]) REFERENCES [engineer].[ProjectOperationDetailContractorServices] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDetailContractorExperts', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDetailContractorExperts', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDetailContractorExperts', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDetailContractorExperts', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDetailContractorExperts', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDetailContractorExperts', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ProjectOperationDetailContractorExperts', 'COLUMN', N'IsActive';
GO

CREATE INDEX [IX_ProjectOperationDetailContractorExperts_ConsumableVolumeExpertId] ON [engineer].[ProjectOperationDetailContractorExperts] ([ConsumableVolumeExpertId]);
GO

CREATE INDEX [IX_ProjectOperationDetailContractorExperts_ProjectOperationDetailContractorServiceId] ON [engineer].[ProjectOperationDetailContractorExperts] ([ProjectOperationDetailContractorServiceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260628133740_CreateProjectOperationDetailContractorExpert', N'8.0.8');
GO

COMMIT;
GO



