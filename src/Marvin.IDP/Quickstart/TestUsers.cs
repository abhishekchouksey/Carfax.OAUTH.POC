// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "1234",
                        Username = "Frank",
                        Password = "password",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.GivenName, "Frank"),
                            new Claim(JwtClaimTypes.FamilyName, "Underwood"),
                            new Claim(JwtClaimTypes.Address, "123 anyStreet"),
                            new Claim(JwtClaimTypes.Role, "Freeuser"),
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "5678",
                        Username = "Clair",
                        Password = "password",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.GivenName, "Clair"),
                            new Claim(JwtClaimTypes.FamilyName, "Underwood"),
                            new Claim(JwtClaimTypes.Address, "123 unknown street"),
                            new Claim(JwtClaimTypes.Role, "paiduser"),
                        }
                    }
                };
            }
        }
    }
}