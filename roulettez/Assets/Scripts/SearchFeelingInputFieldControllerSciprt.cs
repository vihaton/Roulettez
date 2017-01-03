using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class SearchFeelingInputFieldControllerSciprt : MonoBehaviour {
  
    public List<FeelingInterface> feels;
    public GameObject FeelingButtonPrefab;
    public GameObject content;
    public GameObject feelingInputField;
    private TunneCreationScript TCS;
    private Text feeling;
    List<FeelingInterface> searchedFeels;
    // Use this for initialization
    void Start () {
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        feels = TCS.getListOfFeelings();
        searchedFeels = new List<FeelingInterface>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnValueChanged()
    {
        
    }

    public void Search()
    {
        feeling = feelingInputField.GetComponentInChildren(typeof(Text)) as Text;
        Debug.Log("asd"+feeling.text.Length);
        int childs = content.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }
    
    

        searchedFeels.Clear();
        searchedFeels.AddRange(feels);
        Debug.Log("FeelsCount" + searchedFeels.Count);
       
        searchedFeels.RemoveAll(x => !x.feeling.ToLower().StartsWith(feeling.text.ToLower()));
        Debug.Log("FeelsCount" + searchedFeels.Count);
        RectTransform rt = content.GetComponent(typeof(RectTransform)) as RectTransform;
        RectTransform rtf = FeelingButtonPrefab.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, searchedFeels.Count * (rtf.GetComponent<LayoutElement>().minHeight + 2 * rt.GetComponent<VerticalLayoutGroup>().spacing));
        Debug.Log("Searching");
        for (int i = 0; i < searchedFeels.Count; i++)
        {
            GameObject tempObject;
            tempObject = Instantiate(FeelingButtonPrefab, FeelingButtonPrefab.transform.position, FeelingButtonPrefab.transform.rotation) as GameObject;
            tempObject.transform.SetParent(content.transform, false);

            

           

            Text textObject = tempObject.GetComponentInChildren(typeof(Text)) as Text;
            textObject.text = searchedFeels[i].GetFeeling();
            Debug.Log("Searching");
        }
    }
}
