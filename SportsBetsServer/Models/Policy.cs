﻿using Microsoft.AspNetCore.Authorization;

namespace SportsBetsServer.Entities.Models
{
    public class Policy
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }
        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
        public static AuthorizationPolicy FallBackPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        }
    }
}
