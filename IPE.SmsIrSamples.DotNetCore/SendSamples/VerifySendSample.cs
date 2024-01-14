using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.SendSamples;

public static class VerifySendSample
{
    /// <summary>
    /// نمونه ارسال وریفای
    /// https://app.sms.ir/developer/help/verify
    /// </summary>
    public static async Task SendSampleVerifyAsync()
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
            var sendResult = await smsIr.VerifySendAsync(mobile, templateId, verifySendParameters);

            // ارسال شما در اینجا با موفقیت انجام شده‌است.

            //شناسه پیامک ارسال شده
            int messageId = sendResult.Data.MessageId;

            //هزینه ارسال
            decimal cost = sendResult.Data.Cost;

            string resultDescription = "Your message was sent successfully." +
                $"\n - Message Id: {messageId} " +
                $"\n - Cost: {cost}";

            await Console.Out.WriteLineAsync(resultDescription);
        }
        catch (Exception ex) // ارسال ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorDescription = "Unable to send SMS message." +
                $"\n - Error: {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }

    /// <summary>
    /// نمونه ارسال وریفای سینک
    /// https://app.sms.ir/developer/help/verify
    /// </summary>
    public static void SendSampleVerify()
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
            var sendResult = smsIr.VerifySend(mobile, templateId, verifySendParameters);

            // ارسال شما در اینجا با موفقیت انجام شده‌است.

            //شناسه پیامک ارسال شده
            int messageId = sendResult.Data.MessageId;

            //هزینه ارسال
            decimal cost = sendResult.Data.Cost;

            string resultDescription = "Your message was sent successfully." +
                $"\n - Message Id: {messageId} " +
                $"\n - Cost: {cost}";

            Console.Out.WriteLine(resultDescription);
        }
        catch (Exception ex) // ارسال ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorDescription = "Unable to send SMS message." +
                $"\n - Error: {ex.Message}";

            Console.Out.WriteLine(errorDescription);
        }
    }
}