using Engineering.Application.Services.CostOvers.Commands.SubmitCostOverForApproval;
using Engineering.Application.Services.CostOvers.Models.Approval;

namespace Engineering.Application.Services.CostOvers;

public partial class CostOverLogic
{
    /// <summary>پس از پایان ناموفق، تلاش قبلی را می بندد و موجودیت را با یک Commit به پیش نویس برمی گرداند.</summary>
    public async Task<Result<CostOverApprovalRequest?>> RecoverApproval(
        RecoverCostOverApprovalRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var userId = _userInfoProvider.UserId;
        if (companyId is null || companyId <= 0 || userId <= 0 ||
            request.Id <= 0 || request.RequestId == Guid.Empty)
            return Result.Failure<CostOverApprovalRequest>(CostOverErrors.ApprovalRecoveryInvalid);

        var result = await _mediator.Send(new RecoverCostOverApprovalCommand(
            request.Id, companyId.Value, userId, request.RequestId), ct);
        if (result.IsFailure)
            return Result.Failure<CostOverApprovalRequest>(result.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CostOverApprovalRequest(result.Value!.Id);
    }
    /// <summary>تلاش ردشده را می بندد و بازگشت به پیش نویس را با یک Commit ذخیره می کند.</summary>
    public async Task<Result<CostOverApprovalRequest?>> ReturnToDraft(
        CostOverApprovalRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (companyId is null || request.Id <= 0)
            return Result.Failure<CostOverApprovalRequest>(GlobalErrors.InvalidCompany);

        var result = await _mediator.Send(new ReturnCostOverToDraftCommand(request.Id, companyId.Value), ct);
        if (result.IsFailure) return Result.Failure<CostOverApprovalRequest>(result.Error!);

        await _unitOfWork.CommitAsync(ct);
        return request;
    }

    /// <summary>تأیید هزینه بالاسری را درخواست و هر سه تغییر را با یک Commit ذخیره می کند.</summary>
    public async Task<Result<CostOverApprovalResponse?>> SubmitForApproval(
        CostOverApprovalRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (!await CompanyValidator.IsCompanyValid(companyId, _mediator, ct))
            return Result.Failure<CostOverApprovalResponse>(GlobalErrors.InvalidCompany);

        var userId = _userInfoProvider.UserId;
        if (companyId is null || userId <= 0 || request.Id <= 0)
            return Result.Failure<CostOverApprovalResponse>(GlobalErrors.InvalidCompany);

        var result = await _mediator.Send(new SubmitCostOverForApprovalCommand(request.Id, companyId.Value, userId), ct);
        if (result.IsFailure) return Result.Failure<CostOverApprovalResponse>(result.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CostOverApprovalResponse(
            request.Id, result.Value!.RequestId, result.Value.DispatchStatus);
    }
    /// <summary>
    /// مسیر سازگار با API قبلی؛ وضعیت، درخواست و Outbox را از مسیر ارسال با یک Commit ثبت می کند.
    /// </summary>
    public async Task<Result<SetPendingApprovalResponse?>> SetPendingApproval(
        SetPendingApprovalRequest request, CT ct)
    {
        var result = await SubmitForApproval(new CostOverApprovalRequest(request.Id), ct);
        if (result.IsFailure)
            return Result.Failure<SetPendingApprovalResponse>(result.Error!);

        return new SetPendingApprovalResponse(result.Value!.Id);
    }
}
