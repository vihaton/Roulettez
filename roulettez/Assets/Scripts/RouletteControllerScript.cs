using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteControllerScript : MonoBehaviour {

    private float sensitivity;
    private Vector3 mouseReference;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    private bool isRotating;
    private Vector3 rouletteViewPortPosition;
    public Camera camera;   

    void Start () {
        sensitivity = 0.4f;
        rotation = Vector3.zero;
        rouletteViewPortPosition = new Vector3(1f, 0f, 10);
        transform.position = camera.ViewportToWorldPoint(rouletteViewPortPosition);
        transform.localScale = new Vector3(10, 1, 10);
    }
	
	void Update () {
        transform.position = camera.ViewportToWorldPoint(rouletteViewPortPosition);
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
        transform.position = Camera.main.ViewportToWorldPoint(rouletteViewPortPosition);
    }
}
