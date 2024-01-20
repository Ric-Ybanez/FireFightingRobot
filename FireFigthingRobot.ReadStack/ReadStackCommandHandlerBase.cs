using AutoMapper;
using FireFightingRobot.Framework;
using MediatR;

namespace FireFigthingRobot.ReadStack
{
    public abstract class ReadStackCommandHandlerBase<TCommand, TResult> :
       RequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
        protected readonly ReadContext ReadContext;
        protected readonly IMapper Mapper;

        protected ReadStackCommandHandlerBase(ReadContext readContext, IMapper mapper)
        {
            ReadContext = readContext;
            Mapper = mapper;
        }

        protected abstract override TResult Handle(TCommand request);

        protected Result<T> RunQuery<T>(Func<T> func, string errorMessage)
        {
            try
            {
                var queryResult = func();

                return Result.OK(queryResult);
            }

            catch (Exception ex)
            {
                return Result.Fail<T>(errorMessage);
            }
        }
    }
}
