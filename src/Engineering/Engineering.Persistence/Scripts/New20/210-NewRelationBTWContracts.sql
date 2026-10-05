BEGIN TRANSACTION;
GO

ALTER TABLE [engineer].[ContractorContracts] ADD [ProjectId] bigint NULL;
GO

ALTER TABLE [engineer].[ContractorContractHeaders] ADD [CostCenterId] bigint NULL;
GO

CREATE INDEX [IX_ContractorContracts_ProjectId] ON [engineer].[ContractorContracts] ([ProjectId]);
GO

CREATE INDEX [IX_ContractorContractHeaders_CostCenterId] ON [engineer].[ContractorContractHeaders] ([CostCenterId]);
GO

ALTER TABLE [engineer].[ContractorContractHeaders] ADD CONSTRAINT [FK_ContractorContractHeaders_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [engineer].[CostCenters] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [engineer].[ContractorContracts] ADD CONSTRAINT [FK_ContractorContracts_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [engineer].[Projects] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250926070855_NewRelationBTWContracts', N'8.0.8');
GO

COMMIT;
GO

--select ContractorContractHeader CostCenter and Update
SELECT 
    h.Id AS ContractorContractHeaderId,
    MIN(cst.Id) AS CostCenterId
FROM engineer.ContractorContractHeaders h
INNER JOIN engineer.ContractorContracts c ON c.ContractorContractHeaderId = h.Id
INNER JOIN engineer.ContractorContractDetails d ON d.ContractorContractId = c.Id
INNER JOIN engineer.ContractorContractDetailServices ds ON ds.ContractorContractDetailId = d.Id
INNER JOIN engineer.ProjectOperationDetailContractorServices pods ON pods.Id = ds.ProjectOperationDetailContractorServiceId
INNER JOIN engineer.ProjectOperationDetails pod ON pod.Id = pods.ProjectOperationDetailId
INNER JOIN engineer.ProjectOperations po ON po.Id = pod.ProjectOperationId
INNER JOIN engineer.Projects p ON p.Id = po.ProjectId
INNER JOIN engineer.CostCenters cst ON cst.Id = p.CostCenterId
GROUP BY h.Id
ORDER BY h.Id;

UPDATE h
SET h.CostCenterId = q.CostCenterId
FROM engineer.ContractorContractHeaders h
INNER JOIN (
    SELECT 
        h.Id AS ContractorContractHeaderId,
        MIN(cst.Id) AS CostCenterId
    FROM engineer.ContractorContractHeaders h
    INNER JOIN engineer.ContractorContracts c ON c.ContractorContractHeaderId = h.Id
    INNER JOIN engineer.ContractorContractDetails d ON d.ContractorContractId = c.Id
    INNER JOIN engineer.ContractorContractDetailServices ds ON ds.ContractorContractDetailId = d.Id
    INNER JOIN engineer.ProjectOperationDetailContractorServices pods ON pods.Id = ds.ProjectOperationDetailContractorServiceId
    INNER JOIN engineer.ProjectOperationDetails pod ON pod.Id = pods.ProjectOperationDetailId
    INNER JOIN engineer.ProjectOperations po ON po.Id = pod.ProjectOperationId
    INNER JOIN engineer.Projects p ON p.Id = po.ProjectId
    INNER JOIN engineer.CostCenters cst ON cst.Id = p.CostCenterId
    GROUP BY h.Id
) q ON q.ContractorContractHeaderId = h.Id
WHERE h.CostCenterId IS NULL;
--End

--select ContractorContract Project and Update
SELECT 
    c.Id AS ContractorContractId,
    MIN(p.Id) AS ProjectId
FROM engineer.ContractorContracts c
INNER JOIN engineer.ContractorContractDetails d ON d.ContractorContractId = c.Id
INNER JOIN engineer.ContractorContractDetailServices ds ON ds.ContractorContractDetailId = d.Id
INNER JOIN engineer.ProjectOperationDetailContractorServices pods ON pods.Id = ds.ProjectOperationDetailContractorServiceId
INNER JOIN engineer.ProjectOperationDetails pod ON pod.Id = pods.ProjectOperationDetailId
INNER JOIN engineer.ProjectOperations po ON po.Id = pod.ProjectOperationId
INNER JOIN engineer.Projects p ON p.Id = po.ProjectId
GROUP BY c.Id
ORDER BY c.Id;

UPDATE c
SET c.ProjectId = q.ProjectId
FROM engineer.ContractorContracts c
INNER JOIN (
    SELECT 
        c.Id AS ContractorContractId,
        MIN(p.Id) AS ProjectId
    FROM engineer.ContractorContracts c
    INNER JOIN engineer.ContractorContractDetails d ON d.ContractorContractId = c.Id
    INNER JOIN engineer.ContractorContractDetailServices ds ON ds.ContractorContractDetailId = d.Id
    INNER JOIN engineer.ProjectOperationDetailContractorServices pods ON pods.Id = ds.ProjectOperationDetailContractorServiceId
    INNER JOIN engineer.ProjectOperationDetails pod ON pod.Id = pods.ProjectOperationDetailId
    INNER JOIN engineer.ProjectOperations po ON po.Id = pod.ProjectOperationId
    INNER JOIN engineer.Projects p ON p.Id = po.ProjectId
    GROUP BY c.Id
) q ON q.ContractorContractId = c.Id
WHERE c.ProjectId IS NULL;

--End