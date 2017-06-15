using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectAssignHelper helper = new ProjectAssignHelper();
        [Authorize]
        // GET: Projects
        public ActionResult Index()

        {

            UserRolesHelper helper = new UserRolesHelper(db);
            ProjectAssignHelper ph = new ProjectAssignHelper();
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var userroleAdmin = helper.IsUserInRole(userId, "Admin") || helper.IsUserInRole(userId, "Administrator");
            var userroleDeveloper = helper.IsUserInRole(userId, "Developer1") || helper.IsUserInRole(userId, "Developer2") || helper.IsUserInRole(userId, "Developer3") || helper.IsUserInRole(userId, "Developer4");
            var userroleProjectManager = helper.IsUserInRole(userId, "Project Manager1") || helper.IsUserInRole(userId, "Project Manager2") || helper.IsUserInRole(userId, "Project Manager3");
            var userroleSubmitter = helper.IsUserInRole(userId, "Submitter");
            List<Project> DeveloperProjects = new List<Project>();
            List<ApplicationUser> Developers = helper.UsersInRole("Developer1").Concat(helper.UsersInRole("Developer2")).Concat(helper.UsersInRole("Developer3")).Concat(helper.UsersInRole("Developer4")).ToList();
            var projects = db.Projects.Include(p => p.Users).ToList();


            
            ViewBag.user = user.FirstName + " " + user.LastName;
           


            List<Ticket> usertickets = new List<Ticket>();
            List<Project> userprojects = new List<Project>();
            if (helper.IsUserInRole(userId, "Admin") || helper.IsUserInRole(userId, "Administrator"))
            { 
                 foreach (var project in db.Projects)
                 {
                      
                            userprojects.Add(project);
                    
                 }

            
                foreach (var ticket in db.Tickets)
                {

                    usertickets.Add(ticket);
                }
                var nonarchivedtickets = usertickets.Where(t => t.TicketStatus.Name != "Archived");
                var urgenttickets = nonarchivedtickets.Where(t => t.TicketPriority.Name == "Urgent");
                var unassignedtickets = nonarchivedtickets.Where(t => t.TicketStatus.Name == "Unassigned");
                ViewBag.unassignedticketcount = unassignedtickets.Count();
                ViewBag.ticketcount = nonarchivedtickets.Count();
                ViewBag.urgentticketcount = urgenttickets.Count();
                ViewBag.projectcount = userprojects.Count();
                ViewBag.message = "in the Bug Tracker system currently.";
                ViewBag.ticketmessage = "in the Bug Tracker system currently.";
                ViewBag.unassignedmessage = "Unassigned tickets in the Bug Tracker system";
                if (urgenttickets.Count() == 0)
                {
                    ViewBag.urgentmessage = "There are no urgent tickets.";
                }
                else
                {
                    ViewBag.urgentmessage = "Please attend to these tickets if you are assigned to them.";
                }
                return View(userprojects);
            }

            else if (userroleProjectManager)
            {

              
                foreach (var x in db.Projects)
                {

                    if (ph.IsUserOnAProject(userId, x.Id))
                    {
                        userprojects.Add(x);
                    }

                }

                foreach (var project in userprojects)
                {
                    foreach (var ticket in project.Tickets)
                    {
                        usertickets.Add(ticket);
                    }

                }
                var nonarchivedtickets = usertickets.Where(t => t.TicketStatus.Name != "Archived");
                var urgenttickets = nonarchivedtickets.Where(t => t.TicketPriority.Name == "Urgent");
                var unassignedtickets = nonarchivedtickets.Where(t => t.TicketStatus.Name == "Unassigned");
                ViewBag.unassignedticketcount = unassignedtickets.Count();
                ViewBag.ticketcount = nonarchivedtickets.Count();
                ViewBag.urgentticketcount = urgenttickets.Count();
                ViewBag.projectcount = userprojects.Count();
                ViewBag.ticketmessage = "for projects that you're working on.";
                ViewBag.message = "that you're working on.";
                ViewBag.unassignedmessage = "Unassigned tickets in the Bug Tracker system";
                if (urgenttickets.Count() == 0)
                {
                    ViewBag.urgentmessage = "There are no urgent tickets.";
                }
                else
                {
                    ViewBag.urgentmessage = "Please attend to these tickets if you are assigned to them.";
                }

                return View(userprojects);
            }


            else if (userroleDeveloper)
            {

                
                foreach (var x in db.Projects)
                {

                    if (ph.IsUserOnAProject(userId, x.Id))
                    {
                        userprojects.Add(x);
                    }

                }

                foreach (var project in userprojects)
                {
                    foreach (var ticket in project.Tickets)
                    {
                        usertickets.Add(ticket);
                    }

                }

                var nonarchivedtickets = usertickets.Where(t => t.TicketStatus.Name != "Archived");
                var urgenttickets = nonarchivedtickets.Where(t => t.TicketPriority.Name == "Urgent");

                var unassignedtickets = nonarchivedtickets.Where(t => t.TicketStatus.Name == "Unassigned");
                ViewBag.unassignedticketcount = unassignedtickets.Count();
                ViewBag.ticketcount = nonarchivedtickets.Count();
                ViewBag.urgentticketcount = urgenttickets.Count();
                var urgentticketscount = urgenttickets.Count();
                ViewBag.projectcount = userprojects.Count();
                ViewBag.unassignedmessage = "Unassigned tickets in the Bug Tracker system";
                ViewBag.ticketmessage = "for projects that you're working on.";
                ViewBag.message = "that you're working on.";
                if (urgentticketscount == 0)
                {
                    ViewBag.urgentmessage = "There are no urgent tickets.";
                }
                else
                {
                    ViewBag.urgentmessage = "Please attend to these tickets if you are assigned to them.";
                }
                return View(userprojects);
                
            }

            else if (userroleSubmitter)
            {
                var tickets = db.Tickets.Where(t => t.SubmitterUserId == userId).ToList();

                foreach (var x in db.Projects)
                {

                    if (ph.IsUserOnAProject(userId, x.Id))
                    {
                        userprojects.Add(x);
                    }

                }



                var nonarchivedtickets = tickets.Where(t => t.TicketStatus.Name != "Archived");
                 var urgenttickets = nonarchivedtickets.Where(t => t.TicketPriority.Name == "Urgent");
                var unassignedtickets = nonarchivedtickets.Where(t => t.TicketStatus.Name == "Unassigned");
                ViewBag.unassignedticketcount = unassignedtickets.Count();
                ViewBag.ticketcount = nonarchivedtickets.Count();
                ViewBag.urgentticketcount = urgenttickets.Count();
                ViewBag.projectcount = userprojects.Count();
                ViewBag.ticketmessage = "that you've submitted.";
                ViewBag.projectmessage = "N/A";
                ViewBag.urgentmessage = "N/A";
               ViewBag.unassignedmessage = "Unassigned tickets in the Bug Tracker system";

                return View(userprojects);
            }
            return RedirectToAction("Index", "Home");
        }
       











        public PartialViewResult SidebarPartial()
        {

            return PartialView("~/Views/Projects/_SidebarPartial.cshtml");
        }



        public ActionResult MyProjects()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            //var usersonproject = helper.ListUsersOnAProject((int)Id);
            //var allprojects = db.Projects.Where(u => u.Users == user);

            //List<ApplicationUser> ProjectUsers= new List<ApplicationUser>();

            //foreach (var u in allprojects)
            //{
            //    ProjectUsers.Add(u.Users
            //}

            //return 



            //var allusers = allprojects.
            ////var users = db.Users.Where(t => t.Projects == user.Projects).ToList();
            //ViewBag.Users = users;
            return View(user.Projects.ToList());
        }


        [Authorize]
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        [Authorize (Roles = "Admin, Administrator, Project Manager1, Project Manager2, Project Manager3")]
        // GET: Projects/Create
        public ActionResult Create()


        {
            List<ApplicationUser> ProjectManagerId = new List<ApplicationUser>();
            UserRolesHelper helper = new UserRolesHelper(db);
            ProjectAssignHelper ph = new ProjectAssignHelper();
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
          


            var PMs = helper.UsersInRole("Project Manager1");
            var PMs2 = helper.UsersInRole("Project Manager2");
            var PMs3 = helper.UsersInRole("Project Manager3");
            var AD = helper.UsersInRole("Admin");
            var AD2 = helper.UsersInRole("Administrator");
            foreach (var PM in PMs)
            {
                ProjectManagerId.Add(PM);
            }
            foreach (var PM in PMs2)
            {
                ProjectManagerId.Add(PM);
            }
            foreach (var PM in PMs3)
            {
                ProjectManagerId.Add(PM);
            }

            ViewBag.ProjectManagerId = new SelectList(ProjectManagerId, "Id", "FirstName");

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name, ProjectManagerId")] Project project)
        {
            ProjectAssignHelper PA = new ProjectAssignHelper();
            if (ModelState.IsValid)
            {
                
                int openId = db.ProjectStatuses.FirstOrDefault(p=>p.Name == "Open").Id;
                project.ProjectStatusId = openId;
                project.ProjectStartDate = DateTime.Now;
            

                
                var user = db.Users.Find(project.ProjectManagerId);
                project.ProjectManagerName = user.FirstName + " " + user.LastName;
                db.Projects.Add(project);
                db.SaveChanges();

                if (project.ProjectManagerId != null)
                {
                    PA.AddUserToAProject(project.ProjectManagerId, project.Id);
                }

               
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            return View(project);
        }

        [Authorize(Roles = "Admin, Administrator, Project Manager1, Project Manager2, Project Manager3")]
        public ActionResult ProjectAssign(int? Id)


        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(Id);
            if (project == null)
            {
                return HttpNotFound();
            }

            if (project.ProjectManagerId == userId ||  User.IsInRole("Admin") || User.IsInRole("Administrator"))
            {
                ProjectAssignViewModel pa = new ProjectAssignViewModel();
                pa.projectId = Id;
                pa.selectedvalue = helper.ListUsersOnAProject((int)Id).Select(u => u.Id).ToArray();
                pa.Users = new MultiSelectList(db.Users, "Id", "FirstName", pa.selectedvalue);


                return View(pa);
            }

            else
            {
                var Temporary = "You must be an Admin or the project manager for this project in order to assign developers.";
                TempData["message"] = Temporary;
                return RedirectToAction("Index", "Projects");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectAssign(ProjectAssignViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUsers = helper.ListUsersOnAProject((int)model.projectId);

                
                foreach (var user in currentUsers)
                {
                    
                    helper.RemoveUserFromAProject(user.Id, (int)model.projectId);
                }

                //db.Users.Add(model.User);
                //db.SaveChanges();

                if (model.selectedvalue.Any())
                {
                    foreach (var user in model.selectedvalue)
                    {
                        //var userId = model.User.Id;
                        helper.AddUserToAProject (user, (int)model.projectId);
                        db.SaveChanges();
                        //var topics = db.Topics.AsNoTracking().FirstOrDefault(t => t.Id == topicId);
                        //model.Post.Topics.Add(topics);
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ProjectAssign([Bind(Include = "First Name")] ApplicationUser Users, int Id)


        //{


        //    if (ModelState.IsValid)
        //    {

        //        //db.Users.Add(model.User);
        //        //db.SaveChanges();

        //        if (Users.Id.Any())
        //        {
        //            foreach (var User in Users.Projects)
        //            {
        //                //var userId = model.User.Id;
        //                helper.AddUserToRole(Id, db.Roles.Find(role).Name);
        //                db.SaveChanges();
        //                //var topics = db.Topics.AsNoTracking().FirstOrDefault(t => t.Id == topicId);
        //                //model.Post.Topics.Add(topics);
        //            }
        //        }

        //        UsersRolesViewModel userRoleViewModel = new UsersRolesViewModel();
        //        userRoleViewModel.User = db.Users.Find(Id);
        //        userRoleViewModel.Roles = helper.ListUserRoles(Id);
        //        ViewBag.Roles = new MultiSelectList(db.Roles, "Id", "Name");

        //        //ViewBag.Roles = new MultiSelectList(db.Topics, "Id", "TopicName");

        //        return View(userRoleViewModel);


        //    }
        //    ViewBag.Topics = new MultiSelectList(db.Roles, "Id", "Description");
        //    return View(model);
        //}






        [Authorize(Roles = "Admin, Administrator, Project Manager1, Project Manager2, Project Manager3")]
        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            UserRolesHelper helper = new UserRolesHelper(db);
            ProjectAssignHelper ph = new ProjectAssignHelper();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            if (!helper.IsUserInRole(User.Identity.GetUserId(), "Administrator") || !helper.IsUserInRole(User.Identity.GetUserId(), "Admin") || !helper.IsUserInRole(User.Identity.GetUserId(), "Project Manager1") || !helper.IsUserInRole(User.Identity.GetUserId(), "Project Manager2") || !helper.IsUserInRole(User.Identity.GetUserId(), "Project Manager3"))
            {
                if (!db.Users.Find(User.Identity.GetUserId()).Projects.Contains(project))
                {
                    return RedirectToAction("Index");
                }
            }
            List<ApplicationUser> PMList = new List<ApplicationUser>();

            var PM1 = helper.UsersInRole("Project Manager1");
            var PM2 = helper.UsersInRole("Project Manager2");
            var PM3 = helper.UsersInRole("Project Manager3");

            PM1.Concat(PM2).Concat(PM3);
            List<string> Statuses = new List<string>();
            foreach (var statusname in db.Projects)
            {
                if (!Statuses.Contains(statusname.ProjectStatus))
                {
                    Statuses.Add(statusname.ProjectStatus);
                }

            }
          
           
            //ViewBag.SubmitterUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatuses, "Id","Name");
            //ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.ProjectManagerId = new SelectList(PM1, "Id", "FirstName");
            //ViewBag.TicketTitle = new SelectList(db.Tickets, "Id", "Name");
            //ViewBag.TicketDescription = new SelectList(db.TicketTypes, "Id", "Name");
            return View(project);
        }
        
        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProjectManagerId, ProjectStatusId, ProjectStatusName, ")] Project project)
        {
            if (ModelState.IsValid)
            {

                var projectstatus = db.ProjectStatuses.Find(project.ProjectStatusId);
                project.ProjectStatus = projectstatus.Name;
                var User = db.Users.Find(project.ProjectManagerId);
                project.ProjectManagerName = User.FirstName + " " +User.LastName;

               
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }
        [Authorize]
        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
