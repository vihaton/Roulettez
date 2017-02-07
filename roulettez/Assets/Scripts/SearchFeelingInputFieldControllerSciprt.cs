using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SearchFeelingInputFieldControllerSciprt : MonoBehaviour
{
    public List<FeelingInterface> feels;
    public InputField feelingInputField;
    public GameObject roulette;
    private TunneCreationScript TCS;
    List<FeelingInterface> searchedFeels;

    void Start()
    {
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        feels = TCS.getListOfFeelings();
        searchedFeels = new List<FeelingInterface>();
    }

    public void Search()
    {
        Reset();
        int childs = roulette.transform.childCount;
        searchedFeels.Clear();
        searchedFeels.AddRange(feels);
        searchedFeels.RemoveAll(x => !x.feeling.ToLower().StartsWith(feelingInputField.text.ToLower()));
        int k = 0;
        int a = -1;
        for (int i = roulette.transform.childCount - 1; i >= 0; i--)
        {
            for (int j = 0; j < searchedFeels.Count; j++)
            {
                if (roulette.transform.GetChild(i).GetComponent<TextMesh>().text == searchedFeels[j].feeling) { k = 1; a = i; }
            }
            if (k == 0) roulette.transform.GetChild(i).GetComponent<Renderer>().enabled = false;
            k = 0;
        }
        if (a >= 0)
        {
            roulette.transform.RotateAround(transform.position, transform.forward, -roulette.transform.GetChild(a).transform.rotation.eulerAngles.z - 20);
        }
        RouletteControllerScript RCS = roulette.GetComponent<RouletteControllerScript>();
        RCS.UpdatePosition();
    }
    public void Reset()
    {
        int childs = roulette.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            roulette.transform.GetChild(i).GetComponent<Renderer>().enabled = true;
        }
    }
}
