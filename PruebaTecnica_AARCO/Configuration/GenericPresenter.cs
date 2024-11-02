using PruebaTecnica_AARCO.DTO.General;

namespace PruebaTecnica_AARCO.Configuration
{
    public class GenericPresenter : IPresenter<SuccessResult>
    {
        public SuccessResult Content { get; private set; }

        public void Handle(SuccessResult responseType)
        {
            Content = responseType;
        }
    }
}
