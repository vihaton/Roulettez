using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

    public class StatisticsParsingScript : MonoBehaviour {
    private SaveDataContainer saveDataContainer = new SaveDataContainer();
    private FeelingSaveData[] feelings;
    private DateTime date;
    private Dictionary<int, float> table;
    private int currentWeek;
    private int currentMonth;
    private int currentYear;
    private Top3FeelingsScript T3FS;
    private GraphInfoControllerScript GICS;
    private int currentGraphTimeLine = 0; // 0 = week, 1 = month, 2 = year
    public GameObject newGraph;
    public Dictionary<string,int> feelingCountsTable;
    
    private bool useRandomData = false;
    public TextAsset randomData;

    private WMG_Axis_Graph graph;
 
    private WMG_Series seriesX;
    // Use this for initialization
    void Start ()
    {
        T3FS = GameObject.FindObjectOfType<Top3FeelingsScript>();
        GICS = GameObject.FindObjectOfType<GraphInfoControllerScript>();
        table = new Dictionary<int, float>();
        feelingCountsTable = new Dictionary<string, int>();
        graph = newGraph.GetComponent<WMG_Axis_Graph>();
        try
        {
            if (useRandomData) saveDataContainer = SaveDataContainer.LoadFromText(randomData.text);

            else saveDataContainer = SaveDataContainer.Load(Application.persistentDataPath + "/SaveDataContainer.xml");

            feelings = saveDataContainer.SaveDataArray;
           
        }
        
        catch (Exception e)
        {
            Debug.Log("SaveData load failed");
        }
        currentWeek = GetWeekValue(feelings[feelings.GetLength(0) - 1].date);
        currentMonth = GetMonthValue(feelings[feelings.GetLength(0) - 1].date);
        currentYear = feelings[feelings.GetLength(0) - 1].date.Year;
        //if (feelings.GetLength(0) == 0) return;
        //date = feelings[0].date;
        //currentDateValue = 0;
        //for (int i = 0; i < feelings.GetLength(0); i++)
        //{
        //    if (i == feelings.GetLength(0) - 1)
        //    {
        //        currentDateValue += (int)feelings[i].feeling.type;
        //        table.Add(GetDateValue(feelings[i].date), currentDateValue);
        //        break;

        //    }
        //    if (GetDateValue(feelings[i].date) == date.DayOfYear)
        //    {
        //        currentDateValue += (int)feelings[i].feeling.type;
        //        continue;
        //    }
        //    if(currentDateValue==0) currentDateValue = (int)feelings[i].feeling.type;
        //    Debug.Log("Table length: " + table.Count);
        //    table.Add(GetDateValue(date), currentDateValue);
        //    Debug.Log("tallennettava: " + feelings[i].date.DayOfYear);
        //    date = feelings[i].date;
        //    currentDateValue = 0;
        //}
        //CreateGraph();
        CreateGraphWeek();
        }

    //}
    public void CreateGraphWeek()
    {
        ResetGraph();
       
        int week = currentWeek;
        graph.xAxis.AxisNumTicks = 7;
        graph.xAxis.AxisLabelSize = 7;
        float currentDateValue = 0;
        if (feelings.GetLength(0) == 0) return;
        date = feelings[0].date;
            
        int firstDate = 0;
        int lastDate = 0;
        for (int i = 0; i < feelings.GetLength(0); i++)
        {
            if(week == GetWeekValue(feelings[i].date))
            {
                firstDate = i;

                date = feelings[i].date;
                break;
            }
        }
        int index = 1;
            for (int i = firstDate; i < feelings.GetLength(0); i++)
            {

            if (GetWeekValue(feelings[i].date) != currentWeek)
            {
                table.Add(index, currentDateValue);
                lastDate = i-1;
                break;
            }
                if (i == feelings.GetLength(0)-1)
                {
                if (feelings[i].date == date)
                {
                    currentDateValue += (int)feelings[i].feeling.type;
                    table.Add(index, currentDateValue);
                    Debug.Log("Table length: " + table.Count);
                    lastDate = i;
                    break;
                }
                else
                {
                    table.Add(index, currentDateValue);
                    table.Add(index+1, (int)feelings[i].feeling.type);
                    lastDate = i;
                    break;
                }
                
                }
                if (feelings[i].date == date)
                {
                    currentDateValue += (int)feelings[i].feeling.type;
                    continue;
                }
                
                Debug.Log("Table length: " + table.Count);
            Debug.Log("asd: " + date.DayOfWeek.ToString());
            table.Add(index, currentDateValue);
            index++;
                
                Debug.Log("tallennettava: " + feelings[i].date.DayOfYear);
                date = feelings[i].date;
                currentDateValue = (int)feelings[i].feeling.type;
            }
        graph.groups.Clear();
        graph.groups.Add("Mon");
        graph.groups.Add("Tue");
        graph.groups.Add("Wed");
        graph.groups.Add("Thu");
        graph.groups.Add("Fri");
        graph.groups.Add("Sat");
        graph.groups.Add("Sun");
      
        seriesX = graph.addSeries();
            CreateTop3Table(firstDate, lastDate);
            UpdateGraph();
            CreateGraphStyle();
        currentGraphTimeLine = 0;
    }

    public void CreateGraphMonth()
    {
        ResetGraph();

        int month = currentMonth;
        graph.xAxis.AxisNumTicks = 5;
        graph.xAxis.AxisLabelSize = 5;
        float currentWeekValue = 0;
        if (feelings.GetLength(0) == 0) return;
        date = feelings[0].date;

        int firstDate = 0;
        int lastDate = 0;
        for (int i = 0; i < feelings.GetLength(0); i++)
        {
            if (month == GetMonthValue(feelings[i].date))
            {
                firstDate = i;

                date = feelings[i].date;
                break;
            }
        }
        int index = 1;
        for (int i = firstDate; i < feelings.GetLength(0); i++)
        {

            if (GetMonthValue(feelings[i].date) != month)
            {
                table.Add(index, currentWeekValue);
                lastDate = i - 1;
                break;
            }
            if (i == feelings.GetLength(0) - 1)
            {
                if (GetWeekValue(feelings[i].date) == GetWeekValue(date))
                {
                    currentWeekValue += (int)feelings[i].feeling.type;
                    table.Add(index, currentWeekValue);
                    Debug.Log("Table length: " + table.Count);
                    lastDate = i;
                    break;
                }
                else
                {
                    table.Add(index, currentWeekValue);
                    table.Add(index+1, (int)feelings[i].feeling.type);
                    lastDate = i;
                    break;
                }

            }
            if (GetWeekValue(feelings[i].date) == GetWeekValue(date))
            {
                currentWeekValue += (int)feelings[i].feeling.type;
                continue;
            }

            Debug.Log("Table length: " + table.Count);
            Debug.Log("asd: " + date.DayOfWeek.ToString());
            table.Add(index, currentWeekValue);
            index++;

            Debug.Log("tallennettava: " + feelings[i].date.DayOfYear);
            date = feelings[i].date;
            currentWeekValue = (int)feelings[i].feeling.type;
        }
        graph.groups.Clear();
        graph.groups.Add("1.");
        graph.groups.Add("2.");
        graph.groups.Add("3.");
        graph.groups.Add("4.");
        graph.groups.Add("5.");

        seriesX = graph.addSeries();
        CreateTop3Table(firstDate, lastDate);
        UpdateGraph();
        CreateGraphStyle();
        currentGraphTimeLine = 1;
    }

    public void CreateGraphYear()
    {
        ResetGraph();
       
        int year = currentYear;
        graph.xAxis.AxisNumTicks = 12;
        graph.xAxis.AxisLabelSize = 12;
        float currentMonthValue = 0;
        if (feelings.GetLength(0) == 0) return;
        date = feelings[0].date;
       
        int firstDate = 0;
        int lastDate = 0;
        for (int i = 0; i < feelings.GetLength(0); i++)
        {

            if (year == feelings[i].date.Year)
            {
                firstDate = i;
                date = feelings[i].date;
                break;
            }
        }
        
            for (int i = firstDate; i < feelings.GetLength(0); i++)
            {
                if(year!=feelings[i].date.Year)
                {
                    table.Add(date.Month, currentMonthValue);
                    lastDate = i-1;
                    break;
                }
                if (i == feelings.GetLength(0) - 1)
                {
                if (feelings[i].date.Month == date.Month)
                {
                    currentMonthValue += (int)feelings[i].feeling.type;
                    table.Add(feelings[i].date.Month, currentMonthValue);
                    lastDate = i;
                    break;
                }
                else
                {
                    table.Add(date.Month, currentMonthValue);
                    table.Add(feelings[i].date.Month, (int)feelings[i].feeling.type);
                    lastDate = i;
                    break;
                }
                }
                if (feelings[i].date.Month == date.Month)
                {
                    currentMonthValue += (int)feelings[i].feeling.type;
                    continue;
                }
                if (currentMonthValue == 0) currentMonthValue = (int)feelings[i].feeling.type;
                Debug.Log("Table length: " + table.Count);
                table.Add(date.Month, currentMonthValue);
                
                
                Debug.Log("tallennettava: " + feelings[i].date.DayOfYear);
                date = feelings[i].date;
                currentMonthValue = 0;
            }
        graph.groups.Clear();
        graph.groups.Add("Jan");
        graph.groups.Add("Feb");
        graph.groups.Add("Mar");
        graph.groups.Add("Apr");
        graph.groups.Add("May");
        graph.groups.Add("June");
        graph.groups.Add("July");
        graph.groups.Add("Aug");
        graph.groups.Add("Sept");
        graph.groups.Add("Oct");
        graph.groups.Add("Nov");
        graph.groups.Add("Dec");
   

        seriesX = graph.addSeries();
        CreateTop3Table(firstDate, lastDate);
        UpdateGraph();
        CreateGraphStyle();
        currentGraphTimeLine = 2;

    }


    private int GetDayOfWeek(DateTime date)
    {
        if (date.DayOfWeek == DayOfWeek.Sunday) return 7;
        else return (int)date.DayOfWeek;
    }

    private int GetDateValue(DateTime date)
    {
        int yearOffset = date.Year - 2016;
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

    private int GetWeekValue(DateTime date)
    {
        int yearOffset = date.Year - 2016;

        yearOffset = yearOffset + yearOffset * 52;
       
        
        
        return yearOffset + GetWeekOfYear(date);
    }

    private int GetMonthValue(DateTime date)
    {
        int yearOffset = date.Year - 2016;
       
            yearOffset = yearOffset + yearOffset * 12;
        
        
        return yearOffset + date.Month;
    }

    private void CreateTop3Table(int first, int last)
    {

        for (int i = first; i <= last; i++)
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
        T3FS.UpdateFeelings();
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
            seriesData.Add(new Vector2(entry.Key, FeelingValueFunction(entry.Value)));
            Debug.Log("data: " + new Vector2(entry.Key, entry.Value));
        }

        seriesX.pointValues.SetList(seriesData);
        graph.xAxis.AxisMinValue = first;
        GICS.UpdateInfo();
    }

    private float FeelingValueFunction(float value)
    {
        if (value>=0) return 1 + Mathf.Log(value+1,20);
        else return 1 - Mathf.Log(-(value-1), 20);
    }

    private static int GetWeekOfYear(DateTime time)
    {
        // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
        // be the same week# as whatever Thursday, Friday or Saturday are,
        // and we always get those right
        DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        {
            time = time.AddDays(3);
        }

        // Return the week of our adjusted day
        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    public void Previous()
    {
        if (currentGraphTimeLine == 0)
        {
            currentWeek--;
            CreateGraphWeek();
        }
        if (currentGraphTimeLine == 1)
        {
            currentMonth--;
            CreateGraphMonth();
        }
       if (currentGraphTimeLine == 2)
            {
                currentYear--;
                CreateGraphYear();
            }
    }
    public void Next()
    {
        if (currentGraphTimeLine == 0)
        {
            currentWeek++;
            CreateGraphWeek();
        }
        if (currentGraphTimeLine == 1)
        {
            currentMonth++;
            CreateGraphMonth();
        }
        if (currentGraphTimeLine == 2)
            {
                currentYear++;
                CreateGraphYear();
            }
    }
    public void LatestWeek()
    {
        currentWeek = GetWeekValue(feelings[feelings.GetLength(0) - 1].date);
        CreateGraphWeek();
        GICS.UpdateInfo();
    }
    public void LatestMonth()
    {
       
        currentMonth = GetMonthValue(feelings[feelings.GetLength(0) - 1].date);
      
        CreateGraphMonth();
        GICS.UpdateInfo();
    }
    public void LatestYear()
    {
        
        currentYear = feelings[feelings.GetLength(0) - 1].date.Year;
        CreateGraphYear();
        GICS.UpdateInfo();
    }
    private void ResetGraph()
    {
        graph.deleteSeries();
        table.Clear();
        feelingCountsTable.Clear();
    }

    public int GetCurrentWeek()
    {
        return currentWeek;
    }
    public int GetCurrentMonth()
    {
        return currentMonth;

    }
    public int GetCurrentYear()
    {
        return currentYear;
    }

    public int GetGraphTimeLine()
    {
        return currentGraphTimeLine;
    }

    public void UseRandom(bool b)
    {
        useRandomData = b;
    }
}
