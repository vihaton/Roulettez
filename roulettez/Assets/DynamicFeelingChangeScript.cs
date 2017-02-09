using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFeelingChangeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("AAAAAAAAA");
    }
}
