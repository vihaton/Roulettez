using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SearchFeelingInputFieldControllerSciprt : MonoBehaviour {
    public List<FeelingInterface> feels;
    public GameObject FeelingButtonPrefab;
    public GameObject searchedContent;
    public GameObject content;
    public InputField feelingInputField;
    private TunneCreationScript TCS;
    private ContentCreationScript CCS;
    List<FeelingInterface> searchedFeels;

    void Start () {
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        CCS = GameObject.FindObjectOfType<ContentCreationScript>();
        feels = TCS.getListOfFeelings();
        searchedFeels = new List<FeelingInterface>();
    }

    public void Search()
    {
        CCS.deleteContent();
        CCS.createContent();
        int childs = searchedContent.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(searchedContent.transform.GetChild(i).gameObject);
        }
        searchedFeels.Clear();
        searchedFeels.AddRange(feels);
        searchedFeels.RemoveAll(x => !x.feeling.ToLower().StartsWith(feelingInputField.text.ToLower()));
        RectTransform rt = searchedContent.GetComponent(typeof(RectTransform)) as RectTransform;
        RectTransform rtf = FeelingButtonPrefab.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, searchedFeels.Count * (rtf.GetComponent<LayoutElement>().minHeight + 2 * rt.GetComponent<VerticalLayoutGroup>().spacing));
        for (int i = 0; i < searchedFeels.Count; i++)
        {
            GameObject tempObject;
            tempObject = Instantiate(FeelingButtonPrefab, FeelingButtonPrefab.transform.position, FeelingButtonPrefab.transform.rotation) as GameObject;
            tempObject.transform.SetParent(searchedContent.transform, false);
            Text textObject = tempObject.GetComponentInChildren(typeof(Text)) as Text;
            textObject.text = searchedFeels[i].GetFeeling();
           
        }
        Text[] feelsTexts = content.GetComponentsInChildren<Text>() as Text[];
        int k = 0;
        for (int i = feelsTexts.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < searchedFeels.Count; j++)
            {
                if (feelsTexts[i].text == searchedFeels[j].feeling) k = 1;
            }
            if (k == 0) Destroy(feelsTexts[i]);
            k = 0;
        }
    }
}
