using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Send;

public static class GetSendPacksSamples
{
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
}