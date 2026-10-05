namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryDocuments;

public record DeleteRequestMachineryDocumentsCommand(long Id) : ICommand<bool?>;
