using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using AutoMapper;

using MediatRJournal.MediatR.Requests.Journal;

using Xunit;

namespace MediatRJournal.MediatR.Tests.Requests.Journals
{
    public class CreateJournalTests : BaseTest
    {
        private readonly IMapper _mapper;
        private readonly CreateJournal.Handler _handler;

        public CreateJournalTests()
        {
            _mapper = Fixture.Freeze<IMapper>();
            _handler = Fixture.Create<CreateJournal.Handler>();
        }

        [Fact]
        public async Task Handle_ShouldCreateJournalEntry_WhenEntryDoesNotExist()
        {
            var request = Fixture.Create<CreateJournal>();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(CreateJournal.CreateJournalResult.Success, result.Result);
            Assert.NotNull(result.Response);
        }
    }
}