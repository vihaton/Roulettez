using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteControllerScript : MonoBehaviour
{

    private int numberOfFeelings;
    private ContentCreationScript CCS;
    public List<FeelingInterface> feels;
    private float sensitivity;
    private Vector3 mouseReference;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    private bool isRotating;
    //0 = no change, 1 = change prev, 2 = change next
    private int[] changeFeeling;
    private bool[] cantChangeFeeling;


    private Vector3 rouletteViewPortPosition;
    public Camera camera;


    void Start()
    {
        CCS = GameObject.FindObjectOfType<ContentCreationScript>();
        sensitivity = 0.4f;
        rotation = Vector3.zero;
        rouletteViewPortPosition = new Vector3(1f, 0f, 10);
        transform.position = camera.ViewportToWorldPoint(rouletteViewPortPosition);
        transform.localScale = new Vector3(10, 1, 10);

    }

    void Update()
    {
        transform.position = camera.ViewportToWorldPoint(rouletteViewPortPosition);


        //Debug.Log(transform.GetChild(0).transform.rotation.eulerAngles.z);






        if (isRotating)
        {
            
            //Debug.Log(transform.GetChild(0).transform.rotation.eulerAngles);
            mouseOffset = (Input.mousePosition - mouseReference);

            int childs = transform.childCount;
            if (childs > 3) {
                for (int i = childs - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).transform.rotation.eulerAngles.z > 116 && transform.GetChild(i).transform.rotation.eulerAngles.z < 126)
                    {
                        if (changeFeeling[i] == 1 && cantChangeFeeling[i]==false)
                        {
                            changeFeeling[i] = 0;
                            
                        }
                        else if (changeFeeling[i] == 0)
                        {
                            changeFeeling[i] = 1;
                            cantChangeFeeling[i] = true;
                        }
                        else if(cantChangeFeeling[i] == false) {
                            int index = i - 1;
                            int feelingIndex = feels.FindIndex(feeling => feeling == transform.GetChild(index == -1 ? childs - 1 : index % childs).GetComponent<FeelingButtonControllerScript>().feelingInterface) - 1;
                            transform.GetChild(i).GetComponentInChildren<TextMesh>().text = feels[feelingIndex == -1 ? numberOfFeelings - 1 : feelingIndex % numberOfFeelings].feeling;
                            transform.GetChild(i).GetComponentInChildren<FeelingButtonControllerScript>().feelingInterface = feels[feelingIndex == -1 ? numberOfFeelings - 1 : feelingIndex % numberOfFeelings];
                            changeFeeling[i] = 0;
                            Debug.Log(changeFeeling[i]);

                        }
                        Debug.Log(changeFeeling[i]);
                    }
                    if (transform.GetChild(i).transform.rotation.eulerAngles.z > 127 && transform.GetChild(i).transform.rotation.eulerAngles.z < 140)
                    {
                        if (changeFeeling[i] == 2)
                        {
                            changeFeeling[i] = 0;
                        }
                        else if (changeFeeling[i] == 0)
                        {
                            changeFeeling[i] = 2;
                        }
                        else {
                            int index = i - 1;
                            int feelingIndex = feels.FindIndex(feeling => feeling == transform.GetChild(index == -1 ? childs - 1 : index % childs).GetComponent<FeelingButtonControllerScript>().feelingInterface) + 1;
                            transform.GetChild(i).GetComponentInChildren<TextMesh>().text = feels[feelingIndex % numberOfFeelings].feeling;
                            transform.GetChild(i).GetComponentInChildren<FeelingButtonControllerScript>().feelingInterface = feels[feelingIndex % numberOfFeelings];
                            //Debug.Log(canChangeFeeling.Length);
                            changeFeeling[i] = 0;

                            Debug.Log(feelingIndex);
                        }
                    }
                }
            }
            // apply rotation
            rotation.y = -(mouseOffset.x + mouseOffset.y) * sensitivity;

            // rotate
            transform.Rotate(rotation);

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

    public void UpdatePosition()
    {
        transform.position = Camera.main.ViewportToWorldPoint(rouletteViewPortPosition);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
    }

    public void getFeels()
    {
        feels = CCS.feels;
        numberOfFeelings = feels.Count;
        changeFeeling = new int[transform.childCount];
        cantChangeFeeling = new bool[transform.childCount];
    }
}
