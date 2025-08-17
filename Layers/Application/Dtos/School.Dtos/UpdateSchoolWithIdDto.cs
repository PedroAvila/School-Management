using BuildingBlocks.CQRS;

namespace School.Dtos;

public record UpdateSchoolCommand(string Name, string Description);

public record UpdateSchoolWithIdDto(int Id, UpdateSchoolCommand Command)
    : ICommand<UpdateSchoolResult>;
