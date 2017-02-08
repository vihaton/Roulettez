using UnityEngine;

public class NeutralButtonControllerScript : MonoBehaviour
{

    private ContentCreationScript CCS;
    private float sensitivity;
    private Vector3 mouseReference;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    private bool isRotating;

    void Start()
    {
        sensitivity = 0.4f;
        rotation = Vector3.zero;
        CCS = GameObject.FindObjectOfType<ContentCreationScript>();
    }

    void Update()
    {
        if (isRotating)
        {
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
        CCS.deleteContent();
        CCS.createContent(FeelingType.Neutral);
    }
}
