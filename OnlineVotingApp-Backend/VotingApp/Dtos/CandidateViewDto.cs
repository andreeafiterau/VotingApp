﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Dtos
{
    public class CandidateViewDto
    {
        public int IdCandidate { get; set; } //the candidate id

        public int IdUser { get; set; } //the user id for the candidate

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int IdElectoralRoom { get; set; } //where the candidate is signed up
    }
}
