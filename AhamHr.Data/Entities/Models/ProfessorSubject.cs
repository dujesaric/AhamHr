﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhamHr.Data.Entities.Models
{
    public class ProfessorSubject
    {
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
