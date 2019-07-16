using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Utils
{
    public class MSGraphClient
    {
        private GraphServiceClient graphServiceUserClient;
        private GraphServiceClient graphServiceClient;
        private readonly AuthenticationHelper authHelper;

        public TokenCache TokenCache { get; set; }

        public string Authority { get; set; }

        public MSGraphClient(string authority, TokenCache tokenCache)
        {
            this.Authority = authority;
            this.TokenCache = tokenCache;
            this.authHelper = new AuthenticationHelper(this.Authority, this.TokenCache);
        }

        /// <summary>
        /// Calls the Graph /me endpoint using OBO.
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetMeAsync()
        {
            User currentUserObject;

            try
            {
                GraphServiceClient graphClient = this.GetAuthenticatedClientForUser();
                currentUserObject = await graphClient.Me.Request().GetAsync();

                if (currentUserObject != null)
                {
                    Trace.WriteLine($"Got user: {currentUserObject.DisplayName}");
                }
            }
            catch (ServiceException e)
            {
                Trace.Fail($"We could not get the current user details: {e.Error.Message}");
                return null;
            }

            return currentUserObject;
        }

        public async Task<IList<Group>> GetCurrentUserGroupsAsync()
        {
            IUserMemberOfCollectionWithReferencesPage memberOfGroups = null;
            IList<Group> groups = new List<Group>();

            try
            {
                GraphServiceClient graphClient = this.GetAuthenticatedClientForUser();
                memberOfGroups = await graphClient.Me.MemberOf.Request().GetAsync();

                if (memberOfGroups != null)
                {
                    do
                    {
                        foreach (var directoryObject in memberOfGroups.CurrentPage)
                        {
                            if (directoryObject is Group)
                            {
                                Group group = directoryObject as Group;
                                Trace.WriteLine("Got group: " + group.Id);
                                groups.Add(group as Group);
                            }
                        }
                        if (memberOfGroups.NextPageRequest != null)
                        {
                            memberOfGroups = await memberOfGroups.NextPageRequest.GetAsync();
                        }
                        else
                        {
                            memberOfGroups = null;
                        }
                    } while (memberOfGroups != null);
                }

                return groups;
            }
            catch (ServiceException e)
            {
                Trace.Fail("We could not get user groups: " + e.Error.Message);
                return null;
            }
        }

        public async Task<IList<DirectoryRole>> GetCurrentUserDirectoryRolesAsync()
        {
            IUserMemberOfCollectionWithReferencesPage memberOfDirectoryRoles = null;
            IList<DirectoryRole> DirectoryRoles = new List<DirectoryRole>();

            try
            {
                GraphServiceClient graphClient = this.GetAuthenticatedClientForUser();
                memberOfDirectoryRoles = await graphClient.Me.MemberOf.Request().GetAsync();

                if (memberOfDirectoryRoles != null)
                {
                    do
                    {
                        foreach (var directoryObject in memberOfDirectoryRoles.CurrentPage)
                        {
                            if (directoryObject is DirectoryRole)
                            {
                                DirectoryRole DirectoryRole = directoryObject as DirectoryRole;
                                Trace.WriteLine("Got DirectoryRole: " + DirectoryRole.Id);
                                DirectoryRoles.Add(DirectoryRole as DirectoryRole);
                            }
                        }
                        if (memberOfDirectoryRoles.NextPageRequest != null)
                        {
                            memberOfDirectoryRoles = await memberOfDirectoryRoles.NextPageRequest.GetAsync();
                        }
                        else
                        {
                            memberOfDirectoryRoles = null;
                        }
                    } while (memberOfDirectoryRoles != null);
                }

                return DirectoryRoles;
            }
            catch (ServiceException e)
            {
                Trace.Fail("We could not get user DirectoryRoles: " + e.Error.Message);
                return null;
            }
        }

        
        public async Task<UserGroupsAndDirectoryRoles> GetCurrentUserGroupsAndRolesAsync()
        {
            UserGroupsAndDirectoryRoles userGroupsAndDirectoryRoles = new UserGroupsAndDirectoryRoles();
            IUserMemberOfCollectionWithReferencesPage memberOfDirectoryRoles = null;

            try
            {
                GraphServiceClient graphClient = this.GetAuthenticatedClientForUser();
                memberOfDirectoryRoles = await graphClient.Me.MemberOf.Request().GetAsync();

                if (memberOfDirectoryRoles != null)
                {
                    do
                    {
                        foreach (var directoryObject in memberOfDirectoryRoles.CurrentPage)
                        {
                            if (directoryObject is Group)
                            {
                                Group group = directoryObject as Group;
                                Trace.WriteLine($"Got group: {group.Id}- '{group.DisplayName}'");
                                userGroupsAndDirectoryRoles.Groups.Add(group);
                            }
                            else if (directoryObject is DirectoryRole)
                            {
                                DirectoryRole role = directoryObject as DirectoryRole;
                                Trace.WriteLine($"Got DirectoryRole: {role.Id}- '{role.DisplayName}'");
                                userGroupsAndDirectoryRoles.DirectoryRoles.Add(role);
                            }
                        }
                        if (memberOfDirectoryRoles.NextPageRequest != null)
                        {
                            userGroupsAndDirectoryRoles.HasOverageClaim = true;
                            memberOfDirectoryRoles = await memberOfDirectoryRoles.NextPageRequest.GetAsync();
                        }
                        else
                        {
                            memberOfDirectoryRoles = null;
                        }
                    } while (memberOfDirectoryRoles != null);
                }

                return userGroupsAndDirectoryRoles;
            }
            catch (ServiceException e)
            {
                Trace.Fail("We could not get user groups and roles: " + e.Error.Message);
                return null;
            }
        }

        public async Task<IList<string>> GetCurrentUserGroupIdsAsync()
        {
            IList<string> groupObjectIds = new List<string>();
            var groups = await this.GetCurrentUserGroupsAsync();

            return groups.Select(x => x.Id).ToList();
        }

  

        private GraphServiceClient GetAuthenticatedClientForUser()
        {
            if (this.graphServiceUserClient == null)
            {
                string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                // Create Microsoft Graph client.
                try
                {
                    this.graphServiceUserClient = new GraphServiceClient(ConfigHelper.MSGraphBaseUrl,
                                                                         new DelegateAuthenticationProvider(
                                                                             async (requestMessage) =>
                                                                             {
                                                                                 var token = await this.authHelper.GetOnBehalfOfAccessToken(ConfigHelper.GraphResourceId, ConfigHelper.PostLogoutRedirectUri);//"eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEQ29NcGpKWHJ4VHE5Vkc5dGUtN0ZYeTRpTTE2bnhwejkxazdLTUg5dWkzOWl4VkVvdlhSVXhpOXV1XzZFTHU1aVpFMXM5Y3dnT0NGVmxnY21MZDZTME40X0xjbGVEaDRsb18zVUYtdkNidFNBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiQ3RmUUM4TGUtOE5zQzdvQzJ6UWtacGNyZk9jIiwia2lkIjoiQ3RmUUM4TGUtOE5zQzdvQzJ6UWtacGNyZk9jIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8zYmUzNjIzYi00OTQ3LTQzNjgtYmMzNS1hODYyMjA1NDNjNTYvIiwiaWF0IjoxNTYxNjU1NjU5LCJuYmYiOjE1NjE2NTU2NTksImV4cCI6MTU2MTY1OTU0OCwiYWNjdCI6MCwiYWNyIjoiMCIsImFpbyI6IjQyWmdZT2dNbWZQMnplU2p5enI4YldyTWxQYnFCdHQ0V3ZPYXIva3E4b3p0VWNKemg4TUEiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IlByaXZhdG5pQ2Fzb3ZpQVBJIiwiYXBwaWQiOiJiODgxMjBjZS02NWQ1LTRhYWQtOGIxNC1hMjJmMzliOTNlOTQiLCJhcHBpZGFjciI6IjEiLCJmYW1pbHlfbmFtZSI6IlJ1dmNlc2tpIiwiZ2l2ZW5fbmFtZSI6IkZpbHAiLCJpcGFkZHIiOiIxMDkuOTIuMTY3LjE3OSIsIm5hbWUiOiJGaWxpcCBSdXZjZXNraSIsIm9pZCI6IjFjN2MwOTVkLTc3MGUtNGY5Yi04NjZlLTkzM2I4YWU3ODk2MiIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMDRFMkIxQUNDIiwic2NwIjoiRGlyZWN0b3J5LlJlYWQuQWxsIFVzZXIuUmVhZCIsInN1YiI6ImJud20xUmZJNDZrT2RHUmo3R3gzcVVUWXdMRkNTUzBhdUs2QmdyWmZKS0EiLCJ0aWQiOiIzYmUzNjIzYi00OTQ3LTQzNjgtYmMzNS1hODYyMjA1NDNjNTYiLCJ1bmlxdWVfbmFtZSI6ImZpbGlwcnV2Y2Vza2lAcHJpdmF0bmljYXNvdmkub25taWNyb3NvZnQuY29tIiwidXBuIjoiZmlsaXBydXZjZXNraUBwcml2YXRuaWNhc292aS5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJGSk5CaDlCZW1VQ1NQeFhYaFZGWEFBIiwidmVyIjoiMS4wIiwieG1zX3RjZHQiOjE1NjEzNzg4Njl9.NOkhOHwuRm9e8w-x_dFlGedEjDBA2B4nlZFHbDc-9pzh-A1LLkETkrTDO7n6wiJ7s-nonVrs7p1bwctKVkG-43xpJE8EmbCoUbhuluiB10WvkRXcaRZ6wIpjCOuajpY3KapAHUS5MmwgW14zRIyRCLP3ykU9cfxT2NLkTLkfY394cNvSvwmkYVV99ZDbtf6qNLHpLKsjUzTQsJxnR09MUdVZKAIXEw4tU-tOR_5i8jAFE4bTbH98Kz-HJfvVOcIbJkd5EtuI9YBcL9SnFjyh8oGCjka7EEOJUlsnmLq5UmreDxUHJAsIRlo7ZSAMXcR3WvJ2KPKD6oAi1FkIZkOhFQ";
                                                                                 requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                                             }));
                }
                catch (Exception ex)
                {
                    Trace.Fail($"Could not create a graph client {ex}");
                }
            }

            return this.graphServiceUserClient;
        }

        private GraphServiceClient GetAuthenticatedClientForApp()
        {
            if (this.graphServiceClient == null)
            {
                // Create Microsoft Graph client.
                try
                {
                    this.graphServiceClient = new GraphServiceClient(ConfigHelper.MSGraphBaseUrl,
                                                                     new DelegateAuthenticationProvider(
                                                                         async (requestMessage) =>
                                                                         {
                                                                             var token = await this.authHelper.GetOnBehalfOfAccessToken(ConfigHelper.GraphResourceId, ConfigHelper.PostLogoutRedirectUri);//"eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEQ29NcGpKWHJ4VHE5Vkc5dGUtN0ZYeTRpTTE2bnhwejkxazdLTUg5dWkzOWl4VkVvdlhSVXhpOXV1XzZFTHU1aVpFMXM5Y3dnT0NGVmxnY21MZDZTME40X0xjbGVEaDRsb18zVUYtdkNidFNBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiQ3RmUUM4TGUtOE5zQzdvQzJ6UWtacGNyZk9jIiwia2lkIjoiQ3RmUUM4TGUtOE5zQzdvQzJ6UWtacGNyZk9jIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8zYmUzNjIzYi00OTQ3LTQzNjgtYmMzNS1hODYyMjA1NDNjNTYvIiwiaWF0IjoxNTYxNjU1NjU5LCJuYmYiOjE1NjE2NTU2NTksImV4cCI6MTU2MTY1OTU0OCwiYWNjdCI6MCwiYWNyIjoiMCIsImFpbyI6IjQyWmdZT2dNbWZQMnplU2p5enI4YldyTWxQYnFCdHQ0V3ZPYXIva3E4b3p0VWNKemg4TUEiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IlByaXZhdG5pQ2Fzb3ZpQVBJIiwiYXBwaWQiOiJiODgxMjBjZS02NWQ1LTRhYWQtOGIxNC1hMjJmMzliOTNlOTQiLCJhcHBpZGFjciI6IjEiLCJmYW1pbHlfbmFtZSI6IlJ1dmNlc2tpIiwiZ2l2ZW5fbmFtZSI6IkZpbHAiLCJpcGFkZHIiOiIxMDkuOTIuMTY3LjE3OSIsIm5hbWUiOiJGaWxpcCBSdXZjZXNraSIsIm9pZCI6IjFjN2MwOTVkLTc3MGUtNGY5Yi04NjZlLTkzM2I4YWU3ODk2MiIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMDRFMkIxQUNDIiwic2NwIjoiRGlyZWN0b3J5LlJlYWQuQWxsIFVzZXIuUmVhZCIsInN1YiI6ImJud20xUmZJNDZrT2RHUmo3R3gzcVVUWXdMRkNTUzBhdUs2QmdyWmZKS0EiLCJ0aWQiOiIzYmUzNjIzYi00OTQ3LTQzNjgtYmMzNS1hODYyMjA1NDNjNTYiLCJ1bmlxdWVfbmFtZSI6ImZpbGlwcnV2Y2Vza2lAcHJpdmF0bmljYXNvdmkub25taWNyb3NvZnQuY29tIiwidXBuIjoiZmlsaXBydXZjZXNraUBwcml2YXRuaWNhc292aS5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJGSk5CaDlCZW1VQ1NQeFhYaFZGWEFBIiwidmVyIjoiMS4wIiwieG1zX3RjZHQiOjE1NjEzNzg4Njl9.NOkhOHwuRm9e8w-x_dFlGedEjDBA2B4nlZFHbDc-9pzh-A1LLkETkrTDO7n6wiJ7s-nonVrs7p1bwctKVkG-43xpJE8EmbCoUbhuluiB10WvkRXcaRZ6wIpjCOuajpY3KapAHUS5MmwgW14zRIyRCLP3ykU9cfxT2NLkTLkfY394cNvSvwmkYVV99ZDbtf6qNLHpLKsjUzTQsJxnR09MUdVZKAIXEw4tU-tOR_5i8jAFE4bTbH98Kz-HJfvVOcIbJkd5EtuI9YBcL9SnFjyh8oGCjka7EEOJUlsnmLq5UmreDxUHJAsIRlo7ZSAMXcR3WvJ2KPKD6oAi1FkIZkOhFQ";
                                                                             requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                                         }));
                }
                catch (Exception ex)
                {
                    Trace.Fail($"Could not create a graph client {ex}");
                }
            }

            return this.graphServiceClient;
        }
    }
}
