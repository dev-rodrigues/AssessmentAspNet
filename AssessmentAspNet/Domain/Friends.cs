﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentAspNet.Domain {
    public class Friends {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Greeting() {
            return $"Hello, my name is {FristName}";
        }
    }
}