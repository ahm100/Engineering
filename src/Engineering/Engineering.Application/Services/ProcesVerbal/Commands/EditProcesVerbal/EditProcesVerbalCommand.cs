using Engineering.Application.Services.ProcesVerbal.Contracts.EditProcesVerbal;
using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Commands.EditProcesVerbal;

public class EditProcesVerbalCommand : ICommand<EditProcesVerbalResponse?>
{
    public long Id { get; set; }
    public string? TitleFa { get; set; }
    public string? TitleEn { get; set; }
    public DateTime? RecordDateTime { get; set; }
    public string? Location { get; set; }
    public ProcesVerbalType? Type { get; set; }

    public List<string>? Docs { get; set; }
    public List<string>? Items { get; set; }
}