using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore;

public static class SendAsyncSamples
{
    /// <summary>
    /// نمونه ارسال وریفای
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
    /// نمونه ارسال گروهی
    /// https://app.sms.ir/developer/help/bulk
    /// </summary>
    public static async Task SendBulkAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // شماره خط ارسالی - موجود در قسمت شماره‌های من
            long lineNumber = 95007079000006;

            // متن ارسال گروهی
            string messageText = "سرویس پیامکی ایده پردازان با بیش از یک دهه سابقه همراه شماست" +
                "\nSMS.ir";

            // شماره موبایل‌های مقصد
            string[] mobiles = { "9120000000", "9120000001" };

            // پارامتر اختیاری زمان ارسال
            // زمان ارسال به صورت یونیکس تایم می‌باشد مثلا 1704094200
            int? sendDateTime = null;

            // انجام ارسال گروهی
            var sendResult = await smsIr.BulkSendAsync(lineNumber, messageText, mobiles, sendDateTime);

            // ارسال شما در اینجا با موفقیت انجام شده‌است.

            //شناسه مجموعه ارسال ارسال شده
            Guid packId = sendResult.Data.PackId;

            //لیست شناسه پیامک‌های ارسال شده
            int?[] messageIds = sendResult.Data.MessageIds;

            //هزینه ارسال
            decimal cost = sendResult.Data.Cost;

            string resultDescription = "Your message was sent successfully." +
                $"\n - Pack Id: {packId} " +
                $"\n - Message Ids: [{string.Join(", ", messageIds)}] " +
                $"\n - Cost: {cost}";

            await Console.Out.WriteLineAsync(resultDescription);
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

    /// <summary>
    /// نمونه ارسال نظیر به نظیر
    /// https://app.sms.ir/developer/help/likeToLike
    /// </summary>
    public static async Task SendLikeToLikeAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // شماره خط ارسالی - موجود در قسمت شماره‌های من
            long lineNumber = 95007079000006;

            // متن‌های ارسال نظیر به نظیر
            string[] messageTexts =
            {
                "سرویس پیامکی ایده پردازان با بیش از یک دهه سابقه همراه شماست" +
                "\nSMS.ir",
                "پلتفرم آموزش آنلاین، آکادمی ایده پردازان" +
                "\nipdemy.ir"
            };

            // شماره موبایل‌های مقصد
            string[] mobiles = { "9120000000", "9120000001" };

            // پارامتر اختیاری زمان ارسال
            // زمان ارسال به صورت یونیکس تایم می‌باشد مثلا 1704094200
            int? sendDateTime = null;

            // انجام ارسال نظیر به نظیر
            var sendResult = await smsIr.LikeToLikeSendAsync(lineNumber, messageTexts, mobiles, sendDateTime);

            // ارسال شما در اینجا با موفقیت انجام شده‌است.

            //شناسه مجموعه ارسال ارسال شده
            Guid packId = sendResult.Data.PackId;

            //لیست شناسه پیامک‌های ارسال شده
            int?[] messageIds = sendResult.Data.MessageIds;

            //هزینه ارسال
            decimal cost = sendResult.Data.Cost;

            string resultDescription = "Your message was sent successfully." +
                $"\n - Pack Id: {packId} " +
                $"\n - Message Ids: [{string.Join(", ", messageIds)}] " +
                $"\n - Cost: {cost}";

            await Console.Out.WriteLineAsync(resultDescription);
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