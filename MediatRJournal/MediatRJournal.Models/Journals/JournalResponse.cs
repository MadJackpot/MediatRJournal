using System;
using System.Collections.Generic;
using System.Linq;

using MediatRJournal.Models.Journals.Entries;

namespace MediatRJournal.Models.Journals
{
    public record JournalResponse(Guid Id, string Title, List<EntryResponse> Entries);
}