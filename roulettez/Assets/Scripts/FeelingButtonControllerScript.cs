using UnityEngine;
using System.Collections;

public class FeelingButtonControllerScript : MonoBehaviour {

    public FeelingInterface feelingInterface;
    private GameControllerScript GCS;

    void Start()
    {
        GCS = GameObject.FindObjectOfType<GameControllerScript>();
    }

    public void ChooseThisFeeling()
    {
        GCS.UpdateCurrentFeeling(feelingInterface);
    }

}
