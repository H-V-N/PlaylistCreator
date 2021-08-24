using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCache.HealthCheck
{
    public class HealthCheckAppService : SpotifyCacheAppServiceBase, IHealthCheckAppService
    {
        public void GetHealthy()
        {
            return;
        }

        public void GetUnhealthy()
        {
            throw new Exception("I am unhealthy!!!!");
        }
    }
}
