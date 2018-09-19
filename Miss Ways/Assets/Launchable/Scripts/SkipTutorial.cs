using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SkipTutorial : MonoBehaviour {

	void Start ()
    {
        if (PlayerPrefs.GetInt("tutorial") != 3)
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}
