using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SearchFeelingInputFieldControllerSciprt : MonoBehaviour
{
    public List<FeelingInterface> feels;
    private TunneCreationScript TCS;
    public GameObject content;
    public InputField feelingInputField;
    List<FeelingInterface> searchedFeels;
    public GameObject feelingButtonPrefab;
    public GameObject addFeelingButtonPrefab;

    void Start()
    {
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        feels = TCS.getListOfFeelings();
        searchedFeels = new List<FeelingInterface>();
    }

    public void Search()
    {
        DeleteContent();
        searchedFeels.Clear();
        searchedFeels.AddRange(feels);
        searchedFeels.RemoveAll(x => !x.feeling.ToLower().StartsWith(feelingInputField.text.ToLower()));
        RectTransform rt = content.GetComponent(typeof(RectTransform)) as RectTransform;
        RectTransform rtf = feelingButtonPrefab.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, (searchedFeels.Count+1) * (rtf.GetComponent<LayoutElement>().minHeight + 2 * rt.GetComponent<VerticalLayoutGroup>().spacing));
        for (int i = 0; i <= searchedFeels.Count - 1 ; i++)
        {
            GameObject tempObject;
            tempObject = Instantiate(feelingButtonPrefab, feelingButtonPrefab.transform.position, feelingButtonPrefab.transform.rotation) as GameObject;
            tempObject.transform.SetParent(content.transform, false);
            Text textObject = tempObject.GetComponentInChildren(typeof(Text)) as Text;
            textObject.text = searchedFeels[i].GetFeeling();
        }

    }
    
    
    public void DeleteContent()
    {
        int childs = content.transform.childCount;
        for (int i = childs - 1; i >= 1; i--)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }
        RectTransform rt = content.GetComponent(typeof(RectTransform)) as RectTransform;
        RectTransform rtf = feelingButtonPrefab.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, (1) * (rtf.GetComponent<LayoutElement>().minHeight + 2 * rt.GetComponent<VerticalLayoutGroup>().spacing));
    }
}
