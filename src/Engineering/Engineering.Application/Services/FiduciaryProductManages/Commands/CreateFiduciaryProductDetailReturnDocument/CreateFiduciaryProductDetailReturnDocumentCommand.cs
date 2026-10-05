using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailReturnDocument;

public record CreateFiduciaryProductDetailReturnDocumentCommand(FiduciaryProductDetailReturn FiduciaryProductReturnDetail,
                                                                string Url) : ICommand<FiduciaryProductDetailReturnDocument>;
