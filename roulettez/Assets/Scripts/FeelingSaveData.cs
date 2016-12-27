using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;

public class FeelingSaveData {

    [XmlAttribute("date")]
     public DateTime date;

     public string feeling;


    public void SetDate(DateTime DT)
    {
        date = DT;
    }

    /*
        // Use this for initialization
        void Start () {

            date = DateTime.Now;

        }

        // Update is called once per frame
        void Save()
        {
            saveData.Save();
        }

        void Load()
        {

        }
    }

    internal class SerializedAttribute : Attribute
    {
    */
}

