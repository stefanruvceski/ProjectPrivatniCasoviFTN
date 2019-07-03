using Common.Models;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class AuthorizationHelper
    {
        public static async Task<bool> IsInGroup(string role)
        {
            try
            {
                MSGraphClient msGraphClient = new MSGraphClient(ConfigHelper.Authority, new ADALTokenCache(Util.GetSignedInUsersObjectIdFromClaims()));

                //User user = await msGraphClient.GetMeAsync();
                UserGroupsAndDirectoryRoles userGroupsAndDirectoryRoles = await msGraphClient.GetCurrentUserGroupsAndRolesAsync();

                IList<Group> groups = await msGraphClient.GetCurrentUserGroupsAsync();
                List<String> g = groups.ToList().Select(x => x.DisplayName).ToList();

                return g.Contains(role);
            }
            catch (AdalException)
            {
                // Return to error page.
                return false;
            }
        }

        public static async Task<string> GetGroupName()
        {
            MSGraphClient msGraphClient = new MSGraphClient(ConfigHelper.Authority, new ADALTokenCache(Util.GetSignedInUsersObjectIdFromClaims()));

            //User user = await msGraphClient.GetMeAsync();
            UserGroupsAndDirectoryRoles userGroupsAndDirectoryRoles = await msGraphClient.GetCurrentUserGroupsAndRolesAsync();

            IList<Group> groups = await msGraphClient.GetCurrentUserGroupsAsync();
            List<String> g = groups.ToList().Select(x => x.DisplayName).ToList();

            return g[0];
        }
    }
}
