BEGIN TRANSACTION;
GO


ALTER TABLE [engineer].[RequestGoodsSupplyDetails] ALTER COLUMN [ConsumableVolumeProductId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplyDetails] ADD [ProjectProductId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ALTER COLUMN [ProjectOperationId] bigint NULL;
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [IsProjectSupply] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD [ProjectId] bigint NULL;
GO

CREATE INDEX [IX_RequestGoodsSupplyDetails_ProjectProductId] ON [engineer].[RequestGoodsSupplyDetails] ([ProjectProductId]);
GO

CREATE INDEX [IX_RequestGoodsSupplies_ProjectId] ON [engineer].[RequestGoodsSupplies] ([ProjectId]);
GO

ALTER TABLE [engineer].[RequestGoodsSupplies] ADD CONSTRAINT [FK_RequestGoodsSupplies_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [engineer].[RequestGoodsSupplyDetails] ADD CONSTRAINT [FK_RequestGoodsSupplyDetails_ProjectProducts_ProjectProductId] FOREIGN KEY ([ProjectProductId]) REFERENCES [engineer].[ProjectProducts] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251213103239_NewRelationsInSupply', N'8.0.8');
GO

COMMIT;
GO



