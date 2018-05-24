using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationCenterDemo.Controllers
{
    [Route("api/oss-config")]
    public class OssConfigController : Controller
    {
        private readonly OssConfig _ossConfig;

        public OssConfigController(OssConfig ossConfig)
        {
            _ossConfig = ossConfig;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_ossConfig);
        }
    }
}
