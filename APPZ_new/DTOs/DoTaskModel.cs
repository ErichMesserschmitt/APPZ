﻿using APPZ_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.DTOs
{
    public class DoTaskModel
    {
        public int TaskId { get; set; }

        public string TaskTitle { get; set; }
        public int UserId { get; set; }

        public IEnumerable<Question> Questions { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
    }
}
