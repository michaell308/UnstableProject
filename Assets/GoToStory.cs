using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToStory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(viewStory);
    }

    void viewStory()
    {
        SceneManager.LoadScene("StoryScene1", LoadSceneMode.Single);
    }


}
