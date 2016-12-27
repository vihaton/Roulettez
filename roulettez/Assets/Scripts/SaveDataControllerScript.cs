﻿using UnityEngine;
using System.Collections;
using System.IO;

public class SaveDataControllerScript : MonoBehaviour
{
    public SaveDataContainer saveDataContainer = new SaveDataContainer();
    private FeelingSaveData saveData = new FeelingSaveData();
    // Use this for initialization
   

    public void Save()
    {
        saveData.SetDate(System.DateTime.Now);
        saveData.feeling = GameObject.FindObjectOfType<GameControllerScript>().currentFeeling.feeling;
        Debug.Log("saveData" + saveData.feeling);
        try
        {
            saveDataContainer = SaveDataContainer.Load(Path.Combine(Application.persistentDataPath, "SaveDataContainer.xml"));
            FeelingSaveData[] temp = saveDataContainer.SaveDataArray;
            saveDataContainer.SaveDataArray = new FeelingSaveData[temp.Length + 1];
            for (int i = 0; i < temp.Length; i++)
            {
                saveDataContainer.SaveDataArray[i] = temp[i];
            }
            saveDataContainer.SaveDataArray[temp.Length] = saveData;
        }
        catch (FileNotFoundException e)
        {
            saveDataContainer.SaveDataArray = new FeelingSaveData[1];
            saveDataContainer.SaveDataArray[0] = saveData;
        }
        
        
        saveDataContainer.Save(Path.Combine(Application.persistentDataPath, "SaveDataContainer.xml"));
    }

}