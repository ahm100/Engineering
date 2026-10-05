using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationDocument;

public record CreateDailyProjectOperationDocumentCommand(string Url,
                                                         DailyProjectOperation DailyProjectOperation) : ICommand<DailyProjectOperationDocument>;
