using UnityEngine;
using UnityEngine.UI;
using System;


public class AddFeelingButtonControllerScript : MonoBehaviour {
    public Text feelingText;
    public InputField inputField;
    private int feelingType;
    private FeelingInterface newFeeling = new TunneStruct();
    public TunneStructContainer TSC;
    private TunneCreationScript TCS;
   private ContentCreationScript CCS;
    
    void Start () {
        feelingType = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleFeelingType(int type)
    {
        feelingType = type;
    }

    public void AddFeeling()
    {
        if (feelingText.text == "") return;
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        CCS = GameObject.FindObjectOfType<ContentCreationScript>();
        newFeeling.feeling = feelingText.text;
        newFeeling.type = feelingType;
        newFeeling.id = TCS.getListOfFeelings().Capacity * 1000;
        TCS.addToListOfFeelings(newFeeling);
        Debug.Log("FeelingType:" + feelingType);
        CCS.deleteContent();
        SaveFeeling(Application.persistentDataPath + "/feelings.xml", newFeeling);
        CCS.createContent();
        inputField.text = "";
        
    }

    public void SaveFeeling(string path, FeelingInterface feeling)
    {
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
            Debug.Log(e.ToString());
        }

    }
}
