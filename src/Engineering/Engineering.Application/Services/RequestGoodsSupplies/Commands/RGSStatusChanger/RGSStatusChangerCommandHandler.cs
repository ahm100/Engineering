using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSStatusChanger;
using Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateCRHeader;
using Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateTypeCommerce;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCommerce;
using Engineering.Application.WebServices.IdentityServices.Users.Queries.GetUsersByActionId;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using IdentityServer.ClientSdk.Services;
using MessageSender.ClientSdk.Messaging;
using MessageSender.ClientSdk.Messaging.Targets;
using MessageSender.ClientSdk.Services;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSStatusChanger;

public class RGSStatusChangerCommandHandler : ICommandHandler<RGSStatusChangerCommand, RGSStatusChangerResponse?>
{
    private readonly ILogger<RGSStatusChangerCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly IMediator _mediator;
    private readonly IMessageRelay _relay;

    public RGSStatusChangerCommandHandler(
        ILogger<RGSStatusChangerCommandHandler> logger,
        IRequestGoodsSupplyRepository repository,
        IMediator mediator,
        IViewThirdPartyRepository thirdPartyRepo,
        IUserInfoProvider userInfoProvider,
        IMessageRelay relay)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
        _userInfoProvider = userInfoProvider;
        _thirdPartyRepo = thirdPartyRepo;
        _relay = relay;
    }

    public async Task<Result<RGSStatusChangerResponse?>> Handle(RGSStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.RequestGoodsSupplyId, ct);
            if (entity is null)
                return Result.Failure<RGSStatusChangerResponse>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
            if (entity.RequestGoodsSupplyTypes.Count <= 0)
                return Result.Failure<RGSStatusChangerResponse>(RequestGoodsSupplyErrors.DoesNotHaveTypeDetails);

            if (entity.Status == GoodsSupplyStatus.Confirm)
                return Result.Failure<RGSStatusChangerResponse>(RequestGoodsSupplyErrors.IsConfirmed);

            var thirdPartyId = _userInfoProvider.ThirdPartyId;

            if (request.Status == RGSTypeStatus.IsDone)
            {
                entity.SetStatus(GoodsSupplyStatus.Confirm); 
                if (request.Status == RGSTypeStatus.IsDone)
                {
                    var create = await _mediator.Send(new CreateCRHeaderCommand
                    {
                        RequestGoodsSupply = entity,
                        Details = entity.RequestGoodsSupplyTypes
                        .Select(x => new CreateCRCommand
                        {
                            RequestGoodsSupplyType = x,
                            RequestGoodsSupplyTypeDetails = x.RequestGoodsSupplyTypeDetails.ToList()
                        })
                        .ToList()
                    });

                    if (create.IsBad())
                        return create.Failure<RGSStatusChangerResponse>()!;
                }
            }

            foreach (var type in entity.RequestGoodsSupplyTypes)
            {
                type.SetStatus(request.Status, request.Description);
                
            }

            if (RGSTypeRules.AllowForNotif.Contains(request.Status))
            {
                var ids = await _mediator.Send(new GetUsersByActionIdQuery([10812]), ct);
                if (!ids.IsBad() && ids.Value.Value is not null && ids.Value.Value.Data is not null)
                {
                    var thirdParties = await _thirdPartyRepo.GetByUserIds(
                        ids.Value.Value.Data.Listed(x => x.UserId), ct);

                    if (thirdParties is not null && thirdParties.Count > 0)
                    {
                        foreach (var item in thirdParties)
                        {
                            try
                            {
                                await _relay.Send(new MessageEnvelope(
                                    MessageSender.ClientSdk.Enums.MessageChannels.Inbox,

                                    new TemplatedMessage(Guid.NewGuid().ToString(), "engineering-set-operator", new Dictionary<string, object?>
                                    {
                                    { "FullName", item?.FirstName + " " + item?.LastName },
                                    { "RequestNumber", entity.RequestSerialNumber },
                                    })

                                    , new UserTarget(item.UserId.Value)), ct);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex,
                                    "Failed to send notification for request {RequestNumber}",
                                    entity.RequestSerialNumber);
                            }
                        }
                    }
                }
            }

            await _repository.Update(entity);
            return new RGSStatusChangerResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RGSStatusChangerResponse>(SharedErrors.UnknownError);
        }
    }
}
