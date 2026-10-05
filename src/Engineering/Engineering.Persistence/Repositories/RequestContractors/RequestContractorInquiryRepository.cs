using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorInquiryById;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Persistence.Repositories.RequestContractors;

public class RequestContractorInquiryRepository : BaseRepository<EngineeringDBContext, RequestContractorInquiry>, IRequestContractorInquiryRepository
{
    public RequestContractorInquiryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestContractorInquiry?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.RequestContractor)
                .ThenInclude(z => z.ProjectOperationDetail.OperationLocation)
            .Include(x => x.RequestContractor)
                .ThenInclude(z => z.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
                    .ThenInclude(z => z.CostCenter)
            .Include(x => x.RequestContractor)
                .ThenInclude(z => z.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(x => x.RequestContractorInquiryDocuments)
                .Where(x => x.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<GetRequestContractorInquiryByIdResponse?> GetInquiryModelById(long id, CT ct)
    {
        var query = DbSet
                .Where(x => x.Id == id)
                .Select(x => new GetRequestContractorInquiryByIdResponse()
                {
                    Amount = x.Amount,
                    Id = x.Id,
                    ConfirmUser = x.ConfirmedUser,
                    ContractorId = x.ContractorId,
                    CostCenterId = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any() ?
                    x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId :
                    null,
                    CostCenterName = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any() ?
                    x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName :
                    null,
                    Created = x.RequestContractor.Created,
                    CreatorId = x.RequestContractor.CreatorId,
                    CurrencyId = x.CurrencyId,
                    Description = x.RequestContractor.Description,
                    DescriptionStatus = x.RequestContractor.StatusDescription,
                    Discount = x.Discount,
                    FromDate = x.FromDate,
                    InquiryCreated = x.Created,
                    InquiryCreatorId = x.CreatorId,
                    InquiryDescription = x.Description,
                    IsConfirmed = x.IsConfirmed,
                    OperationInfoCode = x.RequestContractor.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                    OperationInfoName = x.RequestContractor.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                    ProjectId = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.Id,
                    ProjectName = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                    ProjectOperationDetailId = x.RequestContractor.ProjectOperationDetail.Id,
                    ProjectOperationId = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Id,
                    PublicCode = x.RequestContractor.ProjectOperationDetail.OperationLocation.PublicCode,
                    PublicName = x.RequestContractor.ProjectOperationDetail.OperationLocation.PublicName,
                    Volume = x.RequestContractor.Volume,
                    Type = x.Type,
                    TotalAmount = x.TotalAmount,
                    ToDate = x.ToDate,
                    RequestContractorId = x.RequestContractor.Id,
                    RequestNumber = x.RequestContractor.RequestNumber,
                    Tax = x.Tax,
                    Status = x.RequestContractor.Status,
                    ServiceInfoId = x.RequestContractor.ServiceInfo.Id,
                    ServiceInfoName = x.RequestContractor.ServiceInfo.ServiceInfoName,
                    ServiceInfoCode = x.RequestContractor.ServiceInfo.ServiceInfoCode,
                    InquiryDocuments = x.RequestContractorInquiryDocuments.Select(z => new InquiryDocumentResponseByIdModel()
                    {
                        Id = z.Id,
                        Url = z.Url
                    }).ToList(),
                });

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetRequestContractorInquiryByRequestIdModel> Data, int RowCount)> GetsInquiryByRequestContractorId(
        long id,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x => x.RequestContractor.Id == id &&
                        (companyId == null || x.RequestContractor.CompanyId == companyId))
            .Select(x => new GetRequestContractorInquiryByRequestIdModel()
            {
                Amount = x.Amount,
                Id = x.Id,
                ConfirmUser = x.ConfirmedUser,
                ContractorId = x.ContractorId,
                CostCenterId = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any() ?
                    x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId :
                    null,
                CostCenterName = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any() ?
                    x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName :
                    null,
                Created = x.RequestContractor.Created,
                CreatorId = x.RequestContractor.CreatorId,
                CurrencyId = x.CurrencyId,
                Description = x.RequestContractor.Description,
                DescriptionStatus = x.RequestContractor.StatusDescription,
                Discount = x.Discount,
                FromDate = x.FromDate,
                InquiryCreated = x.Created,
                InquiryCreatorId = x.CreatorId,
                InquiryDescription = x.Description,
                IsConfirmed = x.IsConfirmed,
                OperationInfoCode = x.RequestContractor.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                OperationInfoName = x.RequestContractor.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectId = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.Id,
                ProjectName = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                ProjectOperationDetailId = x.RequestContractor.ProjectOperationDetail.Id,
                ProjectOperationId = x.RequestContractor.ProjectOperationDetail.ProjectOperation.Id,
                PublicCode = x.RequestContractor.ProjectOperationDetail.OperationLocation.PublicCode,
                PublicName = x.RequestContractor.ProjectOperationDetail.OperationLocation.PublicName,
                Volume = x.RequestContractor.Volume,
                Type = x.Type,
                TotalAmount = x.TotalAmount,
                ToDate = x.ToDate,
                RequestContractorId = x.RequestContractor.Id,
                RequestNumber = x.RequestContractor.RequestNumber,
                Tax = x.Tax,
                Status = x.RequestContractor.Status,
                ServiceInfoId = x.RequestContractor.ServiceInfo.Id,
                ServiceInfoName = x.RequestContractor.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = x.RequestContractor.ServiceInfo.ServiceInfoCode,
                InquiryDocuments = x.RequestContractorInquiryDocuments.Select(z => new InquiryDocumentResponseByRequestIdModel()
                {
                    Id = z.Id,
                    Url = z.Url
                }).ToList(),
            });

        query = query.OrderByDescending(r => r.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}
