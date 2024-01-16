using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Send;

public static class SendVerifySamples
{
    /// <summary>
    /// ارسال وریفای
    /// به منظور ارسال کد اعتبارسنجی، کد تایید، فاکتور خرید و به طور کلی پیامک‌هایی با اولویت بالا
    /// برای استفاده از این نوع ارسال ابتدا قالب پیامک خود را در پنل (بخش ارسال سریع) مشخص نمایید.
    /// این نوع از ارسال با خطوط خدماتی ارسال میشود
    /// https://app.sms.ir/developer/help/verify
    /// </summary>
    public static async Task SendVerifyAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // شماره موبایل مقصد
            string mobile = "9120000000";

            /// <summary>
            /// شناسه قالب ساخته‌شده در سامانه
            /// https://app.sms.ir/developer/fast-send/template
            /// متن قالب نمونه:
            /// جناب #NAME#
            /// کد ورود:
            /// Code:#CODE#
            /// سامانه پیامکی SMS.ir
            /// </summary>
            int templateId = 200000;

            // مقداردهی پارامترهای استفاده شده در قالب
            string name = "آقای محمد محمدی";
            int code = 12345;
            VerifySendParameter[] verifySendParameters = {
                new VerifySendParameter("NAME", name),
                new VerifySendParameter("CODE", code.ToString()),
            };

            // انجام ارسال وریفای
            var response = await smsIr.VerifySendAsync(mobile, templateId, verifySendParameters);

            // ارسال شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            VerifySendResult verifySendResult = response.Data;

            // شناسه پیامک ارسال شده
            int messageId = verifySendResult.MessageId;

            // هزینه ارسال
            decimal cost = verifySendResult.Cost;

            string resultDescription = "Your message was sent successfully." +
                $"\n - Message Id: {messageId} " +
                $"\n - Cost: {cost}";

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

    /// <summary>
    /// ارسال وریفای
    /// به منظور ارسال کد اعتبارسنجی، کد تایید، فاکتور خرید و به طور کلی پیامک‌هایی با اولویت بالا
    /// برای استفاده از این نوع ارسال ابتدا قالب پیامک خود را در پنل (بخش ارسال سریع) مشخص نمایید.
    /// این نوع از ارسال با خطوط خدماتی ارسال میشود
    /// https://app.sms.ir/developer/help/verify
    /// </summary>
    public static void SendVerify()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // شماره موبایل مقصد
            string mobile = "9120000000";

            /// <summary>
            /// شناسه قالب ساخته‌شده در سامانه
            /// https://app.sms.ir/developer/fast-send/template
            /// متن قالب نمونه:
            /// جناب #NAME#
            /// کد ورود:
            /// Code:#CODE#
            /// سامانه پیامکی SMS.ir
            /// </summary>
            int templateId = 200000;

            // مقداردهی پارامترهای استفاده شده در قالب
            string name = "آقای محمد محمدی";
            int code = 12345;
            VerifySendParameter[] verifySendParameters = {
                new VerifySendParameter("NAME", name),
                new VerifySendParameter("CODE", code.ToString()),
            };

            // انجام ارسال وریفای
            var response = smsIr.VerifySend(mobile, templateId, verifySendParameters);

            // ارسال شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            VerifySendResult verifySendResult = response.Data;

            // شناسه پیامک ارسال شده
            int messageId = verifySendResult.MessageId;

            // هزینه ارسال
            decimal cost = verifySendResult.Cost;

            string resultDescription = "Your message was sent successfully." +
                $"\n - Message Id: {messageId} " +
                $"\n - Cost: {cost}";

            Console.Out.WriteLine(resultDescription);
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

            Console.Out.WriteLine(errorDescription);
        }
    }
}