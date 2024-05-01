namespace report_generator.Logic;
using System.IO.Compression;

public class Extractor
{
    public async Task ExtractAsync(string formattedDate, string zipPath)
    {
        if(!Directory.Exists(Constants.extractPath))
        {
            Directory.CreateDirectory(Constants.extractPath);
        }

        DirectoryInfo folder = new DirectoryInfo(Constants.extractPath);
        foreach (FileInfo file in folder.GetFiles())
        {
            file.Delete();
        }

        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.Name.Contains($"AS_ADDR_OBJ_{formattedDate}") || entry.Name.Contains("version.txt"))
                {
                    entry.ExtractToFile($"{Constants.extractPath}/{entry.Name}", true);
                }
            }
        }
    }

    public async Task<string[]> GetPathsAsync()
    {
        string[] xmls = Directory.GetFiles(Constants.extractPath);
        xmls = xmls.Where(filePath => !Path.GetFileName(filePath).Equals("version.txt")).ToArray();
        return xmls;
    }
}