using APPZ_new.Enums;
using APPZ_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.SqlTaskModels
{
    public class SqlTask
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Scope { get; set; }

        public ICollection<SqlAnswer> Answers { get; set; }
        public ICollection<SqlUserTask> UserTasks { get; set; }


        public TaskSeverity Severity { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public SqlTask()
        {
            Answers = new HashSet<SqlAnswer>();
            UserTasks = new HashSet<SqlUserTask>();

        }
    }

    public class SqlAnswer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int SortValue { get; set; }
        public bool IsUnUsed { get; set; }

        public int SqlTaskId { get; set; }

        public virtual SqlTask Task { get; set; }
        public virtual ICollection<SqlUserTask> UserTasks { get; set; }

        public SqlAnswer()
        {
            UserTasks = new HashSet<SqlUserTask>();

        }
    }

    public class SqlUserTask
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TaskId { get; set; }
        public virtual SqlTask Task { get; set; }

        public int Mark { get; set; }

        public ICollection<SqlAnswer> SqlAnswers { get; set; }
        public SqlUserTask()
        {
            SqlAnswers = new HashSet<SqlAnswer>();
        }
    }
}
