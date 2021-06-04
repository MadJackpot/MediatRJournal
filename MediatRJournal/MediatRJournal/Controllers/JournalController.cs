using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using MediatRJournal.MediatR.Requests.Journal;
using MediatRJournal.Models.Journals;
using MediatRJournal.Models.Journals.Entries;

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
                MediatR.Requests.Journal.CreateJournal.CreateJournalResult.Success => Ok(result.Response),
                MediatR.Requests.Journal.CreateJournal.CreateJournalResult.Conflict => new ConflictResult(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult GetJournal(Guid id)
        {
            return Ok(new JournalResponse(id, string.Empty, new List<EntryResponse>()));
        }
    }
}