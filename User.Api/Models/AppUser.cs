using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models
{
    public class AppUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Phone{get;set;}
        public string Avatar{get;set;}
        /// 1 男 0女
        public byte Gender{get;set;}
        public string Address{get;set;}
        public string Email{get;set;}
        public string Tel{get;set;}
        //省
        public string Province{get;set;}
        //省ID
        public int ProvinceID{get;set;} 
        ///城市
        public string City{get;set;}
        ///城市ID
        public int CityID{get;set;}
        public string NameCard{get;set;}

        //用户属性
        public List<UserPorperty>  Porpertys{get;set;}
    }
}
