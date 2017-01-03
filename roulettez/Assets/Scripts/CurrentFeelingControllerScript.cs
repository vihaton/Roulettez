﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentFeelingControllerScript : MonoBehaviour {

    private GameControllerScript GCS;

    // Use this for initialization
    void Start () {
        GCS = GameObject.FindObjectOfType<GameControllerScript>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        gameObject.GetComponentInChildren<Text>().text = "";
        if (gameObject.name == "CurrentFeelingButton1") GCS.currentFeelings[0] = null;
        else if (gameObject.name == "CurrentFeelingButton2") GCS.currentFeelings[1] = null;
        else if (gameObject.name == "CurrentFeelingButton3") GCS.currentFeelings[2] = null;
    }
}
