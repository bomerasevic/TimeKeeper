﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeKeeper.DAL;
namespace TimeKeeper.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            List<TestUser> users = new List<TestUser>();
            using (UnitOfWork unit = new UnitOfWork(new TimeKeeperContext()))
            {
                foreach (var user in unit.Users.Get())
                {
                    List<string> teamList = unit.Members.Get().Where(m => m.Employee.Id == user.Id).Select(t => t.Id.ToString()).ToList();
                    string teams = string.Join(",", teamList);
                    users.Add(new TestUser
                    {
                        SubjectId = user.Id.ToString(),
                        Username = user.Username,
                        Password = user.Password,
                        Claims = new List<Claim>
                        {
                            new Claim("name", user.Name),
                            new Claim("role", user.Role),
                            new Claim("team", teams)
                        }
                    });
                }
            }
            return users;
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "Your roles", new List<string>{ "role" }),
                new IdentityResource("names", "Your names", new List<string>{ "name" }),
                new IdentityResource("teams", "Your engagement(s)", new List<string>{ "team" })
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("timekeeper", "Time Keeper API", new List<string> { "role"})
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "TimeKeeper",
                    ClientId = "tk2019",
                    ClientSecrets = { new Secret("mistral_talents".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false, // da ne izlazi svaki put da li se slazete... :)
                    //RedirectUris = {"https://localhost:44350/signin-oidc"},
                    RedirectUris = {"http://localhost:3000/auth-callback"},
                    //PostLogoutRedirectUris = {"https://localhost:44350/signout-callback-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:3000"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "names",
                        "timekeeper",
                        "teams"
                    },
                    AllowOfflineAccess= true,
                    AllowAccessTokensViaBrowser = true,
                    //AllowedCorsOrigins = {"http://localhost:44350/" },
                    AllowedCorsOrigins = { "http://localhost:3000", "http://localhost:3000", "https://localhost:44350", "http://192.168.60.72" },
                    AccessTokenLifetime = 3600 // 1h trajanje tokena                   
                }
            };
        }
    }
}
