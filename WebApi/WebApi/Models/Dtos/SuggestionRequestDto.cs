using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Misc;

namespace WebApi.Models.Dtos
{
    public class SuggestionRequestDto
    {
        public TeamConstellation Team1 { get; set; }

        public TeamConstellation Team2 { get; set; }
    }
}
