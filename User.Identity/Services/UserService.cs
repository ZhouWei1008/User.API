using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
namespace User.Identity.Services
{
    public class UserService : IUserService
    {
        private  HttpClient _httpClient;
        private readonly string _userServiceUrl = "http://localhost:5000/";


        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CheckOrCreate(string phone)
        {
            var form = new Dictionary<string, string> { { "phone", phone } };
            var content = new FormUrlEncodedContent(form);
            var response= await _httpClient.PostAsync($"{_userServiceUrl}api/users/check-or-create", content);

            if (response.StatusCode == HttpStatusCode.OK) {
                var userid = await response.Content.ReadAsStringAsync();
                int.TryParse(userid, out int intUserId);
                return intUserId;
            }

            return 0;
        }
    }
}
