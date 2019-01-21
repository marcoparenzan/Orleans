using Microsoft.Extensions.Hosting;
using Orleans.Hosting;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Silo.Services
{
    public class SiloService : IHostedService
    {
        private ClusterConfiguration siloConfig;
        private SiloHost silo;

        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            await Start();
        }

        private async Task Start()
        {
            siloConfig = ClusterConfiguration.LocalhostPrimarySilo();
            silo = new SiloHost("Test Silo", siloConfig);
            silo.InitializeSilo();
            await silo.StartSiloAsync();
        }

        async Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            await Stop();
        }

        private async Task Stop()
        {
            // shut the silo down after we are done.
            var ct = new CancellationToken();
            await silo.ShutdownSiloAsync(ct);
        }
    }
}
