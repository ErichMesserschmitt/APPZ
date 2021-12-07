using APPZ_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.Enums
{
    public class QuestionDto : Question
    {
        public int AnswerId { get; set; }
    }
}
