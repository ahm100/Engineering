
namespace Engineering.Application.Commercial.Commerces.Models;

public record CreateCommerceRequestResponse(long Id,
                                            long? RequestNumber,
                                            bool IsCreated);