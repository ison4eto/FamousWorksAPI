using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamousWorksAPI.DTOs
{
    public class ComposerDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country{ get; set; }

        public ComposerDto()
        {
        }

        public ComposerDto(Composer composer)
        {
            this.Id = composer.Id;
            this.FirstName = composer.FirstName;
            this.LastName = composer.LastName;
            this.Country = composer.Country;
        }
    }
}