using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("SaveDateCollection")]
public class SaveDataContainer
{
    [XmlArray("SaveDataArray"), XmlArrayItem("SaveData")]
    public FeelingSaveData[] SaveDataArray;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(SaveDataContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static SaveDataContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(SaveDataContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as SaveDataContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static SaveDataContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(SaveDataContainer));
        return serializer.Deserialize(new StringReader(text)) as SaveDataContainer;
    }
}