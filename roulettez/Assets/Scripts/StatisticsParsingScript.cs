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
    public Dictionary<string,int> feelingCountsTable;

    private WMG_Axis_Graph graph;
 
    private WMG_Series seriesX;
    // Use this for initialization
    void Start ()
    {
        table = new Dictionary<int, float>();
        feelingCountsTable = new Dictionary<string, int>();
        

        graph = newGraph.GetComponent<WMG_Axis_Graph>();
        saveDataContainer = SaveDataContainer.Load(Application.persistentDataPath + "/SaveDataContainer.xml");
        feelings = saveDataContainer.SaveDataArray;
        if (feelings.GetLength(0) == 0) return;
        date = feelings[0].date;
        currentDateValue = 0;
        for (int i = 0; i < feelings.GetLength(0); i++)
        {
            if (i == feelings.GetLength(0) - 1)
            {
                currentDateValue += feelings[i].feeling.type;
                table.Add(GetDateValue(feelings[i].date), currentDateValue);
                break;

            }
            if (GetDateValue(feelings[i].date) == date.DayOfYear)
            {
                currentDateValue += feelings[i].feeling.type;
                continue;
            }
            Debug.Log("Table length: " + table.Count);
            table.Add(GetDateValue(date), currentDateValue);
            Debug.Log("tallennettava: " + feelings[i].date.DayOfYear);
            date = feelings[i].date;
            currentDateValue = 0;
        }

        seriesX = graph.addSeries();


        CreateTop3Table();
        UpdateGraph();
        CreateGraphStyle();

    }

    private int GetDateValue(DateTime date)
    {
        int yearOffset = date.Year - 2017;
        if (DateTime.IsLeapYear(date.Year))
        {
            yearOffset = yearOffset + yearOffset * 366;
        }
        else
        {
            yearOffset = yearOffset + yearOffset * 365;
        }
        return yearOffset + date.DayOfYear;
    }

    private void CreateTop3Table()
    {

        for (int i = 0; i < feelings.GetLength(0); i++)
        {
           String currentFeeling = feelings[i].feeling.feeling;
            if (feelingCountsTable.ContainsKey(currentFeeling))
            {
                int newFeelingCount = feelingCountsTable[currentFeeling] + 1;
                feelingCountsTable.Remove(currentFeeling);
                Debug.Log("Feeling Count Updated on: " + currentFeeling + " With value of: " + newFeelingCount);
                feelingCountsTable.Add(currentFeeling, newFeelingCount);
                continue;
            }

            Debug.Log("New Feeling Added: " + currentFeeling);
            feelingCountsTable.Add(currentFeeling, 1);
        }
    }

    private void CreateGraphStyle()
    {
        seriesX.lineColor = Color.red;
        seriesX.pointColor = Color.black;
        seriesX.seriesName = "Tunnekäyrä";
        StartCoroutine(WaitSeconds());
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(0.01f);
        graph.barWidth = 8.5f;
        Debug.Log("Graph Layout Updated");
        
    }

    private void UpdateGraph()
    {
        List<Vector2> seriesData = new List<Vector2>();
        int first = 999999999;
        foreach (KeyValuePair<int, float> entry in table)
        {
            Debug.Log("Entry: " + entry.Key);
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
