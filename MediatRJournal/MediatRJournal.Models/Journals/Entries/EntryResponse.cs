using System;
using System.Collections.Generic;
using System.Linq;

namespace MediatRJournal.Models.Journals.Entries
{
    public record EntryResponse(Guid Id, string Title, string Content);
}