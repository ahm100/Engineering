using Gita.Backend.Shared.Application.Middlewares.UnhandledExceptions;

namespace Engineering.ClientSdk.Models.Schema;

public interface IEngineeringCanonicalResult
{
    public bool? IsSuccess { get; }

    public bool? IsFailure { get; }

    public ErrorModel? Error { get; }

    public int? PageIndex { get; }

    public int? PageSize { get; }
}

public interface IEngineeringCanonicalResult<out T> : IEngineeringCanonicalResult
{
    public T? Value { get; }
}
