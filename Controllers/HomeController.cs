using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SystemUpdatesPartial()
        {
            return PartialView("~/Views/Home/_SystemUpdatesPartialView.cshtml", db.SystemUpdates.ToList());
        }
        public ActionResult Landing()
        {
            List<TicketData> TicketChartList = new List<TicketData>();

  
            //so it is in the same controller 
            // ..  i was trying to get ticket data per day - monday, tuesday wednesday, etc

            //and find the number of tickets submitted that day and what their status was.  

            //so like monday, 2 urgent tickets, 3 normal tickets, etc
            //tuesday, 1 urgent ticket, 5 normal tickets, etc
            //is that making sense? yeah that looks good
            //the only problem is that i had to hard code the values...lol so they are basically made up
            //see?  // I know this is not good but I could not figure date/time out where it would let me subtract back to the previous monday...which may be more complicated that what you want to do.. i don know
            //ideally the chart would always display the previous 5 days mon-fri ticket submissions
            //okay in that case you can check the day of week is completed with entire 5 days only then you can replace the data entirely so if mon through fri entirely exist you can replace the data for mon-fri basically wiping out the previous data and replace with new data
            //do you understand?
            //i do.  and that wouldd be find because none of thiis needs to persist it is just for display on the chart
            //yes so let me see how you would do  better?
            //hmm I am not sure I know the best logic  

            //better?yep try it
            //ok?
            TicketData TicketData1 = new TicketData();

            var dateago = DateTime.Now.Date.AddDays(-7).ToString("d");
           var dateago2 = DateTime.Now.Date.AddDays(-6).ToString("d");
            var dateago3 = DateTime.Now.Date.AddDays(-5).ToString("d");
            var dateago4 = DateTime.Now.Date.AddDays(-4).ToString("d");
            var dateago5 = DateTime.Now.Date.AddDays(-3).ToString("d");
            var dateago6 = DateTime.Now.Date.AddDays(-2).ToString("d");
            var dateago7 = DateTime.Now.Date.AddDays(-1).ToString("d");
            //right?yes
            //
            var tickets = new List<Ticket>();
            foreach(var ticket in db.Tickets)
            {
                if(ticket.CreatedDate.ToString("d") == dateago)
                {
                    tickets.Add(ticket);
                }
            }
            //now go ahead and replace all linq with the foreach
           /* var tickets = db.Tickets.Where(t => t.CreatedDate.ToString("d") == dateago);*/ // replace rest of them
          /*  var ticketscount = tickets.Count();*/ //what is this for?  // not sure - that may have been from the second time i re wrote it
          //ok now you need if statement? do you mean if the day is saturday or sunday dont show? so if any of those days are not matching it will throw null exception
          //so you don't have any tickets created within the timeframe  // no i am glad you checked that!  but i think it might work otherwise... ? yeah
            TicketData1.DayofWeek = DateTime.Now.Date.AddDays(-7).DayOfWeek.ToString();  // Whatever today's date is minus seven I guess ?  Not sure here  I think select is my problem  
            //Mark am i on the right track? I think the logic is good ok - as far as finding the day of the week that was 7 days ago.... aha.  ok so continue?  or do you need to go? continue ok
            // not sure what would be null? never mind I thought of something else   so let's try it ok whew
            //ok what do you think ok let's try ok go ahead and replace make sure you use the var instead of using the adddays method directly in the linq
        //ok so then i need to make a few more>.....
            //good?  ok?sure

            //success? did you complete everything else?
            //I think so.  everytime I check it I find something else.  I was just walking through it this evening for a final time but this is very minor you r demo login containers are kind of out of lines for the bottom lines
            //i noticed that.  it is stretching them differently depending on which monitor im using - so I think i will go through one time with it published on my regular computer screen not the extra..  the large screen everything 
            //looks fine but then looks off on smaller screen looks great  thansk it has been a ton of work.  I tried to just do it my own way verus what other people had done... im sure it could be better but learned a lot!

            //Tahnks for helping with this this evening and sorry took so long. it's alright glad that it works. also for your dashboard any of the elements that doesn't do anything for your actual app it's better taking them out such as notification lighting onthe right upper side and search and also the on/off switch icon
            // ok got it- i left some things in hoping to make them work...  but need to take them out
            //will work on more in the morning and hopefully im not first but if i am, i'll try to be prepared okay
            //cool thanks again!  I'm going to update the dates in the database to make that chart populate with data.  

            //see you in the AM see you
            //looks like you cannot use datetime methods in linq 
            // ok well ...tried
           // but I think  you are very close you can do a lot of things manually without using linq
            //well it went trhough but i have created tickets this week...they shouldnt all be yes it should be zeros because you don't have anything that you created exactly at 12:00.ohhhhhh
            if(tickets.Count() != 0)
            {
                TicketData1.StatusA = tickets.Where(t => t.TicketPriority.Name == "Urgent").Count();
                TicketData1.StatusB = tickets.Where(t => t.TicketPriority.Name == "Normal").Count();
                TicketData1.StatusC = tickets.Where(t => t.TicketPriority.Name == "Low").Count();
                TicketChartList.Add(TicketData1);
            }
            else
            {
                TicketData1.StatusA = 0;
                TicketData1.StatusB = 0; //makes sense go ahead and apply this to every case ok
                TicketData1.StatusC = 0;
                TicketChartList.Add(TicketData1);
            }

            var tickets2 = new List<Ticket>();
            foreach (var ticket in db.Tickets)
            {
                if (ticket.CreatedDate.ToString("d") == dateago2)
                {
                    tickets2.Add(ticket);
                }
            }
            //somewhere in here right... not sure what to get rid of

            //var tickets2 = db.Tickets.Where(t => t.CreatedDate.ToString("d") == dateago2);
            //var ticketscount2 = tickets.Count();
            TicketData TicketData3 = new TicketData();
            TicketData3.DayofWeek = DateTime.Now.Date.AddDays(-6).DayOfWeek.ToString();
                  if (tickets2.Count() != 0)
            {
                TicketData3.StatusA = tickets2.Where(t => t.TicketPriority.Name == "Urgent").Count();
                TicketData3.StatusB = tickets2.Where(t => t.TicketPriority.Name == "Normal").Count();
                TicketData3.StatusC = tickets2.Where(t => t.TicketPriority.Name == "Low").Count();
                TicketChartList.Add(TicketData3);
            }
                  else
            {
                TicketData3.StatusA = 0;
                TicketData3.StatusB = 0;
                TicketData3.StatusC = 0;
                TicketChartList.Add(TicketData3);
            }

            var tickets3 = new List<Ticket>();
            foreach (var ticket in db.Tickets)
            {
                if (ticket.CreatedDate.ToString("d") == dateago3)
                {
                    tickets3.Add(ticket);
                }
            }
            //var tickets3 = db.Tickets.Where(t => t.CreatedDate.Date.ToString("d") == dateago3);
            //var ticketscount3 = tickets.Count();
            TicketData TicketData4 = new TicketData();
            TicketData4.DayofWeek = DateTime.Now.Date.AddDays(-5).DayOfWeek.ToString();

            if (tickets3.Count() != 0)
            {
                TicketData4.StatusA = tickets3.Where(t => t.TicketPriority.Name == "Urgent").Count();
                TicketData4.StatusB = tickets3.Where(t => t.TicketPriority.Name == "Normal").Count();
                TicketData4.StatusC = tickets3.Where(t => t.TicketPriority.Name == "Low").Count();
                TicketChartList.Add(TicketData4);
            }
            else
            {
                TicketData4.StatusA = 0;
                TicketData4.StatusB = 0;
                TicketData4.StatusC = 0;
                TicketChartList.Add(TicketData4);
            }


            var tickets4 = new List<Ticket>();
            foreach (var ticket in db.Tickets)
            {
                if (ticket.CreatedDate.ToString("d") == dateago4)
                {
                    tickets4.Add(ticket);
                }
            }
            //var tickets4 = db.Tickets.Where(t => t.CreatedDate.Date.ToString("d") == dateago4);
            //var ticketscount4 = tickets.Count();

            TicketData TicketData5 = new TicketData();
            TicketData5.DayofWeek = DateTime.Now.Date.AddDays(-4).DayOfWeek.ToString();
            if (tickets4.Count() != 0)
            {
                TicketData5.StatusA = tickets4.Where(t => t.TicketPriority.Name == "Urgent").Count();
                TicketData5.StatusB = tickets4.Where(t => t.TicketPriority.Name == "Normal").Count();
                TicketData5.StatusC = tickets4.Where(t => t.TicketPriority.Name == "Low").Count();
                TicketChartList.Add(TicketData5);
            }
            else
            {
                TicketData5.StatusA = 0;
                TicketData5.StatusB = 0;
                TicketData5.StatusC = 0;
                TicketChartList.Add(TicketData5);
            }


            var tickets5 = new List<Ticket>();
            foreach (var ticket in db.Tickets)
            {
                if (ticket.CreatedDate.ToString("d") == dateago5)
                {
                    tickets5.Add(ticket);
                }
            }
            //var tickets5 = db.Tickets.Where(t => t.CreatedDate.Date.ToString("d") == dateago5);
            //var ticketscount5 = tickets.Count();
            TicketData TicketData6 = new TicketData();
            TicketData6.DayofWeek = DateTime.Now.Date.AddDays(-3).DayOfWeek.ToString();
            if (tickets5.Count() != 0)
            {

          
                TicketData6.StatusA = tickets5.Where(t => t.TicketPriority.Name == "Urgent").Count();
            TicketData6.StatusB = tickets5.Where(t => t.TicketPriority.Name == "Normal").Count();
            TicketData6.StatusC = tickets5.Where(t => t.TicketPriority.Name == "Low").Count();
            TicketChartList.Add(TicketData6);
            }
            else
            {
                TicketData6.StatusA = 0;
                TicketData6.StatusB = 0;
                TicketData6.StatusC = 0;
                TicketChartList.Add(TicketData6);
            }

            var tickets6 = new List<Ticket>();
            foreach (var ticket in db.Tickets)
            {
                if (ticket.CreatedDate.ToString("d") == dateago6)
                {
                    tickets6.Add(ticket);
                }
            }
            //var tickets6 = db.Tickets.Where(t => t.CreatedDate.Date.ToString("d") == dateago6);
            //var ticketscount6 = tickets.Count();
            TicketData TicketData7 = new TicketData();
            TicketData7.DayofWeek = DateTime.Now.Date.AddDays(-2).DayOfWeek.ToString();
            if (tickets6.Count() != 0)
            {

           
                
            TicketData7.StatusA = tickets6.Where(t => t.TicketPriority.Name == "Urgent").Count();
            TicketData7.StatusB = tickets6.Where(t => t.TicketPriority.Name == "Normal").Count();
            TicketData7.StatusC = tickets6.Where(t => t.TicketPriority.Name == "Low").Count();
            TicketChartList.Add(TicketData7);
            }
            else
            {

                TicketData7.StatusA = 0;
                TicketData7.StatusB = 0;
                TicketData7.StatusC = 0;
                TicketChartList.Add(TicketData7);
            }

            var tickets7 = new List<Ticket>();
            foreach (var ticket in db.Tickets)
            {
                if (ticket.CreatedDate.ToString("d") == dateago7)
                {
                    tickets7.Add(ticket);
                }
            }
            //var tickets7 = db.Tickets.Where(t => t.CreatedDate.Date.ToString("d") == dateago7);
            //var ticketscount7 = tickets.Count();
            TicketData TicketData8 = new TicketData();
            TicketData8.DayofWeek = DateTime.Now.Date.AddDays(-1).DayOfWeek.ToString();
            if(tickets7.Count() !=0)
            {

           
            TicketData8.StatusA = tickets7.Where(t => t.TicketPriority.Name == "Urgent").Count();
            TicketData8.StatusB = tickets7.Where(t => t.TicketPriority.Name == "Normal").Count();
            TicketData8.StatusC = tickets7.Where(t => t.TicketPriority.Name == "Low").Count();
            TicketChartList.Add(TicketData8);
            }
            else
            {
                TicketData8.StatusA = 0;
                TicketData8.StatusB = 0;
                TicketData8.StatusC = 0;
                TicketChartList.Add(TicketData8);
            }
            //This is the controller that I was having the issue with
            //List<UserData> UserChartList = new List<UserData>();
            //UserRolesHelper UH = new UserRolesHelper(db);


            //UserData User1 = new UserData();


            //var Admins = UH.UsersInRole("Admin").Count();
            //User1.Administrators = UH.UsersInRole("Admin").Count();
            ////User1.Developers = Developer1 + Developer2 + Developer3 + Developer4;
            ////User1.ProjectManagers = PM1 + PM2 + PM3;
            ////User1.Submitters = submitters; 
            ////User1.TotalUsers = db.Users.Count();
            //UserChartList.Add(User1);


            //UserData User2 = new UserData();

            //var Developer1 = UH.UsersInRole("Developer1").Count();
            //var Developer2 = UH.UsersInRole("Developer2").Count();
            //var Developer3 = UH.UsersInRole("Developer3").Count();
            //var Developer4 = UH.UsersInRole("Developer4").Count();
            //User2.Developers = Developer1 + Developer2 + Developer3 + Developer4;
            //UserChartList.Add(User2);

            //var PM1 = UH.UsersInRole("Project Manager1").Count();
            //var PM2 = UH.UsersInRole("Project Manager2").Count();
            //var PM3 = UH.UsersInRole("Project Manager3").Count();

            //var submitters = UH.UsersInRole("Submitter").Count();

            List<UserData2> UserChartList = new List<UserData2>();
            UserRolesHelper UH = new UserRolesHelper(db);

            UserData2 User1 = new UserData2();
            var AdminCount = UH.UsersInRole("Admin").Count();
            User1.label = "Administrator";
            User1.value = AdminCount;
            UserChartList.Add(User1);

            UserData2 User2 = new UserData2();
            var DevCount1 = UH.UsersInRole("Developer1").Count();
            var DevCount2 = UH.UsersInRole("Developer2").Count();
            var DevCount3 = UH.UsersInRole("Developer3").Count();
            var DevCount4 = UH.UsersInRole("Developer4").Count();
            //could you put the break point and see the data which you r sending t
        // i did do that - so the second snippet that I sent you was showing how it comes through to the view page - the data comes through but then does not populate a graph
        //the data comes through i
            User2.label = "Developers";
            User2.value = DevCount1 + DevCount2 + DevCount3 + DevCount4;
            UserChartList.Add(User2);

            UserData2 User3 = new UserData2();
            var PMCount1 = UH.UsersInRole("Project Manager1").Count();
            var PMCount2 = UH.UsersInRole("Project Manager2").Count();
            var PMCount3 = UH.UsersInRole("Project Manager3").Count();
        

            User3.label = "Project Managers";
            User3.value = PMCount1 + PMCount2 + PMCount3;
            UserChartList.Add(User3);


            UserData2 User4 = new UserData2();
            var SubmitterCount = UH.UsersInRole("Submitter").Count();
            User4.label = "Submitter";
            User4.value = SubmitterCount;
            UserChartList.Add(User4);



            //will let you drive
            //ok

            //this is where i had tried some things before...

            //I was going to have the chart just constantly update based on today minus 7 days, minus 6, 5, 4, 3....and i guess it would keep changing because todays date will keep changing
            //TicketData1.TicketCount = db.Tickets.Where(t=>t.CreatedDate == DateTime.Now.Date.AddDays(-7));)
              //does this look logical? yes this will bring the entire count of tickets created 7 days ago //ok and then would i be able to 
            //repeat code from above to find out the different statuses among those tickets...low, urgent, etc? sure

            //try it now? yeah ok

            //var ticket = db.Tickets.Where(t => t.CreatedDate == DateTime.Now.Date.AddDays(-7));
            //var urgentcount = ticket.Where(t => t.TicketPriority.Name == "Urgent").Count();
            //var normalcount = ticket.Where(t => t.TicketPriority.Name == "Normal").Count();
            //var lowcount = ticket.Where(t => t.TicketPriority.Name == "Low").Count();
            //TicketData1.Day = DateTime.Now.Date.AddDays(-7);  // so like here you have -7.  then could you have the next one be -6..   etc?  so what was the problem?  well in this
            // examply when i try i have issue with converting because of the date/time type..  i think.  it wouldnt compile properly
            //TicketData1.StatusA = urgentcount;
            //TicketData1.StatusB = normalcount;
            //TicketData1.StatusC = lowcount;
            //TicketChartList.Add(TicketData1);

            //TicketData1.Day = ;

            //ChartData SampleData2 = new ChartData();
            //SampleData2.Year = "2003";
            //SampleData2.Income = 80000;
            //SampleData2.Expense = 20000;
            //ExerciseChartList.Add(SampleData2);

            //ChartData SampleData3 = new ChartData();
            //SampleData3.Year = "2004";
            //SampleData3.Income = 70000;
            //SampleData3.Expense = 30000;
            //ExerciseChartList.Add(SampleData3);

            //ChartData SampleData4 = new ChartData();
            //SampleData4.Year = "2005";
            //SampleData4.Income = 60000;
            //SampleData4.Expense = 40000;
            //ExerciseChartList.Add(SampleData4);
            ViewBag.ArrData2 = UserChartList.ToArray();
            ViewBag.ArrData = TicketChartList.ToArray();

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
            //var projects = db.Projects.Include(p => p.Users).ToList();







            List<Ticket> usertickets = new List<Ticket>();
            List<Project> userprojects = new List<Project>();
            if (helper.IsUserInRole(userId, "Admin") || helper.IsUserInRole(userId, "Administrator"))
            {
                foreach (var project in db.Projects)
                {
                    if (ph.IsUserOnAProject(userId, project.Id))
                    {
                        userprojects.Add(project);
                    }
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
                var submittertickets = db.Tickets.Where(t => t.SubmitterUserId == userId).ToList();

                var nonarchivedtickets = submittertickets.Where(t => t.TicketStatus.Name != "Archived");
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

                var projects = db.Projects.ToList();
                return View(projects);
            }
            return RedirectToAction("Index", "Home");
        }

        
        public ActionResult LayoutPartial()
        {
            
            var currentuser = db.Users.Find(User.Identity.GetUserId());
            if (currentuser != null)
            {


                UserRolesHelper uh = new UserRolesHelper(db);
                UsersRolesViewModel vm = new UsersRolesViewModel();
                vm.User = db.Users.Find(User.Identity.GetUserId());
                vm.Roles = uh.ListUserRoles(vm.User.Id);

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
                //var projects = db.Projects.Include(p => p.Users).ToList();


                List<Project> userprojects = new List<Project>();
                foreach (var project in db.Projects)
                {
                    if (ph.IsUserOnAProject(userId, project.Id))
                    {
                        userprojects.Add(project);
                    }
                }

                ViewBag.projectcount = userprojects.Count();


                List<Ticket> usertickets = new List<Ticket>();

                if (helper.IsUserInRole(userId, "Admin") || helper.IsUserInRole(userId, "Administrator"))
                {
                    foreach (var ticket in db.Tickets)
                    {

                        usertickets.Add(ticket);
                    }
                    ViewBag.ticketcount = usertickets.Count();

                }

                else if (userroleProjectManager)
                {

                    List<Project> UserProjects = new List<Project>();
                    foreach (var x in db.Projects)
                    {

                        if (ph.IsUserOnAProject(userId, x.Id))
                        {
                            UserProjects.Add(x);
                        }

                    }

                    foreach (var project in UserProjects)
                    {
                        foreach (var ticket in project.Tickets)
                        {
                            usertickets.Add(ticket);
                        }

                    }
                    var nonarchivedtickets = usertickets.Where(t => t.TicketStatus.Name != "Archived");
                    var urgenttickets = nonarchivedtickets.Where(t => t.TicketPriority.Name == "Urgent");
                    ViewBag.ticketcount = nonarchivedtickets.Count();
                    ViewBag.urgentticketcount = urgenttickets.Count();

                }


                else if (userroleDeveloper)
                {

                    List<Project> UserProjects = new List<Project>();
                    foreach (var x in db.Projects)
                    {

                        if (ph.IsUserOnAProject(userId, x.Id))
                        {
                            UserProjects.Add(x);
                        }

                    }

                    foreach (var project in UserProjects)
                    {
                        foreach (var ticket in project.Tickets)
                        {
                            usertickets.Add(ticket);
                        }

                    }
                    var nonarchivedtickets = usertickets.Where(t => t.TicketStatus.Name != "Archived");

                    ViewBag.ticketcount = nonarchivedtickets.Count();

                }

                else if (userroleSubmitter)
                {
                    var tickets = db.Tickets.Where(t => t.SubmitterUserId == userId).ToList();

                    var nonarchivedtickets = tickets.Where(t => t.TicketStatus.Name != "Archived");
                    ViewBag.ticketcount = nonarchivedtickets.Count();

                }

















                return PartialView("~/Views/Shared/_LayoutPartial.cshtml", vm);
            }

            return RedirectToAction("Index", "Home");
            }

       
        public ActionResult SidebarPartial2()
        { 

            var currentuser = db.Users.Find(User.Identity.GetUserId());
            if (currentuser != null)
        {

            ProjectAssignHelper ph = new ProjectAssignHelper();
            UserRolesHelper uh = new UserRolesHelper(db);
            UsersRolesViewModel vm = new UsersRolesViewModel();
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            vm.User = db.Users.Find(User.Identity.GetUserId());
            vm.Roles = uh.ListUserRoles(vm.User.Id);
            if (uh.IsUserInRole(userId, "Admin") || uh.IsUserInRole(userId, "Administrator") || uh.IsUserInRole(userId, "Project Manager1") || uh.IsUserInRole(userId, "Project Manager2") || uh.IsUserInRole(userId, "Project Manager3"))
            {
                vm.Projectsb = db.Projects.ToList();
            }
            else
            {
                vm.Projectsb = ph.ListProjectsForAUser(userId);
            }
            
            
          

        
            return PartialView("~/Views/Projects/_SidebarPartial2.cshtml", vm);
        }
            return RedirectToAction("Index", "Home");
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}