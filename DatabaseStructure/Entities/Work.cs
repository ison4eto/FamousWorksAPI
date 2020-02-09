using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStructure.Entities
{
    public class Work
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int ComposerID { get; set; }

        public int EraID { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        public int Year{ get; set; }

        [ForeignKey("ComposerID")]
        public virtual Composer Composer { get; set; }

        [ForeignKey("EraID")]
        public virtual Era Era { get; set; }
    }
}
