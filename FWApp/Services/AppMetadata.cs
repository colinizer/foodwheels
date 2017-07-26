using FWApp.Services;
using FWCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Windows.ApplicationModel;

namespace FWApp.Services
{
    public class AppMetadataService : IAppMetadata
    {
        public string GetVersion()
        {
#if true
            // Get from Package.appxmanifest

            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
#else
            // Get from AssemblyInfo.cs

            return new System.Reflection.AssemblyName(this.GetType().GetTypeInfo().Assembly.FullName).Version.ToString();
#endif
        }
    }
}
