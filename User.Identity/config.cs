using System.Collections;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace User.Identity
{
    public class config
    {
        public static IEnumerable<ApiResource> GetResource()
        {
            return new List<ApiResource>{
                new ApiResource("api","My Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>{
                new Client{
                    ClientId="android",
                    ClientSecrets={
                        new Secret("secret".Sha256())
                    },
                    RefreshTokenExpiration=TokenExpiration.Sliding,
                    AllowOfflineAccess=true,
                    RequireClientSecret=false,
                    AllowedGrantTypes =new List<string>{ "sms_auth_code"},
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowedScopes=new List<string>{
                        "gateway_api",
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource> {
                new ApiResource("gateway_api","user service")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }



        public static List<TestUser> GetTestUesr()
        {
            return new List<TestUser>{
                new TestUser
                {
                    SubjectId = "10000",
                    Username = "Ian",
                    Password = "pwd"
                }
            };
        }

    }
}