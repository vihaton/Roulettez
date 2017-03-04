using UnityEngine.UI;
using UnityEngine;

public class AddFeelingControllerScript : MonoBehaviour
{

    public GameObject LayoutElement;
    public InputField inputField;
    private GameControllerScript GCS;
    public void Activate()
    {
        GCS = GameObject.FindObjectOfType<GameControllerScript>();
        LayoutElement.gameObject.SetActive(true);
        Debug.Log(GCS.lastUsedInputField.text);
        inputField.text = GCS.lastUsedInputField.text;
        inputField.ActivateInputField();
    }
    public void Disable()
    {

        LayoutElement.gameObject.SetActive(false);
    }
}