using IPE.SmsIrSamples.DotNetCore;
using System;

try
{
    //await SendSamples.SendVerifyAsync();
    //await SendSamples.SendBulkAsync();
    //await SendSamples.SendLikeToLikeAsync();
    await Console.Out.WriteLineAsync("Finished!");
}
catch (Exception ex)
{
    await Console.Out.WriteLineAsync(ex.Message);
}