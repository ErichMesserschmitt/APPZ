using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AnswerId")]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }

        User()
        {
            Tasks = new HashSet<UserTask>();
        }
    }
}
