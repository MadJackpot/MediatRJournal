using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using MediatRJournal.Data.Models;
using MediatRJournal.Models.Journals.Entries;

namespace MediatRJournal.MediatR.Maps
{
    internal class EntryMaps : Profile
    {
        public EntryMaps()
        {
            CreateMap<Entry, EntryResponse>();
        }
    }
}