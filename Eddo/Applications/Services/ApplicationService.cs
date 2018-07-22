using Eddo.Runtime.Session;

namespace Eddo.Applications.Services
{
    public abstract class ApplicationService: EddoServiceBase,IApplicationService
    {
        public IEddoSession EddoSession { get; set; }
        public ApplicationService()
        {
            EddoSession = NullEddoSession.Instance;
        }
    }
}
