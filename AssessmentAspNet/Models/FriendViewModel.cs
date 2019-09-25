using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssessmentAspNet.Models {
    public class FriendViewModel {
        public int Id { get; set; }
        [Display (Name = "FristName")]
        public string FristName { get; set; }
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}