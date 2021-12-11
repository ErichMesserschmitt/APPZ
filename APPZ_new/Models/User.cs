using APPZ_new.Enums;
using APPZ_new.SqlTaskModels;
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
        [Column("UserId")]
        public int Id { get; set; }

        public string Name { get; set; }

        public UserRole Role { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }

        public virtual ICollection<SqlUserTask> SqlTasks { get; set; }


        public User()
        {
            Tasks = new HashSet<UserTask>();
            SqlTasks = new HashSet<SqlUserTask>();

        }
    }
}
