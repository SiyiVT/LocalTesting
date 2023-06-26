using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace SitefinityWebApp.Helpers
{
    public class RoleHelper
    {
        public static IQueryable<Role> GetAllRoles()
        {
            RoleManager roleManager = RoleManager.GetManager();

            return roleManager.GetRoles();
        }

        public static Role GetRoleByName(string roleName)
        {
            RoleManager roleManager = RoleManager.GetManager();

            return roleManager.GetRoles().Where(r => r.Name == roleName).SingleOrDefault();
        }

        public static bool IsUserInRole(string userId, string roleName)
        {
            bool isUserInRole = false;

            RoleManager roleManager = RoleManager.GetManager(SecurityConstants.ApplicationRolesProviderName);

            bool roleExists = roleManager.RoleExists(roleName);

            if (!string.IsNullOrEmpty(userId) && roleExists)
            {
                isUserInRole = roleManager.IsUserInRole(new Guid(userId), roleName);
            }

            return isUserInRole;
        }

        public static SitefinityIdentity GetCurrentIdentity()
        {
            return ClaimsManager.GetCurrentIdentity();
        }

        public static string[] GetAuthirizedModule()
        {
            var current_identidy = ClaimsManager.GetCurrentIdentity();
            foreach (var permission in IHHConstants.IHHRolePermissions)
            {
                if (current_identidy.Roles.Any(x => x.Name.Contains(permission.Key)))
                {
                    return permission.Value;
                }
            }
            return new string[] { };
        }

        public static bool IsAdminstrator()
        {
            var current_identidy = ClaimsManager.GetCurrentIdentity();
            return current_identidy.Roles.Any(x => x.Name == "Administrators");
        }

        //public static string GetHospitalNameFromRole()
        //{
        //    var current_identidy = ClaimsManager.GetCurrentIdentity();
        //    foreach (var codeAndName in GleneaglesConstants.GlenHospitalCodeAndName)
        //    {
        //        if (current_identidy.Roles.Any(x => x.Name.Contains(codeAndName.Key)))
        //        {
        //            return codeAndName.Value;
        //        }
        //    }
        //    return String.Empty;
        //}

        //public static string GetRoleNameByHospital(string roleType, string hospName)
        //{
        //    hospName = FrontendUrlHelper.GetHospNameFromSrting(hospName);
        //    var roles = GleneaglesConstants.HospitalAndRoles.GetValueOrNull(hospName);
        //    if (roles != null)
        //    {
        //        foreach (var role in roles)
        //        {
        //            if (role.Contains(roleType))
        //            {
        //                return role;
        //            }
        //        }
        //    }
        //    return String.Empty;
        //}
    }
}