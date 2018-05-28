using System.Collections.Generic;

namespace Eddo.Resources.Embedded
{
    public interface IEmbeddedResourcesConfiguration
    {
        List<EmbeddedResourceSet> Sources { get; }
    }
}