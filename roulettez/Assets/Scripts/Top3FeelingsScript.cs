using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Top3FeelingsScript : MonoBehaviour {
    private Text text;
    public StatisticsParsingScript SPS;
	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
        string[] bestList = new string[3];

       
        int i = 0;


        foreach (KeyValuePair<string, int> entry in SPS.feelingCountsTable.OrderByDescending(key => key.Value))
        {
            Debug.Log("ASD:" + entry.Key + entry.Value);
            if (i == 0) bestList[0] = entry.Key;
            if (i == 1) bestList[1] = entry.Key;
            if (i == 2) bestList[2] = entry.Key;
            if (i == 3) break;
            i++;
        }
        text.text = "Your most frequent feelings are:\n\n1. " + bestList[0] + "\n 2. " + bestList[1] + "\n 3. " + bestList[2];
    }
    public void UpdateFeelings()
    {
        text = GetComponentInChildren<Text>();
        string[] bestList = new string[3];

        int i = 0;

        foreach (KeyValuePair<string, int> entry in SPS.feelingCountsTable.OrderByDescending(key => key.Value))
        {
            Debug.Log("ASD:" + entry.Key + entry.Value);
            if (i == 0) bestList[0] = entry.Key;
            if (i == 1) bestList[1] = entry.Key;
            if (i == 2) bestList[2] = entry.Key;
            if (i == 3) break;
            i++;
        }
        text.text = "Your most frequent feelings are:\n\n1. " + bestList[0] + "\n 2. " + bestList[1] + "\n 3. " + bestList[2];
    }
}
