using UnityEngine.UI;
using UnityEngine;

public class AddFeelingControllerScript : MonoBehaviour
{

    public GameObject LayoutElement;
    public InputField inputField;
    public void Activate()
    {
        LayoutElement.gameObject.SetActive(true);
        inputField.ActivateInputField();
    }
    public void Disable()
    {
        LayoutElement.gameObject.SetActive(false);
    }
    public void SwitchActive()
    {
        if (LayoutElement.gameObject.activeSelf) Disable();
        else Activate();
    }
}