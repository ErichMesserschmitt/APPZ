using APPZ_new.Enums;
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
                if (TotalQuestionsCount == 0)
                    return 0;
                return (int)((double)(10 / TotalQuestionsCount) * CorrectAnswersCount);
            } 
        }
    }
}
