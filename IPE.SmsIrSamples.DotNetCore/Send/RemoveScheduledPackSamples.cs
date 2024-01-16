using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Send;

public static class RemoveScheduledPackSamples
{
    /// <summary>
    /// حذف ارسال زمانبندی شده
    /// به‌منظور حذف و انصراف از ارسال زمانبندی شده می‌توانید از متد زیر استفاده نمایید.
    /// در این متد، از شناسه مجموعه ارسال استفاده می‌شود.
    /// https://app.sms.ir/developer/help/bulk
    /// </summary>
    public static async Task RemoveScheduledPackAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // شناسه مجموعه ارسال
            Guid packId = new Guid("86D96B0E-FD89-4C19-B303-C0B4D3874063");

            // انجام ارسال گروهی
            var response = await smsIr.RemoveScheduledMessagesAsync(packId);

            // ارسال شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            RemoveScheduledMessagesResult result = response.Data;

            // مقدار اعتبار بازگشتی
            decimal returnedCreditCount = result.ReturnedCreditCount;

            // تعداد پیامک‌ها
            decimal smsCount = result.SmsCount;

            string resultDescription = "Your scheduled pack was successfully removed." +
                $"\n - Returned Credit Count: {returnedCreditCount} " +
                $"\n - Sms Count: {smsCount} ";

            await Console.Out.WriteLineAsync(resultDescription);
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