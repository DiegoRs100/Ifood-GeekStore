using System;

namespace GeekStore.Core.Extentions
{
    public static class DateTimeExtentions
    {
        public static string ToUriFormat(this DateTime data)
        {
            return data.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static DateTime RemoverMilisSegundos(this DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day, data.Hour, data.Minute, data.Second, data.Kind);
        }

        public static bool IsInRange(this TimeSpan source, TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime <= endTime)
            {
                if (source < startTime || DateTime.Now.TimeOfDay > endTime)
                    return false;
            }
            else
            {
                if (source < startTime && DateTime.Now.TimeOfDay > endTime)
                    return false;
            }

            return true;
        }
    }
}