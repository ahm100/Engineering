using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetsUserById;
using Gita.Backend.Shared.Domain.Enums.Invoice;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.FiduciaryProductWarehouseStatusChanger;

public class FiduciaryProductWarehouseStatusChangerCommandHandler : ICommandHandler<FiduciaryProductWarehouseStatusChangerCommand, FiduciaryProduct>
{
    private readonly ILogger<FiduciaryProductWarehouseStatusChangerCommandHandler> _logger;
    private readonly IFiduciaryProductRepository _repository;

    public FiduciaryProductWarehouseStatusChangerCommandHandler(ILogger<FiduciaryProductWarehouseStatusChangerCommandHandler> logger, IFiduciaryProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProduct?>> Handle(FiduciaryProductWarehouseStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetFiduciaryProductForChangeStatus(request.Id, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProduct>(FiduciaryProductErrors.FiduciaryProductWithIdNotFound);

            var details = entity.Details.Where(x => x.Status != FiduciaryProductDetailStatus.Rejected).ToList();

            FiduciaryProductDetailStatus detailStatus = FiduciaryProductDetailStatus.Pending;
            if (request.Type == WarehouseInvoiceType.EntryThroughBorrow)
            {
                foreach (var detail in details)
                {
                    var entryProduct = request.Products.FirstOrDefault(x => x.ProductId == detail.ProductId);
                    var existProduct = request.RequestProducts.FirstOrDefault(x => x.ProductId == detail.ProductId);

                    if (entryProduct is not null)
                    {
                        var management = detail.Managements.FirstOrDefault(x => x.InvoiceId == request.InvoiceId);
                        if (management is not null && entryProduct.Status == WarehouseInvoiceProductStatus.Approved)
                            management?.SetApproved(request.Description, request.UserId);
                        else if (management is not null && entryProduct.Status == WarehouseInvoiceProductStatus.Rejected)
                            management?.SetRejected(entryProduct.Description, request.UserId);
                        else
                            management?.SetPending(request.Description, request.UserId);

                        var approveQuantity = detail.Managements
                            .Where(x => x.InvoiceId == entryProduct.InvoiceId && x.Status == FiduciaryProductDetailManagementStatus.Approved && x.Returns != null && x.Returns.Count > 0)
                            .SelectMany(x => x.Returns).Sum(x => x.ReturnCount);
                        if (approveQuantity == detail.LoanCount)
                            detailStatus = FiduciaryProductDetailStatus.Returned;
                        else if (approveQuantity < detail.LoanCount)
                            detailStatus = FiduciaryProductDetailStatus.IncompleteReturned;

                        if (approveQuantity == 0 && detail.Managements.FirstOrDefault(x => x.InvoiceId == entryProduct.InvoiceId)?.Status == FiduciaryProductDetailManagementStatus.Rejected)
                            detailStatus = FiduciaryProductDetailStatus.ReturnRejected;

                        var detailDescription = await DetailDescriptionMacker(request.Description, detailStatus, request.mediator, request.CurrentUser, ct);
                        detail.ChangeStatusWithUser(detailStatus, request.Description, detailDescription, request.UserId);
                    }
                    else if (existProduct is not null)
                    {
                        var management = detail.Managements.FirstOrDefault(x => x.InvoiceId == request.InvoiceId);
                        if (management is not null && existProduct.Status == WarehouseInvoiceProductStatus.Approved)
                            management?.SetApproved(request.Description, request.UserId);
                        else if (management is not null && existProduct.Status == WarehouseInvoiceProductStatus.Rejected)
                            management?.SetRejected(existProduct.Description, request.UserId);
                        else
                            management?.SetPending(request.Description, request.UserId);

                        var approveQuantity = detail.Managements.Where(x => x.InvoiceId == existProduct.InvoiceId && x.Status == FiduciaryProductDetailManagementStatus.Approved && x.Returns != null && x.Returns.Count > 0).SelectMany(x => x.Returns).Sum(x => x.ReturnCount);
                        if (approveQuantity == detail.LoanCount)
                            detailStatus = FiduciaryProductDetailStatus.Returned;
                        else if (approveQuantity < detail.LoanCount)
                            detailStatus = FiduciaryProductDetailStatus.IncompleteReturned;

                        if (approveQuantity == 0 && detail.Managements.FirstOrDefault(x => x.InvoiceId == existProduct.InvoiceId)?.Status == FiduciaryProductDetailManagementStatus.Rejected)
                            detailStatus = FiduciaryProductDetailStatus.ReturnRejected;

                        var detailDescription = await DetailDescriptionMacker(request.Description, detailStatus, request.mediator, request.CurrentUser, ct);
                        detail.ChangeStatusWithUser(detailStatus, request.Description, detailDescription, request.UserId);
                    }
                }
            }
            else if (request.Type == WarehouseInvoiceType.ExitForBorrow)
            {
                foreach (var detail in details)
                {
                    var existProduct = request.RequestProducts.FirstOrDefault(x => x.ProductId == detail.ProductId);
                    var entryProduct = request.Products.FirstOrDefault(x => x.ProductId == detail.ProductId);

                    if (existProduct is not null)
                    {
                        var management = detail.Managements.FirstOrDefault(x => x.InvoiceId == request.InvoiceId);
                        if (management is not null && existProduct.Status == WarehouseInvoiceProductStatus.Approved)
                            management?.SetApproved(request.Description, request.UserId);
                        else if (management is not null && existProduct.Status == WarehouseInvoiceProductStatus.Rejected)
                            management?.SetRejected(existProduct.Description, request.UserId);
                        else
                            management?.SetPending(request.Description, request.UserId);

                        var approveQuantity = detail.Managements.Where(x => x.InvoiceId == existProduct.InvoiceId && x.Status == FiduciaryProductDetailManagementStatus.Approved).Sum(x => x.ConfirmedLoanCount);
                        if (approveQuantity == detail.LoanCount)
                            detailStatus = FiduciaryProductDetailStatus.Delivary;
                        else if (approveQuantity == 0)
                            detailStatus = FiduciaryProductDetailStatus.NoDelivary;
                        else
                            detailStatus = FiduciaryProductDetailStatus.IncompleteDelivered;

                        if (detailStatus == FiduciaryProductDetailStatus.Delivary || detailStatus == FiduciaryProductDetailStatus.IncompleteDelivered)
                            detail.SetDeliverDate(DateTime.Now);

                        if (approveQuantity == 0 && detail.Managements.FirstOrDefault(x => x.InvoiceId == existProduct.InvoiceId)?.Status == FiduciaryProductDetailManagementStatus.Rejected)
                            detailStatus = FiduciaryProductDetailStatus.NoDelivary;

                        var detailDescription = await DetailDescriptionMacker(request.Description, detailStatus, request.mediator, request.CurrentUser, ct);
                        detail.ChangeStatusWithUser(detailStatus, request.Description, detailDescription, request.UserId);
                    }
                    else if (entryProduct is not null)
                    {
                        var management = detail.Managements.FirstOrDefault(x => x.InvoiceId == request.InvoiceId);
                        if (management is not null && entryProduct.Status == WarehouseInvoiceProductStatus.Approved)
                            management?.SetApproved(request.Description, request.UserId);
                        else if (management is not null && entryProduct.Status == WarehouseInvoiceProductStatus.Rejected)
                            management?.SetRejected(entryProduct.Description, request.UserId);
                        else
                            management?.SetPending(request.Description, request.UserId);

                        var approveQuantity = detail.Managements.Where(x => x.InvoiceId == entryProduct.InvoiceId && x.Status == FiduciaryProductDetailManagementStatus.Approved).Sum(x => x.ConfirmedLoanCount);
                        if (approveQuantity == detail.LoanCount)
                            detailStatus = FiduciaryProductDetailStatus.Delivary;
                        else if (approveQuantity == 0)
                            detailStatus = FiduciaryProductDetailStatus.NoDelivary;
                        else
                            detailStatus = FiduciaryProductDetailStatus.IncompleteDelivered;

                        if (detailStatus == FiduciaryProductDetailStatus.Delivary || detailStatus == FiduciaryProductDetailStatus.IncompleteDelivered)
                            detail.SetDeliverDate(DateTime.Now);

                        if (approveQuantity == 0 && detail.Managements.FirstOrDefault(x => x.InvoiceId == entryProduct.InvoiceId)?.Status == FiduciaryProductDetailManagementStatus.Rejected)
                            detailStatus = FiduciaryProductDetailStatus.NoDelivary;

                        var detailDescription = await DetailDescriptionMacker(request.Description, detailStatus, request.mediator, request.CurrentUser, ct);
                        detail.ChangeStatusWithUser(detailStatus, request.Description, detailDescription, request.UserId);
                    }
                }
            }

            var status = FiduciaryProductStatus.Pending;
            if (request.Type == WarehouseInvoiceType.EntryThroughBorrow)
            {
                if (details.All(x => x.Status == FiduciaryProductDetailStatus.Returned))
                    status = FiduciaryProductStatus.FullReturned;
                else if (details.Any(x => x.Status == FiduciaryProductDetailStatus.IncompleteReturned))
                    status = FiduciaryProductStatus.IncompleteReturned;
                else if (details.Any(x => x.Status == FiduciaryProductDetailStatus.ReturnRejected))
                    status = FiduciaryProductStatus.ReturnRejected;
                else if (details.All(x => x.Status == FiduciaryProductDetailStatus.Rejected))
                    status = FiduciaryProductStatus.Rejected;
                else
                    status = FiduciaryProductStatus.Confirmed;
            }
            else if (request.Type == WarehouseInvoiceType.ExitForBorrow)
            {
                if (details.All(x => x.Status == FiduciaryProductDetailStatus.Delivary))
                    status = FiduciaryProductStatus.FullDelivery;
                else if (details.All(x => x.Status == FiduciaryProductDetailStatus.NoDelivary))
                    status = FiduciaryProductStatus.NoDelivery;
                else if (details.Any(x => x.Status == FiduciaryProductDetailStatus.IncompleteDelivered))
                    status = FiduciaryProductStatus.IncompleteDelivery;
                else if (details.All(x => x.Status == FiduciaryProductDetailStatus.Rejected))
                    status = FiduciaryProductStatus.Rejected;
                else
                    status = FiduciaryProductStatus.Confirmed;
            }

            var description = await DescriptionMacker(request.Description, status, request.mediator, request.CurrentUser, ct);
            entity.ChangeStatusWithUser(status, request.Description, description, request.UserId);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProduct>(SharedErrors.UnknownError);
        }
    }

    private async Task<string> DescriptionMacker(string? requestDescription, FiduciaryProductStatus status, IMediator mediator, long currentUserId, CT ct)
    {
        var subSystem = "مهندسی";

        var getUsers = await mediator.Send(new GetsUserByIdQuery([currentUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();

        return $"{subSystem} - {user?.FullName} - {status.GetEnumDescription()} - {requestDescription}";
    }
    private async Task<string> DetailDescriptionMacker(string? requestDescription, FiduciaryProductDetailStatus detailStatus, IMediator mediator, long currentUserId, CT ct)
    {
        var subSystem = "مهندسی";

        var getUsers = await mediator.Send(new GetsUserByIdQuery([currentUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();

        return $"{subSystem} - {user?.FullName} - {detailStatus.GetEnumDescription()} - {requestDescription}";
    }
}