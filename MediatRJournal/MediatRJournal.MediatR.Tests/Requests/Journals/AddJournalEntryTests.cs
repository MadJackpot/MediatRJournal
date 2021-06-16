using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using MediatRJournal.Data.Models;
using MediatRJournal.MediatR.Requests.Journal;

using Xunit;

namespace MediatRJournal.MediatR.Tests.Requests.Journals
{
    public class AddJournalEntryTests : BaseTests
    {
        private readonly AddJournalEntry.Handler _handler;

        public AddJournalEntryTests()
        {
            _handler = Fixture.Create<AddJournalEntry.Handler>();
        }

        [Fact]
        public async Task Handle_ShouldReturnNoJournalResponse_WhenNoJournalExists()
        {
            var request = Fixture.Create<AddJournalEntry>();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(AddJournalEntry.AddJournalEntryResult.NoJournal, result.Result);
            Assert.Null(result.Response);
        }

        [Fact]
        public async Task Handle_ShouldReturnConflict_WhenJournalEntryAlreadyExists()
        {
            var request = Fixture.Create<AddJournalEntry>();

            var entry = Fixture.Build<Entry>()
                .With(x => x.Title, request.Title)
                .Create();

            var journal = Fixture.Build<Journal>()
                .With(x => x.Entries, new List<Entry>{ entry })
                .With(x => x.Id, request.JournalId)
                .Create();

            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(AddJournalEntry.AddJournalEntryResult.Conflict, result.Result);
            Assert.Null(result.Response);
        }

        [Fact]
        public async Task Handle_ShouldHaveSuccessfulResponse_WhenOnlyJournalExists()
        {
            var request = Fixture.Create<AddJournalEntry>();

            var journal = Fixture.Build<Journal>()
                .With(x => x.Id, request.JournalId)
                .Create();

            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(AddJournalEntry.AddJournalEntryResult.Success, result.Result);

            Assert.Equal(request.Title, result.Response!.Title);
            Assert.Equal(request.Content, result.Response!.Content);
        }
    }
}