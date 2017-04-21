using UnityEngine;
using System.Collections.Generic;

 // this script changes the feelings on the roulette dynamically as it rotates
 // also handles updates feelings accordingly when they are clicked
public class FeelingButtonControllerScript : MonoBehaviour {

    private int numberOfFeelings;
    private TunneCreationScript TCS;
    public FeelingInterface feelingInterface;
    public List<FeelingInterface> feels;
    private GameControllerScript GCS;

    private Vector2 mousePrevious;
    private Vector2 mouseCurrent;
    private GameObject roulette;

    void Start()
    {
        feels = new List<FeelingInterface>();
        GCS = GameObject.FindObjectOfType<GameControllerScript>();
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        roulette = transform.parent.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        feels = TCS.getListOfFeelings();
        numberOfFeelings = feels.Count;
        if (transform.position.x < other.transform.position.x)
        {
           
            int childs = transform.parent.transform.childCount;
            for (int i = 0; i < childs; i++)
            {
                if(roulette.transform.GetChild(i) == transform)
                {
                    int index = i + 1;
                    int feelingIndex = feels.FindIndex(feeling => feeling == roulette.transform.GetChild(index%childs).transform.GetComponent<FeelingButtonControllerScript>().feelingInterface) - 1;
                    transform.GetComponentInChildren<TextMesh>().text = feels[feelingIndex == -1 ? numberOfFeelings - 1 : feelingIndex % numberOfFeelings].feeling;
                    transform.GetComponentInChildren<FeelingButtonControllerScript>().feelingInterface = feels[feelingIndex == -1 ? numberOfFeelings - 1 : feelingIndex % numberOfFeelings];
                }
            }

        }
        else if (transform.position.x > other.transform.position.x)
        {
            int childs = transform.parent.transform.childCount;
            for (int i = 0; i < childs; i++)
            {
                if (roulette.transform.GetChild(i) == transform)
                {
                    int index = i - 1;
                    int feelingIndex = feels.FindIndex(feeling => feeling == roulette.transform.GetChild(index == -1 ? childs - 1 : index % childs).transform.GetComponent<FeelingButtonControllerScript>().feelingInterface) + 1;
                    transform.GetComponentInChildren<TextMesh>().text = feels[feelingIndex % numberOfFeelings].feeling;
                    transform.GetComponentInChildren<FeelingButtonControllerScript>().feelingInterface = feels[feelingIndex % numberOfFeelings];
                }
            }

        }
    }

   

    void OnMouseDown()
    {
        // store mouse
       mousePrevious = Input.mousePosition;
    }

    void OnMouseUp()
    {
        mouseCurrent = Input.mousePosition;
        if (Mathf.Abs(mouseCurrent.x-mousePrevious.x)<10 && (mouseCurrent.y - mousePrevious.y)< 10) GCS.UpdateCurrentFeeling(feelingInterface);
    }
}
