using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using School.Dtos;
using School.Entities;
using School.Ports;

namespace School.UseCase.Students.Update;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required.")
            .MaximumLength(100)
            .WithMessage("FirstName must not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("LastName is required.")
            .MaximumLength(100)
            .WithMessage("LastName must not exceed 100 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("The DateOfBirth of birth is mandatory.");
    }
}

public class UpdateStudentUseCaseHandler(IStudentRepository _studentRepository)
    : ICommandHandler<UpdateStudentWithIdDto, UpdateStudentResult>
{
    public async Task<UpdateStudentResult> Handle(
        UpdateStudentWithIdDto commandWithId,
        CancellationToken cancellationToken
    )
    {
        var command = commandWithId.Command;
        var studentId = commandWithId.Id;

        var student =
            await _studentRepository.GetByIdAsync(studentId)
            ?? throw new BusinessException(
                $"Student with ID '{studentId}' does not exist.",
                HttpStatusCode.NotFound
            );

        var updateStudent = new Student
        {
            Id = studentId,
            FirstName = command.FirstName,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
        };

        await _studentRepository.UpdateAsync(updateStudent);

        return new UpdateStudentResult(
            studentId,
            student.Code,
            updateStudent.FirstName,
            updateStudent.LastName,
            updateStudent.DateOfBirth
        );
    }
}
