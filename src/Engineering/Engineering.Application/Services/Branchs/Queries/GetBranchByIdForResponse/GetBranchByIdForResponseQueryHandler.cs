using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Services.Branchs.Models.GetBranchById;
using Engineering.Application.Services.Branchs.Queries.GetBranchByIdForResponse;

public class GetBranchByIdForResponseQueryHandler : IQueryHandler<GetBranchByIdForResponseQuery, GetBranchByIdResponse?>
{
    private readonly ILogger<GetBranchByIdForResponseQueryHandler> _logger;
    private readonly IBranchRepository _repository;

    public GetBranchByIdForResponseQueryHandler(
        ILogger<GetBranchByIdForResponseQueryHandler> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetBranchByIdResponse?>> Handle(GetBranchByIdForResponseQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetBranchByIdWithCategory(request.Id, ct);

            if (item is null)
            {
                return Result.Failure<GetBranchByIdResponse?>(BranchErrors.BranchWithIdNotFound);
            }

            var response = new GetBranchByIdResponse
            {
                Id = item.Id,
                BranchName = item.BranchName,
                BranchCode = item.BranchCode,
                AlternativeId = $"Branch{item.Id}",
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                CategoryCode = item.Category.CategoryCode,
                IsActive = item.IsActive,
                ChildCount = item.Seasons.Count(e => !e.IsDeleted),
                HaveChild = item.Seasons.Any(e => !e.IsDeleted),
                PreferentialReferenceCode = item.PreferentialReferenceCode,
                CompanyId = item.CompanyId,
                Category = new Engineering.Application.Services.Branchs.Models.BranchModels.BranchCategoryModel
                {
                    Id = item.CategoryId,
                    CategoryName = item.Category.CategoryName,
                    CategoryCode = item.Category.CategoryCode,
                    IsActive = item.Category.IsActive
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetBranchByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}