using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using School.Dtos;
using School.Entities;
using School.Ports;

namespace School.UseCase.Teachers.Create;

public class CreateTeacherValidator : AbstractValidator<CreateTeacherDto>
{
    public CreateTeacherValidator()
    {
        RuleFor(x => x.SchoolId).GreaterThan(0).WithMessage("SchoolId must be greater than 0.");

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
    }
}

public class CreateTeacherUseCaseHandler(ITeacherRepository _teacherRepository)
    : ICommandHandler<CreateTeacherDto, CreateTeacherResult>
{
    public async Task<CreateTeacherResult> Handle(
        CreateTeacherDto command,
        CancellationToken cancellationToken
    )
    {
        var existingTeacher = await _teacherRepository.ExistAsync(x =>
            x.FirstName == command.FirstName && x.LastName == command.LastName
        );

        if (existingTeacher)
            throw new BusinessException(
                $"Teacher with FullName '{command.FirstName} - {command.LastName}' already exists.",
                HttpStatusCode.Conflict
            );

        var teacher = new Teacher
        {
            SchoolId = command.SchoolId,
            Code = await _teacherRepository.GenerateCodeAsync(),
            FirstName = command.FirstName,
            LastName = command.LastName,
        };

        int teacherId = await _teacherRepository.CreateAsync(teacher);
        return new CreateTeacherResult(
            teacherId,
            teacher.SchoolId,
            teacher.Code,
            teacher.FirstName,
            teacher.LastName
        );
    }
}
