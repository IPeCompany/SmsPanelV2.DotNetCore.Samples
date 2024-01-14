using System;

try
{
    await Console.Out.WriteLineAsync("Finished!");
}
catch (Exception ex)
{
    await Console.Out.WriteLineAsync(ex.Message);
}