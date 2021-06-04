using System;
using System.Collections.Generic;
using System.Linq;

namespace MediatRJournal.Data.Models
{
    public class Journal
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Entry> Entries { get; set; } = new List<Entry>();
    }
}