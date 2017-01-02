using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;


[XmlRoot("TunneStructCollection")]
public class TunneStructContainer
{
    [XmlArray("TunneStructArray"), XmlArrayItem("TunneStruct")]
    public TunneStruct[] TunneStructArray;

    public void Save(string path)
    {
        Debug.Log("SavePath" + path);
        var serializer = new XmlSerializer(typeof(TunneStructContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }


    public void Save(string path, List<FeelingInterface> listOfFeelings)
    {
        TunneStructArray = new TunneStruct[listOfFeelings.Count];
       for (int i = 0; i < listOfFeelings.Count; i++)
        {
            TunneStructArray[i] = (TunneStruct)listOfFeelings[i];
        }
        Save(path);
    }


    public static TunneStructContainer Load(string path)
    {
        Debug.Log("LoadPath" + path);
        var serializer = new XmlSerializer(typeof(TunneStructContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as TunneStructContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static TunneStructContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(TunneStructContainer));
        return serializer.Deserialize(new StringReader(text)) as TunneStructContainer;
    }
}