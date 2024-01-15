using MD.PersianDateTime.Standard;
using System;

namespace IPE.SmsIrSamples.DotNetCore;

/// <summary>
/// برای تبدیل تاریخ میلادی به زمان یونیکس به دو تابع نوشته شده در زیر دقت کنید:
///
/// ToUnixTime()
/// ToUnixTime2()
///
/// ----------------------------------------
///
/// برای تبدیل تاریخ شمسی به زمان یونیکس شما چندین راه دارید:
///
/// 1. استفاده از پکیج آماده
/// نام پکیج: MD.PersianDateTime
/// آدرس پکیج: https://github.com/Mds92/MD.PersianDateTime
///
/// 2. استفاده از سایت‌های تبدیل
/// https://timestamp.ir/
/// https://navidak.ir/timestamp-conversion
/// </summary>
public static class UnixTimeSamples
{
    // روش اول تبدیل تاریخ میلادی به زمان یونیکس
    public static int ToUnixTime(this DateTime utcDateTime)
    {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(utcDateTime);
        return (int)dateTimeOffset.ToUnixTimeSeconds();
    }

    // روش دوم تبدیل تاریخ میلادی به زمان یونیکس
    public static int ToUnixTime2(this DateTime utcDateTime)
    {
        return Convert.ToInt32(utcDateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
    }

    // تبدیل زمان یونیکس به تاریخ میلادی
    public static DateTime ToDateTime(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime.ToLocalTime();
    }

    // شما با این کد می‌تواند زمان یونیکس ساعت 10 روز بعد را دریافت کنید
    public static int Get10AmOfTheNextDayUnixTime()
    {
        DateTime currentDateTime = DateTime.Now;
        DateTime nextDayAt10AM = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day + 1, 10, 0, 0);
        int nextDayAt10AmUnixTime = (int)new DateTimeOffset(nextDayAt10AM).ToUnixTimeSeconds();
        return nextDayAt10AmUnixTime;
    }

    // شما با این کد می‌تواند زمان یونیکس یک ماه پیش را دریافت کنید
    public static int GetOneMonthBeforeUnixTime()
    {
        return (int)DateTimeOffset.UtcNow.AddMonths(-1).ToUnixTimeSeconds();
    }

    /// <summary>
    /// برای تبدیل تاریخ شمسی به میلادی می‌توانید از پکیج آماده استفاده کنید.:
    /// نام پکیج: MD.PersianDateTime
    /// آدرس پکیج: https://github.com/Mds92/MD.PersianDateTime
    /// </summary>
    public static int GetUnixTimeWithMdPersianDateTimePackage(int persianYear, int persianMonth, int persianDay,
        int hour, int minute, int second)
    {
        PersianDateTime persianDateTime = new PersianDateTime(persianYear, persianMonth, persianDay, hour, minute, second);
        return persianDateTime.ToUniversalTime().ToDateTime().ToUnixTime();
    }
}