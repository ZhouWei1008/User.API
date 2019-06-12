using System;

namespace User.Api.Models
{
    public class UserPorperty
    {
        public int AppUserID { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
   
        int? _requestedHashCode;
        public override int GetHashCode(){
            if(!this.IsTransient()){
                if(!_requestedHashCode.HasValue){
                    _requestedHashCode=(this.Key+this.Value).GetHashCode()^31;
                }
                return _requestedHashCode.Value;
            }else {
                return base.GetHashCode();
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserPorperty))
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            var item = (UserPorperty)obj;
            if (this.IsTransient() || item.IsTransient())
            {
                return false;
            }
            else
            {
                return item.Key == this.Key && item.Value == this.Value;
            }
        }
        private bool IsTransient()
        {
            return string.IsNullOrEmpty(this.Key) || string.IsNullOrEmpty(this.Value);
        }


        public static bool operator ==(UserPorperty left, UserPorperty right)
        {
            if (object.Equals(left, null))
            {
                return (Object.Equals(right, null));
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(UserPorperty left, UserPorperty right)
        {
            return !(left == right);
        }
    }
}
