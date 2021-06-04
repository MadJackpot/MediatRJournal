using System;
using System.Collections.Generic;
using System.Linq;

using AutoFixture;
using AutoFixture.AutoMoq;

using MediatRJournal.Data;

using Microsoft.EntityFrameworkCore;

namespace MediatRJournal.MediatR.Tests
{
    public class BaseTest
    {
        protected IFixture Fixture { get; init; }
        protected JournalContext Context { get; init; }

        public BaseTest()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());

            var options = new DbContextOptionsBuilder<JournalContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            Context = new JournalContext(options);
        }
    }
}