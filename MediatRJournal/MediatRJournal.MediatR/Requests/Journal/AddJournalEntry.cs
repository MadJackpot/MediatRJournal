using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace MediatRJournal.MediatR.Requests.Journal
{
    public class AddJournalEntry : IRequest<Unit>
    {
        public string Title { get; }
        public string Entry { get; }

        public AddJournalEntry(string title, string entry)
        {
            Title = title;
            Entry = entry;
        }

        public class Handler : IRequestHandler<AddJournalEntry, Unit>
        {
            public Task<Unit> Handle(AddJournalEntry request, CancellationToken cancellationToken)
            {
                Console.WriteLine($"{request.Title} - {request.Entry}");

                return Task.FromResult(Unit.Value);
            }
        }
    }
}