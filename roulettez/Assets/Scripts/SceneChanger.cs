using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour
{

    public void LoadScene(int sceneNumber)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
