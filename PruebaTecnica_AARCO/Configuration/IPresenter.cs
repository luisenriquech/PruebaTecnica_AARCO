namespace PruebaTecnica_AARCO.Configuration
{
    public interface IPresenter<ResponseType> : IOutPutPort<ResponseType>
    {
        public ResponseType Content { get; }
    }
}
