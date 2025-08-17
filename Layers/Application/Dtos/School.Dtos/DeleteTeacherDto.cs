using BuildingBlocks.CQRS;

namespace School.Dtos;

public record DeleteTeacherDto(int Id) : ICommand<DeleteTeacherResult>;
