using BuildingBlocks.CQRS;

namespace School.Dtos;

public record AssignStudentToTeacherDto(int StudentId, int TeachearId)
    : ICommand<AssignStudentToTeacherResult>;
