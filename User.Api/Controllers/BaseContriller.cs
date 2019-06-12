using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Api.Dots;
namespace User.Api.Controllers
{
    public class BaseContriller : Controller
    {
        protected UserIdentity UserIdentity => new UserIdentity { UserID = 1, Name = "Ian" };
    }
}