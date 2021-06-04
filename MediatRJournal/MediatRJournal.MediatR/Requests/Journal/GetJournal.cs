using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using MediatRJournal.Data;
using MediatRJournal.Models.Journals;

using Microsoft.EntityFrameworkCore;

namespace MediatRJournal.MediatR.Requests.Journal
{
    public class GetJournal : IRequest<JournalResponse?>
    {
        #region Properties

        public Guid Id { get; }

        #endregion

        #region Constructors

        public GetJournal(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Handler

        internal class Handler : IRequestHandler<GetJournal, JournalResponse?>
        {
            private readonly JournalContext _context;
            private readonly IMapper _mapper;

            public Handler(JournalContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<JournalResponse?> Handle(GetJournal request, CancellationToken cancellationToken)
            {
                var journal = await _context.Journals.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return _mapper.Map<JournalResponse?>(journal);
            }
        }

        #endregion
    }
}