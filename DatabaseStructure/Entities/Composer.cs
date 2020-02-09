using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStructure.Entities
{
    public class Composer
    {
        public Composer()
        {
            this.Works = new HashSet<Work>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [StringLength(255, MinimumLength = 1)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Country { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}
