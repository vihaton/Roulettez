using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFeelingChangerControllerScript : MonoBehaviour {
    public GameObject roulette;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = new Vector3();
        pos.x = roulette.transform.position.x;
        pos.y = roulette.transform.position.y;
        pos.z = roulette.transform.position.z;
        transform.position = pos;
    }
}
