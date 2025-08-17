using BuildingBlocks.CQRS;
using School.Dtos;
using School.Ports;

namespace School.UseCase.Schools.Delete;

public class DeleteSchoolUseCaseHandler(ISchoolRepository _schoolRepository)
    : ICommandHandler<DeleteSchoolDto, DeleteSchoolResult>
{
    public async Task<DeleteSchoolResult> Handle(
        DeleteSchoolDto command,
        CancellationToken cancellationToken
    )
    {
        var result = await _schoolRepository.DeleteAsync(command.Id);
        return new DeleteSchoolResult(result);
    }
}
