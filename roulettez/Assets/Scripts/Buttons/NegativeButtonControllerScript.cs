using UnityEngine;

public class NegativeButtonControllerScript : MonoBehaviour
{

    private RouletteControllerScript RCS;
    private ContentCreationScript CCS;

    private Vector2 mousePrevious;
    private Vector2 mouseCurrent;

    void Start()
    {
        CCS = GameObject.FindObjectOfType<ContentCreationScript>();
        RCS = GameObject.FindObjectOfType<RouletteControllerScript>();
    }

    void OnMouseDown()
    {

        // store mouse
        mousePrevious = Input.mousePosition;
    }

    void OnMouseUp()
    {
        mouseCurrent = Input.mousePosition;
        if (Mathf.Abs(mouseCurrent.x - mousePrevious.x) < 10 && (mouseCurrent.y - mousePrevious.y) < 10)
        {
            CCS.deleteContent();
            RCS.ResetRotation();
            CCS.createContent(FeelingType.Negative);
        }
    }
}