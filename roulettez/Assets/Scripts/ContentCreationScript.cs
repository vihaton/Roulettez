using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ContentCreationScript : MonoBehaviour {
    
    public List<FeelingInterface> feels;
    public GameObject feelingButtonPrefab;
    public GameObject PositiveButtonPrefab;
    public GameObject NegativeButtonPrefab;
    public GameObject NeutralButtonPrefab;
    public GameObject PlankButtonPrefab;
    public GameObject roulette;
    private TunneCreationScript TCS;
    private RouletteControllerScript RCS;

    void Start () {
        RCS = GameObject.FindObjectOfType<RouletteControllerScript>();
        feels = new List<FeelingInterface>();
        createContent();
	}

   public void createContent(FeelingType feelingType)
    {
        TCS = GameObject.FindObjectOfType<TunneCreationScript>();
        feels = TCS.getListOfFeelings().FindAll(feeling=>feeling.GetType().Equals(feelingType));
        int numberOfFeelings = feels.Count;
        float ang = -360f / (10);
        
        for (int i = 0; i < 10; i++)
        {
            GameObject tempObject;
            Vector3 center = new Vector3(0, -1f, 0);
            Vector3 pos = GetButtonPosition(center, 0.49f, ang * i + 115);
            Quaternion rot = transform.rotation;
            tempObject = Instantiate(feelingButtonPrefab, pos, rot) as GameObject;
            tempObject.transform.SetParent(roulette.transform, false);
            tempObject.transform.Rotate(new Vector3(-90, 0, i * ang+115) );
            FeelingButtonControllerScript FBCS = tempObject.GetComponent<FeelingButtonControllerScript>();
            FBCS.feelingInterface = feels[i];
            TextMesh textObject = tempObject.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
            textObject.text = feels[i].GetFeeling();
        }
        RCS.getFeels();
        //for (int i = 10; i<numberOfFeelings; i++)
        //{
        //    GameObject tempObject;
        //    tempObject = Instantiate(PlankButtonPrefab, new Vector3(0,0,0), transform.rotation) as GameObject;
        //    tempObject.transform.SetParent(roulette.transform, false);
        //    FeelingButtonControllerScript FBCS = tempObject.GetComponent<FeelingButtonControllerScript>();
        //    FBCS.feelingInterface = feels[i];
        //    TextMesh textObject = tempObject.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        //    textObject.text = feels[i].GetFeeling();
        //}
    }

    public void createContent()
    {
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
