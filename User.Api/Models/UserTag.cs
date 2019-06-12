using System;
namespace User.Api.Models {
    public class UserTag {
        public int UserID { get; set; }
        public string Tag { get; set; }
        public DateTime CreatedTime{get;set;}
    }
}