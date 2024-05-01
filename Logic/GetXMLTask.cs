using System.ServiceModel;
using ServiceReference;

namespace report_generator.Logic;

public class XmlHandler
{
    
    public async Task<GetLastDownloadFileInfoResponse> GetXMLAsync()
    {
        DownloadServiceClient client = new DownloadServiceClient();
        client.Endpoint.Binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
        client.Endpoint.Address = new EndpointAddress("https://fias.nalog.ru/WebServices/Public/DownloadService.asmx");
        var response = await client.GetLastDownloadFileInfoAsync();
        return response;
    }

    public async Task<string> GetDownloadLinkAsync()
    {
        var response = await GetXMLAsync();
        return response.Body.GetLastDownloadFileInfoResult.GarXMLDeltaURL;
    }

    public async Task<string> GetUploadDateAsync()
    {
        var response = await GetXMLAsync();
        DateTime date = DateTime.Parse(response.Body.GetLastDownloadFileInfoResult.Date);
        return date.AddDays(-1).ToString("yyyyMMdd");
    }

    public async Task<string> GetUnformattedUploadDateAsync()
    {
        var response = await GetXMLAsync();
        return response.Body.GetLastDownloadFileInfoResult.Date;
    }
}