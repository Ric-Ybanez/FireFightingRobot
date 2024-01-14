using MediatR;

namespace FireFightingRobot.Commands;

public abstract class BaseCommandHandler<TCommand, TResult> : RequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
{
    internal TResult UnitTestHandle(TCommand request)
    {
        return Handle(request);
    }

    protected abstract override TResult Handle(TCommand request);
}