using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AddFeelingButtonControllerScript : MonoBehaviour {
    public Text feelingText;
    private int feelingType;
    private FeelingInterface newFeeling = new TunneStruct();

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
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        CCS = GameObject.FindObjectOfType<ContentCreationScript>();
        newFeeling.feeling = feelingText.text;
        newFeeling.type = feelingType;
        TCS.addToListOfFeelings(newFeeling);
        Debug.Log("FeelingType:" + feelingType);
        CCS.deleteContent();
        newFeeling.Save();
        CCS.createContent();
    }
}
