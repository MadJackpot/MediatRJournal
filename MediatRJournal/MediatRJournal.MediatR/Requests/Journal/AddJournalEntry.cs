using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using MediatRJournal.Data;
using MediatRJournal.Data.Models;
using MediatRJournal.Models.Journals.Entries;

using Microsoft.EntityFrameworkCore;

namespace MediatRJournal.MediatR.Requests.Journal
{
    public class AddJournalEntry : IRequest<AddJournalEntry.AddJournalEntryResponse>
    {
        public Guid JournalId { get; set; }
        public string Title { get; }
        public string Content { get; }

        public AddJournalEntry(Guid journalId, string title, string content)
        {
            JournalId = journalId;
            Title = title;
            Content = content;
        }

        internal class Handler : IRequestHandler<AddJournalEntry, AddJournalEntryResponse>
        {
            private readonly JournalContext _context;
            private readonly IMapper _mapper;

            public Handler(JournalContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AddJournalEntryResponse> Handle(AddJournalEntry request, CancellationToken cancellationToken)
            {
                var journal = await _context.Journals.SingleOrDefaultAsync(x => x.Id == request.JournalId, cancellationToken);

                if (journal == null)
                {
                    return new AddJournalEntryResponse { Result = AddJournalEntryResult.NoJournal };
                }

                if (journal.Entries.Any(x => x.Title == request.Title))
                {
                    return new AddJournalEntryResponse { Result = AddJournalEntryResult.Conflict };
                }

                var newEntry = new Entry
                {
                    Title = request.Title,
                    Content = request.Content
                };

                journal.Entries.Add(newEntry);

                await _context.SaveChangesAsync(cancellationToken);

                var response = _mapper.Map<EntryResponse?>(newEntry);
                return new AddJournalEntryResponse { Result = AddJournalEntryResult.Success, Response = response };
            }
        }

        public class AddJournalEntryResponse
        {
            public AddJournalEntryResult Result { get; init; }
            public EntryResponse? Response { get; init; }
        }

        public enum AddJournalEntryResult
        {
            Success,
            Conflict,
            NoJournal
        }
    }
}