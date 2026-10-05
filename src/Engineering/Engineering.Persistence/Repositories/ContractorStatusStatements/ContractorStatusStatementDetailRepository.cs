using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementDetailRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementDetail>, IContractorStatusStatementDetailRepository
{
    public ContractorStatusStatementDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetModeledContractorStatusStatementByIdDetail> Data, int RowCount)> GetsContractorStatusStatementDetail(
        long id,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(oo =>
                oo.ContractorStatusStatement.Id == id)

            .Select(item => new GetModeledContractorStatusStatementByIdDetail()
            {
                Id = item.Id,
                ContractorStatusStatementId = item.ContractorStatusStatement.Id,
                ContractorContractId = item.ContractorContract.Id,
                ContractorContractHeaderId = item.ContractorContract.ContractorContractHeader.Id,
                ContractorContractHeaderDescription = item.ContractorContract.ContractorContractHeader.Description,
                ContractorContractType = item.ContractorContract.ContractorContractType.GetEnumDescription(),
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                TotalAmount = item.TotalAmount,
                PercentageDoingJobWell = item.PercentageDoingJobWell,
                DoingJobWellAmount = item.DoingJobWellAmount,
                PercentageAdvancePayment = item.PercentageAdvancePayment,
                AdvancePaymentAmount = item.AdvancePaymentAmount,
                DailyLatenessPenalty = item.DailyLatenessPenalty,
                WorkDonePercent = item.WorkDonePercent,
                WorkDeliveryPercent = item.WorkDeliveryPercent,
                WorkCompletionPercent = item.WorkCompletionPercent,
                Description = item.Description,
                Services = item.ContractorStatusStatementServices.Select(service => new GetModeledContractorStatusStatementByIdByIdService()
                {
                    Id = service.Id,
                    ThirdPartiesAmount = service.ThirdPartiesAmount,
                    ManagementApprovalAmount = service.ManagementApprovalAmount,
                    ProjectManagerApprovalAmount = service.ProjectManagerApprovalAmount,
                    ProjectOperationId = service.DailyProjectOperation!.ProjectOperationDetail.ProjectOperation.Id,
                    OperationInfoName = service.ContractorContractDetail!.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.OperationInfoName,
                    OperationInfoCode = service.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.OperationInfoCode,
                    ProjectOperationUnitOfMeasurementId = service.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.UnitOfMeasurementId,
                    ContractorContractDetailId = service.ContractorContractDetail.Id,
                    DailyProjectOperationId = service.DailyProjectOperation.Id,
                    ServiceInfoId = service.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                    ServiceInfoName = service.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                    ServiceInfoCode = service.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                    UnitOfMeasurementId = service.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                    DailyServices = service.ContractorStatusStatementServiceDailies.Select(daily => new GetModeledContractorStatusStatementByIdServiceDailies()
                    {
                        Id = daily.Id,
                        DailyProjectOperationServiceId = daily.DailyProjectOperationService.Id,
                        DailyDate = daily.DailyProjectOperationService.DailyProjectOperation.StartDate,
                        TimeSpantLong = daily.TimeSpant,
                        Volume = daily.Volume,
                        Price = daily.UnitPrice,
                        AcceptablePercentage = daily.AcceptablePercentage,
                        AcceptableAmount = daily.AcceptableAmount,
                        AcceptableDescription = daily.AcceptableDescription,
                        ProjectManagementApprovalPercentage = daily.ProjectManagementApprovalPercentage,
                        ProjectManagementApprovedPrice = daily.ProjectManagementApprovedPrice,
                        ProjectManagementApprovedDescription = daily.ProjectManagementApprovedDescription,
                        ManagementApprovalPercentage = daily.ManagementApprovalPercentage,
                        ApprovedPrice = daily.ApprovedPrice,
                        ApprovedDescription = daily.ApprovedDescription,
                    }).ToList(),
                    ThirdParties = service.ContractorStatusStatementServiceThirdParties.Select(thirdParty => new GetModeledContractorStatusStatementByIdByIdServiceThirdParties()
                    {
                        Id = thirdParty.Id,
                        ContractorStatusStatementServiceId = thirdParty.ContractorStatusStatementService.Id,
                        SkillId = thirdParty.SkillId,
                        Type = thirdParty.Type,
                        WorkingDay = thirdParty.WorkingDay,
                        Price = thirdParty.Price,
                    }).ToList(),
                }).ToList(),

            });


        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<List<GetCStatementSContractsModel>> GetCStatementSContracts(
        long id,
        CT ct)
    {
        var query = DbSet

            .Where(oo =>
                oo.ContractorStatusStatement.Id == id &&
                oo.ContractorContract.ContractorContractType.Equals(ContractorContractType.Service))

            .Select(item => new GetCStatementSContractsModel()
            {
                Id = item.Id,
                ContractId = item.ContractorContract.Id,
                ProjectId = item.ContractorContract.Project!.Id,
                ProjectName = item.ContractorContract.Project.ProjectName,
                ContractorId = item.ContractorStatusStatement.ContractorId!.Value,
                ContractorContractHeaderId = item.ContractorContract.ContractorContractHeader.Id,
                ContractorContractHeaderDescription = item.ContractorContract.ContractorContractHeader.Description,
                ContractorContractType = item.ContractorContract.ContractorContractType.GetEnumDescription(),
                StartDateMiladi = item.StartDate,
                EndDateMiladi = item.EndDate,
                TotalAmount = item.TotalAmount,
                PercentageDoingJobWell = item.PercentageDoingJobWell,
                DoingJobWellAmount = item.DoingJobWellAmount,
                PercentageAdvancePayment = item.PercentageAdvancePayment,
                AdvancePaymentAmount = item.AdvancePaymentAmount,
                DailyLatenessPenalty = item.DailyLatenessPenalty,
                Description = item.Description,
                DailyServices = item.ContractorStatusStatementServices.SelectMany(x => x.ContractorStatusStatementServiceDailies
                    .Select(service => new GetCStatementSContractsDailiesModel()
                    {
                        Id = service.Id,
                        DailyServiceId = service.DailyProjectOperationService.Id,
                        DailyId = service.DailyProjectOperationService.DailyProjectOperation.Id,
                        Volume = service.Volume,
                        ContractorContractHeaderId = service.ContractorStatusStatementService.ContractorStatusStatementDetail.ContractorContract.ContractorContractHeaderId,
                        UnitPrice = service.UnitPrice,
                        TotalPrice = service.TotalPrice,
                        AcceptablePercentage = service.AcceptablePercentage,
                        AcceptableAmount = service.AcceptableAmount,
                        AcceptableDescription = service.AcceptableDescription,
                        ProjectManagementApprovalPercentage = service.ProjectManagementApprovalPercentage,
                        ProjectManagementApprovedPrice = service.ProjectManagementApprovedPrice,
                        ProjectManagementApprovedDescription = service.ProjectManagementApprovedDescription,
                        ManagementApprovalPercentage = service.ManagementApprovalPercentage,
                        ApprovedPrice = service.ApprovedPrice,
                        ApprovedDescription = service.ApprovedDescription,
                        Urls = service.DailyProjectOperationService.DailyProjectOperation.DailyProjectOperationDocuments.Select(d => d.Url).ToList(),
                        Created = service.DailyProjectOperationService.Created,
                        CreatorId = service.DailyProjectOperationService.CreatorId,

                        ProjectOperationId = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Id,
                        ProjectOperationDetailId = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.Id,
                        Workload = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Workload,
                        OperationInfoName = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                        OperationInfoCode = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                        ProjectOperationMeasureId = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,

                        ProjectOperationDetailContractorServiceId = service.DailyProjectOperationService.ProjectOperationDetailContractorService.Id,
                        ServiceVolume = service.DailyProjectOperationService.ProjectOperationDetailContractorService.Volume,
                        ServiceInfoId = service.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                        ServiceInfoName = service.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                        ServiceInfoCode = service.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                        ServiceInfoMeasureId = service.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,

                        Length = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Length,
                        Width = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Width,
                        Height = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Height,
                        Weight = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Weight,
                        Number = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Number,
                        PublicCode = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicCode,
                        PublicName = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicName,
                        Description = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Description,
                    })).ToList()
            });

        var entities = await query.ToListAsync(ct);
        return entities;
    }

    public async Task<List<GetCStatementFContractsModel>> GetCStatementFContracts(
        long id,
        CT ct)
    {
        var query = DbSet

            .Where(oo =>
                oo.ContractorStatusStatement.Id == id &&
                oo.ContractorContract.ContractorContractType.Equals(ContractorContractType.Fixed))

            .Select(item => new GetCStatementFContractsModel()
            {
                Id = item.Id,
                ContractId = item.ContractorContract.Id,
                ProjectId = item.ContractorContract.Project!.Id,
                ProjectName = item.ContractorContract.Project.ProjectName,
                ContractorId = item.ContractorStatusStatement.ContractorId!.Value,
                ContractorContractHeaderId = item.ContractorContract.ContractorContractHeader.Id,
                ContractorContractHeaderDescription = item.ContractorContract.ContractorContractHeader.Description,
                ContractorContractType = item.ContractorContract.ContractorContractType.GetEnumDescription(),
                StartDateMiladi = item.StartDate,
                EndDateMiladi = item.EndDate,
                TotalAmount = item.TotalAmount,
                FixedContractPct = item.FixedContractPct,
                FixedContractPctAmount = item.FixedContractPctAmount,
                FixedContractPctDesc = item.FixedContractPctDesc,
                ProjectFixedContractPct = item.ProjectFixedContractPct,
                ProjectFixedContractPctAmount = item.ProjectFixedContractPctAmount,
                ProjectFixedContractPctDesc = item.ProjectFixedContractPctDesc,
                ManagerFixedContractPct = item.ManagerFixedContractPct,
                ManagerFixedContractPctAmount = item.ManagerFixedContractPctAmount,
                ManagerFixedContractPctDesc = item.ManagerFixedContractPctDesc,
                PercentageDoingJobWell = item.PercentageDoingJobWell,
                DoingJobWellAmount = item.DoingJobWellAmount,
                PercentageAdvancePayment = item.PercentageAdvancePayment,
                AdvancePaymentAmount = item.AdvancePaymentAmount,
                DailyLatenessPenalty = item.DailyLatenessPenalty,
                Description = item.Description,
                DailyServices = item.ContractorStatusStatementServices.SelectMany(x => x.ContractorStatusStatementServiceDailies
                    .Select(service => new GetCStatementFContractsDailiesModel()
                    {
                        Id = service.Id,
                        DailyServiceId = service.DailyProjectOperationService.Id,
                        DailyId = service.DailyProjectOperationService.DailyProjectOperation.Id,
                        ContractorContractHeaderId = service.ContractorStatusStatementService.ContractorStatusStatementDetail.ContractorContract.ContractorContractHeaderId,
                        Volume = service.Volume,
                        Urls = service.DailyProjectOperationService.DailyProjectOperation.DailyProjectOperationDocuments.Select(d => d.Url).ToList(),
                        Created = service.DailyProjectOperationService.Created,
                        CreatorId = service.DailyProjectOperationService.CreatorId,

                        ProjectOperationId = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Id,
                        ProjectOperationDetailId = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.Id,
                        Workload = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Workload,
                        OperationInfoName = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                        OperationInfoCode = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                        ProjectOperationMeasureId = service.DailyProjectOperationService.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,

                        ProjectOperationDetailContractorServiceId = service.DailyProjectOperationService.ProjectOperationDetailContractorService.Id,
                        ServiceVolume = service.DailyProjectOperationService.ProjectOperationDetailContractorService.Volume,
                        ServiceInfoId = service.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                        ServiceInfoName = service.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                        ServiceInfoCode = service.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                        ServiceInfoMeasureId = service.DailyProjectOperationService.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,

                        Length = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Length,
                        Width = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Width,
                        Height = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Height,
                        Weight = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Weight,
                        Number = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Number,
                        PublicCode = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicCode,
                        PublicName = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicName,
                        Description = service.DailyProjectOperationService.ProjectOperationDetailContractorService.ProjectOperationDetail.Description,
                    })).ToList()
            });

        var entities = await query.ToListAsync(ct);
        return entities;
    }

}
