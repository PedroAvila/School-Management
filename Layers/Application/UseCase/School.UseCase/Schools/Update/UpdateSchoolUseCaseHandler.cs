using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using School.Dtos;
using School.Ports;

namespace School.UseCase.Schools.Update;

public class UpdateSchoolValidator : AbstractValidator<UpdateSchoolCommand>
{
    public UpdateSchoolValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Address must not exceed 200 characters.");
    }
}

public class UpdateSchoolUseCaseHandler(ISchoolRepository _schoolRepository)
    : ICommandHandler<UpdateSchoolWithIdDto, UpdateSchoolResult>
{
    public async Task<UpdateSchoolResult> Handle(
        UpdateSchoolWithIdDto commandWithId,
        CancellationToken cancellationToken
    )
    {
        var command = commandWithId.Command;
        var schoolId = commandWithId.Id;

        var school =
            await _schoolRepository.GetByIdAsync(schoolId)
            ?? throw new BusinessException(
                $"School with ID '{schoolId}' does not exist.",
                HttpStatusCode.NotFound
            );

        var updateSchool = new Entities.School
        {
            Id = schoolId,
            Name = command.Name,
            Description = command.Description,
        };

        await _schoolRepository.UpdateAsync(updateSchool);

        return new UpdateSchoolResult(
            schoolId,
            school.Code,
            updateSchool.Name,
            updateSchool.Description
        );
    }
}
