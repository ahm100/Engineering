using Engineering.Application.Abstractions.Data.WbsTemplates;
using Engineering.Domain.Entities.WbsTemplates;
using Engineering.Domain.Errors.WbsTemplates;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.WbsTemplates.Commands.CreateWbsTemplate;

public class CreateWbsTemplateCommandHandler : ICommandHandler<CreateWbsTemplateCommand, WbsTemplate?>
{
    private readonly ILogger<CreateWbsTemplateCommandHandler> _logger;
    private readonly IWbsTemplateRepository _repository;
    private readonly IUserInfoProvider _userInfoProv;

    public CreateWbsTemplateCommandHandler(ILogger<CreateWbsTemplateCommandHandler> logger,
        IWbsTemplateRepository repository,
        IUserInfoProvider userInfoProv)
    {
        _logger = logger;
        _repository = repository;
        _userInfoProv = userInfoProv;
    }

    public async Task<Result<WbsTemplate?>> Handle(CreateWbsTemplateCommand request, CT ct)
    {
        try
        {
            var companyId = _userInfoProv.CompanyId;

            var isDuplicateTitle = await _repository.IsDuplicateTitle(request.Title, ct);
            if (isDuplicateTitle is not null)
                return Result.Failure<WbsTemplate?>(WbsTemplateErrors.WbsTemplateNameDuplicate);

            var isDuplicateCode = await _repository.IsDuplicateCode(request.Code, ct);
            if (isDuplicateCode is not null)
                return Result.Failure<WbsTemplate?>(WbsTemplateErrors.WbsTemplateCodeDuplicate);

            var create = new WbsTemplate(request.Title,
                request.Code,
                request.Description,
                request.IsActive,
                companyId);
            var result = await _repository.Create(create, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<WbsTemplate?>(SharedErrors.UnknownError);
        }
    }
}