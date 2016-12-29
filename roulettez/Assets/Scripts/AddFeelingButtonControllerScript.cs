using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AddFeelingButtonControllerScript : MonoBehaviour {
    public Text feelingText;
    private int feelingType;
    private FeelingInterface newFeeling = new TunneStruct();
    // Use this for initialization
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
        newFeeling.feeling = feelingText.text;
        newFeeling.type = feelingType;
        Debug.Log("FeelingType:" + feelingType);
        newFeeling.Save();
        
    }
}
