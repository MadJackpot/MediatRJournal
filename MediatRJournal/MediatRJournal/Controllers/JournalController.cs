using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using MediatRJournal.MediatR.Requests.Journal;
using MediatRJournal.Models.Journals;

using Microsoft.AspNetCore.Mvc;

namespace MediatRJournal.Controllers
{
    public class JournalController : BaseController
    {
        private readonly IMediator _mediator;

        public JournalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateJournal(CreateJournalRequest request)
        {
            var result = await _mediator.Send(new CreateJournal(request.Title));

            return result.Result switch
            {
                MediatR.Requests.Journal.CreateJournal.CreateJournalResult.Success => CreatedAtAction(nameof(GetJournalById), new { id = result.Response!.Id }, result.Response),
                MediatR.Requests.Journal.CreateJournal.CreateJournalResult.Conflict => new ConflictResult(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> GetJournalById(Guid id)
        {
            var journal = await _mediator.Send(new GetJournal(id));

            if (journal == null)
            {
                return new NotFoundResult();
            }

            return Ok(journal);
        }
    }
}