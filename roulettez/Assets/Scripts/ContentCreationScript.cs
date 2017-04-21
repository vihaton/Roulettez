using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

 // this script handles the creation of roulette
public class ContentCreationScript : MonoBehaviour {
    
    public List<FeelingInterface> feels;
    public GameObject feelingButtonPrefab;
    public GameObject PositiveButtonPrefab;
    public GameObject NegativeButtonPrefab;
    public GameObject NeutralButtonPrefab;
    public GameObject roulette;
    private TunneCreationScript TCS;
    public Button BackButton;
    private RouletteControllerScript RCS;
    public bool switched = false;

    void Start () {
        feels = new List<FeelingInterface>();
        RCS = GameObject.FindObjectOfType<RouletteControllerScript>();
        createContent();

	}

   public void createContent(FeelingType feelingType)
    {
        BackButton.gameObject.SetActive(true);
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        feels = TCS.getListOfFeelings().FindAll(feeling=>feeling.GetType().Equals(feelingType));
        int numberOfFeelings = feels.Count;
        float ang = -360f / (10);
        float offset = 90;
        if (!RCS.isRightSide) { RCS.switchSide(); switched = true; }
        for (int i = 0; i < 10; i++)
        {
            GameObject tempObject;
            Vector3 center = new Vector3(0, -1f, 0);
            Vector3 pos = GetButtonPosition(center, 0.49f, ang * i + offset);
            Quaternion rot = transform.rotation;
            tempObject = Instantiate(feelingButtonPrefab, pos, rot) as GameObject;
            tempObject.transform.SetParent(roulette.transform, false);
            tempObject.transform.Rotate(new Vector3(-90, 0, i * ang+ offset) );
            FeelingButtonControllerScript FBCS = tempObject.GetComponent<FeelingButtonControllerScript>();
            FBCS.feelingInterface = feels[i];
            TextMesh textObject = tempObject.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
            textObject.text = feels[i].GetFeeling();
            }
        if (switched) RCS.switchSide();
        switched = false;
    }

    public void createContent()
    {
        if (!RCS.isRightSide) { RCS.switchSide(); switched = true; }
        deleteContent();
        BackButton.gameObject.SetActive(false);
        int numberOfSectors = 3;
        float ang = 360f / (numberOfSectors);
        for (int i = 0; i < numberOfSectors; i++)
        {
            GameObject tempObject;
            Vector3 center = new Vector3(0, -1f, 0);
            Vector3 pos = GetButtonPosition(center, 0.49f, ang * i);
            Quaternion rot = transform.rotation;
            if(i==0) tempObject = Instantiate(PositiveButtonPrefab, pos, rot) as GameObject;
            else if (i == 1) tempObject = Instantiate(NegativeButtonPrefab, pos, rot) as GameObject;
            else tempObject = Instantiate(NeutralButtonPrefab, pos, rot) as GameObject;
            tempObject.transform.SetParent(roulette.transform, false);
            tempObject.transform.Rotate(new Vector3(-90, 0, i * ang));
        }
        if (switched) RCS.switchSide();
        switched = false;
    }

    public void createAllContent()
    {
        deleteContent();
        BackButton.gameObject.SetActive(true);
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        feels = TCS.getListOfFeelings();
        int numberOfFeelings = feels.Count;
        float ang = -360f / (10);
        float offset = 90;
        if (!RCS.isRightSide) { RCS.switchSide(); switched = true; }
        for (int i = 0; i < 10; i++)
        {
            GameObject tempObject;
            Vector3 center = new Vector3(0, -1f, 0);
            Vector3 pos = GetButtonPosition(center, 0.49f, ang * i + offset);
            Quaternion rot = transform.rotation;
            tempObject = Instantiate(feelingButtonPrefab, pos, rot) as GameObject;
            tempObject.transform.SetParent(roulette.transform, false);
            tempObject.transform.Rotate(new Vector3(-90, 0, i * ang + offset));
            FeelingButtonControllerScript FBCS = tempObject.GetComponent<FeelingButtonControllerScript>();
            FBCS.feelingInterface = feels[i];
            TextMesh textObject = tempObject.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
            textObject.text = feels[i].GetFeeling();
        }
        if (switched) RCS.switchSide();
        switched = false;
        RCS.RandomRotation();
    }

    public void deleteContent()
    {
        int childs = roulette.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(roulette.transform.GetChild(i).gameObject);
        }
    }

    Vector3 GetButtonPosition(Vector3 center, float radius, float ang)
    {
        Vector3 pos;
        float a = ang + 90;
        pos.x = center.x - radius * Mathf.Sin(a * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z - radius * Mathf.Cos(a * Mathf.Deg2Rad);
        return pos;
    }
}
