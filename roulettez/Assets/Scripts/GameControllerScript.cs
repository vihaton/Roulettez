using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    public FeelingInterface currentFeeling;
    public Text feelingText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateCurrentFeeling(FeelingInterface FI)
    {
        currentFeeling = FI;
        feelingText.text = currentFeeling.feeling;
    }
}
