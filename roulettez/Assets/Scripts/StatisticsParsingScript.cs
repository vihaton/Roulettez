using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsParsingScript : MonoBehaviour {
    private SaveDataContainer saveDataContainer = new SaveDataContainer();
    private FeelingSaveData[] feelings;
    private DateTime date;
    private Dictionary<int, float> table;
    private float currentDateValue;

    public GameObject newGraph;

    private WMG_Axis_Graph graph;
 
    private WMG_Series seriesX;
    // Use this for initialization
    void Start () {
        table = new Dictionary<int, float>();

        graph = newGraph.GetComponent<WMG_Axis_Graph>();
        saveDataContainer = SaveDataContainer.Load(Application.persistentDataPath + "/SaveDataContainer.xml");
        feelings = saveDataContainer.SaveDataArray;
        if (feelings.GetLength(0) == 0) return;
        date = feelings[0].date;
        currentDateValue = 0;
        for(int i = 0; i < feelings.GetLength(0); i++)
        {
            if(feelings[i].date.DayOfYear == date.DayOfYear)
            {
                currentDateValue += feelings[i].feeling.type;
                continue;
            }
            Debug.Log("tallennettava: " + feelings[i].date.DayOfYear);
            table.Add(date.DayOfYear, currentDateValue);
            date = feelings[i].date;
            currentDateValue = 0;
        }
        seriesX = graph.addSeries();
        


        UpdateGraph();

    }

    private void UpdateGraph()
    {
        List<Vector2> seriesData = new List<Vector2>();
        int first = 999999999;
        foreach (KeyValuePair<int, float> entry in table)
        {
            if (entry.Key < first) first = entry.Key;
            seriesData.Add(new Vector2(entry.Key, entry.Value));
            Debug.Log("data: " + new Vector2(entry.Key, entry.Value));
        }

        seriesX.pointValues.SetList(seriesData);
        graph.xAxis.AxisMinValue = first;




    }

    // Update is called once per frame
    void Update () {
		
	}
}
