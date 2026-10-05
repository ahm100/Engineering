namespace Engineering.Application.Services.SubProjects;

public partial class SubProjectLogic
{
    private async Task<bool> HasAccess(
        long projectId, CT ct)
    {
        var userId = _userInfo.UserId;
        var thirdParties = await _thirdPartyRepository.GetByUserIds([userId], ct);
        return await _repository.HasAccess(
            projectId, userId, thirdParties.Select(oo => oo.Id).ToList(), ct);
    }
}
