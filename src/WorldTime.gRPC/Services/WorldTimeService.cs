using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using WorldTime.Calculations;

namespace WorldTime.gRPC
{
    public class WorldTimeService : WorldTime.WorldTimeBase
    {
        private readonly ITimeCalculator timeCalcualator;
        private readonly ILogger<GreeterService> logger;

        public WorldTimeService(ITimeCalculator timeCalcualator, ILogger<GreeterService> logger)
        {
            this.timeCalcualator = timeCalcualator;
            this.logger = logger;
        }

        public override Task<WorldTimeReply> GetTime(Point request, ServerCallContext context)
        {
            int offset = timeCalcualator.GetOffset(MapPoint(request));

            return Task.FromResult(new WorldTimeReply
            {
                Localtime = DateTime.Now.AddHours(offset).ToShortTimeString()
            });
        }

        private static Calculations.Point MapPoint(Point request)
        {
            return new Calculations.Point()
            {
                Longitude = request.Longitude,
                Latitude = request.Latitude
            };
        }

        // public override Task<WorldTimeReply> SayHello(Point request, ServerCallContext context)
        // {
        //     return Task.FromResult(new WorldTimeReply
        //     {
        //         Localtime = DateTime.Now.ToShortTimeString()
        //     });
        // }

    }
}
