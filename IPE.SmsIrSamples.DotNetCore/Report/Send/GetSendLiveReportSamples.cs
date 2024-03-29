﻿using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using IPE.SmsIrSamples.DotNetCore.Report.Models;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Send;

public static class GetSendLiveReportSamples
{
    /// <summary>
    /// گزارش ارسال‌های روز
    /// با استفاده از متد زیر، گزارشی از ارسال‌های انجام شده در روز جاری قابل دریافت است.
    /// https://app.sms.ir/developer/help/sendLive
    /// </summary>
    public static async Task GetSendLiveReportAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // گزارش‌ها در صفحات حداکثر 100 تایی قابل دریافت می‌باشد.

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
                DeliveryState delivertState = message.DeliveryState.HasValue ? (DeliveryState)message.DeliveryState : DeliveryState.Pending;
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
                "UnauthorizedException" => "The provided token is not valid or access is denied.",
                "LogicalException" => "Please check and correct the request parameters.",
                "TooManyRequestException" => "The request count has exceeded the allowed limit.",
                "UnexpectedException" or "InvalidOperationException" => "An unexpected error occurred on the remote server.",
                _ => "Unable to send the request due to an unspecified error.",
            };

            var errorDescription = "There is a problem with the request." +
                $"\n - Error: {errorName} - {errorNameDescription} - {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }
}