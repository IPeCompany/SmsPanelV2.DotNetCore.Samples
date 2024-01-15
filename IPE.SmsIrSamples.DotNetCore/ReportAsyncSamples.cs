using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore;

public static class ReportAsyncSamples
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

            // انجام ارسال گروهی
            var sendResult = await smsIr.GetLiveReportAsync(pageNumber, pageSize);

            // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش گزارش ارسال‌های روزانه
            MessageReportResult[] messages = sendResult.Data;

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
                int? deliveryDateTimeInUnix = message.DeliveryDateTime;

                // هزینه ارسال این پیامک
                decimal cost = message.Cost;

                // خلاصه‌ای از هر پیامک
                string messageDescription = $"Mobile: {message.Mobile}" +
                    $" - Delivery State: {deliveryStateDescription}" +
                    $" - Delivery UnixTime: {deliveryDateTimeInUnix ?? '-'}" +
                    Environment.NewLine;

                reportDescription += messageDescription;
            }

            await Console.Out.WriteLineAsync(reportDescription);
        }
        catch (Exception ex) // ارسال ناموفق
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