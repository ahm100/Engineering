BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplyDetails] ADD [CostCenterId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [DeliveryDeadline] datetime2 NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [DescriptionEn] nvarchar(1500) NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [RegistrationNumber] nvarchar(250) NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [RequestingOrganizationId] bigint NULL;
GO

CREATE TABLE [engineer].[RequestGoodsSupplyDocuments] (
    [Id] bigint NOT NULL IDENTITY(2, 1),
    [Url] nvarchar(1500) NOT NULL,
    [RequestGoodsSupplyId] bigint NOT NULL,
    [RowVersion] rowversion NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatorId] bigint NOT NULL,
    [Updated] datetime2 NULL,
    [UpdaterId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RequestGoodsSupplyDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RequestGoodsSupplyDocuments_RequestGoodsSupplies_RequestGoodsSupplyId] FOREIGN KEY ([RequestGoodsSupplyId]) REFERENCES [engineer].[RequestGoodsSupplies] ([Id])
);
DECLARE @description AS sql_variant;
SET @description = N'شناسه';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyDocuments', 'COLUMN', N'Id';
SET @description = N'تاریخ ایجاد';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyDocuments', 'COLUMN', N'Created';
SET @description = N'ایجاد کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyDocuments', 'COLUMN', N'CreatorId';
SET @description = N'تاریخ ویرایش';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyDocuments', 'COLUMN', N'Updated';
SET @description = N'ویرایش کننده';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyDocuments', 'COLUMN', N'UpdaterId';
SET @description = N'حذف شدگی';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', N'engineer', 'TABLE', N'RequestGoodsSupplyDocuments', 'COLUMN', N'IsDeleted';
GO

CREATE INDEX [IX_RequestGoodsSupplyDetails_CostCenterId] ON [engineer].[RequestGoodsSupplyDetails] ([CostCenterId]);
GO

CREATE INDEX [IX_RequestGoodsSupplyDocuments_RequestGoodsSupplyId] ON [engineer].[RequestGoodsSupplyDocuments] ([RequestGoodsSupplyId]);
GO

ALTER TABLE [engineer].[RequestGoodsSupplyDetails] ADD CONSTRAINT [FK_RequestGoodsSupplyDetails_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [engineer].[CostCenters] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260720073041_AddNewParamToRGS', N'8.0.8');
GO

COMMIT;
GO



