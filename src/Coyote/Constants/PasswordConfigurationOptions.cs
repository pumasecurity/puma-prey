﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Coyote.Constants
{
    public class PasswordConfigurationOptions
    {
        public static IdentityOptions PasswordOptions => new IdentityOptions
        {
            Password = new PasswordOptions
            {
                RequireDigit = false,
                RequiredLength = 6,
                RequireNonAlphanumeric = false,
                RequireUppercase = true,
                RequireLowercase = true,
            },
            Lockout = new LockoutOptions
            {
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1440),
                MaxFailedAccessAttempts = 5
            },
            User = new UserOptions
            {
                RequireUniqueEmail = true
            }
        };
    }
}