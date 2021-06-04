using System;
using System.Collections.Generic;
using System.Linq;

using MediatRJournal.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatRJournal.Data.Configurations
{
    public class JournalConfiguration : IEntityTypeConfiguration<Journal>
    {
        public void Configure(EntityTypeBuilder<Journal> builder)
        {
            // EFCore will always try and make a property called 'ID' or '{classname}ID' its primary key.
            // No need to do this, but it's just nice to see.
            builder.HasKey(x => x.Id);

            // Also not required, EFCore is smart enough to auto-generate foreign keys for its list of other data objects.
            builder.HasMany(x => x.Entries)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // For our test case, we will make titles of journals unique to have an excuse to work around the problem.
            builder.HasIndex(x => x.Title).IsUnique();
        }
    }
}