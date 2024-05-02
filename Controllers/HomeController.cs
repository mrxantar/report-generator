using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using report_generator.Models;
using report_generator.Logic;
using ServiceReference;

namespace report_generator.Controllers;

public class ReportModelView
{
    public Dictionary<string, List<OrderedEntry>> sortedEntries { get; set; }
    public string uploadDate { get; set; }
}

public class HomeController : Controller
{

    XmlHandler xmlHandler = new XmlHandler();
    Extractor extractor = new Extractor();
    Comparator comparator = new Comparator();
    FileDownloader fileDownloader = new FileDownloader();
    SortEntries sortEntries = new SortEntries();

    private readonly ILogger<HomeController> _logger;

    public async Task<IActionResult> Index()
    {
        
        var downloadLink = await xmlHandler.GetDownloadLinkAsync();
        var uploadDate = await xmlHandler.GetUploadDateAsync();
        var date = await xmlHandler.GetUnformattedUploadDateAsync();
        
        if (! await comparator.CompareDates(date))
        {
            
            await fileDownloader.DownloadFileAsync(downloadLink, Constants.zipPath);
            await extractor.ExtractAsync(uploadDate, Constants.zipPath);
        }

        var xmls = await extractor.GetPathsAsync();

        var entries = await sortEntries.SortAsync(xmls);


        var viewModel = new ReportModelView
        {
            sortedEntries = entries,
            uploadDate = date,
        };
        return View(viewModel);
    }

    public async Task<IActionResult> DownloadNewDump()
    {
        var downloadLink = await xmlHandler.GetDownloadLinkAsync();
        var uploadDate = await xmlHandler.GetUploadDateAsync();
        var date = await xmlHandler.GetUnformattedUploadDateAsync();

        await fileDownloader.DownloadFileAsync(downloadLink, Constants.zipPath);

        await extractor.ExtractAsync(uploadDate, Constants.zipPath);
        var xmls = await extractor.GetPathsAsync();

        var entries = await sortEntries.SortAsync(xmls);


        var viewModel = new ReportModelView
        {
            sortedEntries = entries,
            uploadDate = date,
        };
        return View("./Views/Home/Index.cshtml" ,viewModel);
    }

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
