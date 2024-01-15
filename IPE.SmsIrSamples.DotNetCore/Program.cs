using IPE.SmsIrSamples.DotNetCore;
using System;

try
{
    // send samples
    //await SendAsyncSamples.SendVerifyAsync();
    //await SendAsyncSamples.SendBulkAsync();
    //await SendAsyncSamples.SendLikeToLikeAsync();

    // get send report samples
    //await GetSendReportAsyncSamples.GetSendLiveReportAsync();
    //await GetSendReportAsyncSamples.GetSendArchiveReportAsync();
    //await GetSendReportAsyncSamples.GetSingleMessageReportAsync();
    //await GetSendReportAsyncSamples.GetSendPacksLiveReportAsync();
    //await GetSendReportAsyncSamples.GetSendPackReportAsync();

    await Console.Out.WriteLineAsync("Finished!");
}
catch (Exception ex)
{
    await Console.Out.WriteLineAsync(ex.Message);
}