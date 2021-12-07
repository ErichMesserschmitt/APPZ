using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskStatus = APPZ_new.Enums.TaskStatus;

namespace APPZ_new.Models
{
    public class UserTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserTaskId")]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        public int Mark { get; set; }

        public TaskStatus Status { get; set; }

        public virtual ICollection<Answer> UserAnswers { get; set; }
    }
}
