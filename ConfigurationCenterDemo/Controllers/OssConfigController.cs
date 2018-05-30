using Microsoft.AspNetCore.Mvc;

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
