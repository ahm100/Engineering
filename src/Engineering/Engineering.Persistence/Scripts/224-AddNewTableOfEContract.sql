BEGIN TRANSACTION;
GO

DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
SET @description = N'˜Ï ãÑÌÚ ÊÝÕ?á?';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EngineeringServices', 'COLUMN', N'PreferentialReferenceCode';
GO

ALTER TABLE [engineer].[EmployerStatusStatements] ADD [EmployerContractId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

CREATE TABLE [engineer].[EmployerContractHeads] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Code] nvarchar(250) NULL,
    [VolumeTolerance] decimal(18,2) NULL,
    [PriceTolerance] decimal(18,2) NULL,
    [EmployerId] bigint NOT NULL,
    [CurrencyId] bigint NOT NULL,
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    [Type] int NOT NULL,
    [CompanyId] bigint NOT NULL,
    [ProjectId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerContractHeads] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerContractHeads_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'Id';
SET @description = N'کد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'Code';
SET @description = N'تلرانس حجمی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'VolumeTolerance';
SET @description = N'تلرانس قیمتی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'PriceTolerance';
SET @description = N'شناسه کارفرما';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'EmployerId';
SET @description = N'شناسه ارز';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'CurrencyId';
SET @description = N'تاریخ شروع';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'StartDate';
SET @description = N'تاریخ پایان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'EndDate';
SET @description = N'نوع قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'Type';
SET @description = N'شناسه کمپانی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'CompanyId';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContractHeads', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[EmployerContracts] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [IsFirst] bit NOT NULL,
    [Status] int NOT NULL,
    [Code] nvarchar(250) NULL,
    [CurrencyRate] decimal(18,2) NULL,
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [AdvancePayment] decimal(18,2) NOT NULL,
    [Description] nvarchar(max) NULL,
    [EmployerContractHeadId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EmployerContracts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerContracts_EmployerContractHeads_EmployerContractHeadId] FOREIGN KEY ([EmployerContractHeadId]) REFERENCES [engineer].[EmployerContractHeads] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'Id';
SET @description = N'اولین قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'IsFirst';
SET @description = N'وضعیت قرارداد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'Status';
SET @description = N'کد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'Code';
SET @description = N'تاریخ شروع';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'StartDate';
SET @description = N'تاریخ پایان';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'EndDate';
SET @description = N'مجموع مبالغ';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'TotalAmount';
SET @description = N'پیش پرداخت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'AdvancePayment';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerContracts', 'COLUMN', N'IsActive';
GO

CREATE TABLE [engineer].[EmployerConsiderations] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Type] int NOT NULL,
    [Description] nvarchar(1500) NOT NULL,
    [EmployerContractId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EmployerConsiderations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerConsiderations_EmployerContracts_EmployerContractId] FOREIGN KEY ([EmployerContractId]) REFERENCES [engineer].[EmployerContracts] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderations', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderations', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderations', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderations', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderations', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderations', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderations', 'COLUMN', N'IsActive';
GO

CREATE TABLE [engineer].[EmployerCostOvers] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Percent] decimal(5,2) NOT NULL,
    [EmployerContractId] bigint NOT NULL,
    [CostOverId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerCostOvers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerCostOvers_CostOvers_CostOverId] FOREIGN KEY ([CostOverId]) REFERENCES [engineer].[CostOvers] ([Id]),
    CONSTRAINT [FK_EmployerCostOvers_EmployerContracts_EmployerContractId] FOREIGN KEY ([EmployerContractId]) REFERENCES [engineer].[EmployerContracts] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOvers', 'COLUMN', N'Id';
SET @description = N'درصد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOvers', 'COLUMN', N'Percent';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOvers', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOvers', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOvers', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOvers', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOvers', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[EmployerDocs] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Type] int NOT NULL,
    [RegistrationDate] datetime2 NOT NULL,
    [Version] decimal(18,0) NULL,
    [Description] nvarchar(1500) NOT NULL,
    [EmployerContractId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EmployerDocs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerDocs_EmployerContracts_EmployerContractId] FOREIGN KEY ([EmployerContractId]) REFERENCES [engineer].[EmployerContracts] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'Id';
SET @description = N'نوع سند';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'Type';
SET @description = N'تاریخ ثبت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'RegistrationDate';
SET @description = N'ورژن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'Version';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'IsDeleted';
SET @description = N'وضعیت فعال یا غیر فعال بودن';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocs', 'COLUMN', N'IsActive';
GO

CREATE TABLE [engineer].[EmployerOperations] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [UnitPrice] decimal(18,2) NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [Description] nvarchar(1500) NULL,
    [EmployerContractId] bigint NOT NULL,
    [ProjectOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerOperations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerOperations_EmployerContracts_EmployerContractId] FOREIGN KEY ([EmployerContractId]) REFERENCES [engineer].[EmployerContracts] ([Id]),
    CONSTRAINT [FK_EmployerOperations_ProjectOperations_ProjectOperationId] FOREIGN KEY ([ProjectOperationId]) REFERENCES [engineer].[ProjectOperations] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'Id';
SET @description = N'قیمت واحد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'UnitPrice';
SET @description = N'قیمت کل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'TotalPrice';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperations', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[EmployerCostOverImpacts] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Percent] decimal(5,2) NOT NULL,
    [ParentCostOverId] bigint NOT NULL,
    [ChildCostOverId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerCostOverImpacts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerCostOverImpacts_EmployerCostOvers_ChildCostOverId] FOREIGN KEY ([ChildCostOverId]) REFERENCES [engineer].[EmployerCostOvers] ([Id]),
    CONSTRAINT [FK_EmployerCostOverImpacts_EmployerCostOvers_ParentCostOverId] FOREIGN KEY ([ParentCostOverId]) REFERENCES [engineer].[EmployerCostOvers] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOverImpacts', 'COLUMN', N'Id';
SET @description = N'درصد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOverImpacts', 'COLUMN', N'Percent';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOverImpacts', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOverImpacts', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOverImpacts', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOverImpacts', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerCostOverImpacts', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[EmployerDocUrls] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [URL] nvarchar(1500) NOT NULL,
    [EmployerDocId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EmployerDocUrls] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerDocUrls_EmployerDocs_EmployerDocId] FOREIGN KEY ([EmployerDocId]) REFERENCES [engineer].[EmployerDocs] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocUrls', 'COLUMN', N'Id';
SET @description = N'شناسه مسیر فایل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocUrls', 'COLUMN', N'URL';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocUrls', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocUrls', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocUrls', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocUrls', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerDocUrls', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[EmployerConsiderationDeps] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [EmployerConsiderationId] bigint NOT NULL,
    [EmployerOperationId] bigint NOT NULL,
    [EmployerConsiderationDepId] bigint NULL,
    [ProjectOperationId] bigint NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerConsiderationDeps] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerConsiderationDeps_EmployerConsiderationDeps_EmployerConsiderationDepId] FOREIGN KEY ([EmployerConsiderationDepId]) REFERENCES [engineer].[EmployerConsiderationDeps] ([Id]),
    CONSTRAINT [FK_EmployerConsiderationDeps_EmployerConsiderations_EmployerConsiderationId] FOREIGN KEY ([EmployerConsiderationId]) REFERENCES [engineer].[EmployerConsiderations] ([Id]),
    CONSTRAINT [FK_EmployerConsiderationDeps_EmployerOperations_EmployerOperationId] FOREIGN KEY ([EmployerOperationId]) REFERENCES [engineer].[EmployerOperations] ([Id]),
    CONSTRAINT [FK_EmployerConsiderationDeps_ProjectOperations_ProjectOperationId] FOREIGN KEY ([ProjectOperationId]) REFERENCES [engineer].[ProjectOperations] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderationDeps', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderationDeps', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderationDeps', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderationDeps', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderationDeps', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerConsiderationDeps', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[EmployerOperationDetails] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [EmployerOperationId] bigint NOT NULL,
    [ProjectOperationDetailId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerOperationDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerOperationDetails_EmployerOperations_EmployerOperationId] FOREIGN KEY ([EmployerOperationId]) REFERENCES [engineer].[EmployerOperations] ([Id]),
    CONSTRAINT [FK_EmployerOperationDetails_ProjectOperationDetails_ProjectOperationDetailId] FOREIGN KEY ([ProjectOperationDetailId]) REFERENCES [engineer].[ProjectOperationDetails] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationDetails', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationDetails', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationDetails', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationDetails', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationDetails', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationDetails', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_EmployerStatusStatements_EmployerContractId] ON [engineer].[EmployerStatusStatements] ([EmployerContractId]);
GO

CREATE INDEX [IX_EmployerConsiderationDeps_EmployerConsiderationDepId] ON [engineer].[EmployerConsiderationDeps] ([EmployerConsiderationDepId]);
GO

CREATE INDEX [IX_EmployerConsiderationDeps_EmployerConsiderationId] ON [engineer].[EmployerConsiderationDeps] ([EmployerConsiderationId]);
GO

CREATE INDEX [IX_EmployerConsiderationDeps_EmployerOperationId] ON [engineer].[EmployerConsiderationDeps] ([EmployerOperationId]);
GO

CREATE INDEX [IX_EmployerConsiderationDeps_ProjectOperationId] ON [engineer].[EmployerConsiderationDeps] ([ProjectOperationId]);
GO

CREATE INDEX [IX_EmployerConsiderations_EmployerContractId] ON [engineer].[EmployerConsiderations] ([EmployerContractId]);
GO

CREATE INDEX [IX_EmployerContractHeads_ProjectId] ON [engineer].[EmployerContractHeads] ([ProjectId]);
GO

CREATE INDEX [IX_EmployerContracts_EmployerContractHeadId] ON [engineer].[EmployerContracts] ([EmployerContractHeadId]);
GO

CREATE INDEX [IX_EmployerCostOverImpacts_ChildCostOverId] ON [engineer].[EmployerCostOverImpacts] ([ChildCostOverId]);
GO

CREATE INDEX [IX_EmployerCostOverImpacts_ParentCostOverId] ON [engineer].[EmployerCostOverImpacts] ([ParentCostOverId]);
GO

CREATE INDEX [IX_EmployerCostOvers_CostOverId] ON [engineer].[EmployerCostOvers] ([CostOverId]);
GO

CREATE INDEX [IX_EmployerCostOvers_EmployerContractId] ON [engineer].[EmployerCostOvers] ([EmployerContractId]);
GO

CREATE INDEX [IX_EmployerDocs_EmployerContractId] ON [engineer].[EmployerDocs] ([EmployerContractId]);
GO

CREATE INDEX [IX_EmployerDocUrls_EmployerDocId] ON [engineer].[EmployerDocUrls] ([EmployerDocId]);
GO

CREATE INDEX [IX_EmployerOperationDetails_EmployerOperationId] ON [engineer].[EmployerOperationDetails] ([EmployerOperationId]);
GO

CREATE INDEX [IX_EmployerOperationDetails_ProjectOperationDetailId] ON [engineer].[EmployerOperationDetails] ([ProjectOperationDetailId]);
GO

CREATE INDEX [IX_EmployerOperations_EmployerContractId] ON [engineer].[EmployerOperations] ([EmployerContractId]);
GO

CREATE INDEX [IX_EmployerOperations_ProjectOperationId] ON [engineer].[EmployerOperations] ([ProjectOperationId]);
GO

ALTER TABLE [engineer].[EmployerStatusStatements] ADD CONSTRAINT [FK_EmployerStatusStatements_EmployerContracts_EmployerContractId] FOREIGN KEY ([EmployerContractId]) REFERENCES [engineer].[EmployerContracts] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251025101933_AddNewTableOfEContract', N'8.0.8');
GO

COMMIT;
GO



