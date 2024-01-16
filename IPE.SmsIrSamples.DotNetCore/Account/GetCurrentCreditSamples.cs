using IPE.SmsIrClient;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Account;

public static class GetCurrentCreditSamples
{
    /// <summary>
    /// دریافت مقدار اعتبار فعلی
    /// برای مشاهده مقدار اعتبار فعلی از متد زیر استفاده نمایید.
    /// https://app.sms.ir/developer/help/credit
    /// </summary>
    public static async Task GetCurrentCreditAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // انجام دریافت اعتبار
            var response = await smsIr.GetCreditAsync();

            // دریافت اعتبار شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            decimal credit = response.Data;

            // توضیحات خروجی
            string description = $"Credit: {credit}";

            await Console.Out.WriteLineAsync(description);
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