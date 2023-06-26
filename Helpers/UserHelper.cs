using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace SitefinityWebApp.Helpers
{
    public class UserHelper
    {
        public static string GetCurrentUserNickname()
        {
            var current_identidy = ClaimsManager.GetCurrentIdentity();
            if (current_identidy != null)
            {
                UserProfileManager profileManager = UserProfileManager.GetManager();
                UserManager userManager = UserManager.GetManager();

                User user = userManager.GetUser(current_identidy.UserId);

                SitefinityProfile profile = null;

                if (user != null)
                {
                    profile = profileManager.GetUserProfile<SitefinityProfile>(user);

                    return profile.Nickname;
                }

            }
            return "User Not Found";
        }
    }
}