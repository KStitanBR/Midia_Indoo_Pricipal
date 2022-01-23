using System;
using System.Collections.Generic;
using System.Text;

namespace Midia_Indoo.Helps.IDependecyService
{
    public interface IDeviceSdkService
    {
        bool VersionSdk11();
        string GetRootLocalPath();
    }
}
