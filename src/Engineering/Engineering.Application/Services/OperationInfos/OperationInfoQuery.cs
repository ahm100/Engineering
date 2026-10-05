using Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;
using Engineering.Domain.Errors.Actions;

namespace Engineering.Application.Services.OperationInfos;

public partial class OperationInfoLogic : IOperationInfoLogic
{
    private async Task<Result<GetOIActionByOperationInfoIdResponse>> GetOIActionByOperationInfoIdQuery(
       GetOIActionByOperationInfoIdRequest request,
       CT ct)
    {
        try
        {
            var result = await _operationInfoActionRepository.GetOIActionByOperationInfoId(request.Id, ct);
            if (result == null)
                return Result.Failure<GetOIActionByOperationInfoIdResponse>(ActionErrors.ActionWithIdNotFound)!;

            var response = new GetOIActionByOperationInfoIdResponse(result, result.Count);
            return response;
        }
        catch (Exception)
        {

            throw;
        }

    }
}