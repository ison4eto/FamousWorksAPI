using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamousWorksAPI.DTOs
{
    public class WorkCreateDto
    {
        public int ComposerId { get; set; }

        public int EraId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }
    }
}