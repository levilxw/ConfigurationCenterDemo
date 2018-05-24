using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationCenterDemo
{
    public class OssConfig
    {
        public string BaseUrl { get; set; }
        public string BucketName { get; set; }
        public string AccessId { get; set; }
        public string SecretId { get; set; }
    }
}
