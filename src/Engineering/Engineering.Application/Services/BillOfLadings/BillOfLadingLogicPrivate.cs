
namespace Engineering.Application.Services.BillOfLadings;

public partial class BillOfLadingLogic : IBillOfLadingLogic
{

    private async Task<bool> IsDuplicateName(string name, long? companyId, CT ct)
    {
        var nameIsDuplicate = await GetBillOfLadingByNameHandle(name, companyId, ct);
        return nameIsDuplicate is { IsSuccess: true, Value: not null };
    }

    private async Task<bool> IsDuplicateCode(string code, long? companyId, CT ct)
    {
        var codeIsDuplicate = await GetBillOfLadingByCodeHandle(code, companyId, ct);
        return codeIsDuplicate is { IsSuccess: true, Value: not null };
    }
}