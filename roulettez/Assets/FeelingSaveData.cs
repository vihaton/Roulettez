using UnityEngine;
using System.Collections;
using System;

public class FeelingSaveData : MonoBehaviour {

    public SaveData saveData;

    [Serialized] public DateTime date;

    [Serialized] public string feeling;


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
}