using UnityEngine;
using System.Collections;

public class SearchedFeelingButtonControllerScript : MonoBehaviour
{

    public FeelingInterface feelingInterface;
    private GameControllerScript GCS;
    private SearchFeelingInputFieldControllerSciprt SFIFCS;

    void Start()
    {
        GCS = GameObject.FindObjectOfType<GameControllerScript>();
        SFIFCS = GameObject.FindObjectOfType<SearchFeelingInputFieldControllerSciprt>();
    }

    public void ChooseThisFeeling()
    {
        GCS.UpdateLastUsed(feelingInterface);
        SFIFCS.DeleteContent();
    }

}
