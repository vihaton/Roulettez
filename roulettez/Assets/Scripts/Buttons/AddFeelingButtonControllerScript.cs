using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class AddFeelingButtonControllerScript : MonoBehaviour {
    public Text feelingText;
    public InputField inputField;
    private int feelingType;
    private FeelingInterface newFeeling = new TunneStruct();
    public TunneStructContainer TSC;
    private TunneCreationScript TCS;
    
    void Start () {
        feelingType = 1;
	}
	
    public void ToggleFeelingType(int type)
    {
        feelingType = type;
    }

    public void AddFeeling()
    {
        if (feelingText.text == "") return;
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        newFeeling.feeling = feelingText.text;
        newFeeling.type = (FeelingType)feelingType;
        newFeeling.id = TCS.getListOfFeelings().Capacity * 1000;
        TCS.addToListOfFeelings(newFeeling);
        // Debug.Log("FeelingType:" + feelingType);
        SaveFeeling(Application.persistentDataPath + "/feelings.xml", newFeeling);
        inputField.text = "";
    }

    public void SaveFeeling(string path, FeelingInterface feeling)
    {
        // try to append new feeling to existing xml on device
        // if there is none create one
        try
        {
            TSC = TunneStructContainer.Load(path);
            TunneStruct[] temp = TSC.TunneStructArray;
            TSC.TunneStructArray = new TunneStruct[temp.Length + 1];
            for (int i = 0; i < temp.Length; i++)
            {
                TSC.TunneStructArray[i] = temp[i];
            }
            TSC.TunneStructArray[temp.Length] = (TunneStruct)feeling;
            TSC.Save(path);
        }
        catch (Exception e)
        {
            TunneStructContainer TSC = new TunneStructContainer();
            List<FeelingInterface> feelingList = new List<FeelingInterface>();
            feelingList.Add(feeling);
            TSC.Save(path,feelingList);
            // Debug.Log(e.ToString());
        }

    }
}
