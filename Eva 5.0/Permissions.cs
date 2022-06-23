using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;

namespace Eva_5._0
{
    internal class Permissions<File>
    {
        public Permissions(File file)
        {
            GrantAccess(file as string);
        }

        private void GrantAccess<Directory_File>(Directory_File file)
        {
            try
            {

                DirectoryInfo dInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\" + file);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();



                AuthorizationRuleCollection permissions = dSecurity.GetAccessRules(true, true, typeof(NTAccount));




                if(permissions[0].IdentityReference.ToString() != "Everyone")
                {


                    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);




                    if (((FileSystemAccessRule)permissions[0]).FileSystemRights != FileSystemRights.FullControl)
                    {
                        dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                        dInfo.SetAccessControl(dSecurity);
                    }

                }


            }
            catch { }
        }


        ~Permissions()
        { }
    }
}
