BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplyDetailManagements] ADD [ReferenceId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplyDetailManagements] ADD [RequestGoodsSupplyTypeId] bigint NULL;
GO

CREATE INDEX [IX_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyTypeId] ON [engineer].[RequestGoodsSupplyDetailManagements] ([RequestGoodsSupplyTypeId]);
GO

ALTER TABLE [engineer].[RequestGoodsSupplyDetailManagements] ADD CONSTRAINT [FK_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId] FOREIGN KEY ([RequestGoodsSupplyTypeId]) REFERENCES [engineer].[RequestGoodsSupplyTypes] ([Id]) ON DELETE NO ACTION;
GO


                UPDATE engineer.RequestGoodsSupplyDetailManagements
                SET ReferenceId = ProductId
                WHERE ProductId IS NOT NULL;
                
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260802051750_AddReference&TypeToManagement', N'8.0.8');
GO

COMMIT;
GO



