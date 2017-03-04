using UnityEngine;
using UnityEngine.UI;

public class RouletteControllerScript : MonoBehaviour
{
    private float sensitivity;
    private Vector3 mouseReference;
    private Vector2 mouseOffset;
    private Vector3 rotation;
    private bool isRotating;

    private Vector3 rouletteViewPortPosition;
    public Camera camera;
    public bool isRightSide = true;

    private Rigidbody2D RB;
    private float force;
    private float maxTorque;
    private float torqueSens;

    public GameObject LayoutGroup;
    private HorizontalLayoutGroup layout;

    private SearchFeelingInputFieldControllerSciprt SFIFCS;
    void Start()
    {
        sensitivity = 0.1f;
        maxTorque = 5000;
        torqueSens = 50;
        rotation = Vector3.zero;
        rouletteViewPortPosition = new Vector3(1f, 0f, 10);
        transform.position = camera.ViewportToWorldPoint(rouletteViewPortPosition);
        transform.localScale = new Vector3(10, 1, 10);
        RB = transform.GetComponent<Rigidbody2D>();
        layout = LayoutGroup.GetComponent<HorizontalLayoutGroup>();
        SFIFCS = GameObject.FindObjectOfType<SearchFeelingInputFieldControllerSciprt>();
    }


    void OnMouseDrag()
    {
        mouseOffset = (Input.mousePosition - mouseReference);


        if (isRightSide) force = mouseOffset.x + mouseOffset.y; //Changes the force to be applied
        else force = mouseOffset.x - mouseOffset.y;




        // apply rotation
        if (isRightSide) rotation.y = -(mouseOffset.x + mouseOffset.y) * sensitivity;
        else rotation.y = -(mouseOffset.x - mouseOffset.y) * sensitivity;
        // rotate
        transform.Rotate(rotation);

        // store mouse
        mouseReference = Input.mousePosition;
    }


    void OnMouseDown()
    {
        // rotating flag
        RB.freezeRotation = true;
        isRotating = true;

        // store mouse
        mouseReference = Input.mousePosition;
        SFIFCS.DeleteContent();
    }

    void OnMouseUp()
    {
        RB.freezeRotation = false;
        if (Mathf.Abs(force * torqueSens) < maxTorque) RB.AddTorque(-force * torqueSens);
        else RB.AddTorque(-maxTorque * Mathf.Sign(force));
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
                transform.GetChild(i).Rotate(0, 0, 180);

            }
            rouletteViewPortPosition = new Vector3(0, 0, 10);
            UpdatePosition();
            isRightSide = false;
            layout.childAlignment = TextAnchor.LowerRight;
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
            layout.childAlignment = TextAnchor.LowerLeft;
        }
    }
    public void RandomRotation()
    {
        RB.AddTorque(Random.Range(3000.0f, 5000.0f));

    }
}