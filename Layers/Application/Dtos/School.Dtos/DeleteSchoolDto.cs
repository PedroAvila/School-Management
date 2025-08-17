using BuildingBlocks.CQRS;

namespace School.Dtos;

public record DeleteSchoolDto(int Id) : ICommand<DeleteSchoolResult>;
