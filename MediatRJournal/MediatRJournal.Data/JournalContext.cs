using System;
using System.Collections.Generic;
using System.Linq;

using MediatRJournal.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace MediatRJournal.Data
{
    public class JournalContext : DbContext
    {
        public JournalContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Journal> Journals { get; set; } = default!;
        public DbSet<Entry> Entries { get; set; } = default!;

    }
}