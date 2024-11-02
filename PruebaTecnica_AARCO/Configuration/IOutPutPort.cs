namespace PruebaTecnica_AARCO.Configuration
{
    public interface IOutPutPort<InteractionResponseType>
    {
        void Handle(InteractionResponseType responseType);
    }
}

