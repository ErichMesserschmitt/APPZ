using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("QuestionId")]
        public int Id { get; set; }

        [Required]
        public string QuestionTest { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }


        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        public Question()
        {
            Answers = new HashSet<Answer>();
        }
    }
}
