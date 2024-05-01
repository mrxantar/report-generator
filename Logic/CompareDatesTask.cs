namespace report_generator.Logic;

public class Comparator
{
    public async Task<bool> CompareDates(string date)
    {
        try
        {
            using (StreamReader sr = new StreamReader(Constants.extractPath + "/version.txt"))
            {
                DateTime unformattedDate = DateTime.Parse(sr.ReadLine());
                var prevDate = unformattedDate.ToString("dd.MM.yyyy");
                return date == prevDate;
            }
        }
        catch
        {
            return false;
        }
    }
}