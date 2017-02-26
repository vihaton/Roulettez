using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteControllerScript : MonoBehaviour
{
    private float sensitivity;
    private Vector3 mouseReference;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    private bool isRotating;
    private Vector3 rouletteViewPortPosition;
    public Camera camera;
    private bool isRightSide = true;


    void Start()
    {
        sensitivity = 0.1f;
        rotation = Vector3.zero;
        rouletteViewPortPosition = new Vector3(1f, 0f, 10);
        transform.position = camera.ViewportToWorldPoint(rouletteViewPortPosition);
        transform.localScale = new Vector3(10, 1, 10);

    }

   

    void Update()
    {
        //transform.position = camera.ViewportToWorldPoint(rouletteViewPortPosition);
        if (isRotating)
        {
            
            //Debug.Log(transform.GetChild(0).transform.rotation.eulerAngles);
            mouseOffset = (Input.mousePosition - mouseReference);

            // apply rotation
            if(isRightSide)rotation.y = -(mouseOffset.x + mouseOffset.y) * sensitivity;
            else rotation.y = -(mouseOffset.x - mouseOffset.y) * sensitivity;
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

    public void switchSide()
    {
        if (isRightSide)
        {
            int childs = transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                TextMesh textObject = transform.GetChild(i).GetComponent(typeof(TextMesh)) as TextMesh;
                textObject.anchor = UnityEngine.TextAnchor.MiddleRight;
                BoxCollider collider = transform.GetChild(i).GetComponent(typeof(BoxCollider)) as BoxCollider;
                collider.center = new Vector3(-0.57f, 0, 0);
                transform.GetChild(i).Rotate(0,0, 180);
                
            }
            rouletteViewPortPosition = new Vector3(0, 0, 10);
            UpdatePosition();
            isRightSide = false;
        }
        else
        {
            int childs = transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                TextMesh textObject = transform.GetChild(i).GetComponent(typeof(TextMesh)) as TextMesh;
                BoxCollider collider = transform.GetChild(i).GetComponent(typeof(BoxCollider)) as BoxCollider;
                collider.center = new Vector3(0.73f, 0, 0);
                textObject.anchor = UnityEngine.TextAnchor.MiddleLeft;
                transform.GetChild(i).Rotate(0, 0, 180);

            }
            rouletteViewPortPosition = new Vector3(1f, 0f, 10);
            UpdatePosition();
            isRightSide = true;
        }
    }
}
