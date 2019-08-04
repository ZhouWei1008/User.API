using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerRegister.Expansion.Consul
{
    [Route("HealthCheck")]
    public class HealthCheckController
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return new Microsoft.AspNetCore.Mvc.ContentResult()
            {
                Content = "OK"
            };
        }
    }
}
