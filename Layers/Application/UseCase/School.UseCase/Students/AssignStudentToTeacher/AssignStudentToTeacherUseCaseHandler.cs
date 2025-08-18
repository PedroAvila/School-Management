using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using School.Dtos;
using School.Ports;

namespace School.UseCase.Students.AssignStudentToTeacher;

public class AssignStudentToTeacherValidator : AbstractValidator<AssignStudentToTeacherDto>
{
    public AssignStudentToTeacherValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty().WithMessage("StudentId is required.");

        RuleFor(x => x.TeachearId).NotEmpty().WithMessage("TeachearId is required.");
    }
}

public class AssignStudentToTeacherUseCaseHandler(
    IStudentRepository _studentRepository,
    ITeacherRepository _teacherRepository
) : ICommandHandler<AssignStudentToTeacherDto, AssignStudentToTeacherResult>
{
    public async Task<AssignStudentToTeacherResult> Handle(
        AssignStudentToTeacherDto command,
        CancellationToken cancellationToken
    )
    {
        var existingStudent = await _studentRepository.ExistAsync(x => x.Id == command.StudentId);
        if (!existingStudent)
            throw new BusinessException(
                $"Student with ID '{command.StudentId}' not found.",
                HttpStatusCode.NotFound
            );

        var existingTeacher = await _teacherRepository.ExistAsync(x => x.Id == command.TeachearId);
        if (!existingTeacher)
            throw new BusinessException(
                $"Teacher with ID '{command.TeachearId}' not found.",
                HttpStatusCode.NotFound
            );

        var result = await _studentRepository.AssignStudentToTeacherAsync(
            command.StudentId,
            command.TeachearId
        );

        if (!result)
            throw new BusinessException(
                $"The Student ID '{command.StudentId}' was already assigned to the Teacher ID '{command.TeachearId}'.",
                HttpStatusCode.Conflict
            );

        return new AssignStudentToTeacherResult(result);
    }
}
