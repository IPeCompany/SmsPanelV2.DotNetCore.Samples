using IPE.SmsIrSamples.DotNetCore.Report.Receive;
using IPE.SmsIrSamples.DotNetCore.Report.Send;
using IPE.SmsIrSamples.DotNetCore.Send;
using System;

try
{
    ///////////////// sends

    //await SendVerifySamples.SendVerifyAsync();
    //await SendBulkSamples.SendBulkAsync();
    //await SendLikeToLikeSamples.SendLikeToLikeAsync();

    ///////////////// send reports

    ////// get send reports
    //await GetSendLiveReportSamples.GetSendLiveReportAsync();
    //await GetSendArchiveReportSamples.GetSendArchiveReportAsync();

    ////// get single message
    //await GetSingleMessageSamples.GetSingleMessageReportAsync();

    ////// get send packs
    //await GetSendPackSamples.GetSendPackLiveReportAsync();
    //await GetSendPacksSamples.GetSendPacksLiveReportAsync();

    ///////////////// receive reports

    //await GetLatestReceivesSamples.GetLatestReceivesAsync();
    //await GetReceiveLiveReportSamples.GetReceiveLiveReportAsync();
    //await GetReceiveArchiveReportSamples.GetReceiveArchiveReportAsync();

    await Console.Out.WriteLineAsync("Finished!");
}
catch (Exception ex)
{
    await Console.Out.WriteLineAsync(ex.Message);
}