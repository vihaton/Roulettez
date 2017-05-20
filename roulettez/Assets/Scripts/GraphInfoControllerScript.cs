using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphInfoControllerScript : MonoBehaviour {
    private StatisticsParsingScript SPC;
    public Text text;
	
	void Start () {
        SPC = GameObject.FindObjectOfType<StatisticsParsingScript>();
        
    }
	
	
	public void UpdateInfo () {
        if (SPC.GetGraphTimeLine() == 0)
        {
            int year = SPC.GetCurrentWeek() / 52;

             text.text = (2016 + year).ToString() + " Week " + ((SPC.GetCurrentWeek() % 52)+1).ToString();
           
        }
        else if (SPC.GetGraphTimeLine() == 1)
        {
            int year = SPC.GetCurrentMonth() / 12;
             text.text = (2016+year).ToString() + "/"+((SPC.GetCurrentMonth()%12)+1).ToString();
            
        }

        else if (SPC.GetGraphTimeLine() == 2)
        {
            text.text = SPC.GetCurrentYear().ToString();
        }

    }
}
