using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ContentCreationScript : MonoBehaviour {
    
    public List<FeelingInterface> feels;
    public GameObject FeelingButtonPrefab;
    public GameObject content;

    private TunneCreationScript TCS;
    private TunneStruct thisFeelsMan;
    
	// Use this for initialization
	void Start () {
        feels = new List<FeelingInterface>();
        createContent();
	}

    private void createContent()
    {
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();

        feels = TCS.getListOfFeelings();
        RectTransform rt = content.GetComponent(typeof(RectTransform)) as RectTransform;
        RectTransform rtf = FeelingButtonPrefab.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, feels.Count * (rtf.GetComponent<LayoutElement>().minHeight + 2 * rt.GetComponent<VerticalLayoutGroup>().spacing));

        for (int i = 0; i < feels.Count; i++)
        {
            GameObject tempObject;
            tempObject = Instantiate(FeelingButtonPrefab, FeelingButtonPrefab.transform.position, FeelingButtonPrefab.transform.rotation) as GameObject;
            tempObject.transform.SetParent(content.transform, false);
            
            FeelingButtonControllerScript FBCS = tempObject.GetComponent<FeelingButtonControllerScript>();

            FBCS.feelingInterface = feels[i];

            Text textObject = tempObject.GetComponentInChildren(typeof(Text)) as Text;
            textObject.text = feels[i].GetFeeling();
        }

    }
    

}
