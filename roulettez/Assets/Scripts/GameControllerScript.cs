using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

    public FeelingInterface currentFeeling;
    public FeelingInterface[] currentFeelings = new FeelingInterface[3];
    public Text feelingText1;
    public Text feelingText2;
    public Text feelingText3;

    public void UpdateCurrentFeeling(FeelingInterface FI)
    {
        currentFeeling = FI;
        if (feelingText1.text == "")
        {
            currentFeelings[0] = FI;
            feelingText1.text = currentFeelings[0].feeling;

        }
        else if (feelingText2.text == "")
        {
            currentFeelings[1] = FI;
            feelingText2.text = currentFeelings[1].feeling;

        }
        else if (feelingText3.text == "")
        {
            currentFeelings[2] = FI;
            feelingText3.text = currentFeelings[2].feeling;

        }
    }
    
}
