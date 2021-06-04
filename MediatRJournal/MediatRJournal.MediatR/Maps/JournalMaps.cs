using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using MediatRJournal.Data.Models;
using MediatRJournal.Models.Journals;

namespace MediatRJournal.MediatR.Maps
{
    internal class JournalMaps : Profile
    {
        public JournalMaps()
        {
            CreateMap<Journal, JournalResponse>();
        }
    }
}