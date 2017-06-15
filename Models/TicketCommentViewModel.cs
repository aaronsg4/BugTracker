using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketCommentViewModel
    {
        public Ticket ticket { get; set; }
        public TicketComment ticketComment { get; set; }
    }
}