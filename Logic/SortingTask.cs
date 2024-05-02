using System.Xml.Serialization;

namespace report_generator.Logic;



[XmlRoot("ADDRESSOBJECTS")]
public class AddressObjects
{
    [XmlElement("OBJECT")]
    public List<OrderedEntry> Objects { get; set; }
}

public class OrderedEntry
{
    [XmlAttribute("ID")]
    public string Id { get; set; }

    [XmlAttribute("OBJECTID")]
    public string ObjectId { get; set; }

    [XmlAttribute("OBJECTGUID")]
    public string ObjectGuid { get; set; }

    [XmlAttribute("CHANGEID")]
    public string ChangeId { get; set; }

    [XmlAttribute("NAME")]
    public string Name { get; set; }

    [XmlAttribute("TYPENAME")]
    public string TypeName { get; set; }

    [XmlAttribute("LEVEL")]
    public string Level { get; set; }

    [XmlAttribute("OPERTYPEID")]
    public string OperTypeId { get; set; }

    [XmlAttribute("PREVID")]
    public string PrevId { get; set; }

    [XmlAttribute("NEXTID")]
    public string NextId { get; set; }

    [XmlAttribute("UPDATEDATE")]
    public string UpdateDate { get; set; }

    [XmlAttribute("STARTDATE")]
    public string StartDate { get; set; }

    [XmlAttribute("ENDDATE")]
    public string EndDate { get; set; }

    [XmlAttribute("ISACTUAL")]
    public string IsActual { get; set; }

    [XmlAttribute("ISACTIVE")]
    public string IsActive { get; set; }
}



public class SortEntries
{
    public async Task<Dictionary<string, List<OrderedEntry>>> SortAsync(string[] xmls)
    {
        Dictionary<string, List<OrderedEntry>> sortedEntries = new Dictionary<string, List<OrderedEntry>>();
        XmlSerializer serializer = new XmlSerializer(typeof(AddressObjects));
        LevelHandler levelHandler = new LevelHandler();
        var levelNames = await levelHandler.GetObjectLevelsAsync();

        foreach (string xml in xmls)
        {
            using (FileStream fileStream = new FileStream(xml, FileMode.Open))
            {
                AddressObjects addressObjects = (AddressObjects)serializer.Deserialize(fileStream);
                foreach (OrderedEntry entry in addressObjects.Objects)
                {
                    if (int.Parse(entry.OperTypeId) == 10)
                    {
                        var levelName = levelNames.Levels.Where(x => x.Level == entry.Level).FirstOrDefault();
                        if (!sortedEntries.ContainsKey(levelName.Name))
                        {
                            sortedEntries[levelName.Name] = new List<OrderedEntry>
                            {
                                entry
                            };
                        }
                        else
                        {
                            sortedEntries[levelName.Name].Add(entry);
                        }
                    }
                }
            }   
        }
        foreach (var key in sortedEntries.Keys)
        {
            sortedEntries[key] = sortedEntries[key].OrderBy(element => element.Name).ToList();
        }
        return sortedEntries;
    }
}