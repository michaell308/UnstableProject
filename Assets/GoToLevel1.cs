using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(playGame);
    }

    void playGame()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
