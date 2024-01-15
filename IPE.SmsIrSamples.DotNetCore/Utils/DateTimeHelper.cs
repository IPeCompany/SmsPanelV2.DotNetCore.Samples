using System;

namespace IPE.SmsIrSamples.DotNetCore.Utils;

public static class DateTimeHelper
{
    public static DateTime UnixTimeToDateTime(int unixTime)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
    }
}