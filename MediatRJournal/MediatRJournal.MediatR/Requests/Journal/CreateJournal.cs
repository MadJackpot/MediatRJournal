using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using MediatRJournal.Data;
using MediatRJournal.Models.Journals;
using MediatRJournal.Models.Journals.Entries;

using Microsoft.EntityFrameworkCore;

namespace MediatRJournal.MediatR.Requests.Journal
{
    public class CreateJournal : IRequest<CreateJournal.CreateJournalResponse>
    {
        #region Properties
        public string Title { get; }
        #endregion

        #region Constructors
        public CreateJournal(string title)
        {
            Title = title;
        }
        #endregion

        #region Handler
        // Here we have our handler as an internal class to our request.
        internal class Handler : IRequestHandler<CreateJournal, CreateJournalResponse>
        {
            private readonly JournalContext _context;

            public Handler(JournalContext context)
            {
                _context = context;
            }

            public async Task<CreateJournalResponse> Handle(CreateJournal request, CancellationToken cancellationToken)
            {
                if (await _context.Journals.AnyAsync(x => x.Title == request.Title, cancellationToken))
                {
                    return new CreateJournalResponse {Result = CreateJournalResult.Conflict };
                }

                var newJournal = new Data.Models.Journal
                {
                    Title = request.Title
                };

                _context.Journals.Add(newJournal);

                return new CreateJournalResponse
                {
                    Result = CreateJournalResult.Success,
                    Response = new JournalResponse(newJournal.Id, newJournal.Title, new List<EntryResponse>())
                };
            }
        }
        #endregion

        #region ResponseTypes
        public class CreateJournalResponse
        {
            public CreateJournalResult Result { get; set; }
            public JournalResponse? Response { get; set; }
        }

        public enum CreateJournalResult
        {
            Success,
            Conflict
        }
        #endregion
    }
}