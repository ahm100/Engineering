using Engineering.Application.Abstractions.Interfaces;

namespace Engineering.Infra.Providers;

public class DateTimeImp : IDateTime
{
    public DateTime Now => DateTime.Now;
}