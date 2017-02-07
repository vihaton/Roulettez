using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Top3FeelingsScript : MonoBehaviour {
    private Text text;
    public StatisticsParsingScript SPS;
	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
        string[] bestList = new string[3];

        int bestValue = 0;
        int secondBestValue = 0;
        int thirdBestValue = 0;



        foreach(KeyValuePair <string, int> entry in SPS.feelingCountsTable)
        {
            if (entry.Value > bestValue)
            {
                bestList[0] = entry.Key;
                bestValue = entry.Value;
            }
            else if (entry.Value > secondBestValue)
            {
                bestList[1] = entry.Key;
                secondBestValue = entry.Value;

            }
            else if (entry.Value > thirdBestValue)
            {
                bestList[2] = entry.Key;
                thirdBestValue = entry.Value;

            }
        }

        text.text = "Your most frequent feelings are:\n\n1. " + bestList[0] + "\n 2. " + bestList[1] + "\n 3. " + bestList[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
