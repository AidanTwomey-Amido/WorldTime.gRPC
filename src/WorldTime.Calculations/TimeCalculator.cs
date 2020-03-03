using System;

namespace WorldTime.Calculations
{
    public interface ITimeCalculator
    {
        int GetOffset(Point point);
    }
    public class Point 
    {
         public double Latitude {get; set;}
         public double Longitude {get; set;}
    }
    
    public class GmtOffsetApproximator : ITimeCalculator
    {
        public int GetOffset(Point point)
        {
            // assume time zones all same size - this is not true but easy
            var segmentSize = (180.0 / 12.0);

            double lastSegment = 0;
            double nextSegment = 0;

            var absLongitude = Math.Abs(point.Longitude);

            for (int offset = 0; offset < 12; offset++)
            {
                nextSegment += segmentSize;

                if (absLongitude >= lastSegment && absLongitude < nextSegment)
                    return Math.Sign(point.Longitude) * offset;

                lastSegment = nextSegment;
            }


            throw new ArgumentException($"longitude {point.Longitude} is invalid");
        }
    }
}
