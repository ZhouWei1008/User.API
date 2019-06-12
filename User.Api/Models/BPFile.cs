using System;
namespace User.Api.Models {
    public class BPFile {
        public int ID{set;get;}
        public int UserID{get;set;}

        public string FileName{get;set;}

        public string OriginFilePath{get;set;}
        public string FormatFilePath{get;set;}
        public DateTime CreateTime{get;set;}
     }
}