﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BugTracker.Models;

namespace BugTracker.Models
{
    public class UserRolesHelper

    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserRolesHelper(ApplicationDbContext context)
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            db = context;
        }
        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }
        public IList<string> ListUserRoles(string userId)
        {
            return userManager.GetRoles(userId);
        }
        public bool AddUserToRole (string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = userManager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }
        public IList<ApplicationUser> UsersInRole(string roleName)
        {
            var userIDs = roleManager.FindByName(roleName).Users.Select(r => r.UserId);
            return userManager.Users.Where(u => userIDs.Contains(u.Id)).ToList();
        }


        public IList<ApplicationUser>UsersNotInRole(string roleName)
        {
            var userIDs = Roles.GetUsersInRole(roleName);
            return userManager.Users.Where(u => !userIDs.Contains(u.Id)).ToList();
        }
    }
}