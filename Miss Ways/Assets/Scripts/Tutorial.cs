using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    string tutorial = "tutorial";

    int counter = 0;

    public void NextTutorialPage()
    {
        counter++;
        if (counter > 4)
            TutorialHasBeenRan();
    }
    public void TutorialHasBeenRan()
    {
        PlayerPrefs.SetInt(tutorial, 1);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
