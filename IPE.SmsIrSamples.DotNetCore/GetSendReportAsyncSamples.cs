using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore;

public static class GetSendReportAsyncSamples
{
    /// <summary>
    /// گزارش ارسال‌های روز
    /// https://app.sms.ir/developer/help/sendLive
    /// </summary>
    public static async Task GetSendLiveReportAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // گزارش‌ها در صفحات 100 تایی قابل دریافت می‌باشد.

            // شماره صفحه‌ - دارای پیشفرض 1
            int pageNumber = 1;

            // تعداد آیتم‌های هر صفحه - دارای پیشفرض 100
            // این عدد نمی‌تواند بیشتر از 100 باشد.
            int pageSize = 100;

            // انجام دریافت گزارش
            var response = await smsIr.GetLiveReportAsync(pageNumber, pageSize);

            // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            MessageReportResult[] messages = response.Data;

            // بررسی و نمایش پیامک‌های گزارش
            string reportDescription = string.Empty;
            foreach (var message in messages)
            {
                // شناسه پیامک
                int messageId = message.MessageId;

                // خط استفاده شده برای ارسال
                long lineNumber = message.LineNumber;

                // شماره موبایل مقصد
                long mobile = message.Mobile;

                // پیام فرستاده شده
                string messageText = message.MessageText;

                // زمان ارسال
                int sendDateTimeInUnix = message.SendDateTime;

                // وضعیت دریافت
                DeliveryState delivertState = message.DeliveryState.HasValue ? (DeliveryState)message.DeliveryState : DeliveryState.Unknown;
                string deliveryStateDescription = delivertState.ToString();

                // زمان دریافت
                int? deliveryUnixTime = message.DeliveryDateTime;
                DateTime? deliveryDateTime = deliveryUnixTime.HasValue ?
                    DateTimeOffset.FromUnixTimeSeconds(deliveryUnixTime.Value).DateTime.ToLocalTime() : null;

                // هزینه ارسال این پیامک
                decimal cost = message.Cost;

                // خلاصه‌ای از هر پیامک
                string messageDescription = $"Mobile: {mobile}" +
                    $" - Delivery State: {deliveryStateDescription}" +
                    $" - Delivery DateTime: {deliveryDateTime?.ToString("dddd, dd MMMM yyyy HH:mm:ss") ?? "-"}" +
                    Environment.NewLine;

                reportDescription += messageDescription;
            }

            await Console.Out.WriteLineAsync(reportDescription);
        }
        catch (Exception ex) // درخواست ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorName = ex.GetType().Name;
            string errorNameDescription = errorName switch
            {
                "UnauthorizedException" => "Token is not valid or access denied",
                "LogicalException" => "Please fix the request parameters",
                "TooManyRequestException" => "Request count has exceed the limitation",
                "UnexpectedException" or "InvalidOperationException" => "Remote server error",
                _ => "Can not send the request",
            };

            var errorDescription = "There is a problem with the request." +
                $"\n - Error: {errorName} - {errorNameDescription} - {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }

    /// <summary>
    /// گزارش ارسال‌های آرشیو شده
    /// https://app.sms.ir/developer/help/sendArchive
    /// </summary>
    public static async Task GetSendArchiveReportAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // پارامتر اختیاری تاریخ شروع
            // زمان به صورت یونیکس تایم می‌باشد مثلا 1700598600 معادل 1 آذر 1402 می‌باشد
            int? fromDateUnixTime = null;

            // برای مثال برای گرفتن زمان یونیکس یک ماه پیش
            int oneMonthBeforeUnixTime = (int)DateTimeOffset.UtcNow.AddMonths(-1).ToUnixTimeSeconds();

            // پارامتر اختیاری تاریخ پایان
            // زمان به صورت یونیکس تایم می‌باشد مثلا 1703190600 معادل 1 دی 1402 می‌باشد
            int? toDateUnixTime = null;

            // گزارش‌ها در صفحات 100 تایی قابل دریافت می‌باشد.

            // شماره صفحه‌ - دارای پیشفرض 1
            int pageNumber = 1;

            // تعداد آیتم‌های هر صفحه - دارای پیشفرض 100
            // این عدد نمی‌تواند بیشتر از 100 باشد.
            int pageSize = 100;

            // انجام دریافت گزارش
            var response = await smsIr.GetArchivedReportAsync(pageNumber, pageSize, fromDateUnixTime, toDateUnixTime);

            // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            MessageReportResult[] messages = response.Data;

            // بررسی و نمایش پیامک‌های گزارش
            string reportDescription = string.Empty;
            foreach (var message in messages)
            {
                // شناسه پیامک
                int messageId = message.MessageId;

                // خط استفاده شده برای ارسال
                long lineNumber = message.LineNumber;

                // شماره موبایل مقصد
                long mobile = message.Mobile;

                // پیام فرستاده شده
                string messageText = message.MessageText;

                // زمان ارسال
                int sendDateTimeInUnix = message.SendDateTime;

                // وضعیت دریافت
                DeliveryState delivertState = message.DeliveryState.HasValue ? (DeliveryState)message.DeliveryState : DeliveryState.Unknown;
                string deliveryStateDescription = delivertState.ToString();

                // زمان دریافت
                int? deliveryUnixTime = message.DeliveryDateTime;
                DateTime? deliveryDateTime = deliveryUnixTime.HasValue ?
                    DateTimeOffset.FromUnixTimeSeconds(deliveryUnixTime.Value).DateTime.ToLocalTime() : null;

                // هزینه ارسال این پیامک
                decimal cost = message.Cost;

                // خلاصه‌ای از هر پیامک
                string messageDescription = $"Mobile: {mobile}" +
                    $" - Delivery State: {deliveryStateDescription}" +
                    $" - Delivery DateTime: {deliveryDateTime?.ToString("dddd, dd MMMM yyyy HH:mm:ss") ?? "-"}" +
                    Environment.NewLine;

                reportDescription += messageDescription;
            }

            await Console.Out.WriteLineAsync(reportDescription);
        }
        catch (Exception ex) // درخواست ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorName = ex.GetType().Name;
            string errorNameDescription = errorName switch
            {
                "UnauthorizedException" => "Token is not valid or access denied",
                "LogicalException" => "Please fix the request parameters",
                "TooManyRequestException" => "Request count has exceed the limitation",
                "UnexpectedException" or "InvalidOperationException" => "Remote server error",
                _ => "Can not send the request",
            };

            var errorDescription = "There is a problem with the request." +
                $"\n - Error: {errorName} - {errorNameDescription} - {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }

    /// <summary>
    /// گزارش پیامک - دریافت وضعیت
    /// https://app.sms.ir/developer/help/sendReports
    /// </summary>
    public static async Task GetSingleMessageReportAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // شناسه پیامک
            int messageId = 10000000;

            // انجام دریافت گزارش
            var response = await smsIr.GetReportAsync(messageId);

            // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            MessageReportResult messageReport = response.Data;

            // شناسه پیامک دریافتی
            int returnedMessageId = messageReport.MessageId;

            // خط استفاده شده برای ارسال
            long lineNumber = messageReport.LineNumber;

            // شماره موبایل مقصد
            long mobile = messageReport.Mobile;

            // پیام فرستاده شده
            string messageText = messageReport.MessageText;

            // زمان ارسال
            int sendDateTimeInUnix = messageReport.SendDateTime;

            // وضعیت دریافت
            DeliveryState delivertState = messageReport.DeliveryState.HasValue ?
                (DeliveryState)messageReport.DeliveryState : DeliveryState.Unknown;
            string deliveryStateDescription = delivertState.ToString();

            // زمان دریافت
            int? deliveryUnixTime = messageReport.DeliveryDateTime;
            DateTime? deliveryDateTime = deliveryUnixTime.HasValue ?
                DateTimeOffset.FromUnixTimeSeconds(deliveryUnixTime.Value).DateTime.ToLocalTime() : null;

            // هزینه ارسال این پیامک
            decimal cost = messageReport.Cost;

            // خلاصه‌ای از پیامک
            string messageDescription = $"Line: {lineNumber}" +
                $" - Mobile: {mobile}" +
                $" - Delivery State: {deliveryStateDescription}" +
                $" - Delivery DateTime: {deliveryDateTime?.ToString("dddd, dd MMMM yyyy HH:mm:ss") ?? "-"}";

            await Console.Out.WriteLineAsync(messageDescription);
        }
        catch (Exception ex) // درخواست ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorName = ex.GetType().Name;
            string errorNameDescription = errorName switch
            {
                "UnauthorizedException" => "Token is not valid or access denied",
                "LogicalException" => "Please fix the request parameters",
                "TooManyRequestException" => "Request count has exceed the limitation",
                "UnexpectedException" or "InvalidOperationException" => "Remote server error",
                _ => "Can not send the request",
            };

            var errorDescription = "There is a problem with the request." +
                $"\n - Error: {errorName} - {errorNameDescription} - {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }

    /// <summary>
    /// گزارش محموعه ارسال‌های روز
    /// https://app.sms.ir/developer/help/livePack
    /// </summary>
    public static async Task GetSendPacksLiveReportAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // گزارش‌ها در صفحات 100 تایی قابل دریافت می‌باشد.

            // شماره صفحه‌ - دارای پیشفرض 1
            int pageNumber = 1;

            // تعداد آیتم‌های هر صفحه - دارای پیشفرض 100
            // این عدد نمی‌تواند بیشتر از 100 باشد.
            int pageSize = 100;

            // انجام دریافت گزارش
            var response = await smsIr.GetSendPacksAsync(pageNumber, pageSize);

            // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            PackResult[] packs = response.Data;

            // بررسی و نمایش مجموعه‌های ارسال
            string reportDescription = string.Empty;
            foreach (var pack in packs)
            {
                // شناسه مجموعه ارسال
                Guid packId = pack.PackId;

                // تعداد مخاطبان
                long recipientCount = pack.RecipientCount;

                // تاریخ ایجاد
                int creationUnixTime = pack.CreationDateTime;
                DateTime creationDateTime = DateTimeOffset.FromUnixTimeSeconds(creationUnixTime).DateTime.ToLocalTime();

                // خلاصه‌ای از هر مجموعه ارسال
                string messageDescription = $"Pack Id: {packId}" +
                    $" - Recipient Count: {recipientCount}" +
                    $" - Creation DateTime: {creationDateTime:dddd, dd MMMM yyyy HH:mm:ss}" +
                    Environment.NewLine + Environment.NewLine;

                reportDescription += messageDescription;
            }

            await Console.Out.WriteLineAsync(reportDescription);
        }
        catch (Exception ex) // درخواست ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorName = ex.GetType().Name;
            string errorNameDescription = errorName switch
            {
                "UnauthorizedException" => "Token is not valid or access denied",
                "LogicalException" => "Please fix the request parameters",
                "TooManyRequestException" => "Request count has exceed the limitation",
                "UnexpectedException" or "InvalidOperationException" => "Remote server error",
                _ => "Can not send the request",
            };

            var errorDescription = "There is a problem with the request." +
                $"\n - Error: {errorName} - {errorNameDescription} - {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }

    /// <summary>
    /// گزارش مجموعه ارسال
    /// https://app.sms.ir/developer/help/sendPack
    /// </summary>
    public static async Task GetSendPackReportAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // شناسه مجموعه ارسال
            Guid packId = new Guid("86D96B0E-FD89-4C19-B303-C0B4D3874063");

            // انجام دریافت گزارش
            var response = await smsIr.GetReportAsync(packId);

            // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            MessageReportResult[] messages = response.Data;

            // بررسی و نمایش پیامک‌های گزارش
            string reportDescription = $"Pack Id: {packId}" + Environment.NewLine + Environment.NewLine;
            foreach (var message in messages)
            {
                // شناسه پیامک
                int messageId = message.MessageId;

                // خط استفاده شده برای ارسال
                long lineNumber = message.LineNumber;

                // شماره موبایل مقصد
                long mobile = message.Mobile;

                // پیام فرستاده شده
                string messageText = message.MessageText;

                // زمان ارسال
                int sendDateTimeInUnix = message.SendDateTime;

                // وضعیت دریافت
                DeliveryState delivertState = message.DeliveryState.HasValue ? (DeliveryState)message.DeliveryState : DeliveryState.Unknown;
                string deliveryStateDescription = delivertState.ToString();

                // زمان دریافت
                int? deliveryUnixTime = message.DeliveryDateTime;
                DateTime? deliveryDateTime = deliveryUnixTime.HasValue ?
                    DateTimeOffset.FromUnixTimeSeconds(deliveryUnixTime.Value).DateTime.ToLocalTime() : null;

                // هزینه ارسال این پیامک
                decimal cost = message.Cost;

                // خلاصه‌ای از هر پیامک
                string messageDescription = $"Mobile: {mobile}" +
                    $" - Delivery State: {deliveryStateDescription}" +
                    $" - Delivery DateTime: {deliveryDateTime?.ToString("dddd, dd MMMM yyyy HH:mm:ss") ?? "-"}" +
                    Environment.NewLine;

                reportDescription += messageDescription;
            }

            await Console.Out.WriteLineAsync(reportDescription);
        }
        catch (Exception ex) // درخواست ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorName = ex.GetType().Name;
            string errorNameDescription = errorName switch
            {
                "UnauthorizedException" => "Token is not valid or access denied",
                "LogicalException" => "Please fix the request parameters",
                "TooManyRequestException" => "Request count has exceed the limitation",
                "UnexpectedException" or "InvalidOperationException" => "Remote server error",
                _ => "Can not send the request",
            };

            var errorDescription = "There is a problem with the request." +
                $"\n - Error: {errorName} - {errorNameDescription} - {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }
}