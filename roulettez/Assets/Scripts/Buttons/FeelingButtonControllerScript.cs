using UnityEngine;
using System.Collections.Generic;

public class FeelingButtonControllerScript : MonoBehaviour {

    private int numberOfFeelings;
    private TunneCreationScript TCS;
    public FeelingInterface feelingInterface;
    public List<FeelingInterface> feels;
    private GameControllerScript GCS;
    private float sensitivity;
    private Vector3 mouseReference;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    private bool isRotating;
    private GameObject roulette;

    void Start()
    {
        feels = new List<FeelingInterface>();
        sensitivity = 0.1f;
        rotation = Vector3.zero;
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

    void Update()
    {
        if (isRotating)
        {
            sensitivity = 0.02f;
            // offset
            mouseOffset = (Input.mousePosition - mouseReference);

            // apply rotation
            rotation.y = -(mouseOffset.x + mouseOffset.y) * sensitivity;

            // rotate
            transform.parent.Rotate(rotation);

            // store mouse
            mouseReference = Input.mousePosition;
        }
    }

    void OnMouseDown()
    {
        // rotating flag
        isRotating = true;

        // store mouse
        mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        isRotating = false;
    }

    void OnMouseUpAsButton()
    {
        GCS.UpdateCurrentFeeling(feelingInterface);
    }
}
