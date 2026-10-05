BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD [OperationInfoServiceId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

CREATE INDEX [IX_EmployerOperationServices_OperationInfoServiceId] ON [engineer].[EmployerOperationServices] ([OperationInfoServiceId]);
GO

ALTER TABLE [engineer].[EmployerOperationServices] ADD CONSTRAINT [FK_EmployerOperationServices_OperationInfoServices_OperationInfoServiceId] FOREIGN KEY ([OperationInfoServiceId]) REFERENCES [engineer].[OperationInfoServices] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251130073230_AddOInfoServiceRelationToEOService', N'8.0.8');
GO

COMMIT;
GO



