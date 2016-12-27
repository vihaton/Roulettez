using UnityEngine;
using System.Collections;
using System;

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
}

