
namespace Engineering.Application.Extensions;

public class CreateTransaction : IDisposable
{

    private readonly TransactionScope _transactionScope;
    public CreateTransaction()
    {
        var transactionOptions = new TransactionOptions();
        transactionOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
        _transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
    }

    public void Complete()
    {
        _transactionScope.Complete();
    }

    public void Dispose()
    {
        _transactionScope.Dispose();
    }
}
