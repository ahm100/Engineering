using Gita.Backend.Shared.Domain.Shared.Models;

namespace Engineering.Api.Extensions.Enums;

public record GetEnumsResponse(
    List<EnumObject> Data,
    int RowCount
    );