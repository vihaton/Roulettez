using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ContentCreationScript : MonoBehaviour {
    
    public List<FeelingInterface> feels;
    public GameObject feelingButtonPrefab;
    public GameObject roulette;
    private WMG_Pie_Graph piegraph;
    private List<float> valueList;
    private List<string> labelList;
    private TunneCreationScript TCS;
    private GameControllerScript GCS;
    
	void Start () {
        GCS = FindObjectOfType<GameControllerScript>();
        piegraph = roulette.GetComponent<WMG_Pie_Graph>();
        feels = new List<FeelingInterface>();
        valueList = new List<float>();
        labelList = new List<string>();
        createContent();
	}

   public void createContent()
    {
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        feels = TCS.getListOfFeelings();
        int numberOfFeelings = feels.Count;
        float ang = 360f / (feels.Count);
        for (int i = 0; i < numberOfFeelings; i++)
        {
            valueList.Add(1);
            labelList.Add(feels[i].feeling);
        }
        
        piegraph.sliceValues.SetList(valueList);
        piegraph.sliceLabels.SetList(labelList);
        piegraph.WMG_Pie_Slice_Click += clickEvent;
        
    }
    void clickEvent(WMG_Pie_Graph graph, WMG_Pie_Graph_Slice slice)
    {
        FeelingInterface FI = feels[0];
        foreach(FeelingInterface feel in feels)
        {
            if (feel.feeling.Equals(slice.name)) FI = feel;
        }
        GCS.UpdateCurrentFeeling(FI);


    }

    public void deleteContent()
    {
        int childs = roulette.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(roulette.transform.GetChild(i).gameObject);
        }
    }

    Vector3 GetButtonPosition(Vector3 center, float radius, float ang)
    {
        Vector3 pos;
        float a = ang + 90;
        pos.x = center.x - radius * Mathf.Sin(a * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z - radius * Mathf.Cos(a * Mathf.Deg2Rad);
        return pos;
    }
}
