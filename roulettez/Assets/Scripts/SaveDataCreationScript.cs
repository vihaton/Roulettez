using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class SaveDataCreationScript : MonoBehaviour {
    public FeelingSaveData[] feelings;
    public GameObject FeelingButtonPrefab;
    public GameObject content;
    public GameObject saveDataList;
    public SaveDataContainer saveDataContainer = new SaveDataContainer();
    // Use this for initialization
    void Start () {
        saveDataList = GameObject.Find("SaveDataCanvas");
        saveDataList.GetComponent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void createContent()
    {
        saveDataList = GameObject.Find("SaveDataCanvas");
        if (saveDataList.GetComponent<Canvas>().enabled == true)
        {
            saveDataList.GetComponent<Canvas>().enabled = false;
            return;
        }
        saveDataContainer = SaveDataContainer.Load(Application.persistentDataPath + "/SaveDataContainer.xml");
        feelings = saveDataContainer.SaveDataArray;
        RectTransform rt = content.GetComponent(typeof(RectTransform)) as RectTransform;
        RectTransform rtf = FeelingButtonPrefab.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, feelings.GetLength(0) * (rtf.GetComponent<LayoutElement>().minHeight + 2 * rt.GetComponent<VerticalLayoutGroup>().spacing));


        for (int i = 0; i < feelings.GetLength(0); i++)
        {
            GameObject tempObject;
            tempObject = Instantiate(FeelingButtonPrefab, FeelingButtonPrefab.transform.position, FeelingButtonPrefab.transform.rotation) as GameObject;
            tempObject.transform.SetParent(content.transform, false);
            FeelingButtonControllerScript FBCS = tempObject.GetComponent<FeelingButtonControllerScript>();
            Text textObject = tempObject.GetComponentInChildren(typeof(Text)) as Text;
            textObject.text = feelings[i].feeling + feelings[i].date;
        }

        saveDataList.GetComponent<Canvas>().enabled = true;

    }


    public void hideContent()
    {
        saveDataList.GetComponent<Canvas>().enabled = false;
    }

}
