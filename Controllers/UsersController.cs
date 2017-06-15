using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class UsersController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext(); //static may create problems if this instance of the DB never changes  - if so, take static off and instantiate within each action
        UserRolesHelper helper = new UserRolesHelper(db);
        ProjectAssignHelper pHelper = new ProjectAssignHelper();

        // GET: Users
        [Authorize(Roles = "Admin, Administrator, Developer1, Developer2, Developer3, Developer4, Project Manager1, Project Manager2, Project Manager3")]
        public ActionResult Index()
        {
            List<UsersRolesViewModel> userList = new List<UsersRolesViewModel>();
            foreach(var user in db.Users.ToList())
                
            {
                UsersRolesViewModel uservm = new UsersRolesViewModel();
                uservm.User = user;
                uservm.Roles = helper.ListUserRoles(user.Id);
                uservm.Projectsb = pHelper.ListProjectsForAUser(user.Id);
                uservm.Tickets = new List<Ticket>();
                foreach (var project in uservm.Projectsb) { 
                    foreach (var ticket in project.Tickets)
                        {
                        if (ticket.AssignedToUserId == user.Id)
                        {
                            uservm.Tickets.Add(ticket);
                        
                        }
                    }
                

                }
                
                userList.Add(uservm);
            }
            return View(userList);
        }
        //GET
        public ActionResult Details(string Id)
          

        {
            ProjectAssignHelper ph = new ProjectAssignHelper();
            UserRolesHelper UH = new UserRolesHelper(db);
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            
            if (UH.IsUserInRole(userId, "Admin") || UH.IsUserInRole(userId, "Administrator") || userId == Id)
            {


                if (Id != null)
                {
                    UsersRolesViewModel userRoleViewModel2 = new UsersRolesViewModel();
                    userRoleViewModel2.User = db.Users.Find(Id);
                    userRoleViewModel2.Roles = helper.ListUserRoles(Id);
                    userRoleViewModel2.Tickets = db.Tickets.ToList();
                    userRoleViewModel2.Projectsb = db.Projects.ToList();
                    
                    //userRoleViewModel.Projects = helper.
                    var SubmitterlessRolesList = db.Roles.Where(r => r.Name != "Submitter");
                    ViewBag.Roles = new MultiSelectList(SubmitterlessRolesList, "Name", "Name");

                    //ViewBag.Roles = new MultiSelectList(db.Topics, "Id", "TopicName");

                    return View(userRoleViewModel2);


                }
                else
                {
                    UsersRolesViewModel userRoleViewModel = new UsersRolesViewModel();
                    userRoleViewModel.User = db.Users.Find(userId);
                    userRoleViewModel.Roles = helper.ListUserRoles(userId);
                    userRoleViewModel.Tickets = db.Tickets.ToList();
                    List<Project> UserProjects = new List<Project>();
                    foreach (var project in db.Projects)
                    {
                        if (ph.IsUserOnAProject(userId, project.Id))
                        {
                            UserProjects.Add(project);
                        }
                    }
                    userRoleViewModel.Projectsb = UserProjects;
                    //userRoleViewModel.Projects = helper.
                    var SubmitterlessRolesList = db.Roles.Where(r => r.Name != "Submitter");
                    ViewBag.Roles = new MultiSelectList(SubmitterlessRolesList, "Name", "Name");

                    //ViewBag.Roles = new MultiSelectList(db.Topics, "Id", "TopicName");

                    return View(userRoleViewModel);
                }
               
            }
            var Temporary = "You cannot view Details of this User.  Please revisit your role assignment.  You may view your own user details, but other user's details can only be viewed by Administrators. ";
            TempData["message"] = Temporary;
            return RedirectToAction("Index", "Users");
        }

        //POST
        [Authorize(Roles = "Admin, Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "Roles")] UsersRolesViewModel model, string Id)


        {


            if (ModelState.IsValid)
            {
                var currentRoles = helper.ListUserRoles(Id);
                foreach(var ExistingRoles in currentRoles)
                    
                {
                    if (ExistingRoles != "Submitter")
                    {
                        helper.RemoveUserFromRole(Id, ExistingRoles);
                    }
                }
                               
                //db.Users.Add(model.User);
                //db.SaveChanges();

                if (model.Roles.Any())
                {
                    foreach (var role in model.Roles)
                    {
                        if (role != "Submitter")
                        {
                            //var userId = model.User.Id;
                            helper.AddUserToRole(Id, role);
                            db.SaveChanges();
                            //var topics = db.Topics.AsNoTracking().FirstOrDefault(t => t.Id == topicId);
                            //model.Post.Topics.Add(topics);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
                UsersRolesViewModel userRoleViewModel = new UsersRolesViewModel();
                userRoleViewModel.User = db.Users.Find(Id);
                userRoleViewModel.Roles = helper.ListUserRoles(Id);
                ViewBag.Roles = new MultiSelectList(db.Roles, "Name", "Name");
            return View(model);
        }


    }
}