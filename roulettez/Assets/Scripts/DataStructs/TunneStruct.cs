using UnityEngine;
using System.IO;
using System.Collections;
using System;
using System.Resources;

public class TunneStruct : FeelingInterface {


    public string feeling { get; set; }
    public int id { get; set; }
    public int type { get; set; }

    public TunneStruct()
    {

    }
    public string GetFeeling()
    {
        return feeling;
    }

    public int GetID()
    {
        return id;
    }

    int FeelingInterface.GetType()
    {
        return type;
    }

    public void Save()
    {
         using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(@Application.dataPath + "/Text/feelings.csv", true))
        {
            file.WriteLine(this.feeling + ";" + this.type);
        }
    }
}

