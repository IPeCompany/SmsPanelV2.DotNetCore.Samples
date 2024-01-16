using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using IPE.SmsIrSamples.DotNetCore.Report.Models;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Send;

public static class GetSingleMessageSamples
{
    /// <summary>
    /// گزارش پیامک - دریافت وضعیت
    /// با فراخوانی این متد، به دریافت اطلاعات پیامک و همینطور اطلاع از وضعیت آن اقدام نمایید.
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
}