using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteControllerScript : MonoBehaviour {

    private float sensitivity;
    private Vector3 mouseReference;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    private bool isRotating;
    private Vector3 roulettePosition;
    public GameObject anchor;
    private RectTransform rectTransform;

    void Start () {
        sensitivity = 0.4f;
        rotation = Vector3.zero;
        rectTransform = anchor.GetComponent<RectTransform>();



    }

    void Update () {
        UpdatePosition();

        if (isRotating)
        {
            // offset
            mouseOffset = (Input.mousePosition - mouseReference);

            // apply rotation
            anchor.transform.Rotate(Vector3.forward,666);
            //anchor.transform.Rotate( 0,0,-(mouseOffset.x + mouseOffset.y) * sensitivity);


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
       // anchor.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width * 0.75f,Screen.height*0.3f,0));
    }
}
