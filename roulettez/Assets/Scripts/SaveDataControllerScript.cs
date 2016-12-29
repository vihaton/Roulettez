using UnityEngine;
using System.Collections;
using System.IO;

public class SaveDataControllerScript : MonoBehaviour
{
    public SaveDataContainer saveDataContainer = new SaveDataContainer();
    private FeelingSaveData saveData = new FeelingSaveData();

    public void Save()
    {
        FeelingInterface[] feelingsArray = GameObject.FindObjectOfType<GameControllerScript>().currentFeelings;
        for (int j = 0; j < feelingsArray.GetLength(0); j++)
        {
            if (feelingsArray[j] == null) continue;
            saveData.date = System.DateTime.Now;
            saveData.feeling = feelingsArray[j].feeling;

            try
            {
                saveDataContainer = SaveDataContainer.Load(Application.persistentDataPath + "/SaveDataContainer.xml");
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
            saveDataContainer.Save(Application.persistentDataPath + "/SaveDataContainer.xml");
        }
    }
}