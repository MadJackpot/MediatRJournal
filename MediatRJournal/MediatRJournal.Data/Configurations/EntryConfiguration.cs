using System;
using System.Collections.Generic;
using System.Linq;

using MediatRJournal.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatRJournal.Data.Configurations
{
    public class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            // EFCore will always try and make a property called 'ID' or '{classname}ID' its primary key.
            // No need to do this, but it's just nice to see.
            builder.HasKey(x => x.Id);
        }
    }
}