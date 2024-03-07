using AutoMapper;
using FireFightingRobot.DAL;
using FireFightingRobot.Framework;
using MediatR;
using Serilog;

namespace FireFigthingRobot.ReadStack
{
    public abstract class ReadStackCommandHandlerBase<TCommand, TResult> :
       RequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
        protected readonly ReadContext ReadContext;
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;

        protected ReadStackCommandHandlerBase(ReadContext readContext, IMapper mapper, ILogger logger)
        {
            ReadContext = readContext;
            Mapper = mapper;
            Logger = logger;
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
                Logger.Error(errorMessage, ex.ToString());
                return Result.Fail<T>(errorMessage);
            }
        }
    }
}
