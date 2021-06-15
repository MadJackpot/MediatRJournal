using System;
using System.Collections.Generic;
using System.Linq;

namespace MediatRJournal.Models.Journals.Entries
{
    public class AddJournalEntryRequest
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
    }
}