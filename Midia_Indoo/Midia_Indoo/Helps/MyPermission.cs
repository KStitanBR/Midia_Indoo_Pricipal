using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace Midia_Indoo.Helps
{
    public class MyPermission : BasePermission
    {
        // This method checks if current status of the permission
        public override Task<PermissionStatus> CheckStatusAsync()
        {
            throw new System.NotImplementedException();
        }

        // This method is optional and a PermissionException is often thrown if a permission is not declared
        public override void EnsureDeclared()
        {
            throw new System.NotImplementedException();
        }

        // Requests the user to accept or deny a permission
        public override Task<PermissionStatus> RequestAsync()
        {
            throw new System.NotImplementedException();
        }

        public override bool ShouldShowRationale()
        {
            throw new NotImplementedException();
        }
    }
}
