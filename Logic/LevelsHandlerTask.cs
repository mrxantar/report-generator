using System.Xml.Serialization;
namespace report_generator.Logic;

[XmlRoot("OBJECTLEVELS")]
public class ObjectLevels
{
    [XmlElement("OBJECTLEVEL")]
    public List<ObjectLevel> Levels { get; set; }
}

public class ObjectLevel
{
    [XmlAttribute("LEVEL")]
    public string Level { get; set; }

    [XmlAttribute("NAME")]
    public string Name { get; set; }

    [XmlAttribute("UPDATEDATE")]
    public string UpdateDate { get; set; }

    [XmlAttribute("STARTDATE")]
    public string StartDate { get; set; }

    [XmlAttribute("ENDDATE")]
    public string EndDate { get; set; }

    [XmlAttribute("ISACTIVE")]
    public string IsActive { get; set; }
}

public class LevelHandler
{
    public async Task<ObjectLevels> GetObjectLevelsAsync()
    {
        Dictionary<string, string> levelNames = new Dictionary<string, string>();
        XmlSerializer serializer = new XmlSerializer(typeof(ObjectLevels));
        string path = Directory.GetFiles(Constants.extractPath, "AS_OBJECT_LEVELS_*").Last();
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
            ObjectLevels objectLevels = (ObjectLevels)serializer.Deserialize(fileStream);
            return objectLevels;
        }
    }
}