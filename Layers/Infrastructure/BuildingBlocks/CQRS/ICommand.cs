using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// BuildingBlocks es un proyecto Bloque de Construcción donde se crea la abstracción de MediatR
    /// </summary>
    
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
