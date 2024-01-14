using IPE.SmsIrClient;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.SendSamples;

public static class BulkSendSample
{
    /// <summary>
    /// نمونه ارسال گروهی
    /// https://app.sms.ir/developer/help/bulk
    /// </summary>
    public static async Task SendSampleBulkAsync()
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

            //شناسه دسته پیامکی ارسال شده
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

            var errorDescription = "Unable to send SMS message." +
                $"\n - Error: {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }

    /// <summary>
    /// نمونه ارسال گروهی سینک
    /// https://app.sms.ir/developer/help/bulk
    /// </summary>
    public static void SendSampleBulk()
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
            var sendResult = smsIr.BulkSend(lineNumber, messageText, mobiles, sendDateTime);

            // ارسال شما در اینجا با موفقیت انجام شده‌است.

            //شناسه دسته پیامکی ارسال شده
            Guid packId = sendResult.Data.PackId;

            //لیست شناسه پیامک‌های ارسال شده
            int?[] messageIds = sendResult.Data.MessageIds;

            //هزینه ارسال
            decimal cost = sendResult.Data.Cost;

            string resultDescription = "Your message was sent successfully." +
                $"\n - Pack Id: {packId} " +
                $"\n - Message Ids: [{string.Join(", ", messageIds)}] " +
                $"\n - Cost: {cost}";

            Console.Out.WriteLine(resultDescription);
        }
        catch (Exception ex) // ارسال ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            var errorDescription = "Unable to send SMS message." +
                $"\n - Error: {ex.Message}";

            Console.Out.WriteLine(errorDescription);
        }
    }
}