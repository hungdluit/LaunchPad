﻿using System;
using System.Linq;
using System.Threading.Tasks;
using LaunchPad.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LaunchPad.Policies
{
    //Todo: The only change in the authorization handlers is the role name, let's see if we can refactor to one and pass the required role name in Startup.cs
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly ApplicationDbContext _context;

        public RoleHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RoleRequirement requirement)
        {
            var user = await _context.Users
                .Include(ur => ur.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == context.User.Identity.Name);

            if (user == null)
                return;

            var userIsAdmin = user.UserRoles.Any(ur => ur.Role.Name == requirement.Role);

            if (userIsAdmin)
            {
                context.Succeed(requirement);
            }
        }
    }
}
