﻿using System.Web.Optimization;

namespace DoSoft.Web
{
    public static class BundleExtensions
    {
        public static Bundle ForceOrdered(this Bundle bundle)
        {
            bundle.Orderer = new AsIsBundleOrderer();
            return bundle;
        }
    }
}