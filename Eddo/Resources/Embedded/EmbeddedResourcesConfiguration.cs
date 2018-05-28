﻿using System.Collections.Generic;

namespace Eddo.Resources.Embedded
{
    public class EmbeddedResourcesConfiguration : IEmbeddedResourcesConfiguration
    {
        public List<EmbeddedResourceSet> Sources { get; }

        public EmbeddedResourcesConfiguration()
        {
            Sources = new List<EmbeddedResourceSet>();
        }
    }
}