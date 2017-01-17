using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsParsingScript : MonoBehaviour {
    private SaveDataContainer saveDataContainer = new SaveDataContainer();
    private FeelingSaveData[] feelings;
    private DateTime date;
    private Dictionary<DateTime, float> table;
    private float currentDateValue;
    // Use this for initialization
    void Start () {
        saveDataContainer = SaveDataContainer.Load(Application.persistentDataPath + "/SaveDataContainer.xml");
        feelings = saveDataContainer.SaveDataArray;
        if (feelings.GetLength(0) == 0) return;
        date = feelings[0].date;
        currentDateValue = 0;
        for(int i = 0; i < feelings.GetLength(0); i++)
        {
            if(feelings[i].date == date)
            {
                
            }
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
