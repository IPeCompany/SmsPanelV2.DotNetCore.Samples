using IPE.SmsIrSamples.DotNetCore;
using System;

try
{
    // send samples
    //await SendAsyncSamples.SendVerifyAsync();
    //await SendAsyncSamples.SendBulkAsync();
    //await SendAsyncSamples.SendLikeToLikeAsync();

    // report samples
    //await ReportAsyncSamples.GetSendLiveReportAsync();
    await Console.Out.WriteLineAsync("Finished!");
}
catch (Exception ex)
{
    await Console.Out.WriteLineAsync(ex.Message);
}