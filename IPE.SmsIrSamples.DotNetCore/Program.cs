using IPE.SmsIrSamples.DotNetCore;
using System;

try
{
    //await SendAsyncSamples.SendVerifyAsync();
    //await SendAsyncSamples.SendBulkAsync();
    //await SendAsyncSamples.SendLikeToLikeAsync();
    await Console.Out.WriteLineAsync("Finished!");
}
catch (Exception ex)
{
    await Console.Out.WriteLineAsync(ex.Message);
}