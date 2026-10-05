using Engineering.Application.Services.Projects.Models.GetProjectById;
using Engineering.Application.Services.Projects.Models.GetProjectCategoryProductByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectThirdParties;

namespace Engineering.Application.Services.Projects;

partial class ProjectLogic
{
    private async Task<Result<GetProjectProductByProjectIdResponse?>> GetProjectProductByProjectIdQuery(
       long projectId, CT ct)
    {
        try
        {
            var items = await _ppRepo.GetProjectProductByProjectId(projectId, ct);
            if (items is null)
                return Result.Failure<GetProjectProductByProjectIdResponse>(ProjectErrors.ProjectProductWithIdsNotFound);
            var result = new GetProjectProductByProjectIdResponse(
                items,
                items.Count
            );

            return result ?? Result.Failure<GetProjectProductByProjectIdResponse?>(ProjectErrors.ProjectProductWithIdsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectProductByProjectIdResponse?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetProjectCategoryProductByProjectIdResponse?>> GetProjectCategoryProductByProjectIdQuery(
       long id, CT ct)
    {
        try
        {
            var items = await _ppRepo.GetProjectCategoryProductByProjectId(id, ct);
            if (items is null)
                return Result.Failure<GetProjectCategoryProductByProjectIdResponse>(ProjectErrors.ProjectProductWithIdsNotFound);
            var result = new GetProjectCategoryProductByProjectIdResponse
            (
                items,
                items.Count
            );
            return result ?? Result.Failure<GetProjectCategoryProductByProjectIdResponse?>(ProjectErrors.ProjectProductWithIdsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectCategoryProductByProjectIdResponse?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetFltrProjectThirdPartyResponse?>> GetFltrProjectThirdPartyQuery(
       GetFltrProjectThirdPartyRequest request, CT ct)
    {
        try
        {
            var items = await _projectThirdPartyRepository.GetFltrProjectThirdParty(request.ProjectId,
                request.PageIndex,
                request.PageSize, ct);
            if (items.Data is null || items.RowCount < 1)
                return Result.Failure<GetFltrProjectThirdPartyResponse>(ProjectErrors.ThirdPartiesNotFound);

            var thirdParties = await _thirdPartyRepo.GetByIds(items.Data.Listed(x => x.ThirdPartyId), ct);
            foreach (var item in items.Data)
            {
                var thirdParty = thirdParties.FirstOrDefault(x => x.Id == item.ThirdPartyId);
                item.UserId = thirdParty?.UserId;
                item.FirstName = thirdParty?.FirstName;
                item.LastName = thirdParty?.LastName;
                item.FullName = $"{thirdParty?.FirstName} {thirdParty?.LastName}";
                item.IsIndividual = thirdParty?.IsIndividual;
            }

            var result = new GetFltrProjectThirdPartyResponse(
                items.Data,
                items.RowCount
            );

            return result ?? Result.Failure<GetFltrProjectThirdPartyResponse?>(ProjectErrors.ProjectProductWithIdsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrProjectThirdPartyResponse?>(SharedErrors.UnknownError);
        }
    }
}
