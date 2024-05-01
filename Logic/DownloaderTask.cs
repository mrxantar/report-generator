using System.Net;
using Microsoft.AspNetCore.Routing.Constraints;

namespace report_generator.Logic;

public class FileDownloader
{
    public async Task DownloadFileAsync(string url, string zipPath)
    {
        using (var client = new WebClient())
        {
            client.DownloadFile(url, zipPath);
        }
    }
}