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

    void Start () {
        sensitivity = 0.4f;
        rotation = Vector3.zero;
        roulettePosition = new Vector3(1.9f, 0, 10);
        transform.position = Camera.main.ViewportToWorldPoint(roulettePosition);
        transform.localScale = new Vector3(20, 1, 20);
    }
	
	void Update () {
        if (isRotating)
        {
            // offset
            mouseOffset = (Input.mousePosition - mouseReference);

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
        transform.position = Camera.main.ViewportToWorldPoint(roulettePosition);
    }
}
