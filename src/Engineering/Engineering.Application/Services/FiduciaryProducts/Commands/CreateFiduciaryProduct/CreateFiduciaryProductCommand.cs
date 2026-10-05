using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.CreateFiduciaryProduct;

public record CreateFiduciaryProductCommand(Project Project,
                                            ProjectOperation ProjectOperation,
                                            long ThirdPartyId,
                                            string? Description,
                                            long? CompanyId) : ICommand<FiduciaryProduct>;
