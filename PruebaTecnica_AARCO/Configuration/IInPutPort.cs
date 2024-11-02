using MediatR;

namespace PruebaTecnica_AARCO.Configuration
{
    public interface IInPutPort<InteractionRequestType, InteractionResponseType> : IRequest
    {
        public InteractionRequestType RequestData { get; }

        public IOutPutPort<InteractionResponseType> OutPutPort { get; }
    }
}
