using System;
using System.Collections.Generic;
using System.Linq;

using AutoFixture;
using AutoFixture.AutoMoq;

using AutoMapper;

using MediatRJournal.Data;

using Microsoft.EntityFrameworkCore;

namespace MediatRJournal.MediatR.Tests
{
    public class BaseTests
    {
        protected IFixture Fixture { get; init; }
        protected JournalContext Context { get; init; }
        protected IMapper Mapper { get; init; }

        public BaseTests()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());

            // Create in memory database for unit tests
            var options = new DbContextOptionsBuilder<JournalContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            Context = new JournalContext(options);

            Fixture.Inject(Context);

            // Find mapping profiles and add them to configuration
            Mapper = new Mapper(new MapperConfiguration(mc => mc.AddMaps(typeof(MediatRBase).Assembly)));
            Fixture.Inject(Mapper);
        }
    }
}