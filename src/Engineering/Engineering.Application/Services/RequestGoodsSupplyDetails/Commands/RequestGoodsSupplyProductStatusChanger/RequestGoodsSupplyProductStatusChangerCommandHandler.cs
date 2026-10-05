using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.RequestGoodsSupplyProductStatusChanger;

public class RequestGoodsSupplyProductStatusChangerCommandHandler : ICommandHandler<RequestGoodsSupplyProductStatusChangerCommand, RequestGoodsSupplyProduct>
{
    private readonly ILogger<RequestGoodsSupplyProductStatusChangerCommandHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public RequestGoodsSupplyProductStatusChangerCommandHandler(ILogger<RequestGoodsSupplyProductStatusChangerCommandHandler> logger,
                                                              IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(RequestGoodsSupplyProductStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity.RequestGoodsSupplyDetails.Count <= 0)
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.NoHaveDetails);

            var description = request.LastDescription;
            switch (request.Status)
            {
                case GoodsSupplyDetailStatus.ProjectManagerResend:

                    if (entity.Status == GoodsSupplyDetailStatus.New)
                        break;

                    if (entity.Status != GoodsSupplyDetailStatus.ProjectManagerReturned &&
                        entity.Status != GoodsSupplyDetailStatus.ProjectManagerResend &&
                        entity.Status != GoodsSupplyDetailStatus.ManagementReturned &&
                        entity.Status != GoodsSupplyDetailStatus.GoodsManagerReturned && // ⬅️ ADD THIS LINE
                        entity.Status != GoodsSupplyDetailStatus.NotCompleteSupply &&
                        entity.Status != GoodsSupplyDetailStatus.SupplyUnitReturned)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForProjectManagerResend);

                    entity.SetStatus(GoodsSupplyDetailStatus.ProjectManagerResend, description);
                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerResend, request.LastDescription);
                    });
                    break;

                case GoodsSupplyDetailStatus.ProjectManagerPending:

                    if (entity.Status != GoodsSupplyDetailStatus.New &&
                        entity.Status != GoodsSupplyDetailStatus.ProjectManagerResend)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForProjectManagerPending);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            entity.SetStatus(GoodsSupplyDetailStatus.InCompleteSupply, description);
                        else
                            entity.SetStatus(GoodsSupplyDetailStatus.ProjectManagerPending, null);

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerPending, null);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.ProjectManagerConfirmed:

                    if (entity.Status != GoodsSupplyDetailStatus.ProjectManagerPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForProjectManagerConfirmed);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            entity.SetStatus(GoodsSupplyDetailStatus.InCompleteSupply, description);
                        else
                            entity.SetStatus(GoodsSupplyDetailStatus.ProjectManagerConfirmed, description);

                        bool isSupply = false;
                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (request.RejectedDetailIds is not null && request.RejectedDetailIds.Any(x => x.Equals(oo.Id)))
                            {
                                if (oo.Status == GoodsSupplyDetailStatus.ProjectManagerPending)
                                    oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerRejected, request.LastDescription);
                            }
                            else if (request.ConfirmedDetailIds is not null && request.ConfirmedDetailIds.Any(x => x.Equals(oo.Id)))
                            {
                                if (oo.Status == GoodsSupplyDetailStatus.ProjectManagerPending)
                                    oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerConfirmed, request.LastDescription);
                            }
                            else
                            {
                                if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                    oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerConfirmed, request.LastDescription);
                            }

                            if (request.SendToSupply is not null && request.SendToSupply == true)
                                if (entity.RequestGoodsSupply.IsProjectSupply)
                                    if (oo.ProjectProduct is not null)
                                        if (oo.ProjectProduct.DefaultManagerSet == true)
                                        {
                                            oo.UpdateStatus(GoodsSupplyDetailStatus.ManagementConfirmed, request.LastDescription);
                                            isSupply = true;
                                        }
                        });

                        if (!request.HasGoodsManager && isSupply)
                            entity.SetStatus(GoodsSupplyDetailStatus.ManagementConfirmed, description);

                        if (request.HasGoodsManager)
                        {
                            entity.SetStatus(
                                GoodsSupplyDetailStatus.GoodsManagerPending,
                                description);

                            entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                            {
                                if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                    oo.UpdateStatus(
                                        GoodsSupplyDetailStatus.GoodsManagerPending,
                                        request.LastDescription);
                            });
                        }
                    }
                    break;

                case GoodsSupplyDetailStatus.ProjectManagerReturned:
                    if (entity.Status != GoodsSupplyDetailStatus.ProjectManagerPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForProjectManagerReturned);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            entity.SetStatus(GoodsSupplyDetailStatus.InCompleteSupply, description);
                        else
                            entity.SetStatus(GoodsSupplyDetailStatus.ProjectManagerReturned, description);

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerReturned, request.LastDescription);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.ProjectManagerRejected:
                    if (entity.Status != GoodsSupplyDetailStatus.ProjectManagerPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForProjectManagerRejected);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.CanNotRejected);
                        else
                        {
                            entity.SetStatus(GoodsSupplyDetailStatus.ProjectManagerRejected, description);
                            entity.SetStatus(GoodsSupplyDetailStatus.Closed, description);
                        }

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerRejected, request.LastDescription);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.GoodsManagerConfirmed:

                    if (entity.Status != GoodsSupplyDetailStatus.GoodsManagerPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(
                            RequestGoodsSupplyErrors.InValidStatusForGoodsManagerConfirmed);

                    entity.SetStatus(
                        GoodsSupplyDetailStatus.GoodsManagerConfirmed,
                        description);

                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            oo.UpdateStatus(
                                GoodsSupplyDetailStatus.GoodsManagerConfirmed,
                                request.LastDescription);
                    });

                    // forward to senior expert (entry point of the current flow)
                    entity.SetStatus(
                        GoodsSupplyDetailStatus.ManagementConfirmed,
                        description);

                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            oo.UpdateStatus(
                                GoodsSupplyDetailStatus.ManagementConfirmed,
                                request.LastDescription);
                    });
                    break;

                case GoodsSupplyDetailStatus.GoodsManagerReturned:

                    if (entity.Status != GoodsSupplyDetailStatus.GoodsManagerPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(
                            RequestGoodsSupplyErrors.InValidStatusForGoodsManagerReturned);

                    if (entity.RequestGoodsSupplyDetails.Any(x =>
                        x.Status == GoodsSupplyDetailStatus.CompleteSupply ||
                        x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                        entity.SetStatus(
                            GoodsSupplyDetailStatus.InCompleteSupply,
                            description);
                    else
                        entity.SetStatus(
                            GoodsSupplyDetailStatus.GoodsManagerReturned,
                            description);

                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            oo.UpdateStatus(
                                GoodsSupplyDetailStatus.GoodsManagerReturned,
                                request.LastDescription);
                    });
                    break;

                case GoodsSupplyDetailStatus.GoodsManagerRejected:

                    if (entity.Status != GoodsSupplyDetailStatus.GoodsManagerPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(
                            RequestGoodsSupplyErrors.InValidStatusForGoodsManagerRejected);

                    if (entity.RequestGoodsSupplyDetails.Any(x =>
                        x.Status == GoodsSupplyDetailStatus.CompleteSupply ||
                        x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                        return Result.Failure<RequestGoodsSupplyProduct>(
                            RequestGoodsSupplyErrors.CanNotRejected);

                    entity.SetStatus(
                        GoodsSupplyDetailStatus.GoodsManagerRejected,
                        description);
                    entity.SetStatus(
                        GoodsSupplyDetailStatus.Closed,
                        description);

                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            oo.UpdateStatus(
                                GoodsSupplyDetailStatus.GoodsManagerRejected,
                                request.LastDescription);
                    });
                    break;

                case GoodsSupplyDetailStatus.ManagementPending:
                    if (entity.Status != GoodsSupplyDetailStatus.ProjectManagerConfirmed)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForManagementPending);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            entity.SetStatus(GoodsSupplyDetailStatus.InCompleteSupply, description);
                        else
                            entity.SetStatus(GoodsSupplyDetailStatus.ManagementPending, null);

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ManagementPending, null);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.ManagementConfirmed:
                    if (entity.Status != GoodsSupplyDetailStatus.ManagementPending && entity.Status != GoodsSupplyDetailStatus.ProjectManagerConfirmed)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForManagementConfirmed);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            entity.SetStatus(GoodsSupplyDetailStatus.InCompleteSupply, description);
                        else
                            entity.SetStatus(GoodsSupplyDetailStatus.ManagementConfirmed, description);

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ManagementConfirmed, request.LastDescription);
                        });

                        if (entity.RequestGoodsSupply.Type == GoodsSupplyType.Project)
                        {
                            entity.SetStatus(GoodsSupplyDetailStatus.PendingForSupply, description);
                            entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                            {
                                if (oo.Status == GoodsSupplyDetailStatus.ManagementConfirmed)
                                    oo.UpdateStatus(GoodsSupplyDetailStatus.PendingForSupply, request.LastDescription);
                            });
                        }
                    }
                    break;

                case GoodsSupplyDetailStatus.ManagementReturned:
                    if (entity.Status != GoodsSupplyDetailStatus.ManagementPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForManagementReturned);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            entity.SetStatus(GoodsSupplyDetailStatus.InCompleteSupply, description);
                        else
                            entity.SetStatus(GoodsSupplyDetailStatus.ManagementReturned, description);

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ManagementReturned, request.LastDescription);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.ManagementRejected:
                    if (entity.Status != GoodsSupplyDetailStatus.ManagementPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForManagementRejected);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.CanNotRejected);
                        else
                        {
                            entity.SetStatus(GoodsSupplyDetailStatus.ManagementRejected, description);
                            entity.SetStatus(GoodsSupplyDetailStatus.Closed, description);
                        }

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ManagementRejected, request.LastDescription);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.SupplyUnitPending:
                    if (entity.Status != GoodsSupplyDetailStatus.ManagementConfirmed && entity.Status != GoodsSupplyDetailStatus.SupplyUnitPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForSupplyUnitPending);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            entity.SetStatus(GoodsSupplyDetailStatus.InCompleteSupply, description);
                        else
                            entity.SetStatus(GoodsSupplyDetailStatus.SupplyUnitPending, null);

                        foreach (var item in entity.RequestGoodsSupplyDetails)
                        {
                            if (request.ConfirmedDetailIds is not null && request.ConfirmedDetailIds.Any())
                            {
                                if (request.ConfirmedDetailIds.Any(x => x.Equals(item.Id)))
                                    item.UpdateStatus(GoodsSupplyDetailStatus.PendingForSupply, request.LastDescription);
                                else
                                {
                                    if (item.Status == GoodsSupplyDetailStatus.ManagementConfirmed)
                                        item.UpdateStatus(GoodsSupplyDetailStatus.SupplyUnitPending, null);
                                }

                            }
                            else if (item.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            {
                                item.UpdateStatus(GoodsSupplyDetailStatus.SupplyUnitPending, null);
                            }
                        }
                    }
                    break;

                case GoodsSupplyDetailStatus.SupplyUnitReturned:
                    if (entity.Status != GoodsSupplyDetailStatus.SupplyUnitPending && entity.Status != GoodsSupplyDetailStatus.NotCompleteSupply && entity.Status != GoodsSupplyDetailStatus.ReturnToSupply)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForSupplyUnitReturned);
                    else
                    {
                        entity.SetStatus(GoodsSupplyDetailStatus.SupplyUnitReturned, description);

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.SupplyUnitReturned, request.LastDescription);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.SupplyUnitRejected:
                    if (entity.Status != GoodsSupplyDetailStatus.SupplyUnitPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForSupplyUnitRejected);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.CanNotRejected);
                        else
                        {
                            entity.SetStatus(GoodsSupplyDetailStatus.SupplyUnitRejected, description);
                            entity.SetStatus(GoodsSupplyDetailStatus.Closed, description);
                        }

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.SupplyUnitRejected, request.LastDescription);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.PendingForSupply:
                    if (entity.Status != GoodsSupplyDetailStatus.SupplyUnitPending)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForPendingForSupply);
                    else
                    {
                        if (entity.RequestGoodsSupplyDetails.Any(x => x.Status == GoodsSupplyDetailStatus.CompleteSupply || x.Status == GoodsSupplyDetailStatus.InCompleteSupply))
                            return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.CanNotRejected);
                        else
                            entity.SetStatus(GoodsSupplyDetailStatus.PendingForSupply, description);

                        entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.PendingForSupply, request.LastDescription);
                        });
                    }
                    break;

                case GoodsSupplyDetailStatus.Closed:
                    if (entity.Status != GoodsSupplyDetailStatus.New &&
                        entity.Status != GoodsSupplyDetailStatus.ProjectManagerReturned &&
                        entity.Status != GoodsSupplyDetailStatus.ProjectManagerRejected &&
                        entity.Status != GoodsSupplyDetailStatus.ManagementReturned &&
                        entity.Status != GoodsSupplyDetailStatus.GoodsManagerReturned &&
                        entity.Status != GoodsSupplyDetailStatus.ManagementRejected &&
                        entity.Status != GoodsSupplyDetailStatus.SupplyUnitReturned &&
                        entity.Status != GoodsSupplyDetailStatus.PendingForSupply)
                        return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidStatusForClosed);
                    else
                        entity.SetStatus(request.Status, description);
                    break;
            }
            entity.AddHistory();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyProduct>(SharedErrors.UnknownError);
        }
    }
}
