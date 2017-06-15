using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Project
    {
        public Project()
        {
            Users = new HashSet<ApplicationUser>();
            Tickets = new HashSet<Ticket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectManagerName { get; set; }
        public string ProjectManagerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMMM dd yyyy}")]
        public DateTimeOffset? ProjectStartDate { get; set; }
        public string ProjectStatus { get; set; }
        public int ProjectStatusId { get; set; }

        public virtual ProjectStatus ProjectStatuses { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        
        public virtual ICollection<ApplicationUser>Users { get; set; }
    }
}