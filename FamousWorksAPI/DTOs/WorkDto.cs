using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorksAPI.DTOs
{
    public class WorkDto
    {
        public int ID { get; set; }

        public int ComposerID { get; set; }
        
        public int EraID { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public int Year { get; set; }

        public WorkDto()
        {
        }

        public WorkDto(Work work)
        {
            this.ID = work.ID;
            this.ComposerID = work.ComposerID;
            this.EraID = work.EraID;
            this.Title = work.Title;
            this.Description = work.Description;
            this.Year = work.Year;
        }
    }
}