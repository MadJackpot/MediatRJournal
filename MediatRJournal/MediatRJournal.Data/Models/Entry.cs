using System;
using System.Collections.Generic;
using System.Linq;

namespace MediatRJournal.Data.Models
{
    public class Entry
    {
        public Guid Id { get; set; }
        public Guid JournalId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
    }
}