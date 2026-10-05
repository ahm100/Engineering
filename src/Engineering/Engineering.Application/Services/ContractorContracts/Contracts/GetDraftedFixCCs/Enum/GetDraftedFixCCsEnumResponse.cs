namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Enum;

public record GetDraftedFixCCsEnumResponse(
    List<EnumObject> Draft,
    List<EnumObject> Daily
);