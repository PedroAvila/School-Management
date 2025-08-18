using System.Net;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using School.Dtos;
using School.Entities;
using School.Ports;

namespace School.UseCase.Registrations.Create;

public class CreateRegistrationUseCaseHandler(
    IStudentRepository _studentRepository,
    ISchoolRepository _schoolRepository,
    IRegistrationRepository _registrationRepository
) : ICommandHandler<CreateRegistrationDto, CreateRegistrationResult>
{
    public async Task<CreateRegistrationResult> Handle(
        CreateRegistrationDto command,
        CancellationToken cancellationToken
    )
    {
        var existingStudent = await _studentRepository.ExistAsync(x => x.Id == command.StudentId);
        if (!existingStudent)
            throw new BusinessException(
                $"Student with ID '{command.StudentId}' not found.",
                HttpStatusCode.NotFound
            );

        var existingSchool = await _schoolRepository.ExistAsync(x => x.Id == command.SchoolId);
        if (!existingSchool)
            throw new BusinessException(
                $"School with ID '{command.SchoolId}' not found.",
                HttpStatusCode.NotFound
            );

        var registration = new Registration
        {
            Code = await _registrationRepository.GenerateCodeAsync(),
            StudentId = command.StudentId,
            SchoolId = command.SchoolId,
            RegistrationDate = DateTime.UtcNow,
        };

        int registrationId = await _registrationRepository.CreateAsync(registration);

        return new CreateRegistrationResult(
            registrationId,
            registration.Code,
            registration.StudentId,
            registration.SchoolId,
            registration.RegistrationDate
        );
    }
}
