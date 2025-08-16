using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using School.Dtos;
using School.Ports;

namespace School.UseCase.Schools.Create;

public class CreateSchoolValidator : AbstractValidator<CreateSchoolDto>
{
    public CreateSchoolValidator()
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

public class CreateSchoolUseCaseHandler(ISchoolRepository _schoolRepository)
    : ICommandHandler<CreateSchoolDto, CreateSchoolResult>
{
    public async Task<CreateSchoolResult> Handle(
        CreateSchoolDto command,
        CancellationToken cancellationToken
    )
    {
        var existingSchool = await _schoolRepository.ExistAsync(x => x.Name == command.Name);
        if (existingSchool)
            throw new BusinessException(
                $"School with Name '{command.Name}' already exists.",
                HttpStatusCode.Conflict
            );

        var school = new Entities.School
        {
            Code = await _schoolRepository.GenerateCodeAsync(),
            Name = command.Name,
            Description = command.Description,
        };

        int schoolId = await _schoolRepository.CreateAsync(school);

        return new CreateSchoolResult(schoolId, school.Code, school.Name, school.Description);
    }
}
