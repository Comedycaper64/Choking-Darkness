using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelScript : MonoBehaviour
{
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum + 2);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
