BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EngineeringStandardProducts] ADD [EmployerOperationId] bigint NULL;
GO

CREATE TABLE [engineer].[EmployerOperationProducts] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [ProductGroupId] bigint NOT NULL,
    [MinPrice] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MaxPrice] decimal(18,2) NOT NULL,
    [Tax] decimal(18,2) NOT NULL,
    [TaxPercent] decimal(5,2) NOT NULL,
    [TransportationCost] decimal(18,2) NOT NULL,
    [TransportationCostPercent] decimal(5,2) NOT NULL,
    [IncreaseRate] decimal(5,2) NOT NULL,
    [IsStandard] bit NOT NULL DEFAULT CAST(1 AS bit),
    [Description] nvarchar(1500) NULL,
    [EmployerOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerOperationProducts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerOperationProducts_EmployerOperations_EmployerOperationId] FOREIGN KEY ([EmployerOperationId]) REFERENCES [engineer].[EmployerOperations] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'Id';
SET @description = N'شناسه گروه کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'ProductGroupId';
SET @description = N'حداقل قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'MinPrice';
SET @description = N'حداکثر قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'MaxPrice';
SET @description = N'مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'Tax';
SET @description = N'درصد مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'TaxPercent';
SET @description = N'هزینه حمل و نقل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'TransportationCost';
SET @description = N'درصد هزینه حمل و نقل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'TransportationCostPercent';
SET @description = N'ضریب افزایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'IncreaseRate';
SET @description = N'استاندارد بودن کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'IsStandard';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationProducts', 'COLUMN', N'IsDeleted';
GO

CREATE TABLE [engineer].[EmployerOperationServices] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [MinPrice] decimal(18,2) NOT NULL DEFAULT 0.0,
    [MaxPrice] decimal(18,2) NOT NULL,
    [Tax] decimal(18,2) NOT NULL,
    [TaxPercent] decimal(5,2) NOT NULL,
    [TransportationCost] decimal(18,2) NOT NULL,
    [TransportationCostPercent] decimal(5,2) NOT NULL,
    [IncreaseRate] decimal(5,2) NOT NULL,
    [IsStandard] bit NOT NULL DEFAULT CAST(1 AS bit),
    [Description] nvarchar(1500) NULL,
    [EmployerOperationId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EmployerOperationServices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployerOperationServices_EmployerOperations_EmployerOperationId] FOREIGN KEY ([EmployerOperationId]) REFERENCES [engineer].[EmployerOperations] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'Id';
SET @description = N'حداقل قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'MinPrice';
SET @description = N'حداکثر قیمت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'MaxPrice';
SET @description = N'مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'Tax';
SET @description = N'درصد مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'TaxPercent';
SET @description = N'هزینه حمل و نقل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'TransportationCost';
SET @description = N'درصد هزینه حمل و نقل';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'TransportationCostPercent';
SET @description = N'ضریب افزایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'IncreaseRate';
SET @description = N'استاندارد بودن کالا';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'IsStandard';
SET @description = N'توضیحات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'Description';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'EmployerOperationServices', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_EngineeringStandardProducts_EmployerOperationId] ON [engineer].[EngineeringStandardProducts] ([EmployerOperationId]);
GO

CREATE INDEX [IX_EmployerOperationProducts_EmployerOperationId] ON [engineer].[EmployerOperationProducts] ([EmployerOperationId]);
GO

CREATE INDEX [IX_EmployerOperationServices_EmployerOperationId] ON [engineer].[EmployerOperationServices] ([EmployerOperationId]);
GO

ALTER TABLE [engineer].[EngineeringStandardProducts] ADD CONSTRAINT [FK_EngineeringStandardProducts_EmployerOperations_EmployerOperationId] FOREIGN KEY ([EmployerOperationId]) REFERENCES [engineer].[EmployerOperations] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251110053230_AddEOperationProductAndService', N'8.0.8');
GO

COMMIT;
GO



