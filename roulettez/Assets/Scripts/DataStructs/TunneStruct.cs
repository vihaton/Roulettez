﻿using UnityEngine;
using System.IO;
using System.Collections;
using System;
using System.Resources;
using System.Collections.Generic;

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
        try
        {
            string path = Application.dataPath + "/Text/feelings.csv";
            using (System.IO.StreamWriter file =
               new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(this.feeling + ";" + this.type);
            }
        }
        catch (Exception e)
        {
            string error = e.ToString();
            Console.WriteLine(e);
        }
        
    }

    
}

