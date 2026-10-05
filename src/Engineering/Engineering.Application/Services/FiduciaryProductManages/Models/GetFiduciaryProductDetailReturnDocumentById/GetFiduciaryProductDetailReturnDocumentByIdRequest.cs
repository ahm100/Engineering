namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnDocumentById;

public record GetFiduciaryProductDetailReturnDocumentByIdRequest(long FiduciaryProductDetailReturnId,
                                                                 string[]? OrderBy,
                                                                 int PageIndex,
                                                                 int PageSize) : IHttpRequest;