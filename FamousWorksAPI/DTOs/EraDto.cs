using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamousWorksAPI.DTOs
{
    [Serializable]
    public class EraDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public EraDto(Era era)
        {
            this.Id = era.Id;
            this.Name = era.Name;
        }
    }
}