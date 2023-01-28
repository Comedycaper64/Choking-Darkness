using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(3);
    }
}
