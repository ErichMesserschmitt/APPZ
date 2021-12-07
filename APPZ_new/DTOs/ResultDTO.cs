using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.DTOs
{
    public class ResultDTO
    {
        public int TaskId { get; set; }
        public int TotalQuestionsCount { get; set; }
        public int CorrectAnswersCount { get; set; }

        public int Mark { get
            {
                return (int)((double)(10 / TotalQuestionsCount) * CorrectAnswersCount);
            } 
        }
    }
}
