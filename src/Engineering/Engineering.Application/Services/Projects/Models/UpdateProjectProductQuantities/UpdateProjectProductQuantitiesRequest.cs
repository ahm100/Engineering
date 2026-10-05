namespace Engineering.Application.Services.Projects.Models.UpdateProjectProductQuantities;

public record UpdateProjectProductQuantitiesRequest(
    long ProjectProductId,
    decimal CompletedQuantity,
    decimal InProgressQuantity
    );
