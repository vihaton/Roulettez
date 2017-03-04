using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    public FeelingInterface currentFeeling;
    public FeelingInterface[] currentFeelings = new FeelingInterface[3];
    public InputField feelingInputField1;
    public InputField feelingInputField2;
    public InputField feelingInputField3;
    public InputField lastUsedInputField;

    public void UpdateCurrentFeeling(FeelingInterface FI)
    {
        currentFeeling = FI;
        if (feelingInputField1.text == "")
        {
            currentFeelings[0] = FI;
            feelingInputField1.text = currentFeelings[0].feeling;

        }
        else if (feelingInputField2.text == "")
        {
            currentFeelings[1] = FI;
            feelingInputField2.text = currentFeelings[1].feeling;

        }
        else if (feelingInputField3.text == "")
        {
            currentFeelings[2] = FI;
            feelingInputField3.text = currentFeelings[2].feeling;

        }
    }
    public void UpdateLastUsed(FeelingInterface FI)
    {
        lastUsedInputField.text = FI.feeling;
        if(lastUsedInputField == feelingInputField1) currentFeelings[0] = FI;
        if (lastUsedInputField == feelingInputField2) currentFeelings[1] = FI;
        if (lastUsedInputField == feelingInputField3) currentFeelings[2] = FI;
    }
}
