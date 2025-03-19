using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainManagement.Common.Helpers
{
    public static class TimeZone
    {
        public static DateTime GetCurrentTimeInUtcMinus5(this DateTime dateTime)
        {
            TimeSpan offset = TimeSpan.FromHours(-5);
            DateTime localTime = dateTime.Add(offset);

            return localTime;
        }
    }
}
