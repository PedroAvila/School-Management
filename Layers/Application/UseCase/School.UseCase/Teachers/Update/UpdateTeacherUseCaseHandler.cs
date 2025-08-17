using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using School.Dtos;
using School.Entities;
using School.Ports;

namespace School.UseCase.Teachers.Update;

public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherDto>
{
    public UpdateTeacherValidator()
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

public class UpdateTeacherUseCaseHandler(ITeacherRepository _teacherRepository)
    : ICommandHandler<UpdateTeacherWithIdDto, UpdateTeacherResult>
{
    public async Task<UpdateTeacherResult> Handle(
        UpdateTeacherWithIdDto commandWithId,
        CancellationToken cancellationToken
    )
    {
        var command = commandWithId.Command;
        var teacherId = commandWithId.Id;

        var teacher =
            await _teacherRepository.GetByIdAsync(teacherId)
            ?? throw new BusinessException(
                $"Teacher with ID '{teacherId}' does not exist.",
                HttpStatusCode.NotFound
            );

        var updateTeacher = new Teacher
        {
            Id = teacherId,
            SchoolId = command.SchoolId,
            FirstName = command.FirstName,
            LastName = command.LastName,
        };

        await _teacherRepository.UpdateAsync(updateTeacher);

        return new UpdateTeacherResult(
            teacherId,
            updateTeacher.SchoolId,
            teacher.Code,
            updateTeacher.FirstName,
            updateTeacher.LastName
        );
    }
}
