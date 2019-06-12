using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.JsonPatch;
namespace User.Api.Controllers
{
    [Route("api/user")]
    public class UserController : BaseContriller
    {
        UserContext _userContext = null;
        ILogger<UserController> _logger;

        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Get()
        {
            var user = _userContext.Users
            .AsNoTracking()
            .Include(x => x.Porpertys)
            .SingleOrDefault(x => x.ID == UserIdentity.UserID);
            if (user == null)
            {
                throw new UserOperationException($"用户不存在 ID:{base.UserIdentity.UserID}");
            }
            return Json(user);
        }
        [Route("")]
        [HttpPatch]
        public async Task<IActionResult> PATCH([FromBody]JsonPatchDocument<Models.AppUser> doc)
        {
            var user = await _userContext.Users.SingleOrDefaultAsync(x => x.ID == UserIdentity.UserID);
            doc.ApplyTo(user);

            user.Porpertys.ForEach(_ =>_userContext.Entry(_).State= EntityState.Detached);             

            var originPorpertys = await _userContext.UserPorpertys.AsNoTracking().Where(x => x.AppUserID == UserIdentity.UserID).ToListAsync();
            var allPorpertys = originPorpertys.Union(user.Porpertys).Distinct();

            var removePorpertys = originPorpertys.Except(user.Porpertys);
            var addPorpertys = allPorpertys.Except(originPorpertys);

            foreach (var porperty in removePorpertys)
            {
                _userContext.Remove(porperty);
            }

            foreach (var porperty in addPorpertys)
            {
                _userContext.Add(porperty);
            }
            _userContext.Users.Update(user);
            _userContext.SaveChanges();
            return Json(user);
        }
        [Route("check-or-create")]
        [HttpPost]
        public async Task<IActionResult> CheckOrCreate(string phone) {

            if (_userContext.Users.Any(x => x.Phone == phone)) {
                _userContext.Users.Add(new Models.AppUser { Phone = phone });
            }
            return Ok();
        }

    }
}
