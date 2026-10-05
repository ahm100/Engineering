using Warehouse.ClientSdks.Exceptions;

namespace Engineering.Application.Extensions;

public static class ResultHelpers
{
    public static async Task<Result<T>> GetWarehouseResult<T>(this Task<T> task)
    {
        try
        {
            var result = await task;
            return Result.Success(result);
        }
        catch (WarehouseServiceException ex)
        {
            return Result.Failure<T>(ex.Error ?? SharedErrors.UnknownError)!;
        }
    }
}

