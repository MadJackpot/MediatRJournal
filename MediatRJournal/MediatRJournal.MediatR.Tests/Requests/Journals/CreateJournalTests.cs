using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using AutoMapper;

using MediatRJournal.Data.Models;
using MediatRJournal.MediatR.Requests.Journal;
using MediatRJournal.Models.Journals;

using Moq;

using Xunit;

namespace MediatRJournal.MediatR.Tests.Requests.Journals
{
    public class CreateJournalTests : BaseTests
    {
        private readonly CreateJournal.Handler _handler;

        public CreateJournalTests()
        {
            _handler = Fixture.Create<CreateJournal.Handler>();
        }

        [Fact]
        public async Task Handle_ShouldCreateJournalEntry_WhenEntryDoesNotExist()
        {
            var request = Fixture.Create<CreateJournal>();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(CreateJournal.CreateJournalResult.Success, result.Result);
            Assert.NotNull(result.Response);
            Assert.Equal(request.Title, result.Response!.Title);
        }

        [Fact]
        public async Task Handle_ShouldReturnConflict_WhenEntryExists()
        {
            var request = Fixture.Create<CreateJournal>();

            Context.Journals.Add(new Journal
            {
                Title = request.Title
            });
            await Context.SaveChangesAsync();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(CreateJournal.CreateJournalResult.Conflict, result.Result);
            Assert.Null(result.Response);
        }
    }
}