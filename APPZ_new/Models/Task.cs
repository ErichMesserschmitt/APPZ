using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using APPZ_new.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace APPZ_new.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TaskId")]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskSeverity Severity { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<UserTask> Users { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public Task()
        {
            Users = new HashSet<UserTask>();
            Questions = new HashSet<Question>();
        }
    }
}
