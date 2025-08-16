using BuildingBlocks.CQRS;

namespace School.Dtos;

public record CreateSchoolDto(string Name, string Description) : ICommand<CreateSchoolResult>;
