using BuildingBlocks.CQRS;

namespace School.Dtos;

public record DeleteStudentDto(int Id) : ICommand<DeleteStudentResult>;
