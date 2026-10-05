BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[TransportationRequests] ADD [Volume] decimal(18,0) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[TransportationContractors]') AND [c].[name] = N'SecondPrefix');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[TransportationContractors] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [engineer].[TransportationContractors] ALTER COLUMN [SecondPrefix] bigint NULL;
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [ServicePrice] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'هزینه خدمات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'ServicePrice';
GO

ALTER TABLE [engineer].[TransportationContractors] ADD [TaxPercent] decimal(18,2) NULL;
DECLARE @description AS sql_variant;
SET @description = N'درصد مالیات';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractors', 'COLUMN', N'TaxPercent';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ShippingCosts]') AND [c].[name] = N'Tax');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ShippingCosts] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [engineer].[ShippingCosts] ALTER COLUMN [Tax] decimal(18,2) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ShippingCosts]') AND [c].[name] = N'LoadWeight');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ShippingCosts] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [engineer].[ShippingCosts] ALTER COLUMN [LoadWeight] decimal(18,2) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[engineer].[ShippingCosts]') AND [c].[name] = N'Count');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [engineer].[ShippingCosts] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [engineer].[ShippingCosts] ALTER COLUMN [Count] int NULL;
GO

ALTER TABLE [engineer].[ShippingCosts] ADD [Latitude] decimal(18,9) NULL;
DECLARE @description AS sql_variant;
SET @description = N'عرض جغرافیایی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Latitude';
GO

ALTER TABLE [engineer].[ShippingCosts] ADD [Longitude] decimal(18,9) NULL;
DECLARE @description AS sql_variant;
SET @description = N'طول جغرافیایی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'ShippingCosts', 'COLUMN', N'Longitude';
GO

ALTER TABLE [engineer].[ShippingCosts] ADD [ThirdPartyId] bigint NULL;
GO

CREATE TABLE [engineer].[TransportationContractorInsurances] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [TransportationContractorId] bigint NOT NULL,
    [MinProductPrice] decimal(18,2) NOT NULL,
    [MaxProductPrice] decimal(18,2) NOT NULL,
    [FixedPrice] decimal(18,2) NOT NULL,
    [Multiplication] decimal(18,2) NULL DEFAULT 1.0,
    [Division] decimal(18,2) NULL DEFAULT 1.0,
    [Subtraction] decimal(18,2) NULL DEFAULT 0.0,
    [Addition] decimal(18,2) NULL DEFAULT 0.0,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TransportationContractorInsurances] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportationContractorInsurances_TransportationContractors_TransportationContractorId] FOREIGN KEY ([TransportationContractorId]) REFERENCES [engineer].[TransportationContractors] ([Id]) ON DELETE NO ACTION
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'Id';
SET @description = N'کمترین ارزش بار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'MinProductPrice';
SET @description = N'بیشترین ارزش بار';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'MaxProductPrice';
SET @description = N'مقدار ثابت';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'FixedPrice';
SET @description = N'ضرب';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'Multiplication';
SET @description = N'تقسیم';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'Division';
SET @description = N'تفریق';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'Subtraction';
SET @description = N'جمع';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'Addition';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'TransportationContractorInsurances', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_ShippingCosts_ThirdPartyId] ON [engineer].[ShippingCosts] ([ThirdPartyId]);
GO

CREATE INDEX [IX_TransportationContractorInsurances_TransportationContractorId] ON [engineer].[TransportationContractorInsurances] ([TransportationContractorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251228145214_AddInsuranceEntity', N'8.0.8');
GO

COMMIT;
GO



