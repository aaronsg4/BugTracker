﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketAttachmentsViewModel
    {
        public Ticket ticket { get; set; }
        public TicketAttachment[] TicketAttachments { get; set; }
    }
}