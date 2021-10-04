using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeStoryPage : MonoBehaviour
{
    private Scene currScene;

    void Start()
    {
        currScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && currScene.name != "StoryScene1")
        {
            SceneManager.LoadScene("StoryScene1", LoadSceneMode.Single);
        }
        if(Input.GetKeyDown(KeyCode.D) && currScene.name != "StoryScene2")
        {
            SceneManager.LoadScene("StoryScene2", LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }
    }
}
