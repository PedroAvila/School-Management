using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using School.Dtos;
using School.Entities;
using School.Ports;

namespace School.UseCase.Students.Create;

public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentValidator()
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

public class CreateStudentUseCaseHandler(IStudentRepository _studentRepository)
    : ICommandHandler<CreateStudentDto, CreateStudentResult>
{
    public async Task<CreateStudentResult> Handle(
        CreateStudentDto command,
        CancellationToken cancellationToken
    )
    {
        var existingStudent = await _studentRepository.ExistAsync(x =>
            x.FirstName == command.FirstName && x.LastName == command.LastName
        );

        if (existingStudent)
            throw new BusinessException(
                $"Student with FullName '{command.FirstName} - {command.LastName}' already exists.",
                HttpStatusCode.Conflict
            );

        var student = new Student
        {
            Code = await _studentRepository.GenerateCodeAsync(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
        };

        int studentId = await _studentRepository.CreateAsync(student);

        return new CreateStudentResult(
            studentId,
            student.Code,
            student.FirstName,
            student.LastName,
            DateOnly.FromDateTime(student.DateOfBirth)
        );
    }
}
